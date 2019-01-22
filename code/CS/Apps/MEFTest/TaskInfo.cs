using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.GS;

namespace Org.MEFTest
{
  public class TaskInfo
  {
    public string TaskName { get; set; }
    public string TaskProcessorName { get; set; }
    public string TaskProcessorVersion { get; set; }
    public string TaskProcessorNameAndVersion { get { return this.TaskProcessorName + "_" + this.TaskProcessorVersion; } }
    public string TaskNodeFolder { get; set; }
    public string StatementType { get; set; }
    public string InputDbPrefix { get; set; }
    public string OutputDbPrefix { get; set; }

    public TaskInfo(string taskName, string values)
    {
      this.TaskName = taskName;

      string[] tokens = values.Split(Constants.PipeDelimiter);
      if (tokens.Length != 6)
        throw new Exception("Invalid TaskInfoDictionary entry '" + values + "' - the value should contain 3 pipe-delimited values: " +
                            "TaskProcessor Name, TaskProcessor Version, TaskNode folder name, statement type, input DbSpec prefix, and output DbSpec prefix.");

      this.TaskProcessorName = tokens[0].Trim();
      this.TaskProcessorVersion = tokens[1].Trim();
      this.TaskNodeFolder = tokens[2].Trim();
      this.StatementType = tokens[3].Trim();
      this.InputDbPrefix = tokens[4].Trim();
      this.OutputDbPrefix = tokens[5].Trim();
    }
  }
}
