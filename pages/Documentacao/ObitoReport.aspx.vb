Imports System.Configuration.ConfigurationManager

Partial Class pages_Documentacao_ObitoReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pedido As Camadas.Dominio.Documentos.Pedido
        Dim data As String
        Dim array As String()

        If Not IsPostBack Then
            If Not Session("pedido") Is Nothing Then
                pedido = Session("pedido")

                CType(Master.FindControl("lblTipoDoc"), Label).Text = "CERTIDÃO DE ÓBITO"

                CType(Master.FindControl("lblNomePessoa"), Label).Text = pedido.Solicitante.Nome ' CType(Session("pedido"), Pedido).Solicitante.Nome
                CType(Master.FindControl("lblMatricula"), Label).Text = pedido.Matricula.getMatricula 'CType(Session("pedido"), Pedido).Matricula.getMatricula
                CType(Master.FindControl("lblAverbacao"), Label).Text = IIf(pedido.Averbacao.Trim = String.Empty, "Nenhuma.         ", pedido.Averbacao) ' "OIFJA SFJSI DFOI SFOS FOSDHSFHSDFHSAPFHSUFHUSDAHF USDAHF SDUHF SDPIAHF SPDAHF PSIUDAF HUSDAHF USDAHF SUDAHF SUIDAHF SUIDAHF SUIDAHF USIDAHF UIASDHF USDAHFUSDAIHF SUIDAHF SUIDAFH SUIDAFH SPUDAFH SUDAFH SUIDAFH SAUHF ASUHF SUDAFH SUIDAHF SUDAHF UPSDAHF SDHFSHFPSDAFJH SDAHF ISDAHF OISDH FOIDHSAPFIH ASUDFH ASUFHSAUIDFH SDUIHF IUSDAHF USADH FUSAHFUI."

                CType(Master.FindControl("lblNomeOficio"), Label).Text = AppSettings.Item("NOME_OFICIO").ToString
                CType(Master.FindControl("lblNomeOficial"), Label).Text = AppSettings.Item("NOME_OFICIAL").ToString
                CType(Master.FindControl("lblEndereco"), Label).Text = AppSettings.Item("ENDERECO").ToString
                CType(Master.FindControl("lblMunicipio"), Label).Text = AppSettings.Item("CIDADE").ToString
                CType(Master.FindControl("lblLocalData"), Label).Text = Format(Date.Parse(pedido.Documento.DataRegistro), "dddd, dd MMMM, yyyy") & ", " & CType(Master.FindControl("lblMunicipio"), Label).Text & "."
                CType(Master.FindControl("lblOficialRegistrador"), Label).Text = CType(Master.FindControl("lblNomeOficial"), Label).Text



                Session.Remove("pedido")
            Else
                Response.Write("<h1>DOCUMENTO NÃO ENCONTRADO. VERIFIQUE NOVAMENTE.</h1>")
            End If
        End If
    End Sub
End Class
