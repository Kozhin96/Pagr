<%@ Page Language="C#" MasterPageFile="~/PagrMasterPage.Master" AutoEventWireup="true" CodeFile="post.aspx.cs" Inherits="Pagr.post" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="pageSection">
<div id = "postContainer">
<asp:Repeater ID="Repeater1" runat="server">
<ItemTemplate>
    <div id = "postTitle">
        <asp:Label runat="server" ID="lblTitle" 
        text='<%# Eval("PostTitle") %>' />
    </div>

    <div id = "postDescription">
        <asp:Label runat="server" ID="lblDescription" 
        text='<%# Eval("PostBody") %>' />
    </div>

    <div id = "postSubmissionTime">
        <asp:Label runat="server" ID="lblTimeStamp" 
        text='<%# Eval("Submitted") %>' />
    </div>
<hr class = "hrPost">             
</ItemTemplate>
</asp:Repeater>
</div>
</div>
</asp:Content>  