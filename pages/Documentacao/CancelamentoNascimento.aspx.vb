Imports Infraestrutura
Imports System.Data
Imports Camadas.Negocio
Imports Camadas.Dominio.Documentos
Imports Camadas.Dominio.Administrativo

Partial Class pages_Documentacao_CancelamentoNascimento
    Inherits System.Web.UI.Page

    Private controller As IClienteController = New ClienteController
    Private controllerDocumento As IDocumentoController = New DocumentoController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim idCliente As Integer
        Dim idPedido, idCliente As Integer

        If Not IsPostBack Then

            Me.txtNumeroLivro.Attributes.Add("onblur", "return CompletarZeros(this,5);")
            Me.txtNumeroLivro.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1');")

            Me.txtNumeroFolha.Attributes.Add("onblur", "return CompletarZeros(this,3);")
            Me.txtNumeroFolha.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1');")

            Me.txtNumeroTermo.Attributes.Add("onblur", "return CompletarZeros(this,7);")
            Me.txtNumeroTermo.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1');")

            'Me.txtMotivo.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")

            'Me.txtDatado.Text = Format(DateTime.Now(), "dd/MM/yyyy")
            Me.txtEmissao.Text = Format(DateTime.Now(), "dd/MM/yyyy")

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
                    Me.txtMotivo.Text = Me.getMotivo
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

    Private Function getMotivo() As String
        Dim str As String

        'str = ("Em cumprimento ao mandado do Meritíssimo  Juiz de Direito da <b>1ª VARA da Infância e da Juventude da Capital</b>, datado de ")
        'str += "<b>" & Utils.dataPorExtenso(Me.txtDatado.Text) & "</b>, procedo o cancelamento do Registro  de  Nascimento de, "
        'str += "<b>" & Me.lblNome.Text & "</b>, em virtude de sentença prolatada na Ação de  Adoção, nº 0, tornando-o de  nenhum efeito  jurídico, nos  termos da  Lei, nº 6.015/73."

        str = "Em cumprimento ao mandado do Meritíssimo Juiz de Direito da 1ª VARA da Infância e da Juventude da Capital, datado de "
        str += Utils.dataPorExtenso(Me.txtDatado.Text) & ", procedo o cancelamento do Registro  de  Nascimento de, "
        str += Me.lblNome.Text & ", em virtude de sentença prolatada na Ação de  Adoção, nº 0, tornando-o de  nenhum efeito  jurídico, nos  termos da  Lei, nº 6.015/73."

        Return str
    End Function

    Private Sub listarDadosPedido(ByVal id As Integer)
        Dim pedido As Camadas.Dominio.Documentos.Pedido

        Try
            pedido = New Pedido
            pedido.Codigo = id
            pedido.Documento = New CancelamentoNascimento
            pedido = controllerDocumento.listarPedido(pedido)

            Me.txtNumeroLivro.Text = CType(pedido.Documento, CancelamentoNascimento).NumeroLivro
            Me.txtNumeroFolha.Text = CType(pedido.Documento, CancelamentoNascimento).NumeroFolha
            Me.txtNumeroTermo.Text = CType(pedido.Documento, CancelamentoNascimento).NumeroTermo
            Me.txtDatado.Text = CType(pedido.Documento, CancelamentoNascimento).Datado
            Me.txtEmissao.Text = pedido.DataEmissao
            Me.txtMotivo.Text = CType(pedido.Documento, CancelamentoNascimento).Motivo

            ViewState("CancelamentoNascimentoID") = pedido.Documento.Codigo

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
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


        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session.Remove("gerenciarDocumento")
        Response.Redirect("~/pages/Documentacao/Gerenciar.aspx?cliente=" & ViewState("idCliente"))
    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Try

            Me.salvar()

            Response.Redirect("~/pages/Documentacao/CancelamentoNascimentoReport.aspx")
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Private Sub salvar()
        Dim pedido As Pedido
        Dim nascimento As CancelamentoNascimento

        Try
            If Me.txtNumeroLivro.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO NÚMERO LIVRO OBRIGATÓRIO.")
            If Me.txtNumeroFolha.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO NÚMERO FOLHA OBRIGATÓRIO.")
            If Me.txtNumeroTermo.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO NÚMERO TERMO OBRIGATÓRIO.")
            If Me.txtDatado.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO DATADO OBRIGATÓRIO.")
            If Me.txtEmissao.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO DATA DE EMISSÃO OBRIGATÓRIA.")
            If Me.txtMotivo.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO MOTIVO OBRIGATÓRIO.")

            pedido = New Pedido
            pedido.Codigo = ViewState("idPedido")
            pedido.Solicitante.Codigo = ViewState("idCliente")
            pedido.Solicitante.Nome = lblNome.Text
            pedido.Solicitante.DataNascimento = Me.lblDataNascimento.Text
            pedido.Solicitante.Sexo = Me.lblSexo.Text

            nascimento = New CancelamentoNascimento
            nascimento.Codigo = IIf(ViewState("CancelamentoNascimentoID") Is Nothing, 0, ViewState("CancelamentoNascimentoID"))
            nascimento.NumeroFolha = Me.txtNumeroFolha.Text
            nascimento.NumeroLivro = Me.txtNumeroLivro.Text
            nascimento.NumeroTermo = txtNumeroTermo.Text
            nascimento.TipoLivro = Utils.TipoLivro.CancelamentoNascimento
            nascimento.Datado = Me.txtDatado.Text
            nascimento.Motivo = Me.txtMotivo.Text
            pedido.Documento = nascimento

            pedido.Documento.DataRegistro = Me.txtDatado.Text
            pedido.DataEmissao = Me.txtEmissao.Text

            Session("pedido") = pedido

            controllerDocumento.solicitarDocumento(pedido)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnSalvar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar2.Click
        Try
            Me.salvar()
            Session.Remove("pedido")
            Session.Remove("gerenciarDocumento")
            Response.Redirect("~/pages/Documentacao/Gerenciar.aspx?cliente=" & ViewState("idCliente"))
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub lnkData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkData.Click
        Me.txtMotivo.Text = Me.getMotivo
    End Sub
End Class
