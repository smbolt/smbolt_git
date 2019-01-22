using System;
using System.Collections.Generic;
using System.Text;

namespace NameTags
{
  public static class Enums
  {
    public enum PageSize
    {
      Letter,
      Legal
    }

    public enum PageOrientation
    {
      Portrait,
      Landscape
    }

    public enum PageAlignment
    {
      Left,
      Center
    }

    public enum DisplayMode
    {
      Designer,
      Display,
      Printer
    }

    public enum Action
    {
      ProjectProperties = 0,
      OpenProject = 1,
      SaveProject = 2,
      DesignMode = 3,
      PrintMode = 4,
      PrevPage = 5,
      NextPage = 6,
      EditNames = 7,
      PrintTags = 8,
      AddTextObject = 10,
      AddPictureObject = 11,
      AddShapeObject = 12,
      SetBorderWidth = 13,
      SetBorderColor = 14,
      SetFont = 15,
      SetFillColor = 16,
      ZoomIn = 17,
      ZoomOut = 18,
      SendToBack = 19,
      SendBackward = 20,
      BringForward = 21,
      BringToFront = 22,
      AddDiplomaPicture = 23
    }

    public enum EditMode
    {
      Add,
      Update,
      None
    }

    public enum ObjectType
    {
      TextObject,
      GraphicsObject,
      RectangleObject,
      EllipseObject,
      DiplomaPicture
    }


  }
}
