<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Table ID="Table1" runat="server" Height="160px" Width="1189px" Font-Names="Verdana">
          <asp:TableRow runat="server" Height="80px" HorizontalAlign="Center" VerticalAlign="Top">
            <asp:TableCell runat="server" ColumnSpan="3">
              <h1>Login Page</h1>
            </asp:TableCell>
          </asp:TableRow>

          <asp:TableRow runat="server" Height="500px" VerticalAlign="Top">
            <asp:TableCell runat="server">
              <asp:Login ID="Login1" runat="server">
              </asp:Login>              
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Login1" />            
            </asp:TableCell>

            <asp:TableCell runat="server">
              <asp:CreateUserWizard ID="CreateUserWizard1" runat="server">
                <WizardSteps>
                  <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server"></asp:CreateUserWizardStep>                  
                  <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server"></asp:CompleteWizardStep>
                </WizardSteps>
              </asp:CreateUserWizard>
            </asp:TableCell>
            <asp:TableCell runat="server">
              <asp:ChangePassword ID="ChangePassword1" runat="server">
              </asp:ChangePassword>
            </asp:TableCell>
          </asp:TableRow>

          <asp:TableRow runat="server">
            <asp:TableCell runat="server" ColumnSpan="3">Footer</asp:TableCell>
          </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
