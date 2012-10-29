<%@ Page Language="VB" MasterPageFile="~/CARTORIO.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="pages_principal_Default" %>


<asp:Content ID="Content" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="formulario">
        <div id="principal" class="boxes">
            <h2>Seja Bem Vindo!</h2>
            <asp:Label ID="lblMatricula" runat="server"></asp:Label>
            <br /><br />
            <asp:Button ID="Button1" runat="server" Text="Imprimir" />
            <br /><br />
            <div id="imgPrincipal"></div>
            <br /><br />
        </div> 
    </div>
</asp:Content>