<%@ Page Title="" Language="C#" MasterPageFile="~/PagrMasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Pagr.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="pageSection">
        <div class="LogInBox">
            <div class="divLogInText">
                <asp:Label runat="server" ID="LoginRegisterHeader" CssClass="divLbloginTXT" Text="Log In" />
            </div>
            <div runat="server" id="LogInPanel" class="divLogInPanel">
                <div class="divLogInText">
                    <asp:TextBox CssClass="txtLogin" ID="txtUserOrEmail" runat="server" placeholder="Username/Email Address" />
                    <asp:Label runat="server" ID="SignInUserOrEmailError" CssClass="lblError" Visible="false" />
                </div>
                <div class="divLogInText">
                    <asp:TextBox CssClass="txtLogin" ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" />
                    <asp:Label runat="server" ID="signInPasswordError" CssClass="lblError" Visible="false" />
                </div>
                <div class="divLogInText">
                    <asp:CheckBox ID="cbRememberME" CssClass="divCbRememberME CleardivCbRememberME" runat="server" Text="Remember Me" />
                    <asp:LinkButton CssClass="divSignUpLink" runat="server" Text="Register Account" OnClick="OnClick_OpenRegisterDialog" />
                </div>
                <div class="divLogInText">
                    <asp:Button ID="btnSignIn" CssClass="btnSignIn" Text="Log In" runat="server" OnClick="OnClick_SignUserIn" />
                </div>
            </div>
            <div runat="server" id="SignUpPanel" visible="false" class="divLogInPanel">
                <div class="divLogInText">
                    <asp:TextBox CssClass="txtLogin" ID="txtUserName" runat="server" placeholder="Username" />
                    <asp:Label runat="server" ID="userNameError" CssClass="lblError" Visible="false" />
                </div>
                <div class="divLogInText">
                    <asp:TextBox CssClass="txtLogin" ID="txtEmailAddress" runat="server" placeholder="Email Address" />
                    <asp:Label runat="server" ID="emailAddressError" CssClass="lblError" Visible="false" />
                </div>
                <div class="divLogInText">
<%--                    The problem atm is that the server side method can not read text box type password so for now, when enteting password is visible
                    Must this ASAP--%>
                      <%--<asp:TextBox CssClass="txtLogin" ID="TextBox1" TextMode="Password" runat="server" placeholder="Password" />--%>
                    <asp:TextBox CssClass="txtLogin" ID="txtPasswordRegestration" TextMode="Password" runat="server" placeholder="Password" />
                    <asp:Label runat="server" ID="userRegestrainPasswordError" CssClass="lblError" Visible="false" />
                </div>
                <div class="divLogInText">
                    <asp:Button ID="btnSignUp" CssClass="btnSignIn" Text="Sign Up" OnClick="OnClick_OpenSignUpSecondStage" runat="server" />
                </div>
                <div class="divLogInText">
                    <asp:LinkButton CssClass="divCbRememberME CleardivCbRememberME reLogIN" runat="server" Text="Log In" OnClick="OnClick_OpenSignInPanel" />
                </div>
            </div>
            <div runat="server" id="CompleteSignUpPanel" visible="false" class="divLogInPanel">
                <div class="divLogInText">
                    <asp:Label CssClass="LogInsubheader" runat="server" Text="What Intrests you?" />
                    <asp:Label CssClass="LogInsubheader" runat="server" Text="Please choose minimum of 3 categories" />
                </div>
                <asp:CheckBoxList CssClass="divCbRememberME CleardivCbRememberME" ID="cblIntrests" runat="server">
                    <asp:ListItem>Art</asp:ListItem>
                    <asp:ListItem>Anime</asp:ListItem>
                    <asp:ListItem>Food</asp:ListItem>
                    <asp:ListItem>Videos</asp:ListItem>
                    <asp:ListItem>News</asp:ListItem>
                    <asp:ListItem>Science</asp:ListItem>
                </asp:CheckBoxList>
                <asp:Label runat="server" ID="checkBoxListError" CssClass="lblError" Visible="false" />
                <div class="divLogInText">
                    <asp:LinkButton CssClass="divSignUpLink" runat="server" Text="Skip" OnClick="OnClick_SkipIntrests" />
                    <asp:LinkButton CssClass="divSignUpLink" runat="server" Text="Complete" OnClick="OnClick_CompleteRegestration" />
                </div>
            </div>
        </div>

    </div>
</asp:Content>
