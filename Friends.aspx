<%@ Page Title="" Language="C#" MasterPageFile="~/PagrMasterPage.Master" AutoEventWireup="true" CodeBehind="Friends.aspx.cs" Inherits="Pagr.Friends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="friendsPageSection">
        <asp:Repeater runat="server" ID="repFriendsList">
            <ItemTemplate>
                    <asp:HyperLink runat="server" class="friendContainer" NavigateUrl='<%# Eval("AccountID","~/Profile.aspx?FriendID={0}") %>' >
                <asp:Image runat="server" CssClass="imgFriendUserImage" id="imgButton" ImageUrl="~/Images/NoUserMainImageIcon.png"  />
                <asp:Label runat="server" CssClass="lblFriendsUserName" ID="username" Text='<%# Bind("username") %>' />
                    </asp:HyperLink>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

