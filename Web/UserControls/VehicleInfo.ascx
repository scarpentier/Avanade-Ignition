<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VehicleInfo.ascx.cs" Inherits="Web.UserControls.VehicleInfo" %>
<table width="100%">
    <tr>
        <td>VIN</td>
        <td><asp:TextBox ID="txtVin" runat="server" MaxLength="17" TabIndex="2010"></asp:TextBox></td>
        <td>Primary Driver</td>
        <td>   
        <asp:DropDownList ID="ddlPrimaryDriver"  
                runat="server"  DataTextField="FirstName" DataValueField="ID" 
                TabIndex="2060">
        </asp:DropDownList>
          
        </td>
        <td></td>
        <td rowspan="5" width="320px" valign="top">
            <fieldset class="discount">
                <legend>Discounts</legend>
                <asp:CheckBox ID="daytimeRunningLights"  
                    Text="Does the vehicle have daytime running lights?" runat="server" 
                    TabIndex="2100" /><br/>
       <asp:CheckBox ID="antiLockBrakes"  Text="Does the vehicle have Antilock brakes?" 
                    runat="server" TabIndex="2110" /><br/>
       <asp:CheckBox ID="passiveRestraint"  Text="Does the vehicle have passive restraint?" 
                    runat="server" TabIndex="2120" /><br/>
       <asp:CheckBox ID="reduceUse"  Text="Reduced use discount" runat="server" 
                    TabIndex="2130" /><br/>
       <asp:CheckBox ID="antiTheft"  Text="Does the vehicle have Anti-theft installed?" 
                    runat="server" TabIndex="2140" /><br/>
       <asp:CheckBox ID="garageAddress"  
                    Text="Is the garage address different from the residence address?" 
                    runat="server" TabIndex="2150" /><br/>
            </fieldset>
        </td>
    </tr>

   <tr>
       <td>Make</td>
       <td> <asp:TextBox ID="txtMake" runat="server" TabIndex="2020"></asp:TextBox><ajaxToolkit:AutoCompleteExtender
              MinimumPrefixLength="1" ID="AutoCompleteExtender1" CompletionInterval="5" ServicePath="~/AutoComplete.asmx"
               ServiceMethod="GetVehicleMake" TargetControlID="txtMake" runat="server" >
         </ajaxToolkit:AutoCompleteExtender></td>
       <td>Annual Mileage</td>
       <td><asp:TextBox ID="txtAnnualMileage" runat="server" TabIndex="2070"></asp:TextBox></td>
   </tr>
   <tr>
       <td>Model</td>
       <td> <asp:TextBox ID="txtModel" runat="server" TabIndex="2030"></asp:TextBox><ajaxToolkit:AutoCompleteExtender
              MinimumPrefixLength="1" ID="AutoCompleteExtender2" CompletionInterval="5" ServicePath="~/AutoComplete.asmx"
               ServiceMethod="GetVehicleModel" TargetControlID="txtModel" runat="server" >
         </ajaxToolkit:AutoCompleteExtender>
       </td>
       <td>Days driven per week</td>
       <td><asp:TextBox ID="txtDaysDrivenperwk" runat="server" TabIndex="2080"></asp:TextBox></td>
   </tr>
   <tr>
       <td>Year</td>
       <td> <asp:TextBox ID="txtYear" runat="server" TabIndex="2040"></asp:TextBox></td>
       <td>Miles driven to work</td>
       <td><asp:TextBox ID="txtMilesToWork" runat="server" TabIndex="2090"></asp:TextBox></td>
   </tr>
      <tr>
       <td>Current Value</td>
       <td> <asp:TextBox ID="txtValue" runat="server" TabIndex="2050"></asp:TextBox></td>
   </tr>
