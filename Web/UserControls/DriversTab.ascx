<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DriversTab.ascx.cs" Inherits="Web.UserControls.DriversTab" %>
<%@ Register src="DriverInfo.ascx" tagname="DriverInfo" tagprefix="uc3" %>


<asp:GridView ID="gvDrivers" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="ID" onrowcommand="GridView1_RowCommand" CellPadding="4" 
    ForeColor="#333333" GridLines="None" AllowSorting="True" >
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
                    <asp:ButtonField CommandName="EditDriver" Text="Edit" />
                    <asp:ButtonField CommandName="DeleteDriver" Text="Delete" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" 
                        SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" 
                        SortExpression="LastName" />
                    <asp:BoundField DataField="DateOfBirth" HeaderText="Date of Birth" 
                        SortExpression="DateOfBirth" DataFormatString="{0:d}"  />
                    <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age"/>
                    <asp:BoundField DataField="StateCode" HeaderText="State" SortExpression="StateCode"/>
                    <asp:BoundField DataField="DriverLicenseNumber" HeaderText="License Number" 
                        SortExpression="DriverLicenseNumber" />
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

<p><asp:Button ID="Button1" runat="server" Text="Add new driver" 
    onclick="Button1_Click" /></p>

<uc3:DriverInfo ID="ucDriverInfo" runat="server" Visible="False" />
