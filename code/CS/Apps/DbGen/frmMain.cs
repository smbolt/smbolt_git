using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using Org.DB;
using Org.GS;
using Org.GS.Configuration;

namespace Org.DbGen
{
  public partial class frmMain : Form
  {
    private a a;
    private ConfigDbSpec configDbSpec;
    private ConfigDbSpec sql1DbSpec;
    private ConfigDbSpec sql2DbSpec;
    private string _schema1FullPath = String.Empty;
    private string _schema2FullPath = String.Empty;
    private string _compareResultsFullPath = String.Empty;
    private string _beyondCompareExePath = String.Empty;
    private string _beyondCompareScriptPath = String.Empty;
    private bool mainFormShown = false;

    private DbContext db;

    public frmMain()
    {
      InitializeComponent();
      InitializeForm();
    }

    private void InitializeForm()
    {
      a = new a();

      string dbSpecName = g.GetCI("DbSpecName");
      configDbSpec = g.GetDbSpec(dbSpecName); 
      
      string classPath = g.CI("RepositoryMajorSection") + @"\DB_CLASSES.cs";

      txtLoopCount.Text = "10";
      txtSeriesTimes.Text = "10";

      db = new DbContext(configDbSpec.ConnectionString);

      classPath = classPath.Replace("DB_CLASSES", "Db" + DbHelper.DbCodeGenNamespace);
      txtDbClassesPath.Text = classPath;

      List<string> tables = new List<string>();
      foreach (DbTable t in DbHelper.DbTableSet.Values)
      {
        if (!tables.Contains(t.TableName))
          tables.Add(t.TableName);
      }

      cboTables.Items.AddRange(tables.ToArray());

      //InitializeSqlConfigSpecs();

      //_schema1FullPath = g.AppDataPath + @"\Exports\Schema1.txt";
      //_schema2FullPath = g.AppDataPath + @"\Exports\Schema2.txt";
      //_compareResultsFullPath = g.AppDataPath + @"\Exports\CompareResults.html";
      //_beyondCompareExePath = @"C:\Program Files (x86)\Beyond Compare 4\BCompare.exe";
      //_beyondCompareScriptPath = g.AppDataPath + @"\Imports\bcscriptHtml.txt";
    }

    private void Action(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      string action = g.GetActionFromEvent(sender);

      switch (action)
      {
        case "GET_SCHEMA_1":
          GenerateSchema1();
          break;

        case "GET_SCHEMA_2":
          GenerateSchema2();
          break;

        case "COMPARE_SCHEMAS":
          CompareSchemas();
          break;

        case "MAIN_FORM_SHOWN":
          mainFormShown = true;
          ManageSqlAuthControls();
          break;

        case "MANAGE_SQLAUTH_VISIBILITY":
          ManageSqlAuthControls();
          break;

        case "EXIT":
          this.Close();
          break;
      }

      this.Cursor = Cursors.Default;
    }

    private void ManageSqlAuthControls()
    {
      if (!mainFormShown)
        return;

      lblUserID.Visible = ckSqlAuth1.Checked || ckSqlAuth2.Checked;
      lblPassword.Visible = lblUserID.Visible;
      txtUserID1.Visible = ckSqlAuth1.Checked;
      txtUserID2.Visible = ckSqlAuth2.Checked;
      txtPassword1.Visible = ckSqlAuth1.Checked;
      txtPassword2.Visible = ckSqlAuth2.Checked;
    }

    private void InitializeSqlConfigSpecs()
    {
      sql1DbSpec = g.GetDbSpec("Sql1"); 
      sql2DbSpec = g.GetDbSpec("Sql2");

      if (sql1DbSpec != null)
      {
        ckSqlAuth1.Checked = !sql1DbSpec.DbUseWindowsAuth;
        txtInstance1.Text = sql1DbSpec.DbServer;
        txtDb1.Text = sql1DbSpec.DbName;
        txtUserID1.Text = sql1DbSpec.DbUserId;
        txtPassword1.Text = sql1DbSpec.DbPassword;
      }

      if (sql2DbSpec != null)
      {
        ckSqlAuth2.Checked = !sql2DbSpec.DbUseWindowsAuth;
        txtInstance2.Text = sql2DbSpec.DbServer;
        txtDb2.Text = sql2DbSpec.DbName;
        txtUserID2.Text = sql2DbSpec.DbUserId;
        txtPassword2.Text = sql2DbSpec.DbPassword;
      }

    }

