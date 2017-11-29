<%@ Page Title="" Language="C#" MasterPageFile="~/PagrMasterPage.Master" AutoEventWireup="true" CodeBehind="tabs.aspx.cs" Inherits="Pagr.tabs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="TabsBar">
    <asp:Button ID="GIFSTabButton" runat="server" CssClass="Buttons" Text="GIFS" OnClick="GIFSTabButton_Click"/>         
    <asp:Button ID="VideosTabButton" runat="server" CssClass="Buttons" Text="Videos"  OnClick="VideosTabButton_Click"/> 
    <asp:Button ID="ImagesTabButton" runat="server" CssClass="Buttons" Text="Images"  OnClick="ImagesTabButton_Click"/>
    <asp:Button ID="LinksTabButton" runat="server" CssClass="Buttons" Text="Links"  OnClick="LinksTabButton_Click"/>
    <asp:Button ID="TextTabButton" runat="server" CssClass="Buttons" Text="Text"  OnClick="TextTabButton_Click"/>
    <asp:Button ID="WorkTabButton" runat="server" CssClass="Buttons" Text="Work Appropriate" OnClick="WorkTabButton_Click"/>
    </div>
	
</asp:Content>
