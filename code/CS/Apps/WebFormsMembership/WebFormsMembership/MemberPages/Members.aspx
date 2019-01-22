<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Members.aspx.cs" Inherits="MemberPages_Members" %>

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
              <h1>Members Only Page</h1>
            </asp:TableCell>
          </asp:TableRow>

          <asp:TableRow runat="server" Height="100px" VerticalAlign="Top">
            <asp:TableCell runat="server">           
            </asp:TableCell>
            <asp:TableCell runat="server">
            </asp:TableCell>
            <asp:TableCell runat="server">
              <asp:HyperLink ID="HyperLink1"  Width="200px" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>  
            </asp:TableCell>
          </asp:TableRow>

          <asp:TableRow runat="server" Height="500px" VerticalAlign="Top">
            <asp:TableCell runat="server">           
            </asp:TableCell>
            <asp:TableCell runat="server">
            </asp:TableCell>
            <asp:TableCell runat="server">
            </asp:TableCell>
          </asp:TableRow>

          <asp:TableRow runat="server">
            <asp:TableCell runat="server" ColumnSpan="3">Footer</asp:TableCell>
          </asp:TableRow>
        </asp:Table>
    </form>
</body>
</html>
