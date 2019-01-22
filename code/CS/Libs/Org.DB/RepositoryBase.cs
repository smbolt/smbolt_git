using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using Org.GS;
using Org.DB;

namespace Org.DB
{
  public class DataEntity
  {

  }

  public class RepositoryBase : IDisposable
  {
    protected static CacheManager CacheManager = new CacheManager();
    protected static PropertyInfoSet PropertyInfoSet = new PropertyInfoSet();
    protected static EntityModelMapSet EntityModelMapSet = new EntityModelMapSet();
    public static string EntityModelMapReport {
      get {
        return EntityModelMapSet.GetReport();
      }
    }

    protected static Dictionary<string, EntityType> EntityTypes = new Dictionary<string,EntityType>();

    private static Type _dataEntitiesType;
    private static List<string> _dataAssembliesLoaded = new List<string>();
    private static List<string> _businessObjectAssembliesLoaded = new List<string>();

    protected string ConnectionStringName {
      get;
      private set;
    }
    protected static string ConnectionString {
      get;
      private set;
    }
    protected string DataStoreName {
      get;
      private set;
    }

    private bool _isDisposed = false;

    public RepositoryBase(string connectionStringName, string dataStoreName)
    {
      this.ConnectionStringName = connectionStringName;
      this.DataStoreName = dataStoreName;
    }

    ~RepositoryBase()
    {
      this.Dispose(true);
    }

    public static void AddEntityTypes(Assembly dataAssembly)
    {
      string assemblyName = dataAssembly.GetName().Name;

      if (_dataAssembliesLoaded.Contains(assemblyName))
        return;

      LoadEntityTypes(dataAssembly);

      _dataAssembliesLoaded.Add(assemblyName);
    }

    public static void AddBusinessObjectTypes(Assembly businessObjectAssembly)
    {
      string assemblyName = businessObjectAssembly.GetName().Name;

      if (_businessObjectAssembliesLoaded.Contains(assemblyName))
        return;

      LoadModelTypes(businessObjectAssembly);

      _businessObjectAssembliesLoaded.Add(assemblyName);
    }

    public IList<ModelBase> GetList(string modelName, string orderBy)
    {
      try
      {
        var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];
        EnsureMaps(map.ModelType, map.EntityType);

        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

        if (!map.PropertyInfoPairSet.ContainsKey(orderBy))
          throw new Exception("Could not locate entity field translation for model field '" + orderBy + "' for model name '" + modelName + "'.");

        string entitySort = map.PropertyInfoPairSet[orderBy].EntityPropertyInfo.Name;

        IList<ModelBase> list = new List<ModelBase>();

        object[] parms = new object[] { ConnectionStringName };
        var db = Activator.CreateInstance(_dataEntitiesType, parms);
        IQueryable dbSet = map.DbSetPI.GetValue(db) as IQueryable;

        foreach(object row in dbSet.OrderBy(map.EntityType, entitySort))
        {
          // might want to put this somewhere sharable
          var model = Activator.CreateInstance(map.ModelType);

          ((ModelBase)model).PropertyInfoPairSet = map.PropertyInfoPairSet;

          foreach(var pip in map.PropertyInfoPairSet.Values)
          {
            object value = pip.EntityPropertyInfo.GetValue(row);

            switch(pip.MappingRule)
            {
              case MappingRule.None:
                pip.ModelPropertyInfo.SetValue(model, value);
                break;

              case MappingRule.DoubleToDecimal:
                pip.ModelPropertyInfo.SetValue(model, value.ToDecimal());
                break;

              case MappingRule.DoubleToFloat:
                pip.ModelPropertyInfo.SetValue(model, value.ToFloat());
                break;

              case MappingRule.Boolean1:

                if (value != null && value.ToString().ToUpper().In("Y,1"))
                  pip.ModelPropertyInfo.SetValue(model, true);
                else
                  pip.ModelPropertyInfo.SetValue(model, false);
                break;
            }
          }

          list.Add((ModelBase)model);
        }

        return list;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to build list of model '" + modelName + "'.", ex);
      }
    }

