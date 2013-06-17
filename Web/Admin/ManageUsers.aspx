<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="Web.Admin.ApproveUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Users Management</h2>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" 
        DataKeyNames="UserId" 
        DataSourceID="ManageUsersDataSource" onrowcommand="GridView1_RowCommand" 
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
           
            
            <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" 
                SortExpression="UserName" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="CreateDate" HeaderText="Create Date" ReadOnly="True" 
                SortExpression="CreateDate" />
            <asp:TemplateField HeaderText="Is Approved" SortExpression="IsApproved">
                <ItemTemplate>
                    <asp:CheckBox ID="cbToggleApproval" runat="server" Checked='<%# Bind("IsApproved") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="Remove" Text="Remove"  Visible="False" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

<asp:EntityDataSource ID="ManageUsersDataSource" runat="server" 
    ConnectionString="name=QuotesDBEntities" 
    DefaultContainerName="QuotesDBEntities" EnableFlattening="False" 
    EntitySetName="vw_aspnet_MembershipUsers" EnableUpdate="True">
</asp:EntityDataSource>
    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Save" />
    </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
