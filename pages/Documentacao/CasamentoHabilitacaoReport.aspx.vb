Imports Camadas.Dominio.Documentos
Imports Infraestrutura
Imports System.Configuration.ConfigurationManager

Partial Class pages_Documentacao_CasamentoHabilitacaoReport
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pedido As Pedido
        Dim data = "", str = "", nomes = "", regime As String = ""

        If Not IsPostBack Then
            If Not Session("pedido") Is Nothing Then
                pedido = Session("pedido")

                CType(Master.FindControl("lblTipoDoc"), Label).Text = "CERTIDÃO DE HABILITAÇÃO"

                CType(Master.FindControl("lblNomePessoaLabel"), Label).Visible = False
                CType(Master.FindControl("lblNomePessoa"), Label).Visible = False
                CType(Master.FindControl("lblMatriculaLabel"), Label).Visible = False
                CType(Master.FindControl("lblMatricula"), Label).Visible = False
                CType(Master.FindControl("imgAss"), Image).Visible = True
                CType(Master.FindControl("lblNomeOficial"), Label).Visible = True
                CType(Master.FindControl("pnlAverbacao"), Panel).Visible = False

                CType(Master.FindControl("lblNomeOficio"), Label).Text = AppSettings.Item("NOME_OFICIO").ToString
                CType(Master.FindControl("lblNomeOficial"), Label).Text = AppSettings.Item("NOME_OFICIAL").ToString
                CType(Master.FindControl("lblEndereco"), Label).Text = AppSettings.Item("ENDERECO").ToString
                CType(Master.FindControl("lblMunicipio"), Label).Text = AppSettings.Item("CIDADE").ToString
                CType(Master.FindControl("lblLocalData"), Label).Text = Format(Date.Parse(pedido.DataEmissao), "dddd, dd MMMM, yyyy") & ", " & CType(Master.FindControl("lblMunicipio"), Label).Text & "."
                CType(Master.FindControl("lblOficialRegistrador"), Label).Text = CType(Master.FindControl("lblNomeOficial"), Label).Text

                str = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & AppSettings.Item("DIZERES_HABILITACAO").ToString

                str = str.Replace("{OFICIO}", AppSettings.Item("NOME_OFICIO2").ToString)
                str = str.Replace("{DE1}", IIf(CType(pedido.Documento, Habilitacao).Casal.Conjuge1.Sexo = "F", "da", "do"))
                str = str.Replace("{CONJUGE1}", Me.getDados(CType(pedido.Documento, Habilitacao).Casal.Conjuge1))
                str = str.Replace("{DE2}", IIf(CType(pedido.Documento, Habilitacao).Casal.Conjuge2.Sexo = "F", "da", "do"))
                str = str.Replace("{CONJUGE2}", Me.getDados(CType(pedido.Documento, Habilitacao).Casal.Conjuge2))

                Select Case CType(pedido.Documento, Habilitacao).Regime
                    Case Utils.Regime.COMUNHAO_DE_PARTICIPACAO_FINAL_NOS_AQUESTOS
                        regime = "Comunhão de Participação Final nos Aquestos"
                    Case Utils.Regime.COMUNHAO_DE_SEPARACAO_DE_BENS
                        regime = "Comunhão de Separação de Bens"
                    Case Utils.Regime.COMUNHAO_PARCIAL_DE_BENS
                        regime = "Comunhão Parcial de Bens"
                    Case Utils.Regime.COMUNHAO_UNIVERSAL_DE_BENS
                        regime = "Comunhão Universal de Bens"
                    Case Else
                        regime = "Regime"
                End Select

                str = str.Replace("{REGIME}", regime)

                If CType(pedido.Documento, Habilitacao).NovoNomeConjuge1 = CType(pedido.Documento, Habilitacao).Casal.Conjuge1.Nome And _
                   CType(pedido.Documento, Habilitacao).NovoNomeConjuge2 = CType(pedido.Documento, Habilitacao).Casal.Conjuge2.Nome Then

                    nomes = "mantendo o mesmo nome"
                Else
                    nomes = "passando a ter o seguinte Nome: "

                    If CType(pedido.Documento, Habilitacao).NovoNomeConjuge1 <> CType(pedido.Documento, Habilitacao).Casal.Conjuge1.Nome Then
                        nomes += CType(pedido.Documento, Habilitacao).NovoNomeConjuge1
                    End If

                    If CType(pedido.Documento, Habilitacao).NovoNomeConjuge2 <> CType(pedido.Documento, Habilitacao).Casal.Conjuge2.Nome Then
                        nomes += " e " & CType(pedido.Documento, Habilitacao).NovoNomeConjuge2
                    End If
                End If

                str = str.Replace("{NOVOSNOMES}", nomes)

                lblDocumento.Text = str

                'Me.lblDocumento.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CERTIFICO que, sendo publicado e Edital de Proclamas, recomendados pela Lei, não apareceu, dentro do prazo legal, qualquer pessoa, opondo-se ao CASAMENTO " & IIf(CType(pedido.Documento, Habilitacao).Casal.Conjuge1.Sexo = "F", "da", "do") & " nubente "
                'Me.lblDocumento.Text += Me.getDados(CType(pedido.Documento, Habilitacao).Casal.Conjuge1) & " e " & IIf(CType(pedido.Documento, Habilitacao).Casal.Conjuge2.Sexo = "F", "da", "do") & " nubente "
                'Me.lblDocumento.Text += Me.getDados(CType(pedido.Documento, Habilitacao).Casal.Conjuge2) & ", não existindo impedimento ao casamento, estão os habilitados a contrair núpcias, nos termos do art. 1525 e itens seguintes do Código Civil Brasileiro, pelo regime "

                'Select Case CType(pedido.Documento, Habilitacao).Regime
                '    Case Utils.Regime.COMUNHAO_DE_PARTICIPACAO_FINAL_NOS_AQUESTOS
                '        Me.lblDocumento.Text += "Comunhão de Participação Final nos Aquestos"
                '    Case Utils.Regime.COMUNHAO_DE_SEPARACAO_DE_BENS
                '        Me.lblDocumento.Text += "Comunhão de Separação de Bens"
                '    Case Utils.Regime.COMUNHAO_PARCIAL_DE_BENS
                '        Me.lblDocumento.Text += "Comunhão Parcial de Bens"
                '    Case Utils.Regime.COMUNHAO_UNIVERSAL_DE_BENS
                '        Me.lblDocumento.Text += "Comunhão Universal de Bens"
                '    Case Else
                '        Me.lblDocumento.Text += "Regime"
                'End Select

                'If CType(pedido.Documento, Habilitacao).NovoNomeConjuge1 = CType(pedido.Documento, Habilitacao).Casal.Conjuge1.Nome And _
                '   CType(pedido.Documento, Habilitacao).NovoNomeConjuge2 = CType(pedido.Documento, Habilitacao).Casal.Conjuge2.Nome Then

                '    Me.lblDocumento.Text += ", mantendo o mesmo nome. "
                'Else
                '    Me.lblDocumento.Text += ", passando a ter o seguinte Nome: "

                '    If CType(pedido.Documento, Habilitacao).NovoNomeConjuge1 <> CType(pedido.Documento, Habilitacao).Casal.Conjuge1.Nome Then
                '        lblDocumento.Text += CType(pedido.Documento, Habilitacao).NovoNomeConjuge1
                '    End If

                '    If CType(pedido.Documento, Habilitacao).NovoNomeConjuge2 <> CType(pedido.Documento, Habilitacao).Casal.Conjuge2.Nome Then
                '        lblDocumento.Text += " e " & CType(pedido.Documento, Habilitacao).NovoNomeConjuge2
                '    End If

                '    Me.lblDocumento.Text += "."
                'End If

            Session.Remove("pedido")
        Else
            Response.Write("<h1>DOCUMENTO NÃO ENCONTRADO. VERIFIQUE NOVAMENTE.</h1>")
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
