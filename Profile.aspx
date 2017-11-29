<%@ Page Title="" Language="C#" MasterPageFile="~/PagrMasterPage.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Pagr.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pageSection" id="pageSection">
        <div>
            <div class="blackOverflow" runat="server" visible="false" id="signUpAndRegisterDialog">
                <div class="MasterLogInBox">
                    <div>
                        <asp:LinkButton runat="server" Text="close" CssClass="closeBox" OnClick="OnClickClose_SignInDialogBox" />
                    </div>
                    <div>
                        <asp:FileUpload ID="ticketImageUpload" runat="server" />
                    </div>
                </div>
            </div>
            <div class="profileSecion">
                <div class="userDetailSection">
                    <asp:Label runat="server" CssClass="lblUserDetailHeader" ID="lblUserName" />
                    <div class="divProfileImg">
                        <asp:ImageButton runat="server" CssClass="imgUserImage" ImageUrl="~/Images/NoUserMainImageIcon.png" OnClick="changeProfileImage_OnClick" />
                    </div>
                    <div class="divProfileStat">
                        <asp:Label CssClass="lblUserDetail" runat="server" ID="lblUserPost"></asp:Label>
                        <asp:Label CssClass="lblUserDetail" runat="server" ID="lblUserFollowing"></asp:Label>
                        <asp:Label CssClass="lblUserDetail" runat="server" ID="lblUserFriends"></asp:Label>
                    </div>
                    <div class="divUserDescription">
                        <asp:Label ID="lblUserDescription" CssClass="lblUserDescription" runat="server" Text="My name is ashok and I love anime" />
                    </div>
                </div>
                <div class="userPostSection">
                    <asp:Button runat="server" Text="Link" CssClass="usrTabButon" />
                    <asp:Button runat="server" Text="Text" CssClass="usrTabButon" />
                    <div class="divUserPost" runat="server">
                        <asp:TextBox runat="server" CssClass="PostTextBox" placeholder="Post Title" />
                        <asp:TextBox runat="server" CssClass="PostTextBox" placeholder="Description" />
                        <asp:TextBox runat="server" CssClass="PostTextBox" placeholder="Url" />
                        <asp:FileUpload ID="FileUpload1" CssClass="PostTextBox" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
