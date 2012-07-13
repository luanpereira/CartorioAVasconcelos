Imports Camadas.Dominio.Documentos
Imports Infraestrutura.Utils

Partial Class pages_principal_Default
    Inherits System.Web.UI.Page

    Private seguranca As Seguranca
    Private matricula As Matricula

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'seguranca = New Seguranca

        'Try
        '    seguranca.ValidarAcesso(1)
        'Catch ex As Exception
        '    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        'End Try


        If Not IsPostBack Then
            'matricula = New Matricula("031385", "01", "55", 1992, "00030", "116", "0008396", TipoLivro.Casamento)
            'lblMatricula.Text = matricula.getMatricula
        End If
    End Sub
End Class
