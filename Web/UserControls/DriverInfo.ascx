<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DriverInfo.ascx.cs" Inherits="Web.UserControls.DriverInfo" %>
<table width="100%">
    <tr>
        <td>First Name</td>
        <td><asp:TextBox ID="txtFirstName" runat="server" TabIndex="1010"></asp:TextBox></td><td>Social Security Number</td>
        <td> <asp:TextBox ID="txtSocialSecurityNumber" runat="server" TabIndex="1050"></asp:TextBox></td>
        <td></td>
        <td rowspan="4" width="320px" valign="top">
            <fieldset class="discount">
                <legend>Discounts</legend>
                <asp:CheckBox ID="cbAttendedSafeSchool" 
                    Text="Has the driver attended safe driving school?" runat="server" 
                    TabIndex="1070"/> 
            </fieldset>
        </td>
    </tr>
   <tr>
       <td>Last Name</td>
       <td> <asp:TextBox ID="txtLastName" runat="server" TabIndex="1020"></asp:TextBox></td>
       <td> License Number</td>
       <td><asp:TextBox ID="txtLicenseNumber" runat="server" MaxLength="15" 
               TabIndex="1060"></asp:TextBox></td>
   </tr>
   <tr>
       <td> License State</td>
       
       <td>             
       <asp:DropDownList ID="ddlLicenceState" runat="server" DataTextField="StateCode" 
               DataValueField="ID" DataSourceID="EntityDataSource1" TabIndex="1030">
            </asp:DropDownList>
           <asp:EntityDataSource ID="EntityDataSource1" runat="server" 
               ConnectionString="name=QuotesDBEntities" 
               DefaultContainerName="QuotesDBEntities" EnableFlattening="False" 
               EntitySetName="States">
           </asp:EntityDataSource>
       </td>
   </tr>
   <tr><td> Date of Birth</td>
        <td>
            <asp:TextBox ID="calendarDriverDOB" runat="server" TabIndex="1040"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="extender" TargetControlID="calendarDriverDOB" 
                runat="server"
                StartDate="1900-01-01" ></ajaxToolkit:CalendarExtender></td>
        <td> &nbsp;</td>
        <td> 
            &nbsp;</td>
   </tr>
   </table>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            ValidationGroup="DriverInfoValidationGroup" 
            ControlToValidate="txtFirstName"
            ErrorMessage="Please enter driver's first name" Display="None"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
            TargetControlID="RequiredFieldValidator1" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
            TargetControlID="txtSocialSecurityNumber" Mask="999-99-9999"
            MaskType="Number" InputDirection="LeftToRight" 
            ClearMaskOnLostFocus="true"
            AutoComplete="false"></ajaxToolkit:MaskedEditExtender>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
            ValidationGroup="DriverInfoValidationGroup"
            ControlToValidate="txtSocialSecurityNumber"
            ErrorMessage="SSN is Invalid"
            ValidationExpression="\d{3}\d{2}\d{4}"></asp:RegularExpressionValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
            TargetControlID="RegularExpressionValidator1" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
            ValidationGroup="DriverInfoValidationGroup"  
            ControlToValidate="txtLastName"
            ErrorMessage="Enter driver's last name" 
            Display="None"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"           
            TargetControlID="RequiredFieldValidator3" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
            ValidationGroup="DriverInfoValidationGroup" 
            ControlToValidate="txtLicenseNumber"
            ErrorMessage="Enter driver's license number" 
            Display="None"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
            TargetControlID="RequiredFieldValidator4" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
            ValidationGroup="DriverInfoValidationGroup" 
            ControlToValidate="calendarDriverDOB"
            ErrorMessage="Birthday field cannot be left blank" 
            Display="None" ></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
            TargetControlID="RequiredFieldValidator5" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
            TargetControlID="calendarDriverDOB" Mask="99/99/9999" Enabled="False"
            MaskType="Date" InputDirection="LeftToRight" ></ajaxToolkit:MaskedEditExtender>

            <p style="text-align: right">
                <asp:Button ID="cancel" runat="server" Text="Cancel"
                onclick="cancel_Click1" TabIndex="1080" />&nbsp;<asp:Button ID="saveDriver" 
                    runat="server" Text="Save driver" 
                onclick="saveDriver_Click" 
                ValidationGroup="DriverInfoValidationGroup"
                CausesValidation="true" 
                    OnClientClick="return validation('DriverInfoValidationGroup')" TabIndex="1090"
             /></p>
        