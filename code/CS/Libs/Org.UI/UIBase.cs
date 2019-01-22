using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Org.DB;
using Org.GS;
using Org.GS.Configuration;

namespace Org.UI
{
  public partial class UIBase : UserControl
  {
    public event Action<UIEventArgs> ControlEvent;

    private static Pen _bluePen = new Pen(new SolidBrush(Color.FromArgb(28, 117, 188)), 2.0f);
    public static Pen BluePen {
      get {
        return _bluePen;
      }
    }

    private static Brush _blueBrush = new SolidBrush(Color.FromArgb(28, 117, 188));
    public static Brush BlueBrush {
      get {
        return _blueBrush;
      }
    }

    private static Brush _darkBlueBrush = new SolidBrush(Color.FromArgb(20, 86, 139));
    public static Brush DarkBlueBrush {
      get {
        return _darkBlueBrush;
      }
    }

    public List<NavSection> NavSections = new List<NavSection>();

    private static ControlSpec _controlSpec = null;
    public static ControlSpec ControlSpec
    {
      get {
        return _controlSpec;
      }
      set {
        _controlSpec = value;
      }
    }

    private UIControl _theControl = null;
    private int _prevIndex = -1;
    private bool _gridSelectionLocked = false;
    private bool _gridSelectionChanged = false;
    private bool _gridRowChangeSuppressed = false;
    private bool _suppressTextChangedEvents = false;
    private bool _suppressGridChangedEvents = false;
    private Dictionary<string, Control> _controlMap = null;
    private Label GridValues;
    private Label FormValues;
    private Label PanelModeLabel;
    private Label IsDirtyLabel;
    private Label IsCompleteLabel;
    private Label DebugInfo;
    private Button AddNewButton;
    private Button OkButton;
    private Button CancelButton;

    private bool _inDebug = false;

    private PanelManager _panelManager;
    protected PanelManager PanelManager {
      get {
        return _panelManager;
      }
    }

    private static bool _controlSpecLoaded = false;
    private static bool _xMapTypesLoaded = false;

    protected static Assembly WinFormsAssembly;
    protected static Assembly UIAssembly;


    public UIBase()
    {
      _panelManager = new PanelManager();
    }

    public static void LoadXmlMapper()
    {
      XmlMapper.AddAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }

    public void Initialize(string name, Point location, Size size, bool reloadControlSpec)
    {
      if (reloadControlSpec)
        _controlSpecLoaded = false;

      InitializeComponent();

      this.Name = name;
      this.Location = location;
      this.Size = size;

      InitializeControl();

      try
      {
        if (!UIBase.ControlSpec.UIControls.ContainsKey(name))
          throw new Exception("UIControl '" + name + "' is not defined in the ControlSpec.");

        CreateControl(UIBase.ControlSpec.UIControls[name]);
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred during the creation of the UIControl '" + name + "'.", ex);
      }
    }

    private void InitializeControl()
    {
      if (!_xMapTypesLoaded)
      {
        XmlMapper.AddAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        _xMapTypesLoaded = true;

        WinFormsAssembly = Assembly.GetAssembly(typeof(System.Windows.Forms.Label));
        UIAssembly = Assembly.GetAssembly(typeof(Org.UI.UIBase));
      }

      if (!_controlSpecLoaded)
      {
        LoadControlSpec();
        _controlSpecLoaded = true;
      }
    }

    private void LoadControlSpec()
    {
      string controlSpecPath = String.Empty;

      try
      {
        controlSpecPath = g.ResourcePath + @"\Controls.xml";
        if (!File.Exists(controlSpecPath))
          throw new Exception("Controls.xml does not exist at '" + controlSpecPath + "' - cannot load control definitions.");

        string controlSpec = File.ReadAllText(controlSpecPath);
        var controlSpecXml = XElement.Parse(controlSpec);
        var f = new ObjectFactory2();
        f.LogToMemory = true;
        _controlSpec = f.Deserialize(controlSpecXml) as ControlSpec;
        _controlSpec.LoadControls();
        g.MemoryLog.Clear();
      }
      catch(Exception ex)
      {
        throw new Exception("An exception occurred while attempting to load the ControlSpec object from the Controls.xml file '" +
                            controlSpecPath + "'.", ex);
      }
    }

