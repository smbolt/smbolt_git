using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.QB;
using Org.GS.Configuration;
using Org.GS.Logging;
using Org.GS;
using System.Xml;
using Org.QB.QBXML;
using System.Reflection;

namespace Org.QuickbooksWorkbench
{
  public partial class frmMain : Form
  {
    private QBX _qbx;
    private Logger _logger;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void Action(object c, EventArgs e)
    {
      switch (c.ActionTag())
      {
        case "OpenConnection":
          OpenConnection();
          break;

        case "BeginSession":
          BeginSession();
          break;

        case "SendRequest":
          SendRequest();
          break;

        case "EndSession":
          EndSession();
          break;

        case "CloseConnection":
          CloseConnection();
          break;

        case "CustomerAdd":
          CustomerAdd();
          break;

        case "CustomerQuery":
          CustomerQuery();
          break;

        case "Exit":
          this.Close();
          break;
      }
    }

    private void OpenConnection()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;


        _qbx = new QBX(_logger);
        _qbx.OpenConnection();

        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to open a connection to Quickbooks." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Connecting to Quickbooks.", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void BeginSession()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        _qbx.BeginSession();


        txtOut.Text = "Begin Session";
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to begin a session Quickbooks." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Beginning Quickbooks Session.", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void SendRequest()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        _qbx.SendRequest();





        txtOut.Text = "Send Request";
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to send a request to Quickbooks." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Sending Quickbooks Request.", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void EndSession()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        _qbx.EndSession();

        txtOut.Text = "End Session";
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to end a session Quickbooks." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Ending Quickbooks Session.", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void CloseConnection()
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        _qbx.CloseConnection();

