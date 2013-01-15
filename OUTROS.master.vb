Imports System.Configuration.ConfigurationManager

Partial Class OUTROS
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            lblCabecalho.Text = AppSettings.Item("NOME_OFICIAL").ToString() & ", " & "Oficial de Registro de Casamento Da "
            lblCabecalho.Text += AppSettings.Item("NOME_OFICIO2").ToString() & ", " & AppSettings.Item("CIDADE").ToString()

            lblNomeOficial.Text = AppSettings.Item("NOME_OFICIAL").ToString()

            imgAss.ImageUrl = "~/recursos/Images/assinaturaEnoch.png"

        End If

    End Sub

End Class