</table> 

            <p style="text-align: right">
                <asp:Button ID="btnCancel" runat="server" 
                    Text="Cancel" onclick="btnCancel_Click" TabIndex="2160"
                />&nbsp;<asp:Button ID="btnSaveVehicle" runat="server" Text="Save vehicle" 
                 
                ValidationGroup="VehicleInforValidationGroup"
                CausesValidation="true" 
                    OnClientClick="return validation('VehicleInforValidationGroup')" 
                    onclick="btnSaveVehicle_Click" TabIndex="2170"/></p>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtVin"
            ErrorMessage="Please enter the VIN number" Display="None"
            ValidationGroup="VehicleInforValidationGroup"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
            TargetControlID="RequiredFieldValidator1" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
            ValidationGroup="VehicleInforValidationGroup"
            ControlToValidate="txtVin"
            ErrorMessage ="VIN should contain 17 characters,and no symbol"
            ValidationExpression="\w{17}$"></asp:RegularExpressionValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
            TargetControlID="RegularExpressionValidator5"
            HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="ddlPrimaryDriver"
            ErrorMessage="Please choose a primary driver" Display="None"
            ValidationGroup="VehicleInforValidationGroup"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
            TargetControlID="RequiredFieldValidator2" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>   
            
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtMake"
            ErrorMessage="Please enter the make of the vehicle" Display="None"
            ValidationGroup="VehicleInforValidationGroup"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
            TargetControlID="RequiredFieldValidator3" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>                 

        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ControlToValidate="txtAnnualMileage"
            ErrorMessage="Please enter the annual mileage of the vehicle" Display="None"
            ValidationGroup="VehicleInforValidationGroup"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
            TargetControlID="RequiredFieldValidator4" HighlightCssClass="highlight"
            PopupPosition="Right" ></ajaxToolkit:ValidatorCalloutExtender>
        <asp:RangeValidator ID="RangeValidator3" runat="server"
            ControlToValidate="txtAnnualMileage"
            ErrorMessage="The value must be from 0 to 1,000,000"
            MinimumValue="1" MaximumValue="1000000" Type="Integer"
            ValidationGroup="VehicleInforValidationGroup"></asp:RangeValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
            TargetControlID="RangeValidator3" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>  

            
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
            ControlToValidate="txtModel"
            ErrorMessage="Please enter the model of the vehicle" Display="None"
            ValidationGroup="VehicleInforValidationGroup"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
            TargetControlID="RequiredFieldValidator5" HighlightCssClass="highlight"
            PopupPosition="Right" ></ajaxToolkit:ValidatorCalloutExtender>   
            
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
            ControlToValidate="txtDaysDrivenperwk"
            ErrorMessage="Please enter the number of days the vehicle driven per week" Display="None"
            ValidationGroup="VehicleInforValidationGroup"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
            TargetControlID="RequiredFieldValidator6" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender> 
        <asp:RangeValidator ID="RangeValidator1" runat="server"
            ControlToValidate="txtDaysDrivenperwk"
            ErrorMessage="The value must be from 1 to 7"
            MinimumValue="1" MaximumValue="7" Type="Integer"
            ValidationGroup="VehicleInforValidationGroup"></asp:RangeValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
            TargetControlID="RangeValidator1" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>    

        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
            ControlToValidate="txtYear"
            ErrorMessage="Please enter the year of the vehicle" Display="None"
            ValidationGroup="VehicleInforValidationGroup"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
            TargetControlID="RequiredFieldValidator7" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender> 
        <asp:RangeValidator ID="RangeValidator4" runat="server"
            ControlToValidate="txtYear"
            ErrorMessage="The value must be from 1900 to 2013"
            MinimumValue="1900" MaximumValue="2010" Type="Integer"
            ValidationGroup="VehicleInforValidationGroup"></asp:RangeValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
            TargetControlID="RangeValidator4" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender> 
            
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
            ControlToValidate="txtMilesToWork"
            ErrorMessage="Please enter the miles the vehicle drive to work" Display="None"
            ValidationGroup="VehicleInforValidationGroup"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
            TargetControlID="RequiredFieldValidator8" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>  
        <asp:RangeValidator ID="RangeValidator2" runat="server"
            ControlToValidate="txtMilesToWork"
            ErrorMessage="The value must be from 0 to 1000"
            MinimumValue="0" MaximumValue="1000"
            Type="Integer"
            ValidationGroup="VehicleInforValidationGroup"></asp:RangeValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
            TargetControlID="RangeValidator2" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
            
        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
            ControlToValidate="txtValue"
            ErrorMessage="Please enter the value of the vehicle" Display="None"
            ValidationGroup="VehicleInforValidationGroup"></asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
            TargetControlID="RequiredFieldValidator9" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>  
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
            ValidationGroup="CustomerInforValidationGroup"
            ControlToValidate="txtValue" 
            ErrorMessage="Value must be in integers"
            ValidationExpression="^\d+(\.\d\d)?$"></asp:RegularExpressionValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
            TargetControlID="RegularExpressionValidator3" HighlightCssClass="highlight"
            PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>               
            
