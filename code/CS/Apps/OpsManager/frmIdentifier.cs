using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.GS;
using Org.GS.Logging;

namespace Org.OpsManager
{
  public partial class frmIdentifier : Form
  {
    OpsData _opsData;
    bool _isNewIdentifier;
    Identifier _identifier;

    public frmIdentifier(OpsData opsData, bool isNewIdentifier, Identifier identifier)
    {
      InitializeComponent();
      _opsData = opsData;
      _isNewIdentifier = isNewIdentifier;
      _identifier = identifier;

      Initialize();
    }

    private void Action(object sender, EventArgs e)
    {
      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "Save":
          Save();
          break;

        case "DirtyCheck":
          DirtyCheck();
          break;

        case "Cancel":
          this.Close();
          break;
      }
    }

    private void Save()
    {
      try
      {
        int id = txtIdentifierId.Text.ToInt32();
        string description = txtDescription.Text.Trim();

        if (_isNewIdentifier)
        {
          bool idInUse;
          using (var logRepo = new LoggingRepository(_opsData.LoggingDbSpec))
          {
            switch(_identifier.IdentifierType)
            {
              case IdentifierType.Module:
                idInUse = logRepo.InsertAppLogModule(id, description);
                break;

              case IdentifierType.Event:
                idInUse = logRepo.InsertAppLogEvent(id, description);
                break;

              case IdentifierType.Entity:
                idInUse = logRepo.InsertAppLogEntity(id, description);
                break;

              default:
                return;
            }
          }

          if (idInUse)
          {
            MessageBox.Show("ID " + txtIdentifierId.Text.ToInt32() + " is already in use by another " + _identifier.IdentifierType.ToString() + ".",
                            "OpsManager - Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
        }
        else
        {
          using (var logRepo = new LoggingRepository(_opsData.LoggingDbSpec))
          {
            switch(_identifier.IdentifierType)
            {
              case IdentifierType.Module:
                logRepo.UpdateAppLogModule(id, description);
                break;

              case IdentifierType.Event:
                logRepo.UpdateAppLogEvent(id, description);
                break;

              case IdentifierType.Entity:
                logRepo.UpdateAppLogEntity(id, description);
                break;
            }
          }
        }

        this.Close();
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to save Identifier changes to the database.", ex);
      }
    }

    private void DirtyCheck()
    {
      if (txtDescription.Text.Trim() != _identifier.Description ||
          txtIdentifierId.Text.ToInt32() != _identifier.Id)
        btnSave.Enabled = true;

      else btnSave.Enabled = false;
    }

    private void Initialize()
    {
      try
      {
        this.Text = _identifier.IdentifierType.ToString();

        if (!_isNewIdentifier)
        {
          txtIdentifierId.Enabled = false;
          txtIdentifierId.Text = _identifier.Id.ToString();
          txtDescription.Text = _identifier.Description;
        }
      }
      catch (Exception ex)
      {
        throw new Exception("An exception occurred when trying to Initialize frmIdentifier.", ex);
      }
    }

    private void IntegerOnly_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        e.Handled = true;

      int cursorPosition = txtIdentifierId.SelectionStart;
      int newNumber = (txtIdentifierId.Text.Substring(0,cursorPosition) + e.KeyChar + txtIdentifierId.Text.Substring(cursorPosition)).ToInt32();

      if ((_identifier.IdentifierType == IdentifierType.Module || _identifier.IdentifierType == IdentifierType.Event) && newNumber > 9999)
        e.Handled = true;
      else if (_identifier.IdentifierType == IdentifierType.Entity && newNumber > 999)
        e.Handled = true;
    }
  }

  public class Identifier
  {
    public int Id {
      get;
      set;
    }
    public string Description {
      get;
      set;
    }
    public IdentifierType IdentifierType {
      get;
      set;
    }

    public Identifier(IdentifierType identifierType)
    {
      this.Id = 0;
      this.Description = String.Empty;
      this.IdentifierType = identifierType;
    }
    public Identifier(IdentifierType identifierType, int id, string description)
    {
      this.Id = id;
      this.Description = description;
      this.IdentifierType = identifierType;
    }
  }

  public enum IdentifierType
  {
    Module,
    Event,
    Entity
  }


}