    private void GenerateSchema1()
    {
      this.Cursor = Cursors.WaitCursor;

      DbHelper.UnInitialize();
      var db1 = new DbContext(sql1DbSpec.ConnectionString);
      string code = DbHelper.GenerateDbSchema(DbHelper.SqlInstanceName, DbHelper.DatabaseName, DbHelper.DbTableSet);
      txtOut.Text = code;
      tabMain.SelectedTab = tabPageMain;
      File.WriteAllText(_schema1FullPath, code); 

      this.Cursor = Cursors.Default;
    }

    private void GenerateSchema2()
    {
      this.Cursor = Cursors.WaitCursor;

      DbHelper.UnInitialize();
      var db2 = new DbContext(sql2DbSpec.ConnectionString);
      string code = DbHelper.GenerateDbSchema(DbHelper.SqlInstanceName, DbHelper.DatabaseName, DbHelper.DbTableSet);
      txtOut2.Text = code;
      tabMain.SelectedTab = tabPageSecondary;
      File.WriteAllText(_schema2FullPath, code); 

      this.Cursor = Cursors.Default;
    }

    private void CompareSchemas()
    {
      this.Cursor = Cursors.WaitCursor;

      browserCompare.Navigate("about:blank");
      Application.DoEvents();

      ProcessParms processParms = new ProcessParms();
      processParms.ExecutablePath = _beyondCompareExePath;
      processParms.Args = new string[] {  
          "/silent",
          "@" + _beyondCompareScriptPath,
          _schema1FullPath,
          _schema2FullPath, 
          _compareResultsFullPath
      };

      var processHelper = new ProcessHelper();
      TaskResult taskResult = processHelper.RunExternalProcess(processParms);
      UpdateHtmlFonts(_compareResultsFullPath);
      browserCompare.Navigate(_compareResultsFullPath);
      tabMain.SelectedTab = tabPageCompare;

      this.Cursor = Cursors.Default;
    }

    public void UpdateHtmlFonts(string path)
    {
      string htmlText = File.ReadAllText(path);
      htmlText = htmlText.Replace("body { font-family: sans-serif; font-size: 11pt;", "body { font-family: monospace; font-size: 8pt;");
      htmlText = htmlText.Replace("monospace; font-size: 10pt; }", "monospace; font-size: 8pt; }");
      File.WriteAllText(path, htmlText);
    }

    private void btnGenerateCode_Click(object sender, EventArgs e)
    {
      string code = DbHelper.GenerateCode(DbHelper.DbTableSet, ckRefreshProject.Checked, txtDbClassesPath.Text.Trim());
      txtOut.Text = code;
    }

    private void btnGetTableData_Click(object sender, EventArgs e)
    {
      string table = cboTables.Text.Trim();
      if (table.IsNotBlank())
        GetTableData(table);
    }

    private void btnGetBusinessObject_Click(object sender, EventArgs e)
    {
      string table = cboTables.Text.Trim();
      if (table.IsNotBlank())
        GetBusinessObject(table);
    }

