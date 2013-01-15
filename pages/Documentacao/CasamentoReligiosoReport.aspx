<%@ Page Language="VB" MasterPageFile="~/DOCUMENTOS.master" AutoEventWireup="false" CodeFile="CasamentoReligiosoReport.aspx.vb" Inherits="pages_Documentacao_CasamentoReligiosoReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:Panel ID="Panel1" runat="server" Width="598px" style="display:table;">
        
        <asp:Panel ID="Panel11" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label18" runat="server" Text="Nome completo de Solteiro, data e locais de nascimento, nacionalidade e filiação dos cônjuges." 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblConjuges" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>





        <asp:Panel ID="Panel2" runat="server" style="float:left;" BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="432px">
            <asp:Label ID="Label6" runat="server" Text="Data de registro de casamento." 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblDataCasa" runat="server" CssClass="texto" Font-Size="10pt" 
                Text="texto"></asp:Label>
            <br />
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







        <asp:Panel ID="Panel12" runat="server" BorderStyle="Solid" style="float:left;" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label20" runat="server" Text="Regime de Bens do Casamento." 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblRegime" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>






        <asp:Panel ID="Panel6" runat="server" BorderStyle="Solid" style="float:left;" Height="100%" 
            BorderWidth="1px" Width="598px">
            <asp:Label ID="Label2" runat="server" Text="Nome de cada um dos cônjuges passou a utilizar." 
                    Font-Size="8pt"></asp:Label><br />
            <asp:Label ID="lblNovosNomes" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>

    </asp:Panel>
</asp:Content> 

