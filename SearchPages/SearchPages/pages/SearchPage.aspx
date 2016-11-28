<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="SearchPages.pages.SearchPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1 {
            height: 314px;
            margin-bottom: 3px;
        }
    </style>
</head>
<body style="height: 314px">
    <form id="form1" runat="server">
    <div>

        
        <asp:Label ID="Label1" runat="server" Text="Title: "></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:IntProgGroupProjectConnectionString %>" SelectCommand="SELECT * FROM [Books]"></asp:SqlDataSource>
        <asp:TextBox ID="TitleSearchBox" runat="server" Width="242px"></asp:TextBox>


    
        <br />
        

        
   
        <asp:Button ID="ShowHideButton" runat="server" Text="Show/Hide Options" OnClick="Button2_Click" />

        <br />


    </div>


    <div id="advancedOptions" runat="server">
        
        <p></p>
        Year:<asp:DropDownList ID="searchYearDropdown" runat="server">
        </asp:DropDownList>
        <p>
            Genre:
            <asp:DropDownList ID="searchGenreDropdown" runat="server">
            </asp:DropDownList>
            
        </p>
        <p>
            Is it a book and a movie?</p>

        <asp:RadioButtonList ID="BookAndMovieList" runat="server">
            <asp:ListItem ID="IsBothYes" runat="server" Text="Yes"/>
            <asp:ListItem ID="IsBothNo" runat="server" Text="No" Selected="True"/>
        </asp:RadioButtonList>

        <br />
        <br />

        </div>

        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />


    
    </form>
</body>
</html>
