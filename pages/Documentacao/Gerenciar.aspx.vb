Imports Infraestrutura
Imports System.Data
Imports Camadas.Negocio

Partial Class pages_Documentacao_Gerenciar
    Inherits System.Web.UI.Page

    Private controllerCliente As IClienteController = New ClienteController
    Private controllerDocumento As IDocumentoController = New DocumentoController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idCliente As Integer

        Try
            DropDownList1.DataSource = controllerDocumento.listarTipoDocumento
            DropDownList1.DataTextField = "CT02NOME"
            DropDownList1.DataValueField = "CT02DOCUMENTO"
            DropDownList1.DataBind()

            If Not Request.QueryString("cliente") Is Nothing Then
                idCliente = Integer.Parse(Request.QueryString("cliente"))
            Else
                idCliente = 0
            End If

            If idCliente > 0 Then
                ViewState("idCliente") = idCliente
                Me.listarDadosCliente(idCliente)
                Me.listarDocumentosCliente(idCliente)
                Session.Remove("gerenciarDocumento")
            Else
                ViewState("idCliente") = 0
            End If

        Catch ex As Exception
            ViewState("idCliente") = 0
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('ERRO NO ID. " & ex.Message.Replace("'", "") & "');", True)
        End Try
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
            'If DropDownList1.SelectedValue = 0 Then Throw New CampoObrigatorioException("SELECIONE O TIPO DE DOCUMENTO NOVO A SER SOLICITADO.")
            Response.Redirect("~/pages/Documentacao/Nascimento.aspx?cliente=" & ViewState("idCliente"))
            'Select Case DropDownList1.SelectedValue
            '    Case 1
            '        Response.Redirect("~/pages/Documentacao/Nascimento.aspx?cliente=" & ViewState("idCliente"))
            '    Case 2
            '    Case 3
            '    Case 4
            '    Case 5
            '    Case Else
            '        Throw New Exception("DOCUMENTO AINDA NÃO IMPLEMENTADO. ENTRAR EM CONTATO COM O DESENVOLVEDOR DO SISTEMA.")
            'End Select
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub gvDocumento_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDocumento.RowCommand
        Dim id As Integer = 0

        id = gvDocumento.DataKeys.Item(e.CommandArgument).Value

        If e.CommandName = "Imprimir" Then
            Me.imprimirDocumento(id)
        End If

    End Sub

    Protected Sub gvDocumento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDocumento.SelectedIndexChanged

    End Sub

    Private Sub imprimirDocumento(ByVal idPedido As Integer)
        Dim pedido As Camadas.Dominio.Documentos.Pedido

        Try
            pedido = controllerDocumento.listarPedido(idPedido)
            Session("pedido") = pedido

            'ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('" & Me.Page.Request.ApplicationPath & "/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "", "CriarJanela('/pages/relatorio/ExibirRelatorio.aspx?r=1', '800', '800')", True)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub
End Class
