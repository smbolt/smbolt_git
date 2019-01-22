using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NameTags
{
  [Serializable]
  public class PrintPageSettings
  {
    private Enums.PageSize _pageSize;
    public Enums.PageSize PageSize
    {
      get {
        return _pageSize;
      }
      set {
        _pageSize = value;
      }
    }

    private Enums.PageOrientation _pageOrientation;
    public Enums.PageOrientation PageOrientation
    {
      get {
        return _pageOrientation;
      }
      set {
        _pageOrientation = value;
      }
    }

    private Enums.PageAlignment _pageAlignment;
    public Enums.PageAlignment PageAlignment
    {
      get {
        return _pageAlignment;
      }
      set {
        _pageAlignment = value;
      }
    }

    private float _leftMargin;
    public float LeftMargin
    {
      get {
        return _leftMargin;
      }
      set {
        _leftMargin = value;
      }
    }

    private float _rightMargin;
    public float RightMargin
    {
      get {
        return _rightMargin;
      }
      set {
        _rightMargin = value;
      }
    }

    private float _topMargin;
    public float TopMargin
    {
      get {
        return _topMargin;
      }
      set {
        _topMargin = value;
      }
    }

    private float _bottomMargin;
    public float BottomMargin
    {
      get {
        return _bottomMargin;
      }
      set {
        _bottomMargin = value;
      }
    }

    private float _verticalSpacing;
    public float VerticalSpacing
    {
      get {
        return _verticalSpacing;
      }
      set {
        _verticalSpacing = value;
      }
    }

    private float _horizontalSpacing;
    public float HorizontalSpacing
    {
      get {
        return _horizontalSpacing;
      }
      set {
        _horizontalSpacing = value;
      }
    }

    private float _printAdjustHorizontal;
    public float PrintAdjustHorizontal
    {
      get {
        return _printAdjustHorizontal;
      }
      set {
        _printAdjustHorizontal = value;
      }
    }

    private float _printAdjustVertical;
    public float PrintAdjustVertical
    {
      get {
        return _printAdjustVertical;
      }
      set {
        _printAdjustVertical = value;
      }
    }

    private float _nameTagWidth;
    public float NameTagWidth
    {
      get {
        return _nameTagWidth;
      }
      set {
        _nameTagWidth = value;
      }
    }

    private float _nameTagHeight;
    public float NameTagHeight
    {
      get {
        return _nameTagHeight;
      }
      set {
        _nameTagHeight = value;
      }
    }

    public PrintPageSettings()
    {
      _pageSize = Enums.PageSize.Letter;
      _pageOrientation = Enums.PageOrientation.Portrait;
      _pageAlignment = Enums.PageAlignment.Left;
      _leftMargin = 50.0F;
      _rightMargin = 50.0F;
      _topMargin = 50.0F;
      _bottomMargin = 50.0F;
      _verticalSpacing = 0.0F;
      _horizontalSpacing = 0.0F;
      _printAdjustVertical = 0.0F;
      _printAdjustHorizontal = 0.0F;
      _nameTagWidth = 325.0F;
      _nameTagHeight = 225.0F;
    }

    public SizeF GetPageSize()
    {
      SizeF pageSize = new Size();
      if (_pageOrientation == Enums.PageOrientation.Portrait)
      {
        if (_pageSize == Enums.PageSize.Letter)
        {
          pageSize.Width = 850;
          pageSize.Height = 1100;
        }
        else
        {
          pageSize.Width = 850;
          pageSize.Height = 1400;
        }
      }
      else
      {
        if (_pageSize == Enums.PageSize.Letter)
        {
          pageSize.Width = 1100;
          pageSize.Height = 850;
        }
        else
        {
          pageSize.Width = 1400;
          pageSize.Height = 850;
        }
      }

      return pageSize;
    }

  }
}
