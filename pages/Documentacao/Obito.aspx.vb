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
            Me.txtDeclarante.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtLocal.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtMedico.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtSepultamento.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")

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
                drpCor.DataSource = controllerDocumento.listarCor
                drpCor.DataValueField = "CT12CODIGO"
                drpCor.DataTextField = "CT12NOME"
                drpCor.DataBind()

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
            Me.txtDataFalecimento.Text = CType(pedido.Documento, Obito).DataObito
            Me.txtLocal.Text = CType(pedido.Documento, Obito).Local
            Me.txtCausaFalecimento.Text = CType(pedido.Documento, Obito).CausaMorte
            Me.txtSepultamento.Text = CType(pedido.Documento, Obito).Sepultamento
            Me.txtMedico.Text = CType(pedido.Documento, Obito).Medico
            Me.drpCor.SelectedValue = CType(pedido.Documento, Obito).Cor

            Me.txtServentia.Text = pedido.Matricula.Serventia
            Me.txtAcervo.Text = pedido.Matricula.Acervo
            Me.txtAtribuicao.Text = pedido.Matricula.Atribuicao
            Me.txtAnoReg.Text = pedido.Matricula.AnoRegistro
            Me.txtTipoLivro.Text = pedido.Matricula.TipoLivro
            Me.txtNumeroLivro.Text = pedido.Matricula.NumeroLivro
            Me.txtNumeroFolha.Text = pedido.Matricula.NumeroFolha
            Me.txtNumeroTermo.Text = pedido.Matricula.NumeroTermo

            Me.txtAverbacao.Text = pedido.Averbacao

            ViewState("ObitoID") = pedido.Documento.Codigo

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Private Sub listarDadosCliente(ByVal idCliente As Integer)
        Dim c As Camadas.Dominio.Administrativo.Cliente
        Dim dtb As DataTable

        Try
            c = New Camadas.Dominio.Administrativo.Cliente
            c.Codigo = idCliente

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
        Try
            Me.salvar()
            Session.Remove("pedido")
            Session.Remove("gerenciarDocumento")
            Response.Redirect("~/pages/Documentacao/Gerenciar.aspx?cliente=" & ViewState("idCliente"))
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Try

            Me.salvar()

            Response.Redirect("~/pages/Documentacao/ObitoReport.aspx")
            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session.Remove("gerenciarDocumento")
        Response.Redirect("~/pages/Documentacao/Gerenciar.aspx?cliente=" & ViewState("idCliente"))
    End Sub

    Private Sub salvar()
        Dim pedido As Pedido
        Dim obito As Obito

        Try
            If Me.txtDataRegistro.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO DATA DE REGISTRO OBRIGATÓRIO.")
            If Me.txtHorario.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO HORARIO OBRIGATÓRIO.")
            If Me.txtLocal.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO LOCAL NASCIMENTO OBRIGATÓRIO.")
            If Me.txtDeclarante.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO DECLARENTE OBRIGATÓRIO.")

            If Me.txtServentia.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO SERVENTIA OBRIGATÓRIO.")
            If Me.txtAcervo.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO ACERVO OBRIGATÓRIO.")
            If Me.txtAtribuicao.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO ATRIBUIÇÃO OBRIGATÓRIO.")
            If Me.txtAnoReg.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO ANO REGISTRO OBRIGATÓRIO.")
            If Me.txtTipoLivro.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO TIPO LIVRO OBRIGATÓRIO.")
            If Me.txtNumeroLivro.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO NÚMERO LIVRO OBRIGATÓRIO.")
            If Me.txtNumeroFolha.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO NÚMERO FOLHA OBRIGATÓRIO.")
            If Me.txtNumeroTermo.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO NÚMERO TERMO OBRIGATÓRIO.")

            If Me.txtDataFalecimento.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO DATA FALECIMENTO É OBRIGATÓRIO.")
            If Me.txtCausaFalecimento.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO CAUSA FALECIMENTO É OBRIGATÓRIO.")
            If Me.txtSepultamento.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO SEPULTAMENTO É OBRIGATÓRIO.")
            If Me.txtMedico.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO MEDICO É OBRIGATÓRIO.")
            If Me.drpCor.SelectedValue = 0 Then Throw New CampoObrigatorioException("CAMPO COR É OBRIGATÓRIO.")

            pedido = New Pedido
            pedido.Codigo = ViewState("idPedido")
            pedido.Averbacao = txtAverbacao.Text
            pedido.Solicitante.Codigo = ViewState("idCliente")
            pedido.Solicitante.Nome = lblNome.Text
            pedido.Solicitante.Filiacao.NomePai = lblPai.Text
            pedido.Solicitante.Filiacao.NomeMae = lblMae.Text
            pedido.Solicitante.DataNascimento = Me.lblDataNascimento.Text
            pedido.Solicitante.Sexo = Me.lblSexo.Text

            pedido.Matricula.Acervo = txtAcervo.Text
            pedido.Matricula.AnoRegistro = txtAnoReg.Text
            pedido.Matricula.Atribuicao = txtAtribuicao.Text
            pedido.Matricula.NumeroFolha = txtNumeroFolha.Text
            pedido.Matricula.NumeroLivro = txtNumeroLivro.Text
            pedido.Matricula.NumeroTermo = txtNumeroTermo.Text
            pedido.Matricula.Serventia = txtServentia.Text
            pedido.Matricula.TipoLivro = txtTipoLivro.Text

            obito = New Obito
            obito.Codigo = IIf(ViewState("ObitoID") Is Nothing, 0, ViewState("ObitoID"))
            obito.Horario = Me.txtHorario.Text
            obito.Declarante = txtDeclarante.Text.ToUpper
            obito.DataRegistro = Format(Date.Parse(txtDataRegistro.Text), "yyyy-MM-dd")
            obito.DataObito = Format(Date.Parse(txtDataFalecimento.Text), "yyyy-MM-dd")
            obito.CausaMorte = txtCausaFalecimento.Text.ToUpper
            obito.Sepultamento = txtSepultamento.Text.ToUpper
            obito.Medico = txtMedico.Text.ToUpper
            obito.TipoLivro = txtTipoLivro.Text
            obito.Local = txtLocal.Text.ToUpper
            obito.Cor = drpCor.SelectedValue
            pedido.Documento = obito

            pedido.Documento.DataRegistro = Me.txtDataRegistro.Text

            Session("pedido") = pedido

            controllerDocumento.solicitarDocumento(pedido)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
