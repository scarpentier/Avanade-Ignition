<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerInfo.ascx.cs" Inherits="Web.UserControls.CustomerInfo" %>

<table width="100%">
    <tr>
        <td>First Name</td>
        <td><asp:TextBox ID="txtFirstName" runat="server" TabIndex="10"></asp:TextBox></td>
        <td> Social Security Number</td>
        <td> <asp:TextBox ID="txtSocialSecurityNumber" runat="server" TabIndex="60"></asp:TextBox></td>
        <td>
        </td>  
            <td rowspan="5" class="discountCell">
                <fieldset class="discount">
                    <legend>Discounts</legend>
                                    <asp:CheckBox ID="cbLessThan3years" runat="server" 
                        Text="Customer less than 3 years driving" TabIndex="100" /><br/>
            <asp:CheckBox ID="cbMultiCar" runat="server" Text="Multi-car discount" TabIndex="100" /><br/>
            <asp:CheckBox ID="cbClaimPenalty" runat="server" Text="Claim in the last 5 years" 
                        TabIndex="110" /><br/>
            <asp:CheckBox ID="cbMovingViolation" runat="server" 
                        Text="Moving violation in the last 5 years" TabIndex="120" />
                </fieldset>

        </td>     
    </tr>
        
   <tr>
       <td>Last Name</td>
       <td> <asp:TextBox ID="txtLastName" runat="server" TabIndex="20"></asp:TextBox></td>
       <td>Last Carrier</td>
       <td><asp:TextBox ID="txtPreviousCarrier" runat="server" TabIndex="70"></asp:TextBox>
       <ajaxToolkit:AutoCompleteExtender
              MinimumPrefixLength="1" ID="AutoCompleteExtender1" CompletionInterval="5" ServicePath="~/AutoComplete.asmx"
               ServiceMethod="GetCarriersList" TargetControlID="txtPreviousCarrier" runat="server" >
         </ajaxToolkit:AutoCompleteExtender>
       </td>
   </tr>
        
   <tr>
        <td> Date of Birth</td>
        <td>
            <asp:TextBox ID="calendarPersonDOB" runat="server" TabIndex="30"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="extender" TargetControlID="calendarPersonDOB" 
                runat="server" StartDate="1900-01-01"></ajaxToolkit:CalendarExtender></td>
        <td> &nbsp;</td>
        <td> 
            &nbsp;</td>
    </tr>
    <tr>
    <td> Address </td>
    <td> <asp:TextBox ID="txtAddressLine1" runat="server" TabIndex="40"></asp:TextBox></td>
    <td> City</td>
    <td> <asp:TextBox ID="txtCity" runat="server" TabIndex="80"></asp:TextBox></td>        
    </tr>
        <tr>
        <td> State</td>
        <td> 
            <asp:DropDownList ID="ddlState" runat="server" DataSourceID="EntityDataSource1" 
                DataTextField="StateCode" DataValueField="ID" TabIndex="50">
            </asp:DropDownList>
        </td>   
        <td> Zip Code</td>
        <td> <asp:TextBox ID="txtAddressZip" runat="server" TabIndex="90"></asp:TextBox></td>
   </tr>
        
</table>
        
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ValidationGroup="CustomerInforValidationGroup"
            ControlToValidate="txtFirstName"
            ErrorMessage="Please enter your first name" Display="None"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
            TargetControlID="RequiredFieldValidator1" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
            TargetControlID="txtSocialSecurityNumber" Mask="999-99-9999"
            AutoComplete="false" 
            MaskType="Number"
            ClearMaskOnLostFocus="true"></ajaxToolkit:MaskedEditExtender>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
            ValidationGroup="CustomerInforValidationGroup"
            ControlToValidate="txtSocialSecurityNumber" 
            ErrorMessage="SSN is Invalid"
            ValidationExpression="\d{3}\d{2}\d{4}"></asp:RegularExpressionValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
            TargetControlID="RegularExpressionValidator1" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
            ValidationGroup="CustomerInforValidationGroup" 
            ControlToValidate="txtLastName"
            ErrorMessage="Last name field cannot be left blank" 
            Display="None"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
            TargetControlID="RequiredFieldValidator3" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
            ValidationGroup="CustomerInforValidationGroup" 
            ControlToValidate="calendarPersonDOB"
            ErrorMessage="Birthday field cannot be left blank" 
            Display="None"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
            TargetControlID="RequiredFieldValidator5" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
            TargetControlID="calendarPersonDOB" Mask="99/99/9999"
            MaskType="Date" InputDirection="LeftToRight" Enabled="False" 
            ></ajaxToolkit:MaskedEditExtender>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
            ValidationGroup="CustomerInforValidationGroup" 
            ControlToValidate="txtAddressLine1"
            ErrorMessage="Address field cannot be left blank" 
            Display="None"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
            TargetControlID="RequiredFieldValidator7" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
            ValidationGroup="CustomerInforValidationGroup"
            ControlToValidate="txtCity"
            ErrorMessage="City field cannot be left blank" 
            Display="None"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
            TargetControlID="RequiredFieldValidator8" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
   
        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
            ValidationGroup="CustomerInforValidationGroup"
            ControlToValidate="txtAddressZip"
            ErrorMessage="Please enter a valid zip code"
            Display="None"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
            TargetControlID="RequiredFieldValidator9" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
            ValidationGroup="CustomerInforValidationGroup"
            ControlToValidate="txtAddressZip" 
            ErrorMessage="ZIP code is a 5 digits numberic value"
            ValidationExpression="\d{5}"></asp:RegularExpressionValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
            TargetControlID="RegularExpressionValidator3" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
            <asp:EntityDataSource ID="EntityDataSource1" runat="server" 
                ConnectionString="name=QuotesDBEntities" 
                DefaultContainerName="QuotesDBEntities" EnableFlattening="False" 
                EntitySetName="States">
            </asp:EntityDataSource>
<p style="text-align: right">
        <asp:CheckBox ID="cbAddAsDriver" runat="server" Text="Also add as a driver" 
        Visible="False" TabIndex="130" />&nbsp; &nbsp;<asp:Button ID="UpdateButton" 
            runat="server" onclick="UpdateButton_Click" 
        Text="Save customer" ValidationGroup="CustomerInforValidationGroup"
                CausesValidation="true" 
        OnClientClick="return validation('CustomerInforValidationGroup')" 
            TabIndex="140" /></p>