    protected void CreateControl(UIControl c)
    {
      _theControl = c;

      PopulateProperties(this, c);
      c.ObjectType = this.GetType();
      c.ObjectReference = this;
      c.ParentObjectType = null;
      c.ParentObjectReference = null;
      c.TopObjectType = null;
      c.TopObjectReference = null;

      int childControlCount = c.Count;
      int controlNumber = 0;

      foreach(var childControl in c)
      {
        if (!childControl.Type.StartsWith("Org."))
          childControl.Type = "System.Windows.Forms." + childControl.Type;
        bool isWinForms = childControl.Type.StartsWith("System.Windows.Forms");
        Type controlType = isWinForms ? UIBase.WinFormsAssembly.GetType(childControl.Type) : UIBase.UIAssembly.GetType(childControl.Type);

        object o = Activator.CreateInstance(controlType);
        childControl.ObjectType = controlType;
        childControl.ObjectReference = o;
        childControl.ParentObjectType = this.GetType();
        childControl.ParentObjectReference = this;
        childControl.TopObjectType = this.GetType();
        childControl.TopObjectReference = this;

        this.Controls.Add((Control) o);
        ((Control)o).Dock = DockStyle.Top;
        ((Control)o).BringToFront();
        controlNumber++;
        PopulateProperties(o, childControl);


        if (c.Type.Contains("Navigator"))
        {
          ((Control)o).Dock = DockStyle.Top;
          ((Control)o).BringToFront();
          this.NavSections.Add((NavSection)o);
          ((NavSection)o).Navigator = (UIPanel)this;
        }
        else
        {
          if (controlNumber < childControlCount)
          {
            Panel spacerPanel = new Panel();
            spacerPanel.Size = new Size(((Control)o).Width, 20);
            this.Controls.Add(spacerPanel);
            spacerPanel.Dock = DockStyle.Top;
            spacerPanel.BringToFront();
          }
        }

        foreach(var cc in childControl)
        {
          PopulateChildControls(cc, o);
        }
      }

      _inDebug = c.Debug;

      if (_inDebug)
      {
        this.Height += 200;
        int bottomOfControls = 0;
        int leftOfControls = 0;
        int maxWidthOfControls = 0;
        foreach(var childControl in c)
        {
          if (childControl.ObjectReference != null)
          {
            Control childControlObject = childControl.ObjectReference as Control;
            int bottom = childControlObject.Top + childControlObject.Height;
            if (bottom > bottomOfControls)
              bottomOfControls = bottom;
            if (childControlObject.Left > leftOfControls)
              leftOfControls = childControlObject.Left;
            if (childControlObject.Width > maxWidthOfControls)
              maxWidthOfControls = childControlObject.Width;
          }
        }

        int topOfDebugPanel = bottomOfControls + 20;

        Panel pnlDebug = new Panel();
        pnlDebug.BorderStyle = BorderStyle.FixedSingle;
        ((Control) c.ObjectReference).Controls.Add(pnlDebug);
        pnlDebug.Top = topOfDebugPanel;
        pnlDebug.Left = leftOfControls;
        pnlDebug.Height = 180;
        pnlDebug.Width = maxWidthOfControls;

        Label lblRowHeader = new Label();
        lblRowHeader.BorderStyle = BorderStyle.None;
        lblRowHeader.Text = "Selected Row";
        pnlDebug.Controls.Add(lblRowHeader);
        lblRowHeader.Left = 4;
        lblRowHeader.Top = 4;

        Label lblControlHeader = new Label();
        lblControlHeader.BorderStyle = BorderStyle.None;
        lblControlHeader.Text = "Control Values";
        pnlDebug.Controls.Add(lblControlHeader);
        lblControlHeader.Left = 280;
        lblControlHeader.Top = 4;

        Label lblDebugInfo = new Label();
        lblDebugInfo.BorderStyle = BorderStyle.None;
        lblDebugInfo.Text = "Debug Info";
        pnlDebug.Controls.Add(lblDebugInfo);
        lblDebugInfo.AutoSize = true;
        lblDebugInfo.Left = 400;
        lblDebugInfo.Top = 4;
        DebugInfo = lblDebugInfo;

        Label lblPanelMode = new Label();
        lblPanelMode.BorderStyle = BorderStyle.None;
        lblPanelMode.Text = "Mode:NotSet";
        this.Controls.Add(lblPanelMode);
        lblPanelMode.AutoSize = true;
        lblPanelMode.Left = 100;
        lblPanelMode.Top = 0;
        PanelModeLabel = lblPanelMode;

        Label lblIsDirty = new Label();
        lblIsDirty.BorderStyle = BorderStyle.None;
        lblIsDirty.Text = "Dirty:False";
        this.Controls.Add(lblIsDirty);
        lblIsDirty.AutoSize = true;
        lblIsDirty.Left = 200;
        lblIsDirty.Top = 0;
        IsDirtyLabel = lblIsDirty;

        Label lblIsComplete = new Label();
        lblIsComplete.BorderStyle = BorderStyle.None;
        lblIsComplete.Text = "Dirty:False";
        this.Controls.Add(lblIsComplete);
        lblIsComplete.AutoSize = true;
        lblIsComplete.Left = 300;
        lblIsComplete.Top = 0;
        IsCompleteLabel = lblIsComplete;

        Label lblGridValues = new Label();
        lblGridValues.BorderStyle = BorderStyle.None;
        lblGridValues.AutoSize = false;
        lblGridValues.Width = 270;
        lblGridValues.Height = 150;
        lblGridValues.Text = "Grid Values";
        pnlDebug.Controls.Add(lblGridValues);
        lblGridValues.Left = 4;
        lblGridValues.Top = 25;
        GridValues = lblGridValues;

        Label lblFormValues = new Label();
        lblFormValues.BorderStyle = BorderStyle.None;
        lblFormValues.AutoSize = false;
        lblFormValues.Width = 270;
        lblFormValues.Height = 150;
        lblFormValues.Text = "Form Values";
        pnlDebug.Controls.Add(lblFormValues);
        lblFormValues.Left = 280;
        lblFormValues.Top = 25;
        FormValues = lblFormValues;
      }
    }

