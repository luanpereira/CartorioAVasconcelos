Imports System.Data
Imports Camadas.Negocio
Imports Camadas.Dominio.Documentos

Partial Class pages_Documentacao_Obito
    Inherits System.Web.UI.Page

    Private controller As IClienteController = New ClienteController
    Private controllerDocumento As IDocumentoController = New DocumentoController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idPedido, idCliente As Integer

        If Not IsPostBack Then
            Me.txtAverbacao.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtCausaFalecimento.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtCor.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtDeclarante.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtLocal.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtMedico.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtSepultamento.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")

            Me.txtDataRegistro.Text = Format(DateTime.Now(), "dd/MM/yyyy")

            Try
                If Request.QueryString("cliente") Is Nothing Then
                    idCliente = 0
                Else
                    idCliente = Integer.Parse(Request.QueryString("cliente"))
                End If

                If Request.QueryString("pedido") Is Nothing Then
                    idPedido = 0
                Else
                    idPedido = Integer.Parse(Request.QueryString("pedido"))
                End If

                If idCliente > 0 Then
                    ViewState("idCliente") = idCliente
                    Me.listarDadosCliente(idCliente)
                Else
                    ViewState("idCliente") = 0
                End If

                If idPedido > 0 Then
                    ViewState("idPedido") = idPedido
                    Me.listarDadosPedido(idPedido)
                Else
                    ViewState("idPedido") = 0
                End If
            Catch ex As Exception
                ViewState("idCliente") = 0
                ViewState("idPedido") = 0
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('ERRO NO ID. " & ex.Message.Replace("'", "") & "'); history.back()", True)
            End Try
        End If
    End Sub

    Private Sub listarDadosPedido(ByVal id As Integer)
        Dim pedido As Camadas.Dominio.Documentos.Pedido

        Try
            pedido = New Camadas.Dominio.Documentos.Pedido
            pedido.Codigo = id
            pedido = controllerDocumento.listarPedido(pedido)

            Me.txtDataRegistro.Text = pedido.Documento.DataRegistro
            Me.txtHorario.Text = CType(pedido.Documento, Obito).Horario
            Me.txtLocal.Text = CType(pedido.Documento, Obito).Local
            Me.txtDeclarante.Text = CType(pedido.Documento, Obito).Declarante

            Me.txtServentia.Text = pedido.Matricula.Serventia
            Me.txtAcervo.Text = pedido.Matricula.Acervo
            Me.txtAtribuicao.Text = pedido.Matricula.Atribuicao
            Me.txtAnoReg.Text = pedido.Matricula.AnoRegistro
            Me.txtTipoLivro.Text = pedido.Matricula.TipoLivro
            Me.txtNumeroLivro.Text = pedido.Matricula.NumeroLivro
            Me.txtNumeroFolha.Text = pedido.Matricula.NumeroFolha
            Me.txtNumeroTermo.Text = pedido.Matricula.NumeroTermo

            Me.txtAverbacao.Text = pedido.Averbacao

            ViewState("NascimentoID") = pedido.Documento.Codigo

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Private Sub listarDadosCliente(ByVal idCliente As Integer)
        Dim c As Camadas.Dominio.Administrativo.Cliente
        Dim dtb As DataTable

        Try
            c = New Camadas.Dominio.Administrativo.Cliente
            c.Codigo = ID

            dtb = controller.listarCliente(c)

            lblNome.Text = dtb.Rows(0).Item("CT01NOME").ToString

            lblSexo.Text = IIf(dtb.Rows(0).Item("CT01SEXO").ToString = "F", "Feminino", "Masculino")
            lblNascimdoEM.Text = dtb.Rows(0).Item("NOME_CIDADE_NATURAL").ToString & " - " & dtb.Rows(0).Item("SIGLA_UF_NATURAL").ToString
            lblDataNascimento.Text = Format(DateTime.Parse(dtb.Rows(0).Item("CT01DATANASCIMENTO").ToString), "dd/MM/yyyy")
            lblPai.Text = dtb.Rows(0).Item("CT01PAI").ToString
            lblMae.Text = dtb.Rows(0).Item("CT01MAE").ToString

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub btnSalvar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar2.Click

    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session.Remove("gerenciarDocumento")
        Response.Redirect("~/pages/Documentacao/Gerenciar.aspx?cliente=" & ViewState("idCliente"))
    End Sub
End Class
