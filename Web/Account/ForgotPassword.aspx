<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Web.Account.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Forgot Password</h2>
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
        <UserNameTemplate>
            <fieldset>
                    <legend>Password Recovery</legend>
                    <p>Enter your User Name to receive your password.</p>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                        ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                        ToolTip="User Name is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                    </p>
            </fieldset>
            
            <p class="submitButton">
                    <asp:Button ID="Button1" runat="server" CommandName="Submit" Text="Submit" 
                                        ValidationGroup="PasswordRecovery1" />
                </p>
                <p>
                    <asp:Literal ID="Literal1" runat="server" EnableViewState="False"></asp:Literal>
                </p>
        </UserNameTemplate>
    </asp:PasswordRecovery>
</asp:Content>
