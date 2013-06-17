<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Finish.aspx.cs" Inherits="Web.Finish" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Quote summary</h2>
    <fieldset>
        <legend>Client Information</legend>
        <asp:Label ID="lblClientName" runat="server"></asp:Label><br/>
        <asp:Label ID="lblCity" runat="server"/>, <asp:Label ID="lblState" runat="server"></asp:Label>
    </fieldset>
    <fieldset>
        <legend>Price Breakdown</legend>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </fieldset>
    <asp:Label ID="lblTotal" runat="server" Font-Bold="True"></asp:Label><p/>
</asp:Content>
