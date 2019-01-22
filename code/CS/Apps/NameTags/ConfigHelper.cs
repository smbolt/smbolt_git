using System;
using System.Collections.Generic;
using System.Text;

namespace NameTags
{
  public static class ConfigHelper
  {

    private static string _defaultProjectPath = @"c:\NameTagCreator1.0\";
    public static string DefaultProjectPath
    {
      get {
        return _defaultProjectPath;
      }
      set {
        _defaultProjectPath = value;
      }
    }

    private static string _defaultLogPath = @"c:\NameTagCreator1.0\log\";
    public static string DefaultLogPath
    {
      get {
        return _defaultLogPath;
      }
      set {
        _defaultLogPath = value;
      }
    }

    private static string _defaultLogFileName = @"NameTags.log";
    public static string DefaultLogFileName
    {
      get {
        return _defaultLogFileName;
      }
      set {
        _defaultLogFileName = value;
      }
    }

    private static string _defaultImportFileFullPath = @"C:\NameTagCreator1.0\Names.txt";
    public static string DefaultImportFileFullPath
    {
      get {
        return _defaultImportFileFullPath;
      }
      set {
        _defaultImportFileFullPath = value;
      }
    }

    private static string _defaultPhotoPath = @"C:\Truth School Jr - 2010\";
    public static string DefaultPhotoPath
    {
      get {
        return _defaultPhotoPath;
      }
      set {
        _defaultPhotoPath = value;
      }
    }

    private static string _defaultPhotoSampleFileName = @"Sample.jpg";
    public static string DefaultPhotoSampleFileName
    {
      get {
        return _defaultPhotoSampleFileName;
      }
      set {
        _defaultPhotoSampleFileName = value;
      }
    }

    private static string _appTitle = "Name Tag Creator 1.0";
    public static string AppTitle
    {
      get {
        return _appTitle;
      }
      set {
        _appTitle = value;
      }
    }

  }
}
