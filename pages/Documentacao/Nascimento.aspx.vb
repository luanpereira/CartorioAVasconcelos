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
        'Dim idCliente As Integer
        Dim idPedido, idCliente As Integer

        If Not IsPostBack Then
            Me.txtServentia.Attributes.Add("onblur", "return CompletarZeros(this,6);")
            Me.txtServentia.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")

            Me.txtAcervo.Attributes.Add("onblur", "return CompletarZeros(this,2);")
            Me.txtAcervo.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")

            Me.txtAtribuicao.Attributes.Add("onblur", "return CompletarZeros(this,2);")
            Me.txtAtribuicao.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")

            Me.txtAnoReg.Attributes.Add("onblur", "return CompletarZeros(this,4);")
            Me.txtAnoReg.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")

            Me.txtTipoLivro.Attributes.Add("onblur", "return CompletarZeros(this,1);")

            Me.txtNumeroLivro.Attributes.Add("onblur", "return CompletarZeros(this,5);")
            Me.txtNumeroLivro.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")

            Me.txtNumeroFolha.Attributes.Add("onblur", "return CompletarZeros(this,3);")
            Me.txtNumeroFolha.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")

            Me.txtNumeroTermo.Attributes.Add("onblur", "return CompletarZeros(this,7);")
            Me.txtNumeroTermo.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")

            Me.txtAverbacao.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")

            'Me.txtDataRegistro.Text = Format(DateTime.Now(), "dd/MM/yyyy")
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
            pedido = New Pedido
            pedido.Codigo = id
            pedido.Documento = New Nascimento
            pedido = controllerDocumento.listarPedido(pedido)

            Me.txtDataRegistro.Text = pedido.Documento.DataRegistro
            Me.txtEmissao.Text = pedido.DataEmissao
            Me.txtHorario.Text = CType(pedido.Documento, Nascimento).Horario
            Me.txtLocal.Text = CType(pedido.Documento, Nascimento).Maternidade
            Me.drpDeclarante.SelectedValue = CType(pedido.Documento, Nascimento).Declarante

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

        Try

            Me.salvar()

            Response.Redirect("~/pages/Documentacao/NascimentoReport.aspx")
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Private Sub salvar()
        Dim pedido As Pedido
        Dim nascimento As Nascimento
        Dim gemeo As Cliente

        Try
            If Me.txtDataRegistro.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO DATA DE REGISTRO OBRIGATÓRIO.")
            If Me.txtEmissao.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO DATA DE EMISSÃO OBRIGATÓRIA.")
            If Me.txtHorario.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO HORARIO OBRIGATÓRIO.")
            If Me.txtLocal.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO LOCAL NASCIMENTO OBRIGATÓRIO.")
            If Me.drpDeclarante.SelectedValue = "0" Then Throw New CampoObrigatorioException("CAMPO DECLARENTE OBRIGATÓRIO.")

            If Me.txtServentia.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO SERVENTIA OBRIGATÓRIO.")
            If Me.txtAcervo.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO HORARIO OBRIGATÓRIO.")
            If Me.txtAtribuicao.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO HORARIO OBRIGATÓRIO.")
            If Me.txtAnoReg.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO HORARIO OBRIGATÓRIO.")
            If Me.txtTipoLivro.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO HORARIO OBRIGATÓRIO.")
            If Me.txtNumeroLivro.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO NÚMERO LIVRO OBRIGATÓRIO.")
            If Me.txtNumeroFolha.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO NÚMERO FOLHA OBRIGATÓRIO.")
            If Me.txtNumeroTermo.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO NÚMERO TERMO OBRIGATÓRIO.")

            pedido = New Pedido
            pedido.Codigo = ViewState("idPedido")
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
            nascimento.Codigo = IIf(ViewState("NascimentoID") Is Nothing, 0, ViewState("NascimentoID"))
            nascimento.Horario = Me.txtHorario.Text
            nascimento.Declarante = drpDeclarante.SelectedValue
            nascimento.Maternidade = txtLocal.Text.ToUpper
            nascimento.TipoLivro = txtTipoLivro.Text
            nascimento.Cidade = lblNascimdoEM.Text
            pedido.Documento = nascimento

            pedido.Documento.DataRegistro = Me.txtDataRegistro.Text
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
End Class
