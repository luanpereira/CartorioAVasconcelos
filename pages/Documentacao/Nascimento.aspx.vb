Imports Infraestrutura
Imports System.Data
Imports Camadas.Negocio
Imports Camadas.Dominio.Documentos

Partial Class pages_Documentacao_Nascimento
    Inherits System.Web.UI.Page

    Private controller As IClienteController = New ClienteController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idCliente As Integer
        Dim idPedido As Integer

        If Not IsPostBack Then

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
        Session.Remove("emitirDocumento")
        Response.Redirect("~/pages/principal/")
    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Dim pedido As Pedido
        Dim nascimento As Nascimento

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

            pedido.Matricula.Acervo = txtAcervo.Text
            pedido.Matricula.AnoRegistro = txtAnoReg.Text
            pedido.Matricula.Atribuicao = txtAtribuicao.Text
            pedido.Matricula.NumeroFolha = txtNumeroFolha.Text
            pedido.Matricula.NumeroLivro = txtNumeroLivro.Text
            pedido.Matricula.NumeroTermo = txtNumeroTermo.Text
            pedido.Matricula.Serventia = txtServentia.Text
            pedido.Matricula.TipoLivro = txtTipoLivro.Text

            nascimento = New Nascimento
            nascimento.Declarante = drpDeclarante.SelectedValue
            nascimento.Maternidade = txtLocal.Text
            nascimento.TipoLivro = txtTipoLivro.Text
            pedido.Documento = nascimento

            Session("pedido") = pedido
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('" & Me.Page.Request.ApplicationPath & "/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub txtNumeroLivro_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNumeroLivro.TextChanged
        Me.completarZeros(Me.txtNumeroLivro)
    End Sub

    Private Sub completarZeros(ByRef txt As TextBox)
        Dim zeros As String = ""

        If txt.Text.Length < txt.MaxLength Then
            For i As Int16 = 1 To (txt.MaxLength - txt.Text.Length)
                zeros += "0"
            Next
            txt.Text = zeros + txt.Text
        End If
    End Sub
End Class
