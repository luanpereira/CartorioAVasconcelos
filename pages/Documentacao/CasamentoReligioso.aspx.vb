Imports Infraestrutura
Imports System.Data
Imports Camadas.Negocio
Imports Camadas.Dominio.Documentos
Imports Camadas.Dominio.Administrativo

Partial Class pages_Documentacao_CasamentoReligioso
    Inherits System.Web.UI.Page
    Private controller As IClienteController = New ClienteController
    Private controllerDocumento As IDocumentoController = New DocumentoController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idPedido As Integer
        Dim casal As Casal

        If Not IsPostBack Then
            Session.Remove("casamentoReligioso")

            Me.txtAverbacao.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtNovoNome1.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtNovoNome2.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")

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
                casal = New Casal

                If Request.QueryString("conjuge1") Is Nothing Then
                    casal.Conjuge1.Codigo = 0
                    casal.Conjuge2.Codigo = 0
                Else
                    casal.Conjuge1.Codigo = Integer.Parse(Request.QueryString("conjuge1"))

                    If Not Request.QueryString("conjuge2") Is Nothing Then
                        casal.Conjuge2.Codigo = Integer.Parse(Request.QueryString("conjuge2"))
                    End If
                End If

                If Request.QueryString("pedido") Is Nothing Then
                    idPedido = 0
                    Me.btnHabilitacao.Visible = False
                Else
                    idPedido = Integer.Parse(Request.QueryString("pedido"))
                    Me.btnHabilitacao.Visible = True
                End If

                If casal.Conjuge1.Codigo > 0 Then
                    ViewState("casal") = casal
                    Me.listarDadosCliente(casal)
                Else
                    ViewState("casal") = Nothing
                End If

                If idPedido > 0 Then
                    ViewState("idPedido") = idPedido
                    Me.listarDadosPedido(idPedido)
                Else
                    ViewState("idPedido") = 0
                End If
            Catch ex As Exception
                ViewState("casal") = Nothing
                ViewState("idPedido") = 0
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('ERRO NO ID. " & ex.Message.Replace("'", "") & "'); history.back()", True)
            End Try
        End If

    End Sub

    Private Sub listarDadosPedido(ByVal id As Integer)
        Dim pedido As Camadas.Dominio.Documentos.Pedido

        Try
            pedido = New Pedido
            pedido.Codigo = ID
            pedido.Documento = New Casamento
            pedido = controllerDocumento.listarPedido(pedido)

            Me.listarDadosCliente(CType(pedido.Documento, CasamentoReligioso).Casal)

            Me.txtDataRegistro.Text = pedido.Documento.DataRegistro
            Me.txtEmissao.Text = pedido.DataEmissao
            Me.txtNovoNome1.Text = CType(pedido.Documento, CasamentoReligioso).NovoNomeConjuge1
            Me.txtNovoNome2.Text = CType(pedido.Documento, CasamentoReligioso).NovoNomeConjuge2
            Me.drpRegime.SelectedValue = CType(pedido.Documento, CasamentoReligioso).Regime

            Me.txtServentia.Text = pedido.Matricula.Serventia
            Me.txtAcervo.Text = pedido.Matricula.Acervo
            Me.txtAtribuicao.Text = pedido.Matricula.Atribuicao
            Me.txtAnoReg.Text = pedido.Matricula.AnoRegistro
            Me.txtTipoLivro.Text = pedido.Matricula.TipoLivro
            Me.txtNumeroLivro.Text = pedido.Matricula.NumeroLivro
            Me.txtNumeroFolha.Text = pedido.Matricula.NumeroFolha
            Me.txtNumeroTermo.Text = pedido.Matricula.NumeroTermo

            Me.txtAverbacao.Text = pedido.Averbacao

            ViewState("CasamentoReligiosoID") = pedido.Documento.Codigo

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Private Sub listarDadosCliente(ByVal casal As Casal)
        Dim dtb As DataTable

        Try
            dtb = controller.listarCliente(casal.Conjuge1)
            lblNome1.Text = dtb.Rows(0).Item("CT01NOME").ToString
            lblSexo1.Text = IIf(dtb.Rows(0).Item("CT01SEXO").ToString = "F", "Feminino", "Masculino")
            lblNascimdoEM1.Text = dtb.Rows(0).Item("NOME_CIDADE_NATURAL").ToString & " - " & dtb.Rows(0).Item("SIGLA_UF_NATURAL").ToString
            lblDataNascimento1.Text = Format(DateTime.Parse(dtb.Rows(0).Item("CT01DATANASCIMENTO").ToString), "dd/MM/yyyy")
            txtNovoNome1.Text = lblNome1.Text

            If casal.Conjuge2.Codigo > 0 Then
                dtb = Nothing
                dtb = controller.listarCliente(casal.Conjuge2)
                lblNome2.Text = dtb.Rows(0).Item("CT01NOME").ToString
                lblSexo2.Text = IIf(dtb.Rows(0).Item("CT01SEXO").ToString = "F", "Feminino", "Masculino")
                lblNascimdoEM2.Text = dtb.Rows(0).Item("NOME_CIDADE_NATURAL").ToString & " - " & dtb.Rows(0).Item("SIGLA_UF_NATURAL").ToString
                lblDataNascimento2.Text = Format(DateTime.Parse(dtb.Rows(0).Item("CT01DATANASCIMENTO").ToString), "dd/MM/yyyy")
                txtNovoNome2.Text = lblNome2.Text
            End If

            ViewState("casal") = casal

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub imgPesqConjuge_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPesqConjuge.Click
        Session("casamentoReligioso") = ViewState("casal")
        Response.Redirect("~/pages/administrativo/ConsultarCliente.aspx")
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session.Remove("casamentoReligioso")
        Response.Redirect("~/pages/Documentacao/Gerenciar.aspx?cliente=" & CType(ViewState("casal"), Casal).Conjuge1.Codigo)
    End Sub

    Private Sub salvar()
        Dim pedido As Pedido
        Dim casamento As CasamentoReligioso
        'Dim gemeo As Cliente

        Try
            If CType(ViewState("casal"), Casal).Conjuge2.Codigo = 0 Then Throw New CampoObrigatorioException("VOCÊ DEVE SELECIONAR O CÔNJUGE.")
            If Me.txtDataRegistro.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO DATA DE REGISTRO OBRIGATÓRIO.")
            If Me.txtEmissao.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO DATA DE EMISSÃO OBRIGATÓRIA.")
            If Me.drpRegime.SelectedValue = "0" Then Throw New CampoObrigatorioException("CAMPO REMIGE OBRIGATÓRIO.")

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
            pedido.Solicitante.Codigo = CType(ViewState("casal"), Casal).Conjuge1.Codigo

            pedido.Matricula.Acervo = txtAcervo.Text
            pedido.Matricula.AnoRegistro = txtAnoReg.Text
            pedido.Matricula.Atribuicao = txtAtribuicao.Text
            pedido.Matricula.NumeroFolha = txtNumeroFolha.Text
            pedido.Matricula.NumeroLivro = txtNumeroLivro.Text
            pedido.Matricula.NumeroTermo = txtNumeroTermo.Text
            pedido.Matricula.Serventia = txtServentia.Text
            pedido.Matricula.TipoLivro = txtTipoLivro.Text
            pedido.DataEmissao = txtEmissao.Text

            casamento = New CasamentoReligioso
            casamento.Codigo = IIf(ViewState("CasamentoReligiosoID") Is Nothing, 0, ViewState("CasamentoReligiosoID"))

            casamento.Casal = ViewState("casal")
            'casamento.Casal.Conjuge1 = controller.listarClassCliente(casamento.Casal.Conjuge1)
            'casamento.Casal.Conjuge2 = controller.listarClassCliente(casamento.Casal.Conjuge2)

            casamento.NovoNomeConjuge1 = txtNovoNome1.Text
            casamento.NovoNomeConjuge2 = txtNovoNome2.Text
            casamento.Regime = drpRegime.SelectedValue
            pedido.Documento = casamento

            pedido.Documento.DataRegistro = Me.txtDataRegistro.Text
            pedido.Documento.TipoLivro = txtTipoLivro.Text

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
            Session.Remove("casamentoReligioso")
            Response.Redirect("~/pages/Documentacao/Gerenciar.aspx?cliente=" & CType(ViewState("casal"), Casal).Conjuge1.Codigo)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Try

            Me.salvar()

            CType(CType(Session("pedido"), Pedido).Documento, CasamentoReligioso).Casal.Conjuge1 = controller.listarClassCliente(CType(CType(Session("pedido"), Pedido).Documento, CasamentoReligioso).Casal.Conjuge1)
            CType(CType(Session("pedido"), Pedido).Documento, CasamentoReligioso).Casal.Conjuge2 = controller.listarClassCliente(CType(CType(Session("pedido"), Pedido).Documento, CasamentoReligioso).Casal.Conjuge2)

            Response.Redirect("~/pages/Documentacao/CasamentoReligiosoReport.aspx")
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub btnHabilitacao_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHabilitacao.Click
        Try
            Me.salvar()
            Response.Redirect("~/pages/Documentacao/CasamentoHabilitacaoReport.aspx")
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub
End Class
