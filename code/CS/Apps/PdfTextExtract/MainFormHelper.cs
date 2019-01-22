using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Org.TW;
using Org.TW.Forms;
using Org.TW.ToolPanels;
using Org.GS.UI;
using Org.GS;

namespace Org.PdfTextExtract
{
  public class MainFormHelper
  {
    private static int[] scaleValues = new int[] { 10, 25, 50, 75, 90, 100, 125, 150, 200, 250, 300, 350, 400, 450, 500, 600, 700 };

    //[DebuggerStepThrough]
    public static void InitializeUIState(Form f, ToolWindowManager twm)
    {
      bool isConfigChanged = false;

      if (g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName].UIState == null)
        g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName].UIState = new UIState();

      UIState uiState = g.AppConfig.ProgramConfigSet[g.AppConfig.ConfigName].UIState;

      string mainFormTag = f.Tag.ToString();
      if (!uiState.UIWindowSet.ContainsKey(mainFormTag))
      {
        UIWindow mainWindow = new UIWindow();
        mainWindow.Name = mainFormTag;
        mainWindow.IsMainForm = true;
        mainWindow.StartPosition = StartPosition.CenterScreen;
        mainWindow.UIWindowState = UIWindowState.Normal;
        mainWindow.WindowType = WindowType.Normal;
        mainWindow.WindowLocation.IsDocked = false;
        mainWindow.WindowLocation.IsVisible = true;
        mainWindow.WindowLocation.SizeMode = SizeMode.PercentOfScreen;
        mainWindow.WindowLocation.Location = new Point();
        mainWindow.WindowLocation.Size = new SizeF(0.995F, 0.99F);
        uiState.UIWindowSet.Add(mainWindow.Name, mainWindow);
      }

      Point toolWindowLocation = new Point(650, 250);

      foreach (var kvp in twm.ToolWindowComponentsSet)
      {
        string name = kvp.Key;
        var twc = kvp.Value;

        if (!uiState.UIWindowSet.ContainsKey(name))
        {
          isConfigChanged = true;
          UIWindow toolWindow = new UIWindow();
          toolWindow.Name = name;
          toolWindow.IsMainForm = false;
          toolWindow.StartPosition = StartPosition.Manual;
          toolWindow.UIWindowState = UIWindowState.Normal;
          toolWindow.WindowType = WindowType.ToolWindow;
          toolWindow.WindowLocation.IsDocked = false;
          toolWindow.WindowLocation.IsVisible = true;
          toolWindow.WindowLocation.SizeMode = SizeMode.Literal;
          toolWindow.WindowLocation.Location = toolWindowLocation;
          toolWindowLocation.Offset(25, 25);
          uiState.UIWindowSet.Add(name, toolWindow);
        }      
      }

      if (isConfigChanged)
        g.AppConfig.Save();
    }


    //[DebuggerStepThrough]
    public static void ManageInitialSize(Form f, UIWindow uiWindow)
    {
      Size formSize = Screen.PrimaryScreen.Bounds.Size;
      Point formLocation = new Point(0, 0);

      switch (uiWindow.WindowLocation.SizeMode)
      {
        case SizeMode.Literal:

          break;

        case SizeMode.PercentOfScreen:
          formSize.Width = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width * uiWindow.WindowLocation.Size.Width);
          formSize.Height = Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height * uiWindow.WindowLocation.Size.Height);
          formLocation.X = Convert.ToInt32((Screen.PrimaryScreen.Bounds.Width - formSize.Width) / 2);
          formLocation.Y = Convert.ToInt32((Screen.PrimaryScreen.Bounds.Height - formSize.Height) / 2);
          break;
      }

      f.Size = formSize;
      f.Location = formLocation;
    }

    //[DebuggerStepThrough]
    public static int GetNewScale(int oldScale, string action)
    {
      int newScale = oldScale;

      int index = -1;
      for (int i = 0; i < scaleValues.Length; i++)
      {
        if (scaleValues[i] == oldScale)
        {
          index = i;
          break;
        }
      }

      int increment = 1;
      if (action == "ZoomOut")
        increment = -1;

      if (index > -1)
      {
        index = index + increment;
        if (index > scaleValues.Length - 1)
          index = scaleValues.Length - 1;
        if (index < 0)
          index = 0;

        return scaleValues[index];
      }

      return newScale;
    }
  }
}
