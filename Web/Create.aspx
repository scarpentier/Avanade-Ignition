<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="Web.Create" %>
<%@ Register src="UserControls/DriversTab.ascx" tagname="DriversTab" tagprefix="uc1" %>
<%@ Register src="UserControls/CustomerInfo.ascx" tagname="PersonInfo" tagprefix="uc2" %>
<%@ Register src="UserControls/VehiclesTab.ascx" tagname="VehiclesTab" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        function validation(str) {
            var validated = Page_ClientValidate(str);
            if (validated)
                return true;
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create new quote</h2>
    <fieldset>
    <legend>Customer Information</legend>

        <uc2:PersonInfo ID="ucCustomerInfo" runat="server" />
                </fieldset>
    <p><asp:CheckBox ID="CheckBox1" Text="Customer is also a driver" runat="server" Visible="False" /><p/>

    &nbsp;<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Visible="False">
        <ajaxToolkit:TabPanel runat="server" HeaderText="Drivers" ID="TabPanel1" CssClass="tab">
            <ContentTemplate>
                <uc1:DriversTab ID="ucDriversTab" runat="server" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>

        <ajaxToolkit:TabPanel runat="server" HeaderText="Vehicles" ID="TabPanel2" CssClass="tab">
            <ContentTemplate>
                <uc3:VehiclesTab runat="server" ID="ucVehiclesTab" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        
    </ajaxToolkit:TabContainer>
    <p><asp:Button ID="btnFinish" runat="server" Text="Process quote" 
            onclick="btnFinish_Click" Visible="False" TabIndex="10000" /></p>
</asp:Content>
