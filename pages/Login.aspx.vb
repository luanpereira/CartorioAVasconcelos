Imports Infraestrutura

Partial Class pages_Login
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("usuario") Is Nothing Then Response.Redirect("~/pages/principal")

        If Not IsPostBack Then
            'Me.txtUsuario.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")
        End If
    End Sub

    Protected Sub btnEntrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEntrar.Click
        Dim u As Usuario

        Select Case Utils.AMBIENTE
            Case "P"
                If Me.txtUsuario.Text.ToLower = "cartorio" And Me.txtSenha.Text.ToLower = "cartorio@187" Then
                    u = New Usuario

                    u.Nome = "Joselina - Cartório"
                    u.Usuario = "cartorio"
                    u.Codigo = 2

                    Session("usuario") = u
                    Session("empresa") = 2

                    Response.Redirect("~/pages/principal")
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('USUÁRIO OU SENHA INCORRETOS!');", True)
                End If

            Case "T"
                u = New Usuario

                u.Nome = "Luan Pereira - TESTE"
                u.Usuario = "luan"
                u.Codigo = 2

                Session("usuario") = u
                Session("empresa") = 1

                Response.Redirect("~/pages/principal")

            Case Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('ERRO AO EFETUAR LOGIN!');", True)
        End Select

    End Sub
End Class
