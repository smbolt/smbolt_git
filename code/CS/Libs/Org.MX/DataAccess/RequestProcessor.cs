using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Org.MX.Model;
using Org.GS.Configuration;
using Org.GS.Database;
using Org.GS;

namespace Org.MX.DataAccess
{
  public class RequestProcessor : RequestProcessorBase
  {
    public RequestProcessor(ConfigDbSpec configDbSpec)
      : base(configDbSpec) {  }

    public MetricObjectSet<T> Get<T>()
    {
      try
      {
        var metricObjectSet = new MetricObjectSet<T>();
        var dbTable = typeof(T).ToDbTable();
        string sql = base.BuildSelectQuery(dbTable);

        using (var cmd = new SqlCommand(sql, this.Connection))
        {
          cmd.CommandType = System.Data.CommandType.Text;
          var da = new SqlDataAdapter(cmd);
          var ds = new DataSet();
          da.Fill(ds);

          if (ds.Tables.Count == 0)
            return metricObjectSet;

          var dt = ds.Tables[0];
          foreach (DataRow r in dt.Rows)
          {
            var entity = Activator.CreateInstance(typeof(T));
            object sequencer = null;
            DbColumn sequencerColumn = null;
            foreach (var dbColumn in dbTable.DbColumnSet.Values)
            {
              var pi = entity.GetType().GetProperty(dbColumn.Name);
              if (pi == null)
                throw new Exception("Cannot retrieve PropertyInfo object for property name '" + dbColumn.Name +
                                    "' from entity type '" + entity.GetType().Name + "'.");
              var columnValue = r[dbColumn.Name];
              if (columnValue.GetType() != typeof(System.DBNull))
              {
                pi.SetValue(entity, columnValue);
                if (dbColumn.IsSequencer)
                {
                  if (sequencer != null)
                    throw new Exception("A sequencer column has already been determined to be column '" +
                                        sequencerColumn.Name + "' for entity type '" + entity.GetType().Name + "', " +
                                        "cannot subsequently set column '" + dbColumn.Name + "' to be the primary key.");
                  sequencerColumn = dbColumn;
                  sequencer = pi.GetValue(entity);
                  if (sequencer.GetType() != typeof(System.Int32))
                    throw new Exception("MetricObjectSet entities must have sequencer column of type Int32, found the sequencer " +
                                        "column of entity '" + entity.GetType().Name + "' which is column '" + dbColumn.Name +
                                        " to be of type '" + sequencer.GetType().Name + "'.");
                }
              }
            }

            if (sequencer == null)
              throw new Exception("No sequencer column was located for entity '" + entity.GetType().Name + "'.");

            if (metricObjectSet.ContainsKey(sequencer.ToInt32()))
              throw new Exception("The MetricObjectSet collection for type '" + entity.GetType().Name + "' already contains " + 
                                  "the sequencer '" + sequencer.ToInt32().ToString() + "'.");

            metricObjectSet.Add(sequencer.ToInt32(), (T)entity);
          }
        }

        return metricObjectSet;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to retrieve all entities of type '" + typeof(T).Name + "'.", ex);
      }
    }
  }
}