    private void PopulateChildControls(UIControl c, object control)
    {
      if (!c.Type.StartsWith("Org."))
        c.Type = "System.Windows.Forms." + c.Type;

      bool isWinForms = c.Type.StartsWith("System.Windows.Forms");
      Type controlType = isWinForms ? UIBase.WinFormsAssembly.GetType(c.Type) : UIBase.UIAssembly.GetType(c.Type);

      object o = Activator.CreateInstance(controlType);
      c.ObjectType = controlType;
      c.ObjectReference = o;
      c.TopObjectType = this.GetType();
      c.TopObjectReference = this;

      if (isWinForms)
      {
        ((Control)control).Controls.Add((Control) o);
        c.ParentObjectType = ((Control)o).Parent.GetType();
        c.ParentObjectReference = ((Control)o).Parent;
      }
      else
      {
        ((Control)control).Controls.Add((Control) o);
        c.ParentObjectType = ((Control)o).Parent.GetType();
        c.ParentObjectReference = ((Control)o).Parent;

        if (c.ObjectType.Name == "NavButton")
        {
          ((NavSection)c.ParentObjectReference).NavButtons.Add((NavButton)o);
          ((NavButton)o).NavSection = (NavSection)c.ParentObjectReference;
        }
      }

      PopulateProperties(o, c);

      foreach(var childControl in c)
      {
        PopulateChildControls(childControl, o);
      }
    }

    private void PopulateProperties(object control, UIControl controlSpec)
    {
      var specPiList = GetXMapProperties(controlSpec.GetType());
      string controlType = control.GetType().Name;

      foreach(var specPi in specPiList)
      {
        if (controlType == "UIPanel")
        {
          if (specPi.Name.In("Name,Size,Location"))
            continue;
        }

        if (controlType == "NavSection")
        {
          var navSectionControl = (Control)control;
          navSectionControl.BackColor = Color.White;
          navSectionControl.Dock = DockStyle.Top;
          navSectionControl.BringToFront();
          navSectionControl.Padding = new Padding(3, 37, 3, 3);
        }

        if (controlType == "NavButton")
        {
          var navButton = (NavButton)control;
          var parentControl = navButton.Parent;
          string parentType = parentControl.GetType().Name;
          if (parentType == "NavSection")
          {
            navButton.Dock = DockStyle.Top;
            navButton.BringToFront();
            navButton.BorderStyle = BorderStyle.None;
            navButton.BackColor = Color.White;
          }
        }

        if (specPi.Name == "Type")
          continue;

        XMap specPiXMap = specPi.GetCustomAttribute<XMap>();
        string specPropertyValue = specPi.GetValue(controlSpec).ToString();
        if (specPropertyValue.IsBlank() && specPiXMap.DefaultValue.IsNotBlank())
          specPropertyValue = specPiXMap.DefaultValue;

        if (specPropertyValue.IsBlank())
          continue;

        if (specPi.Name == "EventSpec")
        {
          string[] eventSpec = specPropertyValue.ToTokenArray(Constants.PipeDelimiter);

          if (eventSpec.Length % 2 != 0)
            throw new Exception("Invalid EventSpec '" + eventSpec + "' for control '" + controlSpec.Name + "'.");

          int eventCount = eventSpec.Length / 2;

          for(int i = 0; i < eventCount; i++)
          {
            WireUpEvent(controlSpec.Name, control, eventSpec[i * 2], eventSpec[i * 2 + 1]);
          }
          continue;
        }

        string specPropertyName = specPi.Name;
        if (specPiXMap.ClassName.IsNotBlank())
          specPropertyName = specPiXMap.ClassName;

        PropertyInfo controlProperty = control.GetType().GetProperty(specPropertyName);
        if (controlProperty != null)
        {
          SetPropertyValue(control, controlProperty, specPropertyValue);
        }
      }
    }

