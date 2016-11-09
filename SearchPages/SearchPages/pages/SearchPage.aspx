<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="SearchPages.pages.SearchPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 303px;
        }
    </style>
</head>
<body style="height: 299px">
    <form id="form1" runat="server">
    <div>

        
        <asp:Label ID="Label1" runat="server" Text="Title: "></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:IntProgGroupProjectConnectionString %>" SelectCommand="SELECT * FROM [Books]"></asp:SqlDataSource>
        <asp:TextBox ID="TextBox1" runat="server" Width="242px"></asp:TextBox>



        
    
        <br />
        

        
    
    </div>

        Year:<asp:DropDownList ID="searchYearDropdown" runat="server">
        </asp:DropDownList>
        <p>
            Genre:
            <asp:DropDownList ID="searchGenreDropdown" runat="server">
            </asp:DropDownList>
            
        </p>
        <p>
            Is it a book and a movie?</p>

        <asp:RadioButton ID="RadioButton1" runat="server" Text="Yes" />
        <asp:RadioButton ID="RadioButton2" runat="server" Text="No" />

        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Search" />

    </form>
</body>
</html>
