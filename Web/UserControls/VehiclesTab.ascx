<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VehiclesTab.ascx.cs" Inherits="Web.UserControls.VehiclesTab" %>
<%@ Register src="VehicleInfo.ascx" tagname="VehicleInfo" tagprefix="uc1" %>
<asp:GridView ID="gvVehicles" runat="server"  
     AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" 
    DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="None" 
    AllowSorting="True">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:ButtonField Text="Edit" CommandName="EditVehicle"/>
        <asp:ButtonField Text="Delete" CommandName="DeleteVehicle" />
        <asp:BoundField HeaderText="Year"  DataField="Year" SortExpression="Year"/>
        <asp:BoundField HeaderText="Vehicle Make" DataField="Make" SortExpression="Make"/>
        <asp:BoundField HeaderText="Vehicle Model" DataField="Model" SortExpression="Model"/>
        <asp:BoundField HeaderText="VIN" DataField="Vin" />
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
<p><asp:Button ID="Button1" runat="server" Text="Add new vehicle" onclick="Button1_Click" /></p>
<uc1:VehicleInfo ID="ucVehicleInfo" runat="server" visible="false"/>