    private void GetTableData(string tableName)
    {
      StringBuilder sb = new StringBuilder();
      DbEntityBaseSet entitySet = db.GetEntitySet(tableName);

      foreach (DbEntityBase entity in entitySet)
      {
        sb.Append("Table : " + tableName + g.nl);
        System.Reflection.PropertyInfo[] piSet = entity.GetType().GetProperties();
        foreach (System.Reflection.PropertyInfo pi in piSet)
        {
          if (pi.GetCustomAttributes(typeof(IsDbColumn), true).Count() > 0)
          {
            string propertyName = pi.Name;
            string propertyValue = String.Empty;
            object value = pi.GetValue(entity, null);
            if (value != null)
            {
              string propertyType = value.GetType().Name;
              switch (propertyType)
              {
                case "Int32":
                case "int":
                  int int32Value = Convert.ToInt32(value);
                  propertyValue = int32Value.ToString();
                  break;

                case "Int64":
                case "long":
                  long longValue = (long) value;
                  propertyValue = longValue.ToString();
                  break;

                case "Decimal":
                  decimal decimalValue = Convert.ToDecimal(value);
                  propertyValue = decimalValue.ToString();
                  break;

                case "Single":
                  Single singleValue = Convert.ToSingle(value);
                  propertyValue = singleValue.ToString();
                  break;

                case "DateTime":
                  DateTime dateTimeValue = Convert.ToDateTime(value);
                  propertyValue = dateTimeValue.ToString();
                  break;

                case "String":
                  string stringValue = Convert.ToString(value);
                  propertyValue = stringValue;
                  break;

                case "Byte[]":
                  string byteArrayStringValue = ASCIIEncoding.ASCII.GetString((byte[])value).Replace('\0', '.');
                  if (byteArrayStringValue.Length > 25)
                    propertyValue = byteArrayStringValue.Substring(0, 25);
                  else
                    propertyValue = byteArrayStringValue;
                  break;

                case "XElement":
                  XElement xmlValue = (XElement)value;
                  string xmlString = xmlValue.ToString();
                  if (xmlString.Length > 25)
                    propertyValue = xmlString.Substring(0, 25);
                  else
                    propertyValue = xmlString;
                  break;

                case "Bitmap":
                  Image img = (Image)value;
                  string imageDesc = "Image: +  Height:" + img.Height.ToString() + " Width:" + img.Width.ToString();
                  propertyValue = imageDesc;
                  break;

                case "Boolean":
                  bool booleanValue = Convert.ToBoolean(value);
                  propertyValue = booleanValue.ToString();
                  break;

                case "TimeSpan":
                  TimeSpan timeSpanvalue = (TimeSpan)value;
                  propertyValue = timeSpanvalue.ToString();
                  break;

                default:
                  string defaultStringValue = "DEFAULT *** " + Convert.ToString(value);
                  propertyValue = defaultStringValue;
                  break;

              }
            }
            sb.Append("        " + (propertyName + ":").PadRight(25) + propertyValue + g.nl);
          }
        }
        sb.Append(g.nl);
      }
      txtOut.Text = sb.ToString();
    }

    private void GetBusinessObject(string tableName)
    {
      switch (tableName)
      {
        case "Account":
          GetAccounts();
          break;

        case "TestTable1":
          GetTestTable1();
          break;
      }

    }

    private void GetAccounts()
    {
      //StringBuilder sb = new StringBuilder();

      //DbSet<Account> acct = db.DbSet<Account>()
      //                            //.Where("AccountID > 0")
      //                            ////.And("OrgId = 3")
      //                            ////.And("StatusID = 3")
      //                            //.OrderBy("AccountID")
      //                            .Select();

      //foreach (Account a in acct)
      //{
      //    sb.Append("Table : " + acct.DbTable.TableName + g.nl);
      //    sb.Append("        AccountId:               " + a.AccountId.ToString() + g.nl);
      //    sb.Append("        AccountTypeId:           " + a.AccountTypeId.ToString() + g.nl);
      //    sb.Append("        AccountName:             " + a.AccountName + g.nl);
      //    sb.Append("        OrgId:                   " + a.OrgId.ToString() + g.nl);
      //    sb.Append("        PersonId:                " + a.PersonId.ToString() + g.nl);
      //    sb.Append("        Password:                " + a.Password.ToString() + g.nl);
      //    sb.Append("        StatusId:                " + a.StatusId.ToString() + g.nl);
      //    sb.Append("        SecurityQuestionId:      " + a.SecurityQuestionId.ToString() + g.nl);
      //    sb.Append("        SecurityAnswer:          " + a.SecurityAnswer + g.nl);
      //    sb.Append("        AccountPolicyId:         " + a.AccountPolicyId.ToString() + g.nl2);
      //}

      ////int accountID2 = ((tbl_Account)db.DbSet<tbl_Account>().Where("where clause").SingleOrDefault()).AccountId;
            
      //txtOut.Text = sb.ToString();
    }

