using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;
//using Word = Microsoft.Office.Interop.Word;
using Org.GS;

namespace Org.DocGen
{
  public class WordEngine : IDisposable
  {
    //public void EngageSpeller(string path)
    //{
    //    Microsoft.Office.Interop.Word.Application wordApp;
    //    wordApp = new Microsoft.Office.Interop.Word.Application();
    //    wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
    //    object filename = path;
    //    object missingValue = Type.Missing;

    //    // Using OpenOld so as to be compatible with other versions of Word
    //    Word.Document document = wordApp.Documents.OpenOld(ref filename,
    //                                               ref missingValue, ref missingValue, ref missingValue,
    //                                               ref missingValue, ref missingValue, ref missingValue,
    //                                               ref missingValue, ref missingValue, ref missingValue);


    //    bool isCorrect = wordApp.CheckSpelling("alphabit");

    //    Word.SpellingSuggestions ss = wordApp.GetSpellingSuggestions("alphabit");
    //    foreach (Word.SpellingSuggestion s in ss)
    //    {
    //        string name = s.Name;
    //    }

    //    //RunSpeller(document);




    //    ((Microsoft.Office.Interop.Word._Application)wordApp).Quit(ref missingValue, ref missingValue, ref missingValue);
    //}

    //public void RunSpeller(Word.Document d)
    //{



    //}

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        // free managed resources
        //if (managedResource != null)
        //{
        //    managedResource.Dispose();
        //    managedResource = null;
        //}
      }
      // free native resources if there are any.
      //if (nativeResource != IntPtr.Zero)
      //{
      //    Marshal.FreeHGlobal(nativeResource);
      //    nativeResource = IntPtr.Zero;
      //}
    }

  }
}
