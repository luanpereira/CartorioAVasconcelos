Imports Camadas.Dominio.Documentos
Imports Infraestrutura
Imports System.Configuration.ConfigurationManager

Partial Class pages_Documentacao_ProclamasReport
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pedido As Pedido
        Dim str As String = ""

        If Not IsPostBack Then
            If Not Session("pedido") Is Nothing Then
                pedido = Session("pedido")

                CType(Master.FindControl("lblTitulo"), Label).Text = "EDITAL DE PROCLAMAS Nº " & CType(pedido.Documento, Proclamas).Edital.ToString & " Prazo: " & AppSettings.Item("PRAZO_PROCLAMAS").ToString() & " dias."
                CType(Master.FindControl("lblLocalData"), Label).Text = Format(Date.Parse(pedido.DataEmissao), "dddd, dd MMMM, yyyy") & ", " & AppSettings.Item("CIDADE").ToString() & "."

                str = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & AppSettings.Item("DIZERES_PROCLAMAS").ToString

                str = str.Replace("{OFICIO}", AppSettings.Item("NOME_OFICIO2").ToString)
                str = str.Replace("{CONJUGE1}", Me.getDados(CType(pedido.Documento, Proclamas).Casal.Conjuge1))
                str = str.Replace("{CONJUGE2}", Me.getDados(CType(pedido.Documento, Proclamas).Casal.Conjuge2))

                lblDocumento.Text = str

            End If
        End If
    End Sub

    Private Function getDados(ByVal cliente As Camadas.Dominio.Administrativo.Cliente) As String
        Dim str, estadoCivil As String

        str = "<b>" & cliente.Nome & "</b>, "

        If cliente.Sexo = "F" Then
            str += "nascida"
            estadoCivil = ""
        Else
            str += "nascido"
            estadoCivil = ""
        End If

        str += " aos "
        str += Utils.dataPorExtenso(cliente.DataNascimento) & ", "

        str += "natural de " & cliente.Natural.ToString & ", "
        str += "de nacionalidade brasileira, "

        Select Case cliente.EstadoCivil
            Case "C"
                str += IIf(cliente.Sexo = "F", "casada", "casado")
            Case "S"
                str += IIf(cliente.Sexo = "F", "solteira", "solteiro")
            Case "V"
                str += IIf(cliente.Sexo = "F", "viúva", "viúvo")
            Case "U"
                str += "união estável"
            Case "D"
                str += IIf(cliente.Sexo = "F", "divorciada", "divorciado")
            Case Else
                str += "sem estado civil cadastrado"
        End Select
        str += ", "

        str += IIf(cliente.Profissao.Trim = String.Empty, "sem profissão", cliente.Profissao) & ", "

        If cliente.Sexo = "F" Then
            str += "filha de "
        Else
            str += "filho de "
        End If

        str += cliente.Filiacao.getPais

        'If cliente.Sexo = "F" Then
        '    str += "redidente e domiciliada em " & cliente.Endereco.CidadeEstado & "."
        'Else
        '    str += "redidente e domiciliado em " & cliente.Endereco.CidadeEstado & "."
        'End If

        Return str
    End Function
End Class
