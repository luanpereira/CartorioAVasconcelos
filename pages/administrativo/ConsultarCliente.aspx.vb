Imports System.Data
Imports Camadas.Negocio
Imports Camadas.Dominio.Administrativo
Imports Camadas.Dominio.Documentos
Imports Infraestrutura

Partial Class pages_administrativo_ConsultarCliente
    Inherits System.Web.UI.Page

    Private controller As IClienteController = New ClienteController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cliente As Camadas.Dominio.Administrativo.Cliente

        If Not IsPostBack Then
            Me.txtNome.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")

            cliente = New Camadas.Dominio.Administrativo.Cliente
            gvCliente.DataSource = controller.listarCliente(cliente)
            gvCliente.DataBind()

            'EMIÇÃO DE DOCUMENTOS --------------------
            If Not Session("gerenciarDocumento") Is Nothing Then
                gvCliente.Columns(1).Visible = False
                gvCliente.Columns(0).Visible = True
                Session("casamento") = Nothing
            End If
            '-----------------------------------------

            'DOCUMENTO DE CASAMENTO ------------------
            If Not Session("casamento") Is Nothing Or Not Session("casamentoReligioso") Is Nothing Or _
                Not Session("habilitacao") Is Nothing Or Not Session("proclamas") Is Nothing Then
                gvCliente.Columns(1).Visible = False
                gvCliente.Columns(0).Visible = True
                btnAddCliente.Visible = False
                Session("gerenciarDocumento") = Nothing
            End If
            '-----------------------------------------

        End If

    End Sub

    Protected Sub btnPesquisar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPesquisar.Click
        Dim cliente As Camadas.Dominio.Administrativo.Cliente

        Try
            cliente = New Camadas.Dominio.Administrativo.Cliente

            cliente.Nome = txtNome.Text
            cliente.Cpf = txtCPF.Text
            gvCliente.DataSource = controller.listarCliente(cliente)
            gvCliente.DataBind()

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try


        'Dim dtb As DataTable
        'Dim row As DataRow

        'dtb = New DataTable
        'dtb.Columns.Add("CODIGO")
        'dtb.Columns.Add("NOME")
        'dtb.Columns.Add("CPFCNPJ")
        'dtb.Columns.Add("VENDEDOR")
        'dtb.Columns.Add("TELEFONE")

        'row = dtb.NewRow
        'row.Item("CODIGO") = "1"
        'row.Item("NOME") = "SUPERMERCADO CARONE"
        'row.Item("CPFCNPJ") = "87425835007115"
        'row.Item("VENDEDOR") = "EMANUEL"
        'row.Item("TELEFONE") = "9832212345 9888235212"
        'dtb.Rows.Add(row)

        'gvCliente.DataSource = dtb
        'gvCliente.DataBind()
    End Sub

    Protected Sub btnAddCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCliente.Click
        Response.Redirect("~/pages/administrativo/CadastroCliente.aspx")
    End Sub

    Protected Sub gvCliente_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCliente.RowCommand
        Dim id As Integer = 0

        Select Case e.CommandName
            Case "Pesquisar"
                id = gvCliente.DataKeys.Item(e.CommandArgument).Value

                If id > 0 Then Response.Redirect("~/pages/administrativo/CadastroCliente.aspx?id=" & id)

            Case "Select"
                id = gvCliente.DataKeys.Item(e.CommandArgument).Value

                If Not Session("gerenciarDocumento") Is Nothing Then
                    Response.Redirect("~/pages/documentacao/Gerenciar.aspx?cliente=" & id)
                ElseIf Not Session("casamento") Is Nothing Then
                    Response.Redirect("~/pages/documentacao/Casamento.aspx?conjuge1=" & CType(Session("casamento"), Casal).Conjuge1.Codigo & "&conjuge2=" & id)
                ElseIf Not Session("casamentoReligioso") Is Nothing Then
                    Response.Redirect("~/pages/documentacao/CasamentoReligioso.aspx?conjuge1=" & CType(Session("casamentoReligioso"), Casal).Conjuge1.Codigo & "&conjuge2=" & id)
                ElseIf Not Session("habilitacao") Is Nothing Then
                    Response.Redirect("~/pages/documentacao/CasamentoHabilitacao.aspx?conjuge1=" & CType(Session("habilitacao"), Casal).Conjuge1.Codigo & "&conjuge2=" & id)
                ElseIf Not Session("proclamas") Is Nothing Then
                    Response.Redirect("~/pages/documentacao/Proclamas.aspx?conjuge1=" & CType(Session("proclamas"), Casal).Conjuge1.Codigo & "&conjuge2=" & id)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('SESSÃO VAZIA. POR FAVOR INICIE NOVAMENTE.');", True)
                End If
                'Select Case CType(Session("emitirDocumento"), Utils.TipoLivro)
                '    Case Utils.TipoLivro.Nascimento
                '        Session.Remove("emitirDocumento")
                '        Response.Redirect("~/pages/documentacao/Nascimento.aspx?cliente=" & id)

                '    Case Else

                'End Select


            Case Else

        End Select

    End Sub

    Protected Sub gvCliente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCliente.SelectedIndexChanged

    End Sub

    Protected Sub gvCliente_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCliente.PageIndexChanging
        gvCliente.PageIndex = e.NewPageIndex
        btnPesquisar_Click(Nothing, Nothing)
    End Sub
End Class
