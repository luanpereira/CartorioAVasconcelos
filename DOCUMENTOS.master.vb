Imports Camadas.Dominio.Documentos
Imports System.Configuration.ConfigurationManager

Partial Class DOCUMENTOS
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pedido As Pedido

        If Not IsPostBack Then
            imgBrasao.ImageUrl = "~/recursos/Images/brasaoRepublica.jpg"
            imgAss.ImageUrl = "~/recursos/Images/assinaturaEnoch.png"

            If Not Session("pedido") Is Nothing Then
                pedido = Session("pedido")

                'Select Case pedido.Documento.GetType.Name
                '    Case "Nascimento"
                '        Me.lblTipoDoc.Text = "CERTIDÃO DE NASCIMENTO"
                '    Case Else
                '        Me.lblTipoDoc.Text = "TIPO DE DOCUMENTO NÃO DEFINIDO. TENTE NOVAMENTE."
                'End Select

                'Me.lblNomePessoa.Text = pedido.Solicitante.Nome ' CType(Session("pedido"), Pedido).Solicitante.Nome
                'Me.lblMatricula.Text = pedido.Matricula.getMatricula 'CType(Session("pedido"), Pedido).Matricula.getMatricula
                'Me.lblAverbacao.Text = IIf(pedido.Averbacao.Trim = String.Empty, "Nenhuma.         ", pedido.Averbacao) ' "OIFJA SFJSI DFOI SFOS FOSDHSFHSDFHSAPFHSUFHUSDAHF USDAHF SDUHF SDPIAHF SPDAHF PSIUDAF HUSDAHF USDAHF SUDAHF SUIDAHF SUIDAHF SUIDAHF USIDAHF UIASDHF USDAHFUSDAIHF SUIDAHF SUIDAFH SUIDAFH SPUDAFH SUDAFH SUIDAFH SAUHF ASUHF SUDAFH SUIDAHF SUDAHF UPSDAHF SDHFSHFPSDAFJH SDAHF ISDAHF OISDH FOIDHSAPFIH ASUDFH ASUFHSAUIDFH SDUIHF IUSDAHF USADH FUSAHFUI."

                'Me.lblNomeOficio.Text = AppSettings.Item("NOME_OFICIO").ToString
                'Me.lblNomeOficial.Text = AppSettings.Item("NOME_OFICIAL").ToString
                'Me.lblEndereco.Text = AppSettings.Item("ENDERECO").ToString
                'Me.lblMunicipio.Text = AppSettings.Item("CIDADE").ToString
                'Me.lblLocalData.Text = Format(pedido.Documento.DataRegistro, "dddd, dd MMMM, yyyy") & ", " & lblMunicipio.Text & "."
                'Me.lblOficialRegistrador.Text = Me.lblNomeOficial.Text


            Else

            End If
        End If
    End Sub
End Class

