<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/AdminMasterForm.master" AutoEventWireup="true" CodeFile="PersonSearch.aspx.cs" Inherits="Controls_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHFormContent" Runat="Server">
    <div style="width:18%; margin-left:41%; margin-right:41%;">
        <br />
        <asp:label id="LbNameFirst" runat="server" text="First Name"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:TextBox id="TbNameFirst" runat="server" style="float:right;"></asp:TextBox>
        <br /><br />
        <asp:label id="LbNameLast" runat="server" text="Last Name"></asp:label>&nbsp;&nbsp;&nbsp;
        <asp:TextBox id="TbNameLast" runat="server" style="float:right;"></asp:TextBox>
        <br /><br />
        <asp:Button ID="BtnSearchRecord" runat="server" Text="Search Record" OnClick="BtnSearchRecords_Click" ValidationGroup="SearchRecord" style="width:50%; margin-left:25%; margin-right:25%;"/>
        <br /><br />
        <asp:Button ID="BtnAdd" runat="server" Text="Add Record" OnClick="BtnAdd_Click" style="width:50%; margin-left:25%; margin-right:25%;"/>
        <br /><br />
        <asp:GridView ID="GvPersons" runat="server" AutoGenerateColumns="false" style="width:100%;">
            <Columns>
                <asp:BoundField DataField="Account_ID" HeaderText="Account ID" />
                <asp:BoundField DataField="NameFirst" HeaderText="First Name" />
                <asp:BoundField DataField="NameLast" HeaderText="Last Name" />
                <asp:HyperLinkField Text="Edit" DataNavigateUrlFormatString="ContactMgr.aspx?Acc_ID={0}" DataNavigateUrlFields="Account_ID" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:RequiredFieldValidator ID="RFVSearchNameFirst" Display="Dynamic" runat="server" ControlToValidate="TbNameFirst" ErrorMessage="First Name: Field cannot be empty.<br/>" ValidationGroup="SearchRecord"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVSearchNameLast" Display="Dynamic" runat="server" ControlToValidate="TbNameLast" ErrorMessage="Last Name: Field cannot be empty.<br/>" ValidationGroup="SearchRecord"></asp:RequiredFieldValidator>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" Runat="Server">
</asp:Content>

