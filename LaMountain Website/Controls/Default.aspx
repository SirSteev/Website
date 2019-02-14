<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/AdminMasterForm.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Controls_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHFormContent" Runat="Server">
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
    <asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" style="width:10%; margin-left:45%; margin-right:45%;"/>
    <br /><br />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHFooter" Runat="Server">
</asp:Content>

