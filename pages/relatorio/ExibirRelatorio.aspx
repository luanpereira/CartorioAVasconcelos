<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExibirRelatorio.aspx.vb" Inherits="pages_relatorio_ExibirRelatorio" %>

<%@ Register Assembly="ActiveReports.Web, Version=6.2.4238.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="DataDynamics.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>CARTÓRIO A VASCONCELOS - VISUALIZAÇÃO DE RELATÓRIOS</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <!--<ActiveReportsWeb:WebViewer ID="WebViewer1" runat="server" Height="46" Width="345">
        </ActiveReportsWeb:WebViewer>-->

        <activereportsweb:webviewer id="WebViewer" runat="server" height="650px" width="750px" ViewerType="AcrobatReader" BorderStyle="Solid" BorderWidth="2px" SlidingExpirationInterval="00:00:00">
            <PdfExportOptions Author="Cl&#237;nicas - UniCEUMA" DisplayTitle="True" Title="Relat&#243;rios Cl&#237;nicas - UniCEUMA" />
<FlashViewerOptions MultiPageViewColumns="1" MultiPageViewRows="1"></FlashViewerOptions>

<PdfExportOptions Author="Cartório A Vasconcelos" DisplayTitle="True" 
                Title="Relatórios Cartório A Vasconcelos"></PdfExportOptions>
        </activereportsweb:webviewer>
    
    </div>
    </form>
</body>
</html>
