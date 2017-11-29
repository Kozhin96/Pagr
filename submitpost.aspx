<%@ Page Title="submit a post" Language="C#" MasterPageFile="~/PagrMasterPage.Master" AutoEventWireup="true" CodeBehind="submitpost.aspx.cs" Inherits="Pagr.submitpost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="pageSection" id ="pageSection">
    <div id= "p">
		    <h1 id = "spIntro">post something...</h1>
        <div id = "spTitle">
		    <asp:TextBox ID="spTitleBox" placeholder="title..." runat="server">
            </asp:TextBox>
		</div>
        <div id = "spURL">
            <asp:TextBox id = "spURLBox" placeholder="url..." runat="server">
            </asp:TextBox>
        </div>  
	    <div id = "spDescription">
            <asp:TextBox id = "spDescriptionBox" placeholder="description..." runat="server">
            </asp:TextBox>
		</div>
        <div id = "spPost">
            <asp:Button class="btnPost" Text="post" OnClick="OnClick_SubmitPost" runat="server" />
		</div>
	</div>
</div>
</asp:Content>