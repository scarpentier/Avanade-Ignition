<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <fieldset>
        <legend>Incomplete Quotes</legend>
        <p>Select any incomplete quote you would like to continue</p>
            <asp:GridView ID="IncompleteQuoteGridView" runat="server" 
             AutoGenerateColumns="False" DataKeyNames="ID" 
            onrowcommand="IncompleteQuoteGridView_RowCommand" CellPadding="4" 
            ForeColor="#333333" GridLines="None" AllowSorting="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="DateCreated" HeaderText="Date Created" 
                        SortExpression="DateCreated" />
                    <asp:BoundField DataField="CustomerFirstName" HeaderText="First Name" 
                        SortExpression="CustomerFirstName" />
                    <asp:BoundField DataField="CustomerLastName" HeaderText="Last Name" 
                        SortExpression="CustomerLastName" />
                        <asp:BoundField DataField="StateCode" HeaderText="State" SortExpression="StateCode"/>
                        <asp:BoundField DataField="Drivers" HeaderText="# of Drivers" SortExpression="Drivers"/>
                        <asp:BoundField DataField="Vehicles" HeaderText="# of Vehicles" SortExpression="Vehicles" />
                        <asp:ButtonField CommandName="EditIncomplete" Text="Edit" />
                        <asp:ButtonField CommandName="DeleteIncomplete" Text="Delete" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <EmptyDataTemplate>
                    No incomplete quotes to show
                </EmptyDataTemplate>
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
    </fieldset>
    <fieldset>
        <legend>Completed Quotes</legend>
        <asp:GridView ID="CompletedQuotesGridView" runat="server" 
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
            GridLines="None" onrowcommand="CompletedQuoteGridView_RowCommand" 
            DataKeyNames="ID" AllowSorting="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField HeaderText="Date Created" DataField="DateCreated" SortExpression="DateCreated" />
                <asp:BoundField HeaderText="First Name" DataField="CustomerFirstName" SortExpression="CustomerFirstName"/>
                <asp:BoundField HeaderText="Last Name" DataField="CustomerLastName" SortExpression="CustomerLastName"/>
                <asp:BoundField HeaderText="State" DataField="StateCode" SortExpression="StateCode"/>
                <asp:BoundField HeaderText="# of Drivers" DataField="Drivers" SortExpression="Drivers" />
                <asp:BoundField HeaderText="# of Vehicles" DataField="Vehicles" SortExpression="Vehicles"/>
                <asp:BoundField HeaderText="Price" DataField="Price" SortExpression="Price"/>
                <asp:ButtonField CommandName="CopyQuote" Text="Copy" />
                <asp:ButtonField CommandName="OpenQuote" Text="Open" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <EmptyDataTemplate>
                    No incomplete quotes to show
            </EmptyDataTemplate>
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
    </fieldset>

</asp:Content>