    private void GetTestTable1()
    {
      //StringBuilder sb = new StringBuilder();

      //DbSet<TestTable1> tt = db.DbSet<TestTable1>()
      //                            //.Where("AccountID > 0")
      //                            ////.And("OrgId = 3")
      //                            ////.And("StatusID = 3")
      //                            //.OrderBy("AccountID")
      //                            .Select();

      //foreach (TestTable1 t in tt)
      //{
      //    sb.Append("Table : " + tt.DbTable.TableName + g.nl);
      //    //sb.Append("        AccountId:               " + a.AccountId.ToString() + g.nl);
      //    //sb.Append("        AccountTypeId:           " + a.AccountTypeId.ToString() + g.nl);
      //    //sb.Append("        AccountName:             " + a.AccountName + g.nl);
      //    //sb.Append("        OrgId:                   " + a.OrgId.ToString() + g.nl);
      //    //sb.Append("        PersonId:                " + a.PersonId.ToString() + g.nl);
      //    //sb.Append("        Password:                " + a.Password.ToString() + g.nl);
      //    //sb.Append("        StatusId:                " + a.StatusId.ToString() + g.nl);
      //    //sb.Append("        SecurityQuestionId:      " + a.SecurityQuestionId.ToString() + g.nl);
      //    //sb.Append("        SecurityAnswer:          " + a.SecurityAnswer + g.nl);
      //    //sb.Append("        AccountPolicyId:         " + a.AccountPolicyId.ToString() + g.nl2);
      //}

      ////int accountID2 = ((tbl_Account)db.DbSet<tbl_Account>().Where("where clause").SingleOrDefault()).AccountId;
            
      //txtOut.Text = sb.ToString();
    }

    private void btnClearDisplay_Click(object sender, EventArgs e)
    {
      txtOut.Clear();
    }

    private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (db == null)
        return; 

      db.Dispose();
      db = null;
    }

    private void btnLoopBusinessObject_Click(object sender, EventArgs e)
    {
      RunBusinessObject();
    }

    private void RunBusinessObject()
    {
      ////txtOut.Clear();
      ////Application.DoEvents();

      //DateTime dtStart = DateTime.Now;

      //int loopCount = 10;
      //if (txtLoopCount.Text.IsNumeric())
      //    loopCount = Int32.Parse(txtLoopCount.Text.Trim());

      //string tableName = cboTables.Text.Trim();

      //for (int i = 0; i < loopCount; i++)
      //{
      //    DbSet<Account> acct = db.DbSet<Account>().Select();
      //}

      //TimeSpan ts = DateTime.Now - dtStart;

      //txtOut.Text += ts.TotalMilliseconds.ToString("###,#00,000") + " milliseconds for Business Objects - Count:" + loopCount.ToString() + " times" + g.nl;
    }

    private void btnLoopGenericObject_Click(object sender, EventArgs e)
    {
      RunGenericObject();
    }

    private void RunGenericObject()
    {
      //txtOut.Clear();
      //Application.DoEvents();

      DateTime dtStart = DateTime.Now;

      int loopCount = 10;
      if (txtLoopCount.Text.IsNumeric())
        loopCount = Int32.Parse(txtLoopCount.Text.Trim());

      string tableName = cboTables.Text.Trim();

      for (int i = 0; i < loopCount; i++)
      {
        DbEntityBaseSet entitySet = db.GetEntitySet(tableName);
      }

      TimeSpan ts = DateTime.Now - dtStart;

      txtOut.Text += ts.TotalMilliseconds.ToString("###,#00,000") + " milliseconds for Generic Objects - Count:" + loopCount.ToString() + " times" + g.nl;
    }

    private void btnLoopCodedQuery_Click(object sender, EventArgs e)
    {
      RunCodedQuery();
    }

    private void RunCodedQuery()
    {
      ////txtOut.Clear();
      ////Application.DoEvents();

      //DateTime dtStart = DateTime.Now;

      //int loopCount = 10;
      //if (txtLoopCount.Text.IsNumeric())
      //    loopCount = Int32.Parse(txtLoopCount.Text.Trim());

      //string tableName = cboTables.Text.Trim();

      //for (int i = 0; i < loopCount; i++)
      //{
      //    List<tbl_Account> accounts = db.GetAccounts();
      //}

      //TimeSpan ts = DateTime.Now - dtStart;

      //txtOut.Text += ts.TotalMilliseconds.ToString("###,#00,000") + " milliseconds for Coded Query - Count:" + loopCount.ToString() + " times" + g.nl;
    }

    private void btnRunSeries_Click(object sender, EventArgs e)
    {
      int loopCount = 10;
      if (txtSeriesTimes.Text.Trim().IsNumeric())
        loopCount = Int32.Parse(txtSeriesTimes.Text.Trim());

      for (int i = 0; i < loopCount; i++)
      {
        RunCodedQuery();
        Application.DoEvents();
      }

      for (int i = 0; i < loopCount; i++)
      {
        RunBusinessObject();
        Application.DoEvents();
      }

      for (int i = 0; i < loopCount; i++)
      {
        RunGenericObject();
        Application.DoEvents();
      }
    }