    public IList<T> GetList<T>(string modelSort)
    {
      Type modelType = typeof(T);
      string modelName = modelType.Name;

      try
      {
        var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];
        EnsureMaps(map.ModelType, map.EntityType);

        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

        if (!map.PropertyInfoPairSet.ContainsKey(modelSort))
          throw new Exception("Could not locate entity field translation for model field '" + modelSort + "' for model name '" + modelName + "'.");

        string entitySort = map.PropertyInfoPairSet[modelSort].EntityPropertyInfo.Name;

        IList<T> list = new List<T>();

        object[] entitySetParms = new object[] { ConnectionStringName };
        var db = Activator.CreateInstance(_dataEntitiesType, entitySetParms);

        DateTime beginDT = DateTime.Now;

        var dbMap = _dataEntitiesType.GetCustomAttribute<DbMap>();
        if (dbMap.DbElement == DbElement.ListSet)
        {
          var list2 = map.DbSetPI.GetValue(db) as IEnumerable<EntityBase>;
          TimeSpan ts = DateTime.Now - beginDT;
          int ms = (int)ts.TotalMilliseconds;
          beginDT = DateTime.Now;

          foreach (object entity in list2)
          {
            var model = Activator.CreateInstance(map.ModelType);
            ((ModelBase)model).PropertyInfoPairSet = map.PropertyInfoPairSet;
            var modelBase = (ModelBase)model;

            list.Add((T)MapEntityToModel(entity, model, map.PropertyInfoPairSet));
          }

          ts = DateTime.Now - beginDT;
          ms = (int)ts.TotalMilliseconds;

          return list;
        }

        IQueryable dbSet = map.DbSetPI.GetValue(db) as IQueryable;

        foreach(object entity in dbSet.OrderBy(map.EntityType, entitySort))
        {
          var model = Activator.CreateInstance(map.ModelType);
          ((ModelBase)model).PropertyInfoPairSet = map.PropertyInfoPairSet;
          var modelBase = (ModelBase) model;

          list.Add((T) MapEntityToModel(entity, model, map.PropertyInfoPairSet));
        }

        return list;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to build list of model '" + modelName + "'.", ex);
      }
    }

    public IList<T> GetList<T>()
    {
      Type modelType = typeof(T);
      string modelName = modelType.Name;

      try
      {
        var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];
        EnsureMaps(map.ModelType, map.EntityType);

        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

        IList<T> list = new List<T>();

        object[] entitySetParms = new object[] { ConnectionStringName };
        var db = Activator.CreateInstance(_dataEntitiesType, entitySetParms);

        DateTime beginDT = DateTime.Now;

        var dbMap = _dataEntitiesType.GetCustomAttribute<DbMap>();
        if (dbMap.DbElement == DbElement.ListSet)
        {
          var list2 = map.DbSetPI.GetValue(db) as IEnumerable<EntityBase>;
          TimeSpan ts = DateTime.Now - beginDT;
          int ms = (int)ts.TotalMilliseconds;
          beginDT = DateTime.Now;

          foreach (object entity in list2)
          {
            var model = Activator.CreateInstance(map.ModelType);
            ((ModelBase)model).PropertyInfoPairSet = map.PropertyInfoPairSet;
            var modelBase = (ModelBase)model;

            list.Add((T)MapEntityToModel(entity, model, map.PropertyInfoPairSet));
          }

          ts = DateTime.Now - beginDT;
          ms = (int)ts.TotalMilliseconds;

          return list;
        }

        IQueryable dbSet = map.DbSetPI.GetValue(db) as IQueryable;

        foreach(object entity in dbSet)
        {
          var model = Activator.CreateInstance(map.ModelType);
          ((ModelBase)model).PropertyInfoPairSet = map.PropertyInfoPairSet;
          var modelBase = (ModelBase) model;

          list.Add((T) MapEntityToModel(entity, model, map.PropertyInfoPairSet));
        }

        return list;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to build list of model '" + modelName + "'.", ex);
      }
    }

    public object Insert<T>(ModelBase model)
    {
      Type modelType = typeof(T);
      string modelName = modelType.Name;

      try
      {
        var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];
        EnsureMaps(map.ModelType, map.EntityType);

        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

        object[] entitySetParms = new object[] { ConnectionStringName };
        var db = Activator.CreateInstance(_dataEntitiesType, entitySetParms);

        DateTime beginDT = DateTime.Now;

        var dbMap = _dataEntitiesType.GetCustomAttribute<DbMap>();
        if (dbMap.DbElement == DbElement.ListSet)
        {
          string methodName = "Insert_" + map.EntityType.Name;

          MethodInfo mi = null;
          MethodInfo[] miSet = db.GetType().GetMethods();
          foreach (var methodInfo in miSet)
          {
            if (methodInfo.Name == methodName)
            {
              if (methodInfo.GetParameters().Count() == 1)
              {
                mi = methodInfo;
                break;
              }
            }
          }

          if (mi == null)
            throw new Exception("Method name '" + methodName + "' could not be found in type '" + _dataEntitiesType.Name + "'.");

          var entity = Activator.CreateInstance(map.EntityType);
          entity = MapModelToEntity(model, entity, map.PropertyInfoPairSet, true, @"GPNET\user");

          object[] parms = new object[] { entity };
          int? returnValue = mi.Invoke(db, parms) as int?;
          int id = returnValue.HasValue ? returnValue.Value : -1;

          TimeSpan ts = DateTime.Now - beginDT;
          int ms = (int)ts.TotalMilliseconds;
          beginDT = DateTime.Now;

          return id;
        }

        throw new Exception("EntitySet processing is not yet implemented in RepositoryBase.Insert.");

        //IQueryable dbSet = map.DbSetPI.GetValue(db) as IQueryable;
        //var entity = Activa

        //foreach(object entity in dbSet)
        //{
        //  var model = Activator.CreateInstance(map.ModelType);
        //  ((ModelBase)model).PropertyInfoPairSet = map.PropertyInfoPairSet;
        //  var modelBase = (ModelBase) model;

        //  list.Add((T) MapEntityToModel(entity, model, map.PropertyInfoPairSet));
        //}

        //return list;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to build list of model '" + modelName + "'.", ex);
      }
    }

    public object ExecSp(string schemaName, string spName, object[] spParms)
    {
      try
      {
        object[] entitySetParms = new object[] { ConnectionStringName };
        var db = Activator.CreateInstance(_dataEntitiesType, entitySetParms);

        DateTime beginDT = DateTime.Now;

        var dbMap = _dataEntitiesType.GetCustomAttribute<DbMap>();
        if (dbMap.DbElement == DbElement.ListSet)
        {
          string methodName = "ExecSp_" + schemaName + "_" + spName;

          MethodInfo mi = null;
          MethodInfo[] miSet = db.GetType().GetMethods();
          foreach (var methodInfo in miSet)
          {
            if (methodInfo.Name == methodName)
            {
              if (methodInfo.GetParameters().Count() == 1)
              {
                mi = methodInfo;
                break;
              }
            }
          }

          if (mi == null)
            throw new Exception("Method name '" + methodName + "' could not be found in type '" + _dataEntitiesType.Name + "'.");

          object result = null;
          object[] parms = new object[] { spParms };
          result = mi.Invoke(db, parms);

          TimeSpan ts = DateTime.Now - beginDT;
          int ms = (int)ts.TotalMilliseconds;
          beginDT = DateTime.Now;

          return result;
        }

        throw new Exception("EntitySet processing is not yet implemented in RepositoryBase.Insert.");

        //IQueryable dbSet = map.DbSetPI.GetValue(db) as IQueryable;
        //var entity = Activa

        //foreach(object entity in dbSet)
        //{
        //  var model = Activator.CreateInstance(map.ModelType);
        //  ((ModelBase)model).PropertyInfoPairSet = map.PropertyInfoPairSet;
        //  var modelBase = (ModelBase) model;

        //  list.Add((T) MapEntityToModel(entity, model, map.PropertyInfoPairSet));
        //}

        //return list;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred attempting to execute stored procedure named '" + spName + "' and schema name '" + schemaName + "'.", ex);
      }
    }

    public IList<T> GetListForParent<T>(int parentId)
    {
      Type modelType = typeof(T);
      string modelName = modelType.Name;

      try
      {
        var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];
        EnsureMaps(map.ModelType, map.EntityType);

        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

        IList<T> list = new List<T>();

        object[] entitySetParms = new object[] { ConnectionStringName };
        var db = Activator.CreateInstance(_dataEntitiesType, entitySetParms);

        DateTime beginDT = DateTime.Now;

        var dbMap = _dataEntitiesType.GetCustomAttribute<DbMap>();
        if (dbMap.DbElement == DbElement.ListSet)
        {
          string methodName = "Get_" + map.EntityType.Name;

          MethodInfo mi = null;
          MethodInfo[] miSet = db.GetType().GetMethods();
          foreach(var methodInfo in miSet)
          {
            if (methodInfo.Name == methodName)
            {
              if (methodInfo.GetParameters().Count() == 1)
              {
                mi = methodInfo;
                break;
              }
            }
          }

          if (mi == null)
            throw new Exception("Method name '" + methodName + "' could not be found in type '" + _dataEntitiesType.Name + "'.");
          object[] parms = new object[] { parentId };
          var list2 = mi.Invoke(db, parms) as IEnumerable<EntityBase>;

          TimeSpan ts = DateTime.Now - beginDT;
          int ms = (int)ts.TotalMilliseconds;
          beginDT = DateTime.Now;

          foreach (object entity in list2)
          {
            var model = Activator.CreateInstance(map.ModelType);
            ((ModelBase)model).PropertyInfoPairSet = map.PropertyInfoPairSet;
            var modelBase = (ModelBase)model;

            list.Add((T)MapEntityToModel(entity, model, map.PropertyInfoPairSet));
          }

          ts = DateTime.Now - beginDT;
          ms = (int)ts.TotalMilliseconds;

          return list;
        }

        return list;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to build list of model '" + modelName + "'.", ex);
      }
    }

    public ModelBase Find<T>(int Id)
    {
      Type modelType = typeof(T);
      string modelName = modelType.Name;

      try
      {
        ModelBase model = null;

        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("No entity is mapped to the model named '" + modelName + "' in the ModelToEntityIndex of the EntityModelMapSet.");

        var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];
        EnsureMaps(map.ModelType, map.EntityType);

        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

        object[] entitySetParms = new object[] { ConnectionStringName };
        var db = Activator.CreateInstance(_dataEntitiesType, entitySetParms);

        var dbMap = _dataEntitiesType.GetCustomAttribute<DbMap>();

        if (dbMap.DbElement == DbElement.ListSet)
        {
          // need to implement non-EF based data access
        }


        Type genericDbSetType = typeof(System.Data.Entity.DbSet<>);
        Type dbSetType = genericDbSetType.MakeGenericType(map.EntityType);
        var dbSet = map.DbSetPI.GetValue(db);
        var findMI = dbSetType.GetMethod("Find");
        object[] findParms = new object[] { Id };
        var entity = findMI.Invoke(dbSet, new object[] { findParms});

        if (entity == null)
          return null;

        // map entity to model

        model = (ModelBase) Activator.CreateInstance(map.ModelType);

        ((ModelBase)model).PropertyInfoPairSet = map.PropertyInfoPairSet;

        return MapEntityToModel(entity, model, map.PropertyInfoPairSet);
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to build list of model '" + modelName + "'.", ex);
      }
    }

    public IList<T> GetSpResult<T>(SpParmSet spParmSet)
    {
      Type modelType = typeof(T);
      string modelName = modelType.Name;
      string spResultName = "entity name not yet determined";

      try
      {
        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("No entity is mapped to the model named '" + modelName + "' in the ModelToEntityIndex of the EntityModelMapSet.");
        string entityName = EntityModelMapSet.ModelToEntityIndex[modelName];
        var map = EntityModelMapSet[entityName];
        EnsureMaps(map.ModelType, map.EntityType);

        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

        var list = new List<T>();

        spResultName = map.EntityType.FullName;
        string spName = entityName.Replace("_Result", String.Empty);

        object[] entitySetParms = new object[] { ConnectionStringName };
        var db = Activator.CreateInstance(_dataEntitiesType, entitySetParms);
        object[] spParms = BuildSpParms(spParmSet);

        var spResults = map.DbSetMI.Invoke(db, spParms) as System.Data.Entity.Core.Objects.ObjectResult;
        foreach(var spResult in spResults)
        {
          var model = Activator.CreateInstance(map.ModelType);
          ((ModelBase)model).PropertyInfoPairSet = map.PropertyInfoPairSet;
          var modelBase = (ModelBase) model;
          list.Add((T) MapEntityToModel(spResult, model, map.PropertyInfoPairSet));
        }


        return list;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to build list of model '" + modelName +
                            "' from stored procedure result '" +spResultName + "'.", ex);
      }
    }

    private object[] BuildSpParms(SpParmSet spParmSet)
    {
      object[] spParms = new object[spParmSet.Count];

      for(int i = 0; i < spParmSet.Count; i++)
      {
        var spParm = spParmSet.ElementAt(i);
        spParms[i] = spParm.ParmValue;
      }

      return spParms;
    }

    private T MapEntityToModel<T>(object entity, T model, PropertyInfoPairSet pipSet)
    {
      if (entity == null)
        return model;

      if (pipSet == null)
        throw new Exception("No property mapping specification was made available to map entity properties to model properties for entity type'" +
                            entity.GetType().Name + "' and model type '" + model.GetType().Name + "'.");
      try
      {
        foreach(var pip in pipSet.Values)
        {
          var value = pip.EntityPropertyInfo.GetValue(entity);

          switch(pip.MappingRule)
          {
            case MappingRule.None:
              pip.ModelPropertyInfo.SetValue(model, value);
              break;

            case MappingRule.DoubleToDecimal:
              pip.ModelPropertyInfo.SetValue(model, value.ToDecimal());
              break;

            case MappingRule.DecimalToInt:
              pip.ModelPropertyInfo.SetValue(model, System.Convert.ToInt32(value));
              break;

            case MappingRule.Boolean1:

              if (value != null && value.ToString().ToUpper().In("Y,1"))
                pip.ModelPropertyInfo.SetValue(model, true);
              else
                pip.ModelPropertyInfo.SetValue(model, false);
              break;
          }
        }

        return model;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while mapping entity properties to model properties for entity type '" + entity.GetType().Name +
                            "' and model type '" + model.GetType().Name + "'.", ex);
      }
    }

    private ModelBase MapEntityToModel(object entity, ModelBase model, PropertyInfoPairSet pipSet)
    {
      if (entity == null)
        return model;

      if (pipSet == null)
        throw new Exception("No property mapping specification was made available to map entity properties to model properties for entity type'" +
                            entity.GetType().Name + "' and model type '" + model.GetType().Name + "'.");
      try
      {
        foreach(var pip in pipSet.Values)
        {
          var value = pip.EntityPropertyInfo.GetValue(entity);

          switch(pip.MappingRule)
          {
            case MappingRule.None:
              pip.ModelPropertyInfo.SetValue(model, value);
              break;

            case MappingRule.DoubleToDecimal:
              pip.ModelPropertyInfo.SetValue(model, value.ToDecimal());
              break;

            case MappingRule.Boolean1:

              if (value != null && value.ToString().ToUpper().In("Y,1"))
                pip.ModelPropertyInfo.SetValue(model, true);
              else
                pip.ModelPropertyInfo.SetValue(model, false);
              break;
          }
        }

        return model;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while mapping entity properties to model properties for entity type '" + entity.GetType().Name +
                            "' and model type '" + model.GetType().Name + "'.", ex);
      }
    }

    private T MapModelToEntity<T>(object model, T entity, PropertyInfoPairSet pipSet, bool isNew, object userId)
    {
      if (model == null)
        return entity;

      if (pipSet == null)
        throw new Exception("No property mapping specification was made available to map model properties to entity properties for model type'" +
                            model.GetType().Name + "' and entity type '" + entity.GetType().Name + "'.");
      try
      {
        foreach(var pip in pipSet.Values)
        {
          var value = pip.ModelPropertyInfo.GetValue(model);

          switch(pip.MappingRule)
          {
            case MappingRule.None:
              pip.EntityPropertyInfo.SetValue(entity, value);
              break;

            case MappingRule.DoubleToDecimal:
              pip.EntityPropertyInfo.SetValue(entity, value.ToDecimal());
              break;

            case MappingRule.DecimalToInt:
              pip.EntityPropertyInfo.SetValue(entity, System.Convert.ToInt32(value));
              break;

            case MappingRule.Boolean1:

              if (value != null && value.ToString().ToUpper().In("Y,1"))
                pip.EntityPropertyInfo.SetValue(entity, true);
              else
                pip.EntityPropertyInfo.SetValue(entity, false);
              break;
          }
        }

        return entity;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while mapping model properties to entity properties for model type '" + model.GetType().Name +
                            "' and entity type '" + entity.GetType().Name + "'.", ex);
      }
    }

    private EntityBase MapModelToEntity(object model, EntityBase entity, PropertyInfoPairSet pipSet, bool isNew, object userId)
    {
      if (model == null)
        return entity;

      if (pipSet == null)
        throw new Exception("No property mapping specification was made available to map model properties to entity properties for model type'" +
                            model.GetType().Name + "' and entity type '" + entity.GetType().Name + "'.");
      try
      {
        foreach(var pip in pipSet.Values)
        {
          var value = pip.ModelPropertyInfo.GetValue(model);

          switch(pip.MappingRule)
          {
            case MappingRule.None:
              pip.EntityPropertyInfo.SetValue(entity, value);
              break;

            case MappingRule.DoubleToDecimal:
              pip.EntityPropertyInfo.SetValue(entity, value.ToDecimal());
              break;

            case MappingRule.DecimalToInt:
              pip.EntityPropertyInfo.SetValue(entity, System.Convert.ToInt32(value));
              break;

            case MappingRule.Boolean1:

              if (value != null && value.ToString().ToUpper().In("Y,1"))
                pip.EntityPropertyInfo.SetValue(entity, true);
              else
                pip.EntityPropertyInfo.SetValue(entity, false);
              break;
          }
        }

        return entity;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while mapping model properties to entity properties for model type '" + model.GetType().Name +
                            "' and entity type '" + entity.GetType().Name + "'.", ex);
      }
    }

    public TaskResult Add(ModelBase model)
    {
      Type modelType = model.GetType();
      string modelName = modelType.Name;
      var taskResult = new TaskResult();
      taskResult.TaskName = modelName + ".Add";

      try
      {
        var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];
        EnsureMaps(map.ModelType, map.EntityType);

        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

        object[] entitySetParms = new object[] { ConnectionStringName };
        var db = Activator.CreateInstance(_dataEntitiesType, entitySetParms);

        var pkPip = map.PropertyInfoPairSet.Where(x => x.Value.IsPrimaryKey);
        if (pkPip == null)
          throw new Exception("Cannot locate PropertyInfoPair for the primary key for model '" + modelName + "'.");
        var pkPiKvp = pkPip.FirstOrDefault();
        PropertyInfo pkPi = pkPiKvp.Value.ModelPropertyInfo;
        var pk = pkPi.GetValue(model);

        Type genericDbSetType = typeof(System.Data.Entity.DbSet<>);
        Type dbSetType = genericDbSetType.MakeGenericType(map.EntityType);
        var dbSet = map.DbSetPI.GetValue(db);
        var findMI = dbSetType.GetMethod("Find");
        object[] findParms = new object[] { pk };
        var entity = findMI.Invoke(dbSet, new object[] { findParms});
        if (entity != null)
        {
          taskResult.TaskResultStatus = TaskResultStatus.AlreadyExists;
          taskResult.EndDateTime = DateTime.Now;
        }

        entity = Activator.CreateInstance(map.EntityType);

        MapModelToEntity(model, entity, map.PropertyInfoPairSet, false, null);

        var attachMI = dbSetType.GetMethod("Attach");
        attachMI.Invoke(dbSet, new object[] { entity });

        var dbContext = (System.Data.Entity.DbContext) db;
        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
        int rowsUpdated = dbContext.SaveChanges();

        if (rowsUpdated != 1)
        {
          taskResult.TaskResultStatus = TaskResultStatus.Failed;
          taskResult.Message = "Rows updated was " + rowsUpdated.ToString() + " when adding model '" + modelName + "'.";
          taskResult.EndDateTime = DateTime.Now;
          return taskResult;
        }

        taskResult.Message = "Model '" + modelName + "' successfully added.";
        taskResult.EndDateTime = DateTime.Now;
        return taskResult;
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to add model '" + modelName + "'.", ex);
      }
    }

    public void Update(ModelBase model, object userId)
    {
      Type modelType = model.GetType();
      string modelName = modelType.Name;

      try
      {
        var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];
        EnsureMaps(map.ModelType, map.EntityType);

        if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
          throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

        object[] entitySetParms = new object[] { ConnectionStringName };
        var db = Activator.CreateInstance(_dataEntitiesType, entitySetParms);

        var pkPip = map.PropertyInfoPairSet.Where(x => x.Value.IsPrimaryKey);
        if (pkPip == null)
          throw new Exception("Cannot locate PropertyInfoPair for the primary key for model '" + modelName + "'.");
        var pkPiKvp = pkPip.FirstOrDefault();
        PropertyInfo pkPi = pkPiKvp.Value.ModelPropertyInfo;
        var pk = pkPi.GetValue(model);

        Type genericDbSetType = typeof(System.Data.Entity.DbSet<>);
        Type dbSetType = genericDbSetType.MakeGenericType(map.EntityType);
        var dbSet = map.DbSetPI.GetValue(db);
        var findMI = dbSetType.GetMethod("Find");
        object[] findParms = new object[] { pk };
        var entity = findMI.Invoke(dbSet, new object[] { findParms});
        if (entity == null)
          throw new Exception("Entity not found for update, model name is '" + modelName + "' primary key value is '" + pk.ToString() + "'.");
        MapModelToEntity(model, entity, map.PropertyInfoPairSet, false, userId);
        var dbContext = (System.Data.Entity.DbContext) db;
        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        int rowsUpdated = dbContext.SaveChanges();

        if (rowsUpdated != 1)
          throw new Exception("Row update count was not equal to 1 when updating model '" + modelName + "'.");
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred attempting to update model '" + modelName + "'.", ex);
      }
    }

    public PropertyInfoPairSet GetPipSet(string modelName)
    {
      if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
        throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

      var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];
      return map.PropertyInfoPairSet;
    }

    public static dynamic Convert(dynamic source, Type dest)
    {
      return System.Convert.ChangeType(source, dest);
    }

    private static void LoadEntityTypes(Assembly dataAssembly)
    {
      try
      {
        bool useListSets = false;

        // get the data context - object marked with DbElement.EntitySet annotation
        var dataAssemblyTypes = dataAssembly.GetTypes().Where(t => t.GetCustomAttributes<DbMap>().Count() > 0);
        _dataEntitiesType = dataAssemblyTypes.Where(t => t.GetCustomAttribute<DbMap>().DbElement == DbElement.EntitySet).FirstOrDefault();

        if (_dataEntitiesType == null)
        {
          _dataEntitiesType = dataAssemblyTypes.Where(t => t.GetCustomAttribute<DbMap>().DbElement == DbElement.ListSet).FirstOrDefault();
          if (_dataEntitiesType != null)
            useListSets = true;
        }

        if (_dataEntitiesType == null)
          throw new Exception("No Entity Type found which is annotated with DbMap.DbElement = DbElement.EntitySet for data assembly '" + dataAssembly.FullName);

        string dataEntitiesTypeName = _dataEntitiesType.Name;

        var dataEntitiesDbMap = _dataEntitiesType.GetCustomAttribute<DbMap>();

        // get the PropertyInfo objects for the entity classes (DbSets)
        var dbSetPIs = new Dictionary<string, PropertyInfo>();
        string setPropertyType = "DbSet`1";
        if (useListSets)
          setPropertyType = "List`1";

        var piList = _dataEntitiesType.GetProperties().Where(p => p.PropertyType.Name == setPropertyType); ;
        foreach (var pi in piList)
        {
          string name = pi.Name;
          Type[] args = pi.PropertyType.GetGenericArguments();
          if (args.Length == 1)
          {
            if (!dbSetPIs.ContainsKey(args[0].FullName))
            {
              dbSetPIs.Add(args[0].FullName, pi);
            }
          }
        }

        // get the MethodInfo objects for the stored proc executors (ObjectResults)
        var dbSetMIs = new Dictionary<string, MethodInfo>();
        var miList = _dataEntitiesType.GetMethods().Where(m => m.ReturnType.Name == "ObjectResult`1");
        foreach(var mi in miList)
        {
          string name = mi.Name;
          Type[] args = mi.ReturnType.GetGenericArguments();
          if (args.Length == 1)
          {
            if (!dbSetMIs.ContainsKey(args[0].FullName))
            {
              dbSetMIs.Add(args[0].FullName, mi);
            }
          }
        }

        // get the entity types
        EntityTypes = new Dictionary<string, EntityType>();
        List<Type> entityTypes = dataAssembly.GetTypes().Where(x => x.GetCustomAttributes<DbMap>().Count() > 0).ToList();

        foreach (Type entityType in entityTypes)
        {
          DbMap dbMap = (DbMap)entityType.GetCustomAttributes(typeof(DbMap), true).FirstOrDefault();
          if (dbMap != null)
          {
            if (dbMap.DbElement == DbElement.Table || dbMap.DbElement == DbElement.StoredProcedure)
            {
              var et = new EntityType();
              et.TypeOfEntity = entityType;
              if (dbSetPIs.ContainsKey(entityType.FullName))
                et.DbSetPI = dbSetPIs[entityType.FullName];
              if (dbSetMIs.ContainsKey(entityType.FullName))
                et.DbSetMI = dbSetMIs[entityType.FullName];
              string typeName = entityType.Name;
              if (!EntityTypes.ContainsKey(typeName))
                EntityTypes.Add(typeName, et);
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred during the mapping of Entity Types.", ex);
      }
    }

    private static void LoadModelTypes(Assembly businessObjectAssembly)
    {
      List<Type> modelTypes = businessObjectAssembly.GetTypes().Where(x => x.GetCustomAttributes<DbMap>().Count() > 0).ToList();

      foreach (Type modelType in modelTypes)
      {
        DbMap dbMap = (DbMap) modelType.GetCustomAttributes(typeof(DbMap), true).FirstOrDefault();
        if (dbMap != null)
        {
          if (dbMap.DbElement == DbElement.Model)
          {
            string dataStoreName = dbMap.EntityStore;
            string modelTypeName = modelType.FullName;
            string entityTypeName = dbMap.EntityName;
            if (!EntityTypes.ContainsKey(entityTypeName))
              continue;
            //throw new Exception("Entity type '" + entityTypeName + "' specified in DbMap.EntityName for model '" + modelTypeName + "' does not exist " +
            //                    "in the EntityTypes collection.");
            Type entityType = EntityTypes[entityTypeName].TypeOfEntity;
            PropertyInfo dbSetPI = EntityTypes[entityTypeName].DbSetPI;
            MethodInfo dbSetMI = EntityTypes[entityTypeName].DbSetMI;
            if (!EntityModelMapSet.ContainsKey(dataStoreName + "." + entityType.Name))
            {
              var entityModelMap = new EntityModelMap(dataStoreName + "." + entityType.Name, entityType, modelType, dbSetPI, dbSetMI);
              EntityModelMapSet.Add(entityModelMap.Name, entityModelMap);
              if (EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelType.Name))
                EntityModelMapSet.ModelToEntityIndex[modelType.Name] = entityModelMap.Name;
              else
                EntityModelMapSet.ModelToEntityIndex.Add(modelType.Name, entityModelMap.Name);
            }
          }
        }
      }
    }

    private object EnsureMaps_LockObject = new object();
    protected void EnsureMaps(Type modelType, Type entityType)
    {
      if (PropertyInfoSet.ContainsKey(modelType.FullName) &&
          PropertyInfoSet.ContainsKey(entityType.FullName) &&
          EntityModelMapSet.ContainsKey(DataStoreName + "." + entityType.Name) &&
          EntityModelMapSet[DataStoreName + "." + entityType.Name].PropertiesLoaded)
        return;

      if (Monitor.TryEnter(EnsureMaps_LockObject, 1000))
      {
        try
        {
          if (!PropertyInfoSet.ContainsKey(modelType.FullName))
          {
            var piList = modelType.GetProperties().ToList();
            PropertyInfoSet.Add(modelType.FullName, new Dictionary<string, PropertyInfo>());
            foreach (var pi in piList)
              PropertyInfoSet[modelType.FullName].Add(pi.Name, pi);
          }

          if (!PropertyInfoSet.ContainsKey(entityType.FullName))
          {
            var piList = entityType.GetProperties().ToList();
            PropertyInfoSet.Add(entityType.FullName, new Dictionary<string, PropertyInfo>());
            foreach (var pi in piList)
              PropertyInfoSet[entityType.FullName].Add(pi.Name, pi);
          }

          // deferred property loading for existing maps
          if (EntityModelMapSet.ContainsKey(DataStoreName + "." + entityType.Name))
          {
            var entityModelMap = EntityModelMapSet[DataStoreName + "." + entityType.Name];
            foreach (var pi in PropertyInfoSet[modelType.FullName].Values)
            {
              var dbMap = pi.GetCustomAttributes<DbMap>().Where(x => x.DbElement == DbElement.Column && x.EntityStore == DataStoreName).FirstOrDefault();
              if (dbMap != null)
              {
                if (!entityModelMap.PropertyInfoPairSet.ContainsKey(pi.Name))
                {
                  var propertyInfoSet = PropertyInfoSet[entityType.FullName];
                  if (!propertyInfoSet.ContainsKey(dbMap.ColumnName))
                    throw new Exception("PropertyInfoSet for entity '" + entityType.FullName + "' does not contain a property " +
                                        "for the mapped name '" + dbMap.ColumnName + "'.");
                  PropertyInfo entityPropertyInfo = propertyInfoSet[dbMap.ColumnName];
                  entityModelMap.PropertyInfoPairSet.Add(pi.Name, new PropertyInfoPair(entityPropertyInfo, pi, dbMap.IsNullable, dbMap.IsPrimaryKey, dbMap.MappingRule));
                }
              }
            }
            entityModelMap.PropertiesLoaded = true;
          }
          else // create map and load properties
          {
            throw new Exception("EntityModelMapSet does not contain entry for " + DataStoreName + "." + entityType.Name + ".");
          }

        }
        catch (Exception ex)
        {
          throw new Exception("An exception occurred attempting to establish the PropertyInfoSets and EntityModelMapSet in the WellMasterRepository for types '" +
                              modelType.FullName + "' and '" + entityType.FullName + "' for data store + '" + DataStoreName + "'.", ex);
        }
        finally
        {
          Monitor.Exit(EnsureMaps_LockObject);
        }
      }
      else
      {
        throw new Exception("The RepositoryBase failed to obtain a lock in 1000 milliseconds for establishing PropertyInfoSets and EntityModelMapSet for types '" +
                            modelType.FullName + "' and '" + entityType.FullName + "' for data store + '" + DataStoreName + "'.");
      }
    }

    public CacheEntry GetFromCache(string modelName)
    {
      var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];
      EnsureMaps(map.ModelType, map.EntityType);

      if (!EntityModelMapSet.ModelToEntityIndex.ContainsKey(modelName))
        throw new Exception("Model name '" + modelName + "' does not exist in the ModelToEntityIndex of the EntityModelMapSet.");

      var entityPi =  map.PropertyInfoPairSet.Where(x => x.Value.IsPrimaryKey).FirstOrDefault();
      if (entityPi.Key == null)
        throw new Exception("Could not find primary key for entity which corresponds to model '" + modelName + "'.");
      string modelKey = entityPi.Value.ModelPropertyInfo.Name;

      return CacheManager.GetModelCache(modelName, modelKey, this);
    }

    public static string GetKeyNameForModel(string modelName)
    {
      var map = EntityModelMapSet[EntityModelMapSet.ModelToEntityIndex[modelName]];

      foreach (var pip in map.PropertyInfoPairSet)
      {
        if (pip.Value.IsPrimaryKey)
          return pip.Key;
      }

      return String.Empty;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposeManagedResources)
    {
      if (!_isDisposed)
      {
        if (disposeManagedResources)
        {

        }
        this._isDisposed = true;
      }
    }
  }
}
