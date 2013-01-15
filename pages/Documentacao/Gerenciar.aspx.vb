Imports Infraestrutura
Imports System.Data
Imports Camadas.Negocio
Imports System.Threading
Imports Camadas.Dominio.Documentos
Imports Infraestrutura.Utils

Partial Class pages_Documentacao_Gerenciar
    Inherits System.Web.UI.Page

    Private controllerCliente As IClienteController = New ClienteController
    Private controllerDocumento As IDocumentoController = New DocumentoController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idCliente As Integer

        If Not IsPostBack Then
            Try
                Session.Remove("gerenciarDocumento")

                drpDoc.DataSource = controllerDocumento.listarTipoDocumento
                drpDoc.DataTextField = "CT02NOME"
                drpDoc.DataValueField = "CT02TIPOLIVRO"
                drpDoc.DataBind()

                If Not Request.QueryString("cliente") Is Nothing Then
                    idCliente = Integer.Parse(Request.QueryString("cliente"))
                Else
                    idCliente = 0
                End If

                If idCliente > 0 Then
                    ViewState("idCliente") = idCliente
                    Me.listarDadosCliente(idCliente)
                    Me.listarDocumentosCliente(idCliente)
                Else
                    ViewState("idCliente") = 0
                    Me.listarDocumentos()
                End If

            Catch ex As Exception
                ViewState("idCliente") = 0
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('ERRO NO ID. " & ex.Message.Replace("'", "") & "');", True)
            End Try

        End If
    End Sub

    Protected Sub btnPesquisarCLiente_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPesquisarCLiente.Click
        'Session("emitirDocumento") = Utils.TipoLivro.Nascimento
        Session("gerenciarDocumento") = True
        Response.Redirect("~/pages/administrativo/ConsultarCliente.aspx")
    End Sub

    Private Sub listarDocumentosCliente(ByVal id As Integer)
        Dim c As Camadas.Dominio.Administrativo.Cliente

        Try
            c = New Camadas.Dominio.Administrativo.Cliente
            c.Codigo = id
            gvDocumento.DataSource = controllerDocumento.listarDocumentosByClienteID(c)
            gvDocumento.DataBind()

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try

    End Sub

    Private Sub listarDocumentos()
        'LISTA OS 50 ULTIMOS

        Try

            gvDocumento.DataSource = controllerDocumento.listarDocumentos()
            gvDocumento.DataBind()

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

            dtb = controllerCliente.listarCliente(c)

            lblNome.Text = dtb.Rows(0).Item("CT01NOME").ToString

            lblSexo.Text = IIf(dtb.Rows(0).Item("CT01SEXO").ToString = "F", "Feminino", "Masculino")
            lblNascimdoEM.Text = dtb.Rows(0).Item("NOME_CIDADE_NATURAL").ToString & " - " & dtb.Rows(0).Item("SIGLA_UF_NATURAL").ToString
            lblDataNascimento.Text = Format(DateTime.Parse(dtb.Rows(0).Item("CT01DATANASCIMENTO").ToString), "dd/MM/yyyy")

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub btnNovoDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNovoDoc.Click

        Try
            If Not ViewState("idCliente") Is Nothing And ViewState("idCliente") = 0 Then Throw New CampoObrigatorioException("PARA SOLICITAR DOCUMENTO SELECIONE O CLIENTE CLICANDO NA LUPA.")
            If drpDoc.SelectedValue = 0 Then Throw New CampoObrigatorioException("SELECIONE O TIPO DE DOCUMENTO NOVO A SER SOLICITADO.")

            Select Case drpDoc.SelectedValue
                Case TipoLivro.Nascimento
                    Response.Redirect("~/pages/Documentacao/Nascimento.aspx?cliente=" & ViewState("idCliente"))
                Case TipoLivro.Casamento
                    Response.Redirect("~/pages/Documentacao/Casamento.aspx?conjuge1=" & ViewState("idCliente"))
                Case TipoLivro.CasamentoReligioso
                    Response.Redirect("~/pages/Documentacao/CasamentoReligioso.aspx?conjuge1=" & ViewState("idCliente"))
                Case TipoLivro.Obito
                    Response.Redirect("~/pages/Documentacao/Obito.aspx?cliente=" & ViewState("idCliente"))
                Case TipoLivro.Habilitacao
                    Response.Redirect("~/pages/Documentacao/CasamentoHabilitacao.aspx?conjuge1=" & ViewState("idCliente"))
                Case TipoLivro.Proclamas
                    Response.Redirect("~/pages/Documentacao/Proclamas.aspx?conjuge1=" & ViewState("idCliente"))
                Case TipoLivro.CancelamentoNascimento
                    Response.Redirect("~/pages/Documentacao/CancelamentoNascimento.aspx?cliente=" & ViewState("idCliente"))
                Case Else
                    Throw New Exception("DOCUMENTO AINDA NÃO IMPLEMENTADO. ENTRAR EM CONTATO COM O DESENVOLVEDOR DO SISTEMA.")
            End Select
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub gvDocumento_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvDocumento.PageIndexChanging
        Dim idCliente As Integer

        gvDocumento.PageIndex = e.NewPageIndex

        If Not Request.QueryString("cliente") Is Nothing Then
            idCliente = Integer.Parse(Request.QueryString("cliente"))
            Me.listarDocumentosCliente(idCliente)
        Else
            Me.listarDocumentos()
        End If

    End Sub

    Protected Sub gvDocumento_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDocumento.RowCommand
        Dim id = 0, idCliente As Integer = 0
        Dim tipoLivro As Utils.TipoLivro

        Select Case e.CommandName
            Case "Imprimir"
                id = gvDocumento.DataKeys(e.CommandArgument).Values("CT03CODIGO").ToString
                tipoLivro = gvDocumento.DataKeys(e.CommandArgument).Values("CT02TIPOLIVRO").ToString

                Me.imprimirDocumento(id, tipoLivro)

            Case "Editar"
                id = gvDocumento.DataKeys(e.CommandArgument).Values("CT03CODIGO").ToString
                idCliente = gvDocumento.DataKeys(e.CommandArgument).Values("CT01CODIGO").ToString
                tipoLivro = gvDocumento.DataKeys(e.CommandArgument).Values("CT02TIPOLIVRO").ToString

                ViewState("idCliente") = idCliente

                Select Case tipoLivro
                    Case tipoLivro.Nascimento
                        Response.Redirect("~/pages/Documentacao/Nascimento.aspx?cliente=" & idCliente & "&pedido=" & id)
                    Case tipoLivro.Casamento
                        Response.Redirect("~/pages/Documentacao/Casamento.aspx?pedido=" & id)
                    Case tipoLivro.CasamentoReligioso
                        Response.Redirect("~/pages/Documentacao/CasamentoReligioso.aspx?pedido=" & id)
                    Case tipoLivro.Obito
                        Response.Redirect("~/pages/Documentacao/Obito.aspx?cliente=" & idCliente & "&pedido=" & id)
                    Case tipoLivro.Habilitacao
                        Response.Redirect("~/pages/Documentacao/CasamentoHabilitacao.aspx?cliente=" & idCliente & "&pedido=" & id)
                    Case tipoLivro.Proclamas
                        Response.Redirect("~/pages/Documentacao/Proclamas.aspx?cliente=" & idCliente & "&pedido=" & id)
                    Case tipoLivro.CancelamentoNascimento
                        Response.Redirect("~/pages/Documentacao/CancelamentoNascimento.aspx?cliente=" & idCliente & "&pedido=" & id)
                    Case Else
                        Throw New Exception("DOCUMENTO AINDA NÃO IMPLEMENTADO. ENTRAR EM CONTATO COM O DESENVOLVEDOR DO SISTEMA.")
                End Select

            Case Else
                '    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('COMANDO NÃO ENCONTRADO. TENTE NOVAMENTE.');", True)
        End Select

    End Sub

    Private Sub imprimirDocumento(ByVal idPedido As Integer, ByVal tipoLivro As TipoLivro)
        Dim pedido As New Camadas.Dominio.Documentos.Pedido
        Dim url As String = ""

        Try
            pedido.Codigo = idPedido

            Select Case tipoLivro
                Case tipoLivro.Nascimento
                    pedido.Documento = New Nascimento
                    url = "~/pages/Documentacao/NascimentoReport.aspx"
                Case tipoLivro.Casamento
                    pedido.Documento = New Casamento
                    url = "~/pages/Documentacao/CasamentoReport.aspx"
                Case tipoLivro.CasamentoReligioso
                    pedido.Documento = New CasamentoReligioso
                    url = "~/pages/Documentacao/CasamentoReligiosoReport.aspx"
                Case tipoLivro.Obito
                    pedido.Documento = New Obito
                    url = "~/pages/Documentacao/ObitoReport.aspx"
                Case tipoLivro.Proclamas
                    pedido.Documento = New Proclamas
                    url = "~/pages/Documentacao/ProclamasReport.aspx"
                Case tipoLivro.Habilitacao
                    pedido.Documento = New Habilitacao
                    url = "~/pages/Documentacao/CasamentoHabilitacaoReport.aspx"
                Case tipoLivro.CancelamentoNascimento
                    pedido.Documento = New CancelamentoNascimento
                    url = "~/pages/Documentacao/CancelamentoNascimentoReport.aspx"
                Case Else
                    Throw New Exception("DOCUMENTO AINDA NÃO IMPLEMENTADO. ENTRAR EM CONTATO COM O DESENVOLVEDOR DO SISTEMA.")
            End Select

            pedido = controllerDocumento.listarPedido(pedido)
            Session("pedido") = pedido
            Response.Redirect(url)

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try


    End Sub

    Protected Sub drpDoc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpDoc.SelectedIndexChanged
        Select Case drpDoc.SelectedValue
            Case 2

            Case Else

        End Select

    End Sub
End Class
