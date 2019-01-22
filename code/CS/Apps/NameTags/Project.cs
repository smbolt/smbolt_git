using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace NameTags
{
  [Serializable]
  public class Project
  {
    private string _name;
    public string Name
    {
      get {
        return _name;
      }
      set {
        _name = value;
      }
    }

    private string _fullFileName;
    public string FullFileName
    {
      get {
        return _fullFileName;
      }
      set
      {
        _fullFileName = value;
        _name = Path.GetFileNameWithoutExtension(_fullFileName);
      }
    }

    private bool _isValidProject;
    public bool IsValidProject
    {
      get {
        return _isValidProject;
      }
      set {
        _isValidProject = value;
      }
    }

    private bool _isDirty;
    public bool IsDirty
    {
      get {
        return _isDirty;
      }
      set {
        _isDirty = value;
      }
    }

    private float _scale;
    public float Scale
    {
      get {
        return _scale;
      }
      set
      {
        _scale = value;
        _isDirty = true;
      }
    }

    private PrintPageSettings _defaultPrintPageSettings;
    private PrintPageSettings _printPageSettings;

    public PrintPageSettings PrintPageSettings
    {
      get {
        return _printPageSettings;
      }
      set
      {
        _printPageSettings = value;
        _isDirty = true;
      }
    }

    private DrawingObjectSet _drawingObjects;
    public DrawingObjectSet DrawingObjects
    {
      get {
        return _drawingObjects;
      }
      set
      {
        _drawingObjects = value;
        _isDirty = true;
      }
    }

    private PersonSet _persons;
    public PersonSet Persons
    {
      get {
        return _persons;
      }
      set
      {
        _persons = value;
        _isDirty = true;
      }
    }

    public Project(string name)
    {
      _name = name;
      _fullFileName = String.Empty;
      _defaultPrintPageSettings = new PrintPageSettings();
      _printPageSettings = new PrintPageSettings();
      _persons = new PersonSet();
      _drawingObjects = new DrawingObjectSet();
      _scale = 2.0F;
      _isValidProject = false;
      _isDirty = false;
    }


    public void InitializeDrawingObjects()
    {
      foreach (KeyValuePair<int, DrawingObject> kvpDrawingObject in _drawingObjects)
      {
        kvpDrawingObject.Value.SelectionChanged += new SelectionChangedHandler(_drawingObjects.SelectedObjectsChanged);
        kvpDrawingObject.Value.SetDrawingObjectsDirty += new SetDrawingObjectsDirtyHandler(_drawingObjects.SetDirty);
      }
    }

    public void AddPerson(Person person)
    {
      if(_persons.ContainsKey(person.FullName))
      {
        person.FullName += "-" + _persons.Count.ToString().Trim();
      }

      _persons.Add(person.FullName, person);
      _isDirty = true;
    }

    public int [] AddObject(DrawingObject obj)
    {
      int key = GetHighestKey();
      key++;
      _drawingObjects.AddObject(key, obj);
      return _drawingObjects.SelectedObjectKeys;
    }

    private int GetHighestKey()
    {
      int highestKey = -1;
      foreach (KeyValuePair<int, DrawingObject> kvpDrawingObject in _drawingObjects)
      {
        if (kvpDrawingObject.Key > highestKey)
          highestKey = kvpDrawingObject.Key;
      }
      return highestKey;
    }


    public void DeleteSelected()
    {
      int keyToDelete = -1;
      foreach (KeyValuePair<int, DrawingObject> kvpDrawingObject in _drawingObjects)
      {
        if (kvpDrawingObject.Value.Selected)
          keyToDelete = kvpDrawingObject.Key;
      }

      if (keyToDelete != -1)
      {
        _drawingObjects.RemoveObject(keyToDelete);
      }
    }

    public void UnlockAll()
    {
      foreach (var drawingObject in _drawingObjects.Values)
      {
        drawingObject.IsLocked = false;
      }
    }


    public void DrawObjects(System.Drawing.Graphics gr, PointF origin, PointF adjust, Enums.DisplayMode mode, Dictionary<string,string> dictionary)
    {
      foreach (KeyValuePair<int, DrawingObject> kvpDrawingObject in _drawingObjects)
      {
        kvpDrawingObject.Value.DrawObject(gr, _scale, origin, adjust, mode, dictionary);
      }
    }

    public int ObjectCount()
    {
      return _drawingObjects.Count;
    }

    public void DeselectAll()
    {
      _drawingObjects.DeselectAll();
    }


    public int[] GetAtXY(float x, float y, bool IsControlPressed)
    {
      _drawingObjects.GetObjectKeys = new int[0];

      for (int i = _drawingObjects.Count - 1; i > -1; i--)
      {
        if (_drawingObjects.Values[i].SelectAtXY(x, y, _scale))
        {
          _drawingObjects.GetObjectKeys = new int[1];
          _drawingObjects.GetObjectKeys[0] = _drawingObjects.Keys[i];
          return _drawingObjects.GetObjectKeys;
        }
      }

      return _drawingObjects.GetObjectKeys;
    }

    public int[] SelectAtXY(float x, float y, bool IsControlPressed)
    {
      if (!IsControlPressed)
        _drawingObjects.DeselectAll();


      for (int i = _drawingObjects.Count - 1; i > -1; i--)
      {
        if (_drawingObjects.Values[i].SelectAtXY(x, y, _scale))
        {
          _drawingObjects.Values[i].Selected = true;
          return _drawingObjects.SelectedObjectKeys;
        }
      }

      return _drawingObjects.SelectedObjectKeys;
    }

    public DrawingObject GetObjectByKey(int key)
    {
      return (DrawingObject) _drawingObjects[key];
    }

    public DrawingObject GetObjectByName(string objectName)
    {
      DeselectAll();

      foreach (var drawingObject in _drawingObjects.Values)
      {
        if (drawingObject.Name == objectName)
        {
          drawingObject.Selected = true;
          return drawingObject;
        }
      }

      return null;
    }

    public int SelectedPersonCount()
    {
      int selectedPersonCount = 0;

      foreach (KeyValuePair<string, Person> kvpPerson in _persons)
      {
        if (kvpPerson.Value.Selected)
          selectedPersonCount++;
      }

      return selectedPersonCount;
    }

    public List<Person> GetPersonPrintList()
    {
      List<Person> personList = new List<Person>();

      foreach (KeyValuePair<string, Person> kvpPerson in _persons)
      {
        if (kvpPerson.Value.Selected)
          personList.Add(kvpPerson.Value);
      }

      return personList;
    }

    public int GetPersonPrintCount()
    {
      int personPrintCount = 0;

      foreach (KeyValuePair<string, Person> kvpPerson in _persons)
      {
        if (kvpPerson.Value.Selected)
          personPrintCount++;
      }

      return personPrintCount;
    }

    public SizeF GetPageSize()
    {
      SizeF sz = new SizeF(0F, 0F);

      switch(this.PrintPageSettings.PageOrientation)
      {
        case Enums.PageOrientation.Landscape:
          switch(this.PrintPageSettings.PageSize)
          {
            case Enums.PageSize.Legal:
              sz.Width = 1400;
              sz.Height = 850;
              break;

            default: // letter
              sz.Width = 1100;
              sz.Height = 850;
              break;
          }
          break;

        default: // portrait
          switch (this.PrintPageSettings.PageSize)
          {
            case Enums.PageSize.Legal:
              sz.Width = 850;
              sz.Height = 1400;
              break;

            default: // letter
              sz.Width = 850;
              sz.Height = 1100;
              break;
          }
          break;
      }

      return sz;
    }

    public Size GetTagMatrix()
    {
      Size tagMatrix = new Size(0, 0);
      SizeF pageSize = GetPageSize();

      // find space for name tags (less margins, but not spacing between tags - for now
      float horzTagSpace = pageSize.Width - (_printPageSettings.LeftMargin + _printPageSettings.RightMargin);
      float vertTagSpace = pageSize.Height - (_printPageSettings.TopMargin + _printPageSettings.BottomMargin);

      // compute rows and columns of tags without the spacing between tags - for now
      float fCols = horzTagSpace / _printPageSettings.NameTagWidth;
      float fRows = vertTagSpace / _printPageSettings.NameTagHeight;
      int nCols = Convert.ToInt32(fCols);
      int nRows = Convert.ToInt32(fRows);
      // round down
      while(nCols > fCols)
        nCols--;
      while(nRows > fRows)
        nRows--;

      // now insert the spacing and recompute the rows
      horzTagSpace -= ((nCols - 1) * _printPageSettings.HorizontalSpacing);
      vertTagSpace -= ((nRows - 1) * _printPageSettings.VerticalSpacing);
      fCols = horzTagSpace / _printPageSettings.NameTagWidth;
      fRows = vertTagSpace / _printPageSettings.NameTagHeight;
      nCols = Convert.ToInt32(fCols);
      nRows = Convert.ToInt32(fRows);
      // round down
      while(nCols > fCols)
        nCols--;
      while(nRows > fRows)
        nRows--;

      if (nRows < 1)
        nRows = 1;
      if (nCols < 1)
        nCols = 1;

      tagMatrix.Width = nCols;
      tagMatrix.Height = nRows;
      return tagMatrix;
    }

  }
}
