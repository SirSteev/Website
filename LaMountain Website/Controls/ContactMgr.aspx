<%@ Page Title="Admin Contact Manager" Language="C#" MasterPageFile="~/Controls/AdminMasterForm.master" AutoEventWireup="true" CodeFile="ContactMgr.aspx.cs" Inherits="Controls_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" Runat="Server">
    <br /><br />
    Options:
    <asp:Button ID="BtnSearchRecord" runat="server" Text="Search Record" OnClick="BtnSearchRecord_Click" CausesValidation="false"/>
    <asp:Button ID="BtnUpdateRecord" runat="server" Text="Update Record" OnClick="BtnUpdateRecord_Click" ValidationGroup="UpdateRecord"/>
    <asp:Button ID="BtnClearFields" runat="server" Text="Clear Fields" OnClick="BtnClearFields_Click" CausesValidation="false"/>
    <asp:Button ID="BtnAddRecord" runat="server" Text="Add Record" OnClick="BtnSubmit_Click" ValidationGroup="AddRecord"/>
    <asp:Button ID="BtnDeleteRecord" runat="server" Text="Delete Record" OnClick="BtnDeleteRecord_Click" CausesValidation="false"/>
    <br /><br />
    <asp:Label ID="LbHeaderError" runat="server" Text=""></asp:Label>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHFormContent" Runat="Server">

    <script type="text/javascript">
        function isNumberKey(evt)
        {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;    
            return true;
        }
        function isLetterKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 33 || charCode > 64) && (charCode < 91 || charCode > 96) && (charCode < 123 || charCode > 126))
                return true;
            return false;
        }
    </script>

    <!-- Left side -->
    <div id="ColumnLeft">
        <br />
        <asp:label id="LbNameFirst" runat="server" text="First Name"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:TextBox id="TbNameFirst" runat="server" style="float:right"></asp:TextBox>
        <br /><br />
        <asp:label id="LbNameLast" runat="server" text="Last Name"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:TextBox id="TbNameLast" runat="server" style="float:right"></asp:TextBox>
        <br /><br />
        <asp:label id="LbZip" runat="server" text="Zip Code"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:TextBox id="TbZipCode" runat="server" onkeypress="return isNumberKey(event)" style="float:right"></asp:TextBox>
        <br /><br />
        <asp:label id="LbPhoneCell" runat="server" text="Cell Phone"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:TextBox id="TbPhoneCell" runat="server" onkeypress="return isNumberKey(event)" style="float:right"></asp:TextBox>
        <br /><br />
        <asp:label id="LbEmail" runat="server" text="E-Mail"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:TextBox id="TbEmail" runat="server" style="float:right"></asp:TextBox>
        <br /><br />
        <asp:label id="LbPassword" runat="server" text="Password"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:TextBox id="TbPassword" runat="server" TextMode="Password" style="float:right"></asp:TextBox>
        <br /><br />
        <asp:label id="LbConfirmPassword" runat="server" text="Confirm Password"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:TextBox id="TbConfirmPassword" runat="server" TextMode="Password" style="float:right"></asp:TextBox>
        <br /><br />
        <asp:label id="LbAdminLevel" runat="server" text="Admin Level"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:dropdownlist id="DdAdminLevel" runat="server" style="float:right" AutoPostBack="True"></asp:dropdownlist>
        <br /><br />
        
    </div>

    <!-- Right side -->
    <div id="ColumnRight">
        <br />
        <asp:label id="LbState" runat="server" text="State"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:dropdownlist id="DdState" runat="server" style="float:right" AutoPostBack="True" onselectedindexchanged="StateChange"></asp:dropdownlist>
        <br /><br />
        <asp:label id="LbBirthday" runat="server" text="Birthday Year:"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:dropdownlist id="DdYear" runat="server" AutoPostBack="True" onselectedindexchanged="BirthYearChange"></asp:dropdownlist>
        <asp:label id="LbBirthdayMonth" runat="server" text="Month:"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:dropdownlist id="DdMonth" runat="server" AutoPostBack="True" onselectedindexchanged="BirthMonthChange" style="float:right;"></asp:dropdownlist>
        <asp:calendar id="CalBirthday" runat="server"></asp:calendar>
        <br />
    </div>
    <div style="width:20%; margin-left:44%; margin-right:auto;">
    <br />
        <asp:RequiredFieldValidator ID="RFVSearchNameFirst" Display="Dynamic" runat="server" ControlToValidate="TbNameFirst" ErrorMessage="First Name: Field cannot be empty.<br/>" ValidationGroup="SearchRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVSearchNameLast" Display="Dynamic" runat="server" ControlToValidate="TbNameLast" ErrorMessage="Last Name: Field cannot be empty.<br/>" ValidationGroup="SearchRecord"></asp:RequiredFieldValidator>
        
        <asp:RequiredFieldValidator ID="RFVUpdateNameFirst" Display="Dynamic" runat="server" ControlToValidate="TbNameFirst" ErrorMessage="First Name: Field cannot be empty.<br/>" ValidationGroup="UpdateRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVUpdateNameLast" Display="Dynamic" runat="server" ControlToValidate="TbNameLast" ErrorMessage="Last Name: Field cannot be empty.<br/>" ValidationGroup="UpdateRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVUpdateZipcode" Display="Dynamic" runat="server" ControlToValidate="TbZipcode" ErrorMessage="Zipcode: Field cannot be empty.<br/>" ValidationGroup="UpdateRecord"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="REVUpdateZipcode" Display="Dynamic" runat="server" ControlToValidate="TbZipcode" ValidationExpression="\d{5}" ErrorMessage="Zipcode: Must be 5 digits long.<br/>" ValidationGroup="UpdateRecord"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RFVUpdatePhoneCell" Display="Dynamic" runat="server" ControlToValidate="TbPhoneCell" ErrorMessage="Cell Phone: Field cannot be empty.<br/>" ValidationGroup="UpdateRecord"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="REVUpdatePhoneCell" Display="Dynamic" runat="server" ControlToValidate="TbPhoneCell" ValidationExpression="\d{10}" ErrorMessage="Cell Phone Number: Must be 10 digits long.<br/>" ValidationGroup="UpdateRecord"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RFVUpdateEmail" Display="Dynamic" runat="server" ControlToValidate="TbEmail" ErrorMessage="E-Mail: Field cannot be empty.<br/>" ValidationGroup="UpdateRecord"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="REVUpdateEmail" Display="Dynamic" runat="server" ControlToValidate="TbEmail" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="E-Mail: Invalid E-Mail.<br/>" ValidationGroup="UpdateRecord"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RFVUpdateState" Display="Dynamic" runat="server" ControlToValidate="DdState" ErrorMessage="State: Select a state.<br/>" ValidationGroup="UpdateRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVUpdateYear" Display="Dynamic" runat="server" ControlToValidate="DdYear" ErrorMessage="Birthday Year: Select a year.<br/>" ValidationGroup="UpdateRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVUpdateMonth" Display="Dynamic" runat="server" ControlToValidate="DdMonth" ErrorMessage="Birthday Month: Select a month.<br/>" ValidationGroup="UpdateRecord"></asp:RequiredFieldValidator>

        <asp:RequiredFieldValidator ID="RFVAddNameFirst" Display="Dynamic" runat="server" ControlToValidate="TbNameFirst" ErrorMessage="First Name: Field cannot be empty.<br/>" ValidationGroup="AddRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVAddNameLast" Display="Dynamic" runat="server" ControlToValidate="TbNameLast" ErrorMessage="Last Name: Field cannot be empty.<br/>" ValidationGroup="AddRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVAddZipcode" Display="Dynamic" runat="server" ControlToValidate="TbZipcode" ErrorMessage="Zipcode: Field cannot be empty.<br/>" ValidationGroup="AddRecord"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="REVAddZipcode" Display="Dynamic" runat="server" ControlToValidate="TbZipcode" ValidationExpression="\d{5}" ErrorMessage="Zipcode: Must be 5 digits long.<br/>" ValidationGroup="AddRecord"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RFVAddPhoneCell" Display="Dynamic" runat="server" ControlToValidate="TbPhoneCell" ErrorMessage="Cell Phone: Field cannot be empty.<br/>" ValidationGroup="AddRecord"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="REVAddPhoneCell" Display="Dynamic" runat="server" ControlToValidate="TbPhoneCell" ValidationExpression="\d{10}" ErrorMessage="Cell Phone Number: Must be 10 digits long.<br/>" ValidationGroup="AddRecord"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RFVAddEmail" Display="Dynamic" runat="server" ControlToValidate="TbEmail" ErrorMessage="E-Mail: Field cannot be empty.<br/>" ValidationGroup="AddRecord"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="REVAddEmail" Display="Dynamic" runat="server" ControlToValidate="TbEmail" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="E-Mail: Invalid E-Mail.<br/>" ValidationGroup="AddRecord"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RFVAddState" Display="Dynamic" runat="server" ControlToValidate="DdState" ErrorMessage="State: Select a state.<br/>" ValidationGroup="AddRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVAddYear" Display="Dynamic" runat="server" ControlToValidate="DdYear" ErrorMessage="Birthday Year: Select a year.<br/>" ValidationGroup="AddRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVAddMonth" Display="Dynamic" runat="server" ControlToValidate="DdMonth" ErrorMessage="Birthday Month: Select a month.<br/>" ValidationGroup="AddRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVAddPassword" Display="Dynamic" runat="server" ControlToValidate="TbPassword" ErrorMessage="Password: Field cannot be empty.<br/>" ValidationGroup="AddRecord"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="REVAddPassword" Display="Dynamic" runat="server" ControlToValidate="TbPassword" ValidationExpression="\w{8,50}" ErrorMessage="Password: Must be atleast 8 digits long.<br/>" ValidationGroup="AddRecord"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RFVAddConfirmPassword" Display="Dynamic" runat="server" ControlToValidate="TbConfirmPassword" ErrorMessage="Confirm Password: Field cannot be empty.<br/>" ValidationGroup="AddRecord"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CVAddPassword" runat="server" Display="Dynamic" ControlToValidate="TbConfirmPassword" Operator="Equal" Type="String" ControlToCompare="TbPassword" ErrorMessage="Password: Confirm Password does not match.<br/>" ValidationGroup="AddRecord"></asp:CompareValidator>
    </div>

</asp:Content>

