<%@ Page Language="VB" MasterPageFile="~/DOCUMENTOS.master" AutoEventWireup="false" CodeFile="CasamentoHabilitacaoReport.aspx.vb" Inherits="pages_Documentacao_CasamentoHabilitacaoReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <asp:Panel ID="Panel1" runat="server" Width="598px" style="display:table;">

        <asp:Panel ID="Panel7" runat="server" style="float:left;padding:10px; line-height:23px; " BorderStyle="Solid" Height="100%" 
            BorderWidth="1px" Width="590px" HorizontalAlign="Justify">
            <asp:Label ID="lblDocumento" runat="server" Text="texto" 
                Font-Size="12pt" CssClass="texto"></asp:Label><br />
        </asp:Panel>

    </asp:Panel>
</asp:Content>