    private void WireUpEvent(string controlName, object control, string eventName, string methodName)
    {
      // get the event from the control
      EventInfo ei = control.GetType().GetEvent(eventName);
      if (ei == null)
        throw new Exception("Cannot locate event '" + eventName + "' for control '" + controlName + "'.");

      string eventTargetAll = methodName;
      string targetControlName = String.Empty;
      object targetControl = null;
      string targetMethod = String.Empty;
      if (eventTargetAll.Contains("."))
      {
        string[] targetTokens = eventTargetAll.ToTokenArray(Constants.DotDelimiter);
        if (targetTokens.Length != 2)
          throw new Exception("Event target specification is invalid, must be in form as 'Control.Method' but found '" + eventTargetAll + "'.");
        targetControlName = targetTokens[0];
        targetMethod = targetTokens[1];
        targetControl = GetEventTargetControl(control, targetControlName);
      }
      else
      {
        targetMethod = eventTargetAll;
        targetControl = this;
      }

      if (targetControl == null)
        throw new Exception("Event target control named '" + targetControlName + "' not found.");

      MethodInfo handler = targetControl.GetType().GetMethod(targetMethod, BindingFlags.Public | BindingFlags.Instance);

      if (handler == null)
        throw new Exception("Cannot locate event handler '" + methodName + "' for event '" + eventName + "' for control '" + controlName + "'.");

      Type delegateType = ei.EventHandlerType;
      Delegate eventDelegate = Delegate.CreateDelegate(delegateType, targetControl, handler);

      MethodInfo handlerMi = ei.GetAddMethod();
      Object[] handlerArgs = { eventDelegate };
      handlerMi.Invoke(control, handlerArgs);
    }

    private object GetEventTargetControl(object control, string objectName)
    {
      string typeName = control.GetType().FullName;
      bool isWinForms = typeName.StartsWith("System.Windows.Forms.");

      if (isWinForms)
      {
        Control wControl = ((Control)control);
        string controlName = wControl.Name;
        if (controlName == objectName)
          return control;

        Control parent = wControl.Parent;

        if (parent == null)
          return null;

        if (parent.Name == objectName)
          return parent;

        foreach(Control siblingControl in parent.Controls)
        {
          if (siblingControl.Name == objectName)
            return siblingControl;
        }

        return GetEventTargetControl(parent, objectName);
      }
      else
      {
        throw new Exception("Non-WinForms controls are not yet implemented.");
      }
    }

    private List<PropertyInfo> GetXMapProperties(Type type)
    {
      List<PropertyInfo> xmapPiList = new List<PropertyInfo>();
      List<PropertyInfo> piList = type.GetProperties().ToList();

      foreach (PropertyInfo pi in piList)
      {
        XMap propXMap = (XMap)pi.GetCustomAttributes(typeof(XMap), true).ToList().FirstOrDefault();
        if (propXMap != null)
          xmapPiList.Add(pi);
      }
      return xmapPiList;
    }

