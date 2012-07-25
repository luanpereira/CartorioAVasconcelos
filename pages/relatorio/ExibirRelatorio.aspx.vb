Imports Camadas.Relatorios
Imports Camadas.Dominio.Documentos

Partial Class pages_relatorio_ExibirRelatorio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'TODOS OS RELATÓRIOS DO SISTEMA PASSAM POR AQUI
        Select Case Request.QueryString("r")
            Case 1
                'EMISSÃO DA CERTIDÃO DE NASCIMENTO
                Dim rel As New Camadas.Relatorios.Documento
                Dim relNascimento As New Camadas.Relatorios.CertidaoNascimento
                Dim pedido As Pedido = CType(Session("pedido"), Pedido)


                rel.Pedido = pedido
                relNascimento.Pedido = pedido
                rel.SubRelDocumento = relNascimento
                'relNascimento -------------------------------
                WebViewer.Report = rel
                '---------------------------------------
                Session.Remove("pedido")
        End Select

    End Sub
End Class

