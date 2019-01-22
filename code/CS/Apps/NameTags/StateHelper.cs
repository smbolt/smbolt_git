using System;
using System.Collections.Generic;
using System.Text;

namespace NameTags
{
  public static class StateHelper
  {
    private static bool _isInitialized = false;
    public static bool IsInitialized
    {
      get {
        return _isInitialized;
      }
      set {
        _isInitialized = value;
      }
    }

    private static float _scale = 1.0F;
    public static float Scale
    {
      get {
        return _scale;
      }
      set {
        _scale = value;
      }
    }

    private static float _prevScale = 1.0F;
    public static float PrevScale
    {
      get {
        return _prevScale;
      }
      set {
        _prevScale = value;
      }
    }


    private static float _prevX = 0.0F;
    public static float PrevX
    {
      get {
        return _prevX;
      }
      set {
        _prevX = value;
      }
    }

    private static float _prevY = 0.0F;
    public static float PrevY
    {
      get {
        return _prevY;
      }
      set {
        _prevY = value;
      }
    }

    private static bool _isInDesignMode;
    public static bool IsInDesignMode
    {
      get {
        return _isInDesignMode;
      }
      set {
        _isInDesignMode = value;
      }
    }

    private static int _tagsPerPage;
    public static int TagsPerPage
    {
      get {
        return _tagsPerPage;
      }
      set {
        _tagsPerPage = value;
      }
    }

    private static int _totalTags;
    public static int TotalTags
    {
      get {
        return _totalTags;
      }
      set {
        _totalTags = value;
      }
    }

    private static int _firstTagOnPage;
    public static int FirstTagOnPage
    {
      get {
        return _firstTagOnPage;
      }
      set {
        _firstTagOnPage = value;
      }
    }

    private static int _lastTagOnPage;
    public static int LastTagOnPage
    {
      get {
        return _lastTagOnPage;
      }
      set {
        _lastTagOnPage = value;
      }
    }
  }
}