    private void SetPropertyValue(object o, PropertyInfo pi, string propertyValue)
    {
      // For enumeration types
      if (pi.PropertyType.IsEnum)
      {
        if (!Enum.IsDefined(pi.PropertyType, propertyValue))
          throw new Exception("Enum type '" + pi.PropertyType.Name + "' cannot be set to the value '" + propertyValue + "'.");

        object enumValue = Enum.ToObject(pi.PropertyType, Enum.Parse(pi.PropertyType, propertyValue));
        pi.SetValue(o, enumValue, null);
        return;
      }

      // For other types
      switch (pi.PropertyType.Name)
      {
        case "String":
          pi.SetValue(o, propertyValue, null);
          break;

        case "Boolean":
          pi.SetValue(o, g.GetBooleanValue(propertyValue), null);
          break;

        case "Int32":
          pi.SetValue(o, g.GetInt32Value(propertyValue), null);
          break;

        case "Int64":
          pi.SetValue(o, g.GetInt64Value(propertyValue), null);
          break;

        case "DateTime":
          pi.SetValue(o, g.GetDateTimeValue(propertyValue), null);
          break;

        case "Single":
          pi.SetValue(o, g.GetSingleValue(propertyValue), null);
          break;

        case "Point":
          pi.SetValue(o, g.GetPointValue(propertyValue), null);
          break;

        case "PointF":
          pi.SetValue(o, g.GetPointFValue(propertyValue), null);
          break;

        case "Size":
          pi.SetValue(o, g.GetSizeValue(propertyValue), null);
          break;

        case "SizeF":
          pi.SetValue(o, g.GetSizeFValue(propertyValue), null);
          break;

        case "Color":
          pi.SetValue(o, g.GetColorValue(propertyValue), null);
          break;

        case "Object":
          if (pi.Name == "Tag")
            pi.SetValue(o, propertyValue);
          break;

        default:
          throw new Exception("Type '" + pi.PropertyType.Name + "' not yet supported in 'UIPanel.SetPropertyValue()' method.");
      }
    }

    public void SetFocus()
    {
      if (_theControl == null)
        return;

      UIControl focusControl = GetInitialFocusControl(_theControl);

      if (focusControl == null)
        return;

      if (focusControl.ObjectReference != null)
      {
        object o = focusControl.ObjectReference;
        if (o.GetType().FullName.Contains("System.Windows.Forms."))
        {
          Control c = (Control) o;
          c.Focus();
          return;
        }
      }
    }

    public UIControl GetInitialFocusControl(UIControl c)
    {
      if (c.InitialFocus == "True")
        return c;

      foreach(var cc in c)
      {
        var focusControl = GetInitialFocusControl(cc);
        if (focusControl != null)
          return focusControl;
      }

      return null;
    }

    public string GetControlMap(string name)
    {
      if (_controlSpec == null)
        return String.Empty;

      return _controlSpec.GetControlMap(name);
    }

    private string Get_AssociatedModelName()
    {
      if (this.Name.IsBlank())
        return String.Empty;

      var uiControl = _controlSpec.UIControls[this.Name];
      if (uiControl == null)
        return String.Empty;

      return uiControl.Model;
    }

    private string Get_ModelSort()
    {
      if (this.Name.IsBlank())
        return String.Empty;

      var uiControl = _controlSpec.UIControls[this.Name];
      if (uiControl == null)
        return String.Empty;

      return uiControl.ModelSort;
    }

    public void ClickEvent(object sender, EventArgs e)
    {
      Control control = (Control)sender;
      TaggedControlSet tcs = null;
      string controlName = control.Name;
      string controlText = control.Text;
      string tag = control.Tag.ObjectToTrimmedString();
      string controlType = sender.GetType().Name;

      switch(controlType)
      {
        case "NavSection":
          var navSection = (NavSection)sender;
          this.ControlEvent(new UIEventArgs("NavButton", navSection.Name, controlText, navSection.Tag.ObjectToTrimmedString(), "Click"));
          break;

        case "NavButton":
          var navButton = (NavButton)sender;
          navButton.IsSelected = true;
          this.ControlEvent(new UIEventArgs("NavButton", navButton.Name, controlText, navButton.Tag.ObjectToTrimmedString(), "Click"));
          break;
      }

      if (_inDebug)
      {
        PanelModeLabel.Text = "Mode:" + _panelManager.PanelMode.ToString();
        IsDirtyLabel.Text = "Dirty:" + _panelManager.IsDirty.ToString();
        IsCompleteLabel.Text = "Complete:" + _panelManager.IsComplete.ToString();
      }
    }

    public void MouseEnterEvent(object sender, EventArgs e)
    {
      if (this.ControlEvent == null)
        return;

      string tag = String.Empty;
      string controlName = String.Empty;
      string controlType = sender.GetType().ToString();
      string controlText = String.Empty;

      if (controlType.StartsWith("System.Windows.Forms."))
      {
        tag = ((Control)sender).Tag.ObjectToTrimmedString();
        controlName = ((Control)sender).Name;
        controlText = ((Control)sender).Text;
      }

      if (tag.IsNotBlank())
        this.ControlEvent(new UIEventArgs(controlType, controlName, controlText,  tag, "MouseEnter"));
    }


    private void UIBase_Paint(object sender, PaintEventArgs e)
    {
      if (this.Name == "NavMain")
        return;

      e.Graphics.DrawRectangle(BluePen, new Rectangle(10, 10, this.Width - 90, this.Height - 30));
    }
  }
}
