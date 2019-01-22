<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Table ID="Table1" runat="server" Height="439px" Width="1101px" Font-Names="Verdana">
          <asp:TableRow runat="server" Height="50px" HorizontalAlign="Center" VerticalAlign="Top">
            <asp:TableCell runat="server" ColumnSpan="3">
              <h1>Web Forms Membership Home Page</h1>
            </asp:TableCell>
          </asp:TableRow>
          <asp:TableRow runat="server" HorizontalAlign="Right" VerticalAlign="Top">
            <asp:TableCell runat="server">
            </asp:TableCell>
            <asp:TableCell runat="server"> 
              <asp:HyperLink ID="HyperLink1"  Width="200px" runat="server" NavigateUrl="~/MemberPages/Members.aspx">Members-Only</asp:HyperLink>
              <asp:HyperLink ID="HyperLink2"  Width="200px" runat="server" NavigateUrl="~/OpenPage.aspx">Open Page</asp:HyperLink>   
            </asp:TableCell>
            <asp:TableCell runat="server" VerticalAlign="Top">
              <asp:LoginStatus ID="LoginStatus1" runat="server" />   
          </asp:TableCell>
          </asp:TableRow>
          <asp:TableRow runat="server">
            <asp:TableCell runat="server">
              <asp:LoginView ID="LoginView1" runat="server">
                <AnonymousTemplate>
                  You are not logged in. Click the Login link to sign in...                
                </AnonymousTemplate>                
                <LoggedInTemplate>
                  You are logged in. Welcome!<asp:LoginName ID="LoginName1" runat="server" />                
                </LoggedInTemplate>              
              </asp:LoginView>            
            </asp:TableCell>
            <asp:TableCell runat="server"></asp:TableCell>
            <asp:TableCell runat="server"></asp:TableCell>
          </asp:TableRow>
          <asp:TableRow runat="server">
            <asp:TableCell runat="server" ColumnSpan="3">Footer</asp:TableCell>
          </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
