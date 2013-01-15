Imports Infraestrutura
Imports System.Data
Imports Camadas.Negocio
Imports Camadas.Dominio.Documentos
Imports Camadas.Dominio.Administrativo

Partial Class pages_Documentacao_Proclamas
    Inherits System.Web.UI.Page

    Private controller As IClienteController = New ClienteController
    Private controllerDocumento As IDocumentoController = New DocumentoController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idPedido As Integer
        Dim casal As Casal

        If Not IsPostBack Then
            Session.Remove("proclamas")

            Me.txtNumero.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1');")
            Me.txtAno.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1');")

            ' Me.txtDataRegistro.Text = Format(DateTime.Now(), "dd/MM/yyyy")
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
                Else
                    idPedido = Integer.Parse(Request.QueryString("pedido"))
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
                    'Me.listarEditais()
                    txtAno.Text = Now.Year
                End If
            Catch ex As Exception
                ViewState("casal") = Nothing
                ViewState("idPedido") = 0
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('ERRO NO ID. " & ex.Message.Replace("'", "") & "'); history.back()", True)
            End Try
        End If

    End Sub

    Private Sub listarEditais()
        Dim edital As Camadas.Dominio.Documentos.Edital

        Try

            'edital = controllerDocumento.listarUltimoEdital()
            'Me.lblUltimoEdital.Text = edital.ToString

            edital = controllerDocumento.gerarNumeroEditalProclamas(Now.Year)
            Me.txtAno.Text = edital.Ano
            Me.txtNumero.Text = edital.Numero

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Private Sub listarDadosPedido(ByVal id As Integer)
        Dim pedido As Camadas.Dominio.Documentos.Pedido

        Try
            pedido = New Pedido
            pedido.Codigo = ID
            pedido.Documento = New Proclamas
            pedido = controllerDocumento.listarPedido(pedido)

            Me.listarDadosCliente(CType(pedido.Documento, Proclamas).Casal)

            Me.txtNumero.Text = CType(pedido.Documento, Proclamas).Edital.Numero
            Me.txtAno.Text = CType(pedido.Documento, Proclamas).Edital.Ano

            Me.txtEmissao.Text = pedido.DataEmissao

            ViewState("ProclamasID") = pedido.Documento.Codigo

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

            If casal.Conjuge2.Codigo > 0 Then
                dtb = Nothing
                dtb = controller.listarCliente(casal.Conjuge2)
                lblNome2.Text = dtb.Rows(0).Item("CT01NOME").ToString
                lblSexo2.Text = IIf(dtb.Rows(0).Item("CT01SEXO").ToString = "F", "Feminino", "Masculino")
                lblNascimdoEM2.Text = dtb.Rows(0).Item("NOME_CIDADE_NATURAL").ToString & " - " & dtb.Rows(0).Item("SIGLA_UF_NATURAL").ToString
                lblDataNascimento2.Text = Format(DateTime.Parse(dtb.Rows(0).Item("CT01DATANASCIMENTO").ToString), "dd/MM/yyyy")
            End If

            ViewState("casal") = casal

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Private Sub salvar()
        Dim pedido As Pedido
        Dim casamento As Proclamas
        'Dim gemeo As Cliente

        Try
            If CType(ViewState("casal"), Casal).Conjuge2.Codigo = 0 Then Throw New CampoObrigatorioException("VOCÊ DEVE SELECIONAR O CÔNJUGE.")
            If Me.txtNumero.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO NÚMERO DE EDITAL OBRIGATÓRIO.")
            If Me.txtAno.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO ANO DE EDITAL OBRIGATÓRIO.")
            If Me.txtEmissao.Text.Trim = String.Empty Then Throw New CampoObrigatorioException("CAMPO DATA DE EMISSÃO OBRIGATÓRIA.")

            pedido = New Pedido
            pedido.Codigo = ViewState("idPedido")
            pedido.Solicitante.Codigo = CType(ViewState("casal"), Casal).Conjuge1.Codigo

            pedido.DataEmissao = txtEmissao.Text

            casamento = New Proclamas
            casamento.Codigo = IIf(ViewState("ProclamasID") Is Nothing, 0, ViewState("ProclamasID"))
            casamento.Edital.Ano = txtAno.Text
            casamento.Edital.Numero = txtNumero.Text

            casamento.Casal = ViewState("casal")

            pedido.Documento = casamento

            pedido.Documento.DataRegistro = pedido.DataEmissao
            pedido.Documento.TipoLivro = Utils.TipoLivro.Proclamas

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
            Session.Remove("proclamas")
            Response.Redirect("~/pages/Documentacao/Gerenciar.aspx?cliente=" & CType(ViewState("casal"), Casal).Conjuge1.Codigo)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Session.Remove("proclamas")
        Response.Redirect("~/pages/Documentacao/Gerenciar.aspx?cliente=" & CType(ViewState("casal"), Casal).Conjuge1.Codigo)
    End Sub

    Protected Sub imgPesqConjuge_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPesqConjuge.Click
        Session("proclamas") = ViewState("casal")
        Response.Redirect("~/pages/administrativo/ConsultarCliente.aspx")
    End Sub
End Class
