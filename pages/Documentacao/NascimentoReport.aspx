<%@ Page Language="VB" MasterPageFile="~/DOCUMENTOS.master" AutoEventWireup="false" CodeFile="NascimentoReport.aspx.vb" Inherits="pages_Documentacao_NascimentoReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:Panel ID="Panel1" runat="server" Width="598px" style="display:table;">
        <asp:Panel ID="Panel2" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="432px">
            <asp:Label ID="Label6" runat="server" Text="Data de nascimento" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblDataNascimento" runat="server" Text="texto" 
                Font-Size="10pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>

        <asp:Panel ID="Panel3" runat="server" BorderStyle="Solid" 
            style="margin-left: 5px; float:right;" HorizontalAlign="Center" Height="100%" 
            BorderWidth="1px" Width="60px">
            <asp:Label ID="Label1" runat="server" Text="Ano" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblAno" runat="server" Text="0000" 
                Font-Size="12pt" CssClass="texto"></asp:Label>
            </asp:Panel>

        <asp:Panel ID="Panel4" runat="server" HorizontalAlign="Center" BorderStyle="Solid" 
            style="margin-left: 5px; float:right;" Height="100%" 
            BorderWidth="1px" Width="42px">
            <asp:Label ID="Label3" runat="server" Text="Mês" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblMes" runat="server" Text="00" 
                Font-Size="12pt" CssClass="texto"></asp:Label>
        </asp:Panel>

        <asp:Panel ID="Panel5" runat="server" BorderStyle="Solid" 
            HorizontalAlign="Center" style="margin-left: 5px; float:right;" Height="100%" 
            BorderWidth="1px" Width="42px">
            <asp:Label ID="Label5" runat="server" Text="Dia" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblDia" runat="server" Text="00" 
                Font-Size="12pt" CssClass="texto"></asp:Label>
        </asp:Panel>




        <asp:Panel ID="Panel7" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="484px">
            <asp:Label ID="Label10" runat="server" Text="Município de Nascimento e Unidade da Federação" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblMunicipioNascimento" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>

        <asp:Panel ID="Panel6" runat="server" style="float:right;" HorizontalAlign="Center" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="101px">
            <asp:Label ID="Label8" runat="server" Text="Hora" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblHora" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>



        <asp:Panel ID="Panel8" runat="server" style="float:left;" 
            BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="169px">
            <asp:Label ID="Label12" runat="server" Text="Município de Registro e UF" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblMunicipioRegistro" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>

        <asp:Panel ID="Panel10" runat="server" style="float:right; margin-left: 5px;" 
            BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="83px" HorizontalAlign="Center">
            <asp:Label ID="Label16" runat="server" Text="Sexo" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblSexo" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>

        <asp:Panel ID="Panel9" runat="server" style="float:right; margin-left: 5px;" 
            BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="330px">
            <asp:Label ID="Label14" runat="server" Text="Local de Nascimento" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblLocal" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>




        <asp:Panel ID="Panel11" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label18" runat="server" Text="Filiação" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblFiliacao" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>



        <asp:Panel ID="Panel12" runat="server" BorderStyle="Solid" style="float:left;" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label20" runat="server" Text="Avós" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblAvos" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>



        <asp:Panel ID="Panel13" runat="server" style="float:left;" 
            HorizontalAlign="Center" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="55px">
            <asp:Label ID="Label22" runat="server" Text="Gêmeo" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblGemeo" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>

        <asp:Panel ID="Panel14" runat="server" style="float:right;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="530px">
            <asp:Label ID="Label24" runat="server" Text="Nome Gêmeo" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblNomeGemeo" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>



        <asp:Panel ID="Panel16" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label28" runat="server" Text="Data do registro" 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblDataRegistro" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>
    </asp:Panel>
</asp:Content> 