        txtOut.Text = "Close Connecction";
        this.Cursor = Cursors.Default;
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to close the connection to Quickbooks." + g.crlf2 + ex.ToReport(),
                        g.AppInfo.AppName + " - Error Closing Quickbooks Connection.", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void CustomerAdd()
    {
      try
      {
        var req = new QBXMLRq();

        using (var factory = new QBXmlFactory())
        {
          var customerAddRq = factory.Create(TransactionType.CustomerAddRq) as CustomerAddRq;
          var customerAdd = new CustomerAdd();

          customerAdd.Name = "Stephen Bolt";
          customerAdd.IsActive = true;
          customerAdd.ClassRef = new Ref() {
            ListID = "ClassRef", FullName = "ClassRefName"
          };
          customerAdd.ParentRef = new Ref() {
            ListID = "ParentRef", FullName = "ParentRefName"
          };
          customerAdd.CompanyName = "ADSDI LLC";
          customerAdd.Salutation = "Mr.";
          customerAdd.FirstName = "Stephen";
          customerAdd.MiddleName = "Michael";
          customerAdd.LastName = "Bolt";
          customerAdd.JobTitle = "Systems Architect";
          customerAdd.BillAddress = new Address()
          {
            Addr1 = "10313 S. Trafalgar Dr.",
            Addr2 = "Test Addr2",
            Addr3 = "Test Addr3",
            Addr4 = "Test Addr4",
            Addr5 = "Test Addr5",
            City = "Oklahoma City",
            State = "Oklahoma",
            PostalCode = "73139",
            Country = "USA",
            Note = "Primary residence"
          };
          customerAdd.ShipAddress = new Address()
          {
            Addr1 = "10313 S. Trafalgar Dr.",
            Addr2 = "Test Addr2",
            Addr3 = "Test Addr3",
            Addr4 = "Test Addr4",
            Addr5 = "Test Addr5",
            City = "Oklahoma City",
            State = "Oklahoma",
            PostalCode = "73139",
            Country = "USA",
            Note = "Primary residence"
          };
          customerAdd.ShipToAddress = new Address()
          {
            Addr1 = "10313 S. Trafalgar Dr.",
            Addr2 = "Test Addr2",
            Addr3 = "Test Addr3",
            Addr4 = "Test Addr4",
            Addr5 = "Test Addr5",
            City = "Oklahoma City",
            State = "Oklahoma",
            PostalCode = "73139",
            Country = "USA",
            Note = "Primary residence"
          };
          customerAdd.Phone = "405-650-2029";
          customerAdd.AltPhone = "405-650-8888";
          customerAdd.Fax = "405-691-4444";
          customerAdd.Email = "stephen.m.bolt@gmail.com";
          customerAdd.Cc = "sbolt@gulfportenergy.com";
          customerAdd.Contact = "Steve";
          customerAdd.AltContact = "Robyn";
          customerAdd.AdditionalContactRefList = new AdditionalContactRefList();
          customerAdd.AdditionalContactRefList.Add(new AdditionalContactRef() {
            ContactName = "AddlContactName1", ContactValue = "Value1"
          });
          customerAdd.AdditionalContactRefList.Add(new AdditionalContactRef() {
            ContactName = "AddlContactName2", ContactValue = "Value2"
          });
          customerAdd.AdditionalContactRefList.Add(new AdditionalContactRef() {
            ContactName = "AddlContactName3", ContactValue = "Value3"
          });
          customerAdd.AdditionalContactRefList.Add(new AdditionalContactRef() {
            ContactName = "AddlContactName4", ContactValue = "Value4"
          });
          customerAdd.AdditionalContactRefList.Add(new AdditionalContactRef() {
            ContactName = "AddlContactName5", ContactValue = "Value5"
          });

          // add some contacts in the contacts list

          customerAdd.CustomerTypeRef = new Ref() {
            ListID = "CustomerTypeRef", FullName = "CustomerTypeRefName"
          };
          customerAdd.TermsRef = new Ref() {
            ListID = "TermsRef", FullName = "TermsRefName"
          };
          customerAdd.SalesRepRef = new Ref() {
            ListID = "SalesRepRef", FullName = "SalesRepRefName"
          };
          customerAdd.OpenBalance = 555.55m;
          customerAdd.OpenBalanceDate = DateTime.Now;
          customerAdd.SalesTaxCodeRef = new Ref() {
            ListID = "SalesTaxCodeRef", FullName = "SalesTaxCodeRefName"
          };
          customerAdd.ItemSalesTaxRef = new Ref() {
            ListID = "ItemSalesTaxeRef", FullName = "ItemSalesTaxRefName"
          };
          customerAdd.ResaleNumber = "ResaleNumber";
          customerAdd.AccountNumber = "Account12345";
          customerAdd.CreditLimit = 2000.00m;
          customerAdd.PreferredPaymentMethodRef = new Ref() {
            ListID = "PreferredPaymentMethodRef", FullName = "PreferredPaymentMethodefName"
          };
          customerAdd.CreditCardInfo = new CreditCardInfo()
          {
            CreditCardNumber = "1234-5678-8765-4321",
            ExpirationMonth = 7,
            ExpirationYear = 2021,
            NameOnCard = "Stephen M Bolt",
            CreditCardAddress = "10313 S Trafalgar Dr",
            CreditCardPostalCode = "73139"
          };
          customerAdd.JobStatus = JobStatus.Awarded;
          customerAdd.JobStartDate = new DateTime(2015, 1, 31);
          customerAdd.JobProjectedEndDate = new DateTime(2030, 12, 31);
          customerAdd.JobEndDate = new DateTime(2015, 1, 30);
          customerAdd.JobDesc = "Systems Architect";
          customerAdd.JobTypeRef = new Ref() {
            ListID = "JobTypeRef", FullName = "JobTypeRefName"
          };
          customerAdd.Notes = "Notes about the customer";

          // add some additional notes (list)

          customerAdd.PreferredDeliveryMethod = DeliveryMethod.Email;
          customerAdd.PriceLevelRef = new Ref() {
            ListID = "PriceLevelRef", FullName = "PriceLevelRefName"
          };
          customerAdd.ExternalGUID = "ABCD-123456-654321-DCBA";
          customerAdd.CurrencyRef = new Ref() {
            ListID = "CurrencyRef", FullName = "CurrencyRefName"
          };

          customerAddRq.CustomerAdd = customerAdd;

          req.QBXMLMsgsRq.Add(customerAddRq);
        }

        var f = new ObjectFactory2();
        var xml = f.Serialize(req);

        txtOut.Text = xml.ToString();

      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to create a CustomerAddRq message." + ex.ToReport(),
                        "Quickbooks Workbench - Error Creating CustomerAddRq",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void CustomerQuery()
    {
      try
      {
        var req = new QBXMLRq();

        using (var factory = new QBXmlFactory())
        {
          var customerQueryRq = factory.Create(TransactionType.CustomerQueryRq) as CustomerQueryRq;
          customerQueryRq.NameFilter = new NameFilter() {
            MatchCriterion = MatchCriterion.Contains, Name = "Test"
          };


          req.QBXMLMsgsRq.Add(customerQueryRq);
        }

        var f = new ObjectFactory2();
        var xml = f.Serialize(req);

        txtOut.Text = xml.ToString();

      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MessageBox.Show("An exception occurred while attempting to create a CustomerQueryRq message." + ex.ToReport(),
                        "Quickbooks Workbench - Error Creating CustomerQueryRq",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    private void InitializeForm()
    {
      try
      {
        new a();
      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during the initialization of the application object 'a'." + ex.ToReport(),
                        "Quickbooks Workbench - Error Initializing of Application Object 'a'",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      try
      {
        _logger = new Logger();

        XmlMapper.AddAssembly(Assembly.GetAssembly(typeof(Org.QB.QBXML.QbXmlBase)));


      }
      catch (Exception ex)
      {
        MessageBox.Show("An exception occurred during application initialization." + ex.ToReport(),
                        "Quickbooks Workbench - Error During Application Initialization",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }
  }
}
