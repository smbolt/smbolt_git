using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Org.GS
{
  [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Field | System.AttributeTargets.Property, AllowMultiple = true)]
  public class OrgConfigItem : System.Attribute { }

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgVersion : System.Attribute
  {
    public string Value;
    public OrgVersion(string value)
    {
      this.Value = value;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgMainWindowTitle : System.Attribute
  {
    public string Text;
    public OrgMainWindowTitle(string text = "")
    {
      this.Text = text;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgApplicationType : System.Attribute
  {
    public ApplicationType ApplicationType;
    public OrgApplicationType(string applicationTypeString)
    {
      if (!Enum.IsDefined(typeof(ApplicationType), applicationTypeString))
        throw new Exception("Enum type 'ApplicationType' cannot be set to the value '" + applicationTypeString + "'.");

      this.ApplicationType = g.ToEnum<ApplicationType>(applicationTypeString, GS.ApplicationType.NotSet);
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgConfigName : System.Attribute
  {
    public string Value;
    public OrgConfigName(string value)
    {
      this.Value = value;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgModuleName : System.Attribute
  {
    public string Value;
    public OrgModuleName(string value)
    {
      this.Value = value;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgModuleTitle : System.Attribute
  {
    public string Value;
    public OrgModuleTitle(string value)
    {
      this.Value = value;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgModuleCode : System.Attribute
  {
    public int Value;
    public OrgModuleCode(int value)
    {
      this.Value = value;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgWebRootAssembly : System.Attribute {}

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgModuleAssembly : System.Attribute {}

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgControlLibrary : System.Attribute {}

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgModuleHost : System.Attribute {}

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgAssemblyTag : System.Attribute {}

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgFreeUntil : System.Attribute
  {
    public string Value;
    public OrgFreeUntil(string value)
    {
      this.Value = value;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgFreeAfter : System.Attribute
  {
    public string Value;
    public OrgFreeAfter(string value)
    {
      this.Value = value;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Assembly)]
  public class OrgLicenseExpiringInterval : System.Attribute
  {
    public int Days;
    public OrgLicenseExpiringInterval(int days)
    {
      this.Days = days;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Class)]
  public class TransactionVersion : Attribute
  {
    public string VersionString;
    public TransactionVersion(string versionString = "1.0.0.0")
    {
      this.VersionString = versionString;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Field | System.AttributeTargets.Property, AllowMultiple = false)]
  public class ObjectMapPrefix : Attribute
  {
    public string MapPrefix;
    public ObjectMapPrefix(string prefix)
    {
      this.MapPrefix = prefix;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = false)]
  public class XMap : Attribute
  {
    public string CollectionElements;

    private XType _xType;
    public XType XType
    {
      get
      {
        if (this.CollectionElements.IsNotBlank())
          return XType.Element;
        else
          return this._xType;
      }
      set
      {
        this._xType = value;
      }
    }
    public string Name;
    public string ClassName;
    public string WrapperElement;
    public string DefaultValue;
    public bool IsKey;
    public string CompositeKey;
    public bool SequenceDuplicates;
    public string Format;
    public string KeyName;
    public bool IsRequired;
    public bool IsExplicit;
    public bool MyParent;
    public bool UseKeyValue;
    public bool IsObject;

    [DebuggerStepThrough]
    public XMap(string collectionElements = "", XType xType = XType.Attribute, string name = "", string className = "", string wrapperElement = "",
                string defaultValue = "", bool isKey = false, string compositeKey = "", string format = "", bool sequenceDuplicates = false, string keyName = "",
                bool isRequired = false, bool isExplicit = false, bool myParent = false, bool useKeyValue = false, bool isObject = false)
    {
      this.CollectionElements = collectionElements;
      this.XType = xType;
      this.Name = name;
      this.ClassName = className;
      this.WrapperElement = wrapperElement;
      this.DefaultValue = defaultValue;
      this.IsKey = isKey;
      this.CompositeKey = compositeKey;
      this.Format = format;
      this.SequenceDuplicates = sequenceDuplicates;
      this.KeyName = keyName;
      this.IsRequired = isRequired;
      this.IsExplicit = isExplicit;
      this.MyParent = myParent;
      this.UseKeyValue = useKeyValue;
      this.IsObject = isObject;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Class)]
  public class XElementSequence : Attribute
  {
    public Dictionary<int, string> NameSequence {
      get;
      private set;
    }
    public XElementSequence(string value = "")
    {
      this.NameSequence = new Dictionary<int, string>();
      if (value.IsBlank())
        return;
      string[] elementNames = value.Trim().Split(Constants.CommaDelimiter, StringSplitOptions.RemoveEmptyEntries);
      for (int i = 0; i < elementNames.Length; i++)
      {
        if (this.NameSequence.ContainsValue(elementNames[i].Trim()))
          throw new Exception("Duplicate element name '" + elementNames[i] + "'.");
        this.NameSequence.Add(i, elementNames[i].Trim());
      }
    }
  }

  [System.AttributeUsage(System.AttributeTargets.Constructor, AllowMultiple = true)]
  public class XParm : Attribute
  {
    public XParmSource ParmSource;
    public string Name;
    public string AttrName;
    public bool Required;

    public XParm(XParmSource parmSource = XParmSource.NotSet, string name = "", string attrName = "", bool required = true)
    {
      this.ParmSource = parmSource;
      this.Name = name;
      this.AttrName = attrName;
      this.Required = required;
    }
  }

  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
  public class IsDbTable : Attribute
  {
  }

  [AttributeUsage(AttributeTargets.Property)]
  public class IsDbColumn : Attribute
  {
    public bool SkipOnInsert;
    public bool IsNullable;
    public bool SkipMapping;
    public IsDbColumn(bool skipOnInsert = false, bool isNullable = false, bool skipMapping = false)
    {
      this.SkipOnInsert = skipOnInsert;
      this.IsNullable = isNullable;
      this.SkipMapping = skipMapping;
    }
  }

  public enum DbElement
  {
    NotSet,
    EntitySet,
    ListSet,
    Table,
    StoredProcedure,
    Model,
    Column
  }


  [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = true)]
  public class DbMap : Attribute
  {
    public DbElement DbElement;
    public string EntityStore;
    public string SchemaName;
    public string EntityName;
    public string ColumnName;
    public bool IsNullable;
    public bool IsPrimaryKey;
    public bool IsIdentity;
    public MappingRule MappingRule;

    [DebuggerStepThrough]
    public DbMap(DbElement dbElement, string entityStore, string schemaName, string entityName, string columnName = "", bool isNullable = false, bool isPrimaryKey = false,
                 bool isIdentity = false, MappingRule mappingRule = MappingRule.None)
    {
      this.DbElement = dbElement;
      this.EntityStore = entityStore;
      this.SchemaName = schemaName;
      this.EntityName = entityName;
      this.ColumnName = columnName;
      this.IsNullable = isNullable;
      this.IsPrimaryKey = isPrimaryKey;
      this.IsIdentity = isIdentity;
      this.MappingRule = mappingRule;
    }
  }

  [System.AttributeUsage(System.AttributeTargets.All)]
  public class EntityMap : Attribute
  {
    public string TableName;
    public string ColumnName;
    public string BaseName;
    public bool Sequencer;

    [DebuggerStepThrough]
    public EntityMap(string tableName = "", string columnName = "", string baseName = "", bool sequencer = false)
    {
      this.TableName = tableName;
      this.ColumnName = columnName;
      this.BaseName = baseName;
      this.Sequencer = sequencer;
    }
  }


}
