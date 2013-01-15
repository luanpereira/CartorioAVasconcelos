Imports Camadas.Dominio.Documentos
Imports Infraestrutura
Imports System.Configuration.ConfigurationManager

Partial Class pages_Documentacao_CancelamentoNascimentoReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pedido As Pedido
        Dim str As String

        If Not IsPostBack Then
            If Not Session("pedido") Is Nothing Then
                pedido = Session("pedido")

                CType(Master.FindControl("lblTipoDoc"), Label).Text = "CERTIDÃO DE CANCELAMENTO DE REGISTRO DE NASCIMENTO"

                CType(Master.FindControl("lblNomePessoa"), Label).Text = pedido.Solicitante.Nome
                CType(Master.FindControl("lblMatriculaLabel"), Label).Visible = False
                CType(Master.FindControl("lblMatricula"), Label).Visible = False
                CType(Master.FindControl("pnlAverbacao"), Panel).Visible = False

                CType(Master.FindControl("lblNomeOficio"), Label).Text = AppSettings.Item("NOME_OFICIO").ToString
                CType(Master.FindControl("lblNomeOficial"), Label).Text = AppSettings.Item("NOME_OFICIAL").ToString
                CType(Master.FindControl("lblEndereco"), Label).Text = AppSettings.Item("ENDERECO").ToString
                CType(Master.FindControl("lblMunicipio"), Label).Text = AppSettings.Item("CIDADE").ToString
                CType(Master.FindControl("lblLocalData"), Label).Text = Format(Date.Parse(pedido.DataEmissao), "dddd, dd MMMM, yyyy") & ", " & CType(Master.FindControl("lblMunicipio"), Label).Text & "."
                CType(Master.FindControl("lblOficialRegistrador"), Label).Text = CType(Master.FindControl("lblNomeOficial"), Label).Text

                str = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Usando das atribuições que me confere a LEI, CERTIFICO, a requerimento verbal de parte interessada, que à folhas "
                str += "<b>" & CType(pedido.Documento, CancelamentoNascimento).NumeroFolha & "</b>, do Livro n.º <b>A-" & CType(pedido.Documento, CancelamentoNascimento).NumeroLivro & "</b>, "
                str += "onde consta o termo n.º <b>" & CType(pedido.Documento, CancelamentoNascimento).NumeroTermo & "</b> de nascimento de "
                str += "<b>" & pedido.Solicitante.Nome & "</b> à margem do assento, contem Averbação de Cancelamento de Registro de Nascimento, a seguir transcrita: """
                str += CType(pedido.Documento, CancelamentoNascimento).Motivo & """."
                Me.lblDocumento.Text = str

                Session.Remove("pedido")
            Else
                Response.Write("<h1>DOCUMENTO NÃO ENCONTRADO. VERIFIQUE NOVAMENTE.</h1>")
            End If
        End If
    End Sub
End Class
