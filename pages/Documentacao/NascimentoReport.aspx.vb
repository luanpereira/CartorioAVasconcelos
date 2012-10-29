Imports Camadas.Dominio.Documentos
Imports Infraestrutura
Imports System.Configuration.ConfigurationManager

Partial Class pages_Documentacao_NascimentoReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pedido As Pedido
        Dim data As String
        Dim array As String()

        If Not IsPostBack Then
            If Not Session("pedido") Is Nothing Then
                pedido = Session("pedido")

                CType(Master.FindControl("lblTipoDoc"), Label).Text = "CERTIDÃO DE NASCIMENTO"

                CType(Master.FindControl("lblNomePessoa"), Label).Text = pedido.Solicitante.Nome ' CType(Session("pedido"), Pedido).Solicitante.Nome
                CType(Master.FindControl("lblMatricula"), Label).Text = pedido.Matricula.getMatricula 'CType(Session("pedido"), Pedido).Matricula.getMatricula
                CType(Master.FindControl("lblAverbacao"), Label).Text = IIf(pedido.Averbacao.Trim = String.Empty, "Nenhuma.         ", pedido.Averbacao) ' "OIFJA SFJSI DFOI SFOS FOSDHSFHSDFHSAPFHSUFHUSDAHF USDAHF SDUHF SDPIAHF SPDAHF PSIUDAF HUSDAHF USDAHF SUDAHF SUIDAHF SUIDAHF SUIDAHF USIDAHF UIASDHF USDAHFUSDAIHF SUIDAHF SUIDAFH SUIDAFH SPUDAFH SUDAFH SUIDAFH SAUHF ASUHF SUDAFH SUIDAHF SUDAHF UPSDAHF SDHFSHFPSDAFJH SDAHF ISDAHF OISDH FOIDHSAPFIH ASUDFH ASUFHSAUIDFH SDUIHF IUSDAHF USADH FUSAHFUI."

                CType(Master.FindControl("lblNomeOficio"), Label).Text = AppSettings.Item("NOME_OFICIO").ToString
                CType(Master.FindControl("lblNomeOficial"), Label).Text = AppSettings.Item("NOME_OFICIAL").ToString
                CType(Master.FindControl("lblEndereco"), Label).Text = AppSettings.Item("ENDERECO").ToString
                CType(Master.FindControl("lblMunicipio"), Label).Text = AppSettings.Item("CIDADE").ToString
                CType(Master.FindControl("lblLocalData"), Label).Text = Format(Date.Parse(pedido.Documento.DataRegistro), "dddd, dd MMMM, yyyy") & ", " & CType(Master.FindControl("lblMunicipio"), Label).Text & "."
                CType(Master.FindControl("lblOficialRegistrador"), Label).Text = CType(Master.FindControl("lblNomeOficial"), Label).Text

                lblFiliacao.Text = pedido.Solicitante.Filiacao.getPais
                lblAvos.Text = pedido.Solicitante.Filiacao.getAvos

                '-- DATA DE NASCIMENTO ------------------
                lblDataNascimento.Text = Utils.dataPorExtenso(pedido.Solicitante.DataNascimento)

                data = pedido.Solicitante.DataNascimento
                array = data.Split("/")
                lblDia.Text = array(0)
                lblMes.Text = array(1)
                lblAno.Text = array(2)
                '----------------------------------------

                lblHora.Text = CType(pedido.Documento, Nascimento).Horario.Substring(0, 2) & "H " & CType(pedido.Documento, Nascimento).Horario.Substring(3, 2) & "Min"
                lblMunicipioNascimento.Text = CType(pedido.Documento, Nascimento).Cidade

                lblMunicipioRegistro.Text = ConfigurationManager.AppSettings.Item("CIDADE").ToString
                lblLocal.Text = CType(pedido.Documento, Nascimento).Maternidade
                lblSexo.Text = pedido.Solicitante.Sexo

                lblGemeo.Text = IIf(Not pedido.Solicitante.Gemeo.Nome.ToUpper = "NÃO", "Sim", "Não")
                lblNomeGemeo.Text = IIf(Not pedido.Solicitante.Gemeo.Nome.ToUpper = "NÃO", pedido.Solicitante.Gemeo.Nome, "-")
                lblDataRegistro.Text = Utils.dataPorExtenso(pedido.Documento.DataRegistro)

                Session.Remove("pedido")
            Else
                Response.Write("<h1>DOCUMENTO NÃO ENCONTRADO. VERIFIQUE NOVAMENTE.</h1>")
            End If
        End If
    End Sub
End Class
