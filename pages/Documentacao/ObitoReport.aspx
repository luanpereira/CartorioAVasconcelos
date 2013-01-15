<%@ Page Language="VB" MasterPageFile="~/DOCUMENTOS.master" AutoEventWireup="false" CodeFile="ObitoReport.aspx.vb" Inherits="pages_Documentacao_ObitoReport" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:Panel ID="Panel1" runat="server" Width="598px" style="display:table;">
        <asp:Panel ID="Panel2" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="86px">
            <asp:Label ID="Label6" runat="server" Text="Sexo" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblSexo" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>

        <asp:Panel ID="Panel4" runat="server" BorderStyle="Solid" 
            style="margin-left: 5px; float:right;" Height="100%" 
            BorderWidth="1px" Width="412px">
            <asp:Label ID="Label3" runat="server" Text="Estado civil e Idade" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblEstadoCivilIdade" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label>
        </asp:Panel>

        <asp:Panel ID="Panel5" runat="server" BorderStyle="Solid" 
            HorizontalAlign="Center" style="margin-left: 5px; float:right;" Height="100%" 
            BorderWidth="1px" Width="83px">
            <asp:Label ID="Label5" runat="server" Text="Cor" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblCor" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label>
        </asp:Panel>



        <asp:Panel ID="Panel3" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="378px">
            <asp:Label ID="Label1" runat="server" Text="Naturalidade" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblNatural" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>

        <asp:Panel ID="Panel8" runat="server" BorderStyle="Solid" 
            style="margin-left: 5px; float:right;" Height="100%" 
            BorderWidth="1px" Width="210px">
            <asp:Label ID="Label9" runat="server" Text="Documento de Identificação" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblDocId" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label>
        </asp:Panel>


        <asp:Panel ID="Panel11" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label18" runat="server" Text="Filiação" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblFiliacao" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>


        <asp:Panel ID="Panel6" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label4" runat="server" Text="Data e Hora de Falecimento" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblDataHorra" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>


        <asp:Panel ID="Panel7" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label8" runat="server" Text="Local de Falecimento" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblLocal" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>


        <asp:Panel ID="Panel9" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label12" runat="server" Text="Causa da Morte" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblCausaMorte" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>



        <asp:Panel ID="Panel10" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="378px">
            <asp:Label ID="Label14" runat="server" Text="Sepultamento" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblSepultamento" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>

        <asp:Panel ID="Panel12" runat="server" BorderStyle="Solid" 
            style="margin-left: 5px; float:right;" Height="100%" 
            BorderWidth="1px" Width="210px">
            <asp:Label ID="Label16" runat="server" Text="Declarante" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblDeclarante" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label>
        </asp:Panel>


        <asp:Panel ID="Panel13" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label19" runat="server" Text="Médico que atestou o óbito" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblMedico" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>
    </asp:Panel> 
</asp:Content>
