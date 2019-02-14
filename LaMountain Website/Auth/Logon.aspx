<%@ Page Title="Account Login" Language="C#" MasterPageFile="~/MasterForm.master" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHFormContent" Runat="Server">
    <br />
    <div style="width:15%; margin-left:auto; margin-right:auto;">
    <asp:Label ID="LbUserEmail" runat="server" Text="Email: "></asp:Label>
        <asp:TextBox ID="TbUserEmail" runat="server" style="float:right;"></asp:TextBox>
        <br /><br />
        <asp:Label ID="LbPassword" runat="server" Text="Password: "></asp:Label>
        <asp:TextBox ID="TbPassword" runat="server" TextMode="Password" style="float:right;"></asp:TextBox>
        <br /><br />
        <div style="width:25%; margin-left:auto; margin-right:auto;">
            <asp:Label ID="LbLoginError" runat="server" Text="" style=""></asp:Label>
        </div>
        <br />
    </div>
    <asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" ValidationGroup="CheckFields" style="width:10%; margin-left:45%; margin-right:45%;"/>
    <br /><br />
    <asp:Button ID="BtnCreateAccount" runat="server" Text="Create Account" OnClick="BtnCreateAccount_Click" style="width:10%; margin-left:45%; margin-right:45%;"/>
    <br /><br />
    <asp:RequiredFieldValidator ID="RFVUserEmail" Display="Dynamic" runat="server" ControlToValidate="TbUserEmail" ErrorMessage="Email: Field cannot be empty.<br/>" ValidationGroup="CheckFields" style="width:10%; margin-left:45%; margin-right:45%;"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFVPassword" Display="Dynamic" runat="server" ControlToValidate="TbPassword" ErrorMessage="Password: Field cannot be empty.<br/>" ValidationGroup="CheckFields" style="width:10%; margin-left:45%; margin-right:45%;"></asp:RequiredFieldValidator>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHFooter" Runat="Server">
    
</asp:Content>

