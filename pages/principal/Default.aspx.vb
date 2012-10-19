Imports Camadas.Dominio.Documentos
Imports Infraestrutura.Utils
Imports Camadas.Dominio.Administrativo

Partial Class pages_principal_Default
    Inherits System.Web.UI.Page

    Private seguranca As Seguranca
    Private matricula As Matricula

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'seguranca = New Seguranca

        'Try
        '    seguranca.ValidarAcesso(1)
        'Catch ex As Exception
        '    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        'End Try


        If Not IsPostBack Then
            'matricula = New Matricula("031385", "01", "55", 1992, "00030", "116", "0008396", TipoLivro.Casamento)
            'lblMatricula.Text = matricula.getMatricula
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim pedido As Pedido
        Dim nascimento As Nascimento
        Dim gemeo As Cliente

        Try
            pedido = New Pedido
            pedido.Averbacao = "Nenhum"
            pedido.Solicitante.Codigo = 123
            pedido.Solicitante.Nome = "JOAO DA SILVA"
            pedido.Solicitante.Filiacao.NomePai = "ANOTNIO SANTOS"
            pedido.Solicitante.Filiacao.NomeMae = "MAROLI ARAUJO"
            pedido.Solicitante.Filiacao.NomeAvoPaterno1 = "ARISTEU NASCIMENTO"
            pedido.Solicitante.Filiacao.NomeAvoPaterno2 = "MARIA ALVES"
            pedido.Solicitante.Filiacao.NomeAvoMaterno1 = "RAIMUNDO PEREIRA"
            pedido.Solicitante.Filiacao.NomeAvoMaterno2 = "JOANA MAMEDE"
            pedido.Solicitante.DataNascimento = "03/05/1988"
            pedido.Solicitante.Sexo = "M"

            gemeo = New Cliente
            gemeo.Nome = "NAO"
            pedido.Solicitante.Gemeo = gemeo

            pedido.Matricula.Acervo = "01"
            pedido.Matricula.AnoRegistro = 1994
            pedido.Matricula.Atribuicao = "55"
            pedido.Matricula.NumeroFolha = "212"
            pedido.Matricula.NumeroLivro = "21212"
            pedido.Matricula.NumeroTermo = "0043424"
            pedido.Matricula.Serventia = "031385"
            pedido.Matricula.TipoLivro = "1"

            nascimento = New Nascimento
            nascimento.Horario = "08:30"
            nascimento.Declarante = "P"
            nascimento.Maternidade = "HOSPITAL SAO RAFAEL"
            nascimento.TipoLivro =
            nascimento.Cidade = "SAO LUIS"
            pedido.Documento = nascimento

            pedido.Documento.DataRegistro = "06/10/2012"

            Session("pedido") = pedido

            Response.Redirect("~/pages/relatorio/ExibirRelatorio.aspx?r=1")
            'If System.Configuration.ConfigurationManager.AppSettings.Item("AMBIENTE").ToString = "T" Then
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('" & Me.Page.Request.ApplicationPath & "/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)
            ' Else
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('http://www.cartorioavasconcelos.com.br/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)
            'End If

            '<httpHandlers>
            '  <add verb="*" path="*.rpx" type="DataDynamics.ActiveReports.Web.Handlers.RpxHandler, ActiveReports.Web, Version=6.2.4238.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff" />
            '  <add verb="*" path="*.ActiveReport" type="DataDynamics.ActiveReports.Web.Handlers.CompiledReportHandler, ActiveReports.Web, Version=6.2.4238.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff" />
            '  <add verb="*" path="*.ArCacheItem" type="DataDynamics.ActiveReports.Web.Handlers.WebCacheAccessHandler, ActiveReports.Web, Version=6.2.4238.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff" />
            '</httpHandlers>

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub
End Class
