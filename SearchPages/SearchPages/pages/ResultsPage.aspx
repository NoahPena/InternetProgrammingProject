<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultsPage.aspx.cs" Inherits="SearchPages.pages.ResultsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Results:<br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Go Back" />
    
    </div>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:IntProgGroupProjectConnectionString %>" SelectCommand="SELECT * FROM [BOOK_INVENTORY]"></asp:SqlDataSource>
    </form>
</body>
</html>
