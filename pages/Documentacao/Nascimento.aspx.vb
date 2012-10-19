Imports Infraestrutura
Imports System.Data
Imports Camadas.Negocio
Imports Camadas.Dominio.Documentos
Imports Camadas.Dominio.Administrativo

Partial Class pages_Documentacao_Nascimento
    Inherits System.Web.UI.Page

    Private controller As IClienteController = New ClienteController
    Private controllerDocumento As IDocumentoController = New DocumentoController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idCliente As Integer
        Dim idPedido As Integer

        If Not IsPostBack Then
            Me.txtServentia.Attributes.Add("onblur", "return CompletarZeros(this,6);")
            Me.txtAcervo.Attributes.Add("onblur", "return CompletarZeros(this,2);")
            Me.txtAtribuicao.Attributes.Add("onblur", "return CompletarZeros(this,2);")
            Me.txtAnoReg.Attributes.Add("onblur", "return CompletarZeros(this,4);")
            Me.txtTipoLivro.Attributes.Add("onblur", "return CompletarZeros(this,1);")
            Me.txtNumeroLivro.Attributes.Add("onblur", "return CompletarZeros(this,5);")
            Me.txtNumeroFolha.Attributes.Add("onblur", "return CompletarZeros(this,3);")
            Me.txtNumeroTermo.Attributes.Add("onblur", "return CompletarZeros(this,7);")

            Me.txtDataRegistro.Text = Format(DateTime.Now(), "dd/MM/yyyy")

            Try
                idCliente = Integer.Parse(Request.QueryString("cliente"))
                If idCliente > 0 Then
                    ViewState("idCliente") = idCliente
                    Me.listarDadosCliente(idCliente)
                Else
                    ViewState("idCliente") = 0
                End If

            Catch ex As Exception
                ViewState("idCliente") = 0
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('ERRO NO ID. " & ex.Message.Replace("'", "") & "'); history.back()", True)
            End Try
        End If
    End Sub

    Private Sub listarDadosCliente(ByVal id As Integer)
        Dim c As Camadas.Dominio.Administrativo.Cliente
        Dim dtb As DataTable

        Try
            c = New Camadas.Dominio.Administrativo.Cliente
            c.Codigo = id

            dtb = controller.listarCliente(c)

            lblNome.Text = dtb.Rows(0).Item("CT01NOME").ToString

            lblSexo.Text = IIf(dtb.Rows(0).Item("CT01SEXO").ToString = "F", "Feminino", "Masculino")
            lblNascimdoEM.Text = dtb.Rows(0).Item("NOME_CIDADE_NATURAL").ToString & " - " & dtb.Rows(0).Item("SIGLA_UF_NATURAL").ToString
            lblDataNascimento.Text = Format(DateTime.Parse(dtb.Rows(0).Item("CT01DATANASCIMENTO").ToString), "dd/MM/yyyy")
            lblPai.Text = dtb.Rows(0).Item("CT01PAI").ToString
            lblMae.Text = dtb.Rows(0).Item("CT01MAE").ToString
            lblAvoPaterno1.Text = dtb.Rows(0).Item("CT01AVOPATERNO1").ToString
            lblAvoPaterno2.Text = dtb.Rows(0).Item("CT01AVOPATERNO2").ToString
            lblAvoMaterno1.Text = dtb.Rows(0).Item("CT01AVOMATERNO1").ToString
            lblAvoMaterno2.Text = dtb.Rows(0).Item("CT01AVOMATERNO2").ToString
            lblGemeo.Text = IIf(dtb.Rows(0).Item("NOME_GEMEO").ToString.Trim = String.Empty, "NÃO", dtb.Rows(0).Item("NOME_GEMEO").ToString)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub btnPesquisarCLiente_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisarCLiente.Click
        Session("emitirDocumento") = Utils.TipoLivro.Nascimento
        Response.Redirect("~/pages/administrativo/ConsultarCliente.aspx")
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session.Remove("gerenciarDocumento")
        Response.Redirect("~/pages/Documentacao/Gerenciar.aspx?cliente=" & ViewState("idCliente"))
    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Dim pedido As Pedido
        Dim nascimento As Nascimento
        Dim gemeo As Cliente

        Try
            pedido = New Pedido
            pedido.Averbacao = txtAverbacao.Text
            pedido.Solicitante.Codigo = ViewState("idCliente")
            pedido.Solicitante.Nome = lblNome.Text
            pedido.Solicitante.Filiacao.NomePai = lblPai.Text
            pedido.Solicitante.Filiacao.NomeMae = lblMae.Text
            pedido.Solicitante.Filiacao.NomeAvoPaterno1 = lblAvoPaterno1.Text
            pedido.Solicitante.Filiacao.NomeAvoPaterno2 = lblAvoPaterno2.Text
            pedido.Solicitante.Filiacao.NomeAvoMaterno1 = lblAvoMaterno1.Text
            pedido.Solicitante.Filiacao.NomeAvoMaterno2 = lblAvoMaterno2.Text
            pedido.Solicitante.DataNascimento = Me.lblDataNascimento.Text
            pedido.Solicitante.Sexo = Me.lblSexo.Text

            gemeo = New Cliente
            gemeo.Nome = Me.lblGemeo.Text
            pedido.Solicitante.Gemeo = gemeo

            pedido.Matricula.Acervo = txtAcervo.Text
            pedido.Matricula.AnoRegistro = txtAnoReg.Text
            pedido.Matricula.Atribuicao = txtAtribuicao.Text
            pedido.Matricula.NumeroFolha = txtNumeroFolha.Text
            pedido.Matricula.NumeroLivro = txtNumeroLivro.Text
            pedido.Matricula.NumeroTermo = txtNumeroTermo.Text
            pedido.Matricula.Serventia = txtServentia.Text
            pedido.Matricula.TipoLivro = txtTipoLivro.Text

            nascimento = New Nascimento
            nascimento.Horario = Me.txtHorario.Text
            nascimento.Declarante = drpDeclarante.SelectedValue
            nascimento.Maternidade = txtLocal.Text.ToUpper
            nascimento.TipoLivro = txtTipoLivro.Text
            nascimento.Cidade = lblNascimdoEM.Text
            pedido.Documento = nascimento

            pedido.Documento.DataRegistro = Me.txtDataRegistro.Text

            Session("pedido") = pedido

            controllerDocumento.solicitarDocumento(pedido)

            'Response.Redirect("~/pages/relatorio/ExibirRelatorio.aspx?r=1")
            'If System.Configuration.ConfigurationManager.AppSettings.Item("AMBIENTE").ToString = "T" Then
            ' ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('" & Me.Page.Request.ApplicationPath & "/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)
            'Else
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)
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
