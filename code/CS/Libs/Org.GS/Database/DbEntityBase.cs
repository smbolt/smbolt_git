using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Org.GS.Database
{
  public class DbEntityBase
  {
    public string AliasedTypeName {
      get;
      set;
    }
    public string TableName {
      get;
      set;
    }
    public DbTable DbTable {
      get;
      set;
    }
    public DbEntityBase OriginalValue {
      get;
      set;
    }

    public virtual bool IsLoadingForUpdate {
      get;
      set;
    }

    private bool _isLoadedForUpdate;
    public bool IsLoadedForUpdate
    {
      get {
        return _isLoadedForUpdate;
      }
      set {
        _isLoadedForUpdate = value;
      }
    }

    public bool IsUpdated
    {
      get {
        return this.IsThisEntityUpdated();
      }
    }

    public DbEntityBase()
    {
      string className = this.GetType().Name;
      this.TableName = className;
      //this.DbTable = DbHelper.DbTableSet[this.TableName];
    }


    private bool IsThisEntityUpdated()
    {
      PropertyInfo[] piSet = this.GetType().GetProperties();

      foreach (PropertyInfo pi in piSet)
      {
        object[] attrs = pi.GetCustomAttributes(false);

        if (pi.GetCustomAttributes(typeof(IsDbColumn), false).Count() > 0)
        {
          string propertyType = pi.PropertyType.Name;
          string propertyName = pi.Name;
          string origFieldName = "__" + propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
          FieldInfo ofi = this.GetType().GetField(origFieldName, BindingFlags.NonPublic | BindingFlags.Instance);

          object currentValue = pi.GetValue(this, null);
          object originalValue = ofi.GetValue(this);

          if (pi.PropertyType.IsGenericType)
          {
            Type underlyingType = Nullable.GetUnderlyingType(pi.PropertyType);
            propertyType = underlyingType.Name;
          }

          if (DbUtility.ValueIsUpdated(propertyName, propertyType, originalValue, currentValue))
            return true;
        }
      }

      return false;
    }

    public DbEntityBase MapFromObject(object source, string context)
    {
      string aliasedSourceTypeName = g.ObjectMap.GetAliasedTypeName(source);

      if (this.AliasedTypeName == null)
        this.AliasedTypeName = g.ObjectMap.GetAliasedTypeName(this);

      string sourceType = source.GetType().Name;
      string destType = this.GetType().Name;

      PropertyInfo[] sourceProps = source.GetType().GetProperties();
      PropertyInfo[] destProps = this.GetType().GetProperties();
      List<PropertyInfo> unmappedProperties = new List<PropertyInfo>();
      foreach (PropertyInfo pi in destProps)
        unmappedProperties.Add(pi);

      foreach (PropertyInfo dpi in destProps)
      {
        foreach (PropertyInfo spi in sourceProps)
        {
          string sourcePropType = spi.PropertyType.Name;
          if (sourcePropType.Contains("Nullable"))
            sourcePropType = Nullable.GetUnderlyingType(spi.PropertyType).Name;

          string destPropType = dpi.PropertyType.Name;
          if (destPropType.Contains("Nullable"))
            destPropType = Nullable.GetUnderlyingType(dpi.PropertyType).Name;

          if (dpi.Name == spi.Name && sourcePropType == destPropType)
          {
            dpi.SetValue(this, spi.GetValue(source, null), null);
            unmappedProperties.Remove(dpi);
            break;
          }
        }
      }

      ObjectMapContext omc;
      if (unmappedProperties.Count > 0)
      {
        context = context.SetIfBlank("Default");
        if (g.ObjectMap.ContainsKey(context))
        {
          omc = g.ObjectMap.GetContext(context);

          foreach (PropertyInfo unmappedProperty in unmappedProperties)
          {
            string destPropString = this.AliasedTypeName + "." + unmappedProperty.Name;
            //omc.MapProperty(this, unmappedProperty, destPropString, source, aliasedSourceTypeName);
          }
        }
      }

      return this;
    }
  }
}
