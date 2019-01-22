using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Org.GS;

namespace Org.Dx.Business.TextProcessing
{
  [ObfuscationAttribute(Exclude = true, ApplyToMembers = true)]
  [XMap(CollectionElements = "FormatSpec", XType = XType.Element)] 
  public class FormatSpecSet : Dictionary<string, FormatSpec>
  {
    private object RecognizeFormat_LockObject = new object();
    public string RecognizeFormat(string text)
    {
      try
      {
        string format = "UNDEFINED";

        if (text == null)
          return format;

        string txt = text.Trim().ToLower();
        if (txt.Length == 0)
          return format;

        // single threaded approach
        foreach (var spec in this)
        {
          if (spec.Value.IsMatch(txt))
            return spec.Key;
        }

        return "UNDEFINED";

        // need to consider the multi-threaded approach, it was taking as long as the single threaded approach above.

        CancellationTokenSource cts = new CancellationTokenSource();
        ParallelOptions po = new ParallelOptions();
        po.CancellationToken = cts.Token;
        po.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

        try
        {
          Parallel.ForEach(this, po, (kvpSpec) =>
          {
            if (kvpSpec.Value.IsMatch(txt))
            {
              if (Monitor.TryEnter(RecognizeFormat_LockObject, 1000));
              {
                format = kvpSpec.Key;
                cts.Cancel();
                Monitor.Exit(RecognizeFormat_LockObject);
              }
            }
          });
        }
        catch (OperationCanceledException e)
        {
          return format;
        }
        finally
        {
          cts.Dispose();
        }

        return format;
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred while attempting to determine if the text is of a recognized format.", ex);
      }
    }

    public void SetTextStructure(Text text)
    {
      //foreach (var formatSpec in this.Values)
      //  text.TextList.AddRange(formatSpec.TextStructureDefinition.GetLowerLevels(text.RawText, formatSpec.RecogSpecSet, formatSpec.Name));
    }

    public List<StatementFile> GetStatementFiles(Text text)
    {
      var statementFiles = new List<StatementFile>();
      foreach (var formatSpec in this.Values)
        statementFiles.AddRange(formatSpec.DataMapSet.GetStatementFiles(text, formatSpec.RecogSpecSet, formatSpec.Name));

      return statementFiles;
    }
  }
}