    private void btnTestTableRecord_Click(object sender, EventArgs e)
    {
      InsertTestTableRecords();
    }

    private void InsertTestTableRecords()
    {
      try
      {
        //TestTable1 tt = new TestTable1();

        //tt.int_pk = 0;

        //tt.bigint_n = null;  
        //tt.bigint_nn = 9223372036854775807;  

        //tt.binary_1000_n = null;
        //tt.binary_1000_nn = new byte[10] { 0,1,2,3,4,5,6,7,8,255 };

        //tt.bit_n = null;
        //tt.bit_nn = false;

        //tt.char_20_n = null;
        //tt.char_20_nn = "abcdefghijklmnopqrst";

        //tt.date_n = null;
        //tt.date_nn = DateTime.Now;

        //tt.datetime_n = null;
        //tt.datetime_nn = DateTime.Now;

        //tt.datetime2_7_n = null;
        //tt.datetime2_7_nn = DateTime.Now;

        //tt.datetimeoffset_7_n = null;
        //tt.datetimeoffset_7_nn = TimeSpan.MinValue;

        //tt.decimal18_0_n = null;
        //tt.decimal18_0_nn = 9M;

        //tt.decimal18_2_n = null; 
        //tt.decimal18_2_nn = 9.99M;

        //tt.float_n = null;
        //tt.float_nn = 9.99F;
                    
        //tt.image_n = null;
        //tt.image_nn = Image.FromFile(@"C:\_data\TestImage.png");

        //tt.int_n = null;
        //tt.int_nn = 1;

        //tt.money_n = null;
        //tt.money_nn = 3.00M;

        //tt.nchar_20_n = null;
        //tt.nchar_20_nn = "abcdefghijklmnopqrst";

        //tt.ntext_n = null;
        //tt.ntext_nn = "abcdefghijklmnopqrst";

        //tt.numeric_18_0_n = null;
        //tt.numeric_18_0_nn = 99M;

        //tt.numeric_18_2_n = null;
        //tt.numeric_18_2_nn = 99.99M;

        //tt.nvarchar_50_n = null;
        //tt.nvarchar_50_nn = "abcdefghijklmnopqrst";

        //tt.real_n = null;
        //tt.real_nn = 9.99F;

        //tt.smalldatetime_n = null;
        //tt.smalldatetime_nn = DateTime.Now;

        //tt.smallint_n = null;
        //tt.smallint_nn = 0;

        //tt.smallmoney_n = null;
        //tt.smallmoney_nn = 9.99M;

        //tt.text_n = null;
        //tt.text_nn = "abcdefghijklmnopqrst";

        //tt.time_7_n = null;
        //tt.time_7_nn = new TimeSpan(1, 1, 1);

        //tt.tinyint_n = null;
        //tt.tinyint_nn = 1;

        //tt.uniqueidentifier_n = null;
        //tt.uniqueidentifier_nn = Guid.NewGuid();

        //tt.varbinary_1000_n = null;
        //tt.varbinary_1000_nn = File.ReadAllBytes(@"C:\_data\binarydata.dat");

        //tt.varchar_50_n = null;
        //tt.varchar_50_nn = "abcdefghijklmnopqrst";

        //tt.xml_n = null;
        //tt.xml_nn = new XElement("testElement");

        //DbSet<TestTable1> ttSet = new DbSet<TestTable1>(db);
        //ttSet.Add(tt);

        //db.Insert<TestTable1>(ttSet);
      }
      catch(Exception ex)
      {
          string message = ex.Message;
          MessageBox.Show(ex.Message, "DbGen - Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnCheckExists_Click(object sender, EventArgs e)
    {
      if (txtColumn.Text.IsBlank())
      {
        txtOut.Text = "No table/columns specified";
        return;
      }

      if (txtValue.Text.IsBlank())
      {
        txtOut.Text = "No value specified.";
        return;
      }

      string column = txtColumn.Text.Trim();
      string value = txtValue.Text.Trim();


      if (DbHelper.EntityExists(db, column, value))
        txtOut.Text = "Entity exists.";
      else
        txtOut.Text = "Entity does not exist.";

    }
  }
}
