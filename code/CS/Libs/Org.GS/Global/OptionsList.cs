using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GS
{
  public class OptionsList
  {
    private List<string> _options;
    public int OptionCount
    {
      get
      {
        if (_options == null)
          _options = new List<string>();
        return _options.Count;
      }
    }

    public OptionsList()
    {
      _options = new List<string>();
    }

    public OptionsList(string options)
    {
      if (options.IsBlank())
        _options = new List<string>();
      else
        _options = options.Trim().Split(Constants.PipeDelimiter, StringSplitOptions.RemoveEmptyEntries).ToList().TrimItems();
    }

    public OptionsList(List<string> options)
    {
      if (options == null)
        _options = new List<string>();
      else
        _options = options;
    }

    public bool OptionIsDefined(string optionName)
    {
      if (_options == null)
        _options = new List<string>();

      optionName = optionName.Trim();
      if (_options.Contains(optionName))
        return true;

      return false;
    }

    public string ElementAt(int index)
    {
      if (_options == null)
        _options = new List<string>();

      if (index > _options.Count - 1)
        return String.Empty;

      return _options.ElementAt(index);
    }

  }
}
