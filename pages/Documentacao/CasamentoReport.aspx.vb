Imports Camadas.Dominio.Documentos
Imports Infraestrutura
Imports System.Configuration.ConfigurationManager

Partial Class pages_Documentacao_CasamentoReport
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pedido As Pedido
        Dim data As String
        Dim array As String()

        If Not IsPostBack Then
            If Not Session("pedido") Is Nothing Then
                pedido = Session("pedido")

                CType(Master.FindControl("lblTipoDoc"), Label).Text = "CERTIDÃO DE CASAMENTO"

                CType(Master.FindControl("lblNomePessoa"), Label).Text = CType(pedido.Documento, Casamento).NovoNomeConjuge1 & "<BR />" & CType(pedido.Documento, Casamento).NovoNomeConjuge2
                CType(Master.FindControl("lblMatricula"), Label).Text = pedido.Matricula.getMatricula
                CType(Master.FindControl("lblAverbacao"), Label).Text = IIf(pedido.Averbacao.Trim = String.Empty, "Nenhuma.         ", pedido.Averbacao)

                CType(Master.FindControl("lblNomeOficio"), Label).Text = AppSettings.Item("NOME_OFICIO").ToString
                CType(Master.FindControl("lblNomeOficial"), Label).Text = AppSettings.Item("NOME_OFICIAL").ToString
                CType(Master.FindControl("lblEndereco"), Label).Text = AppSettings.Item("ENDERECO").ToString
                CType(Master.FindControl("lblMunicipio"), Label).Text = AppSettings.Item("CIDADE").ToString
                CType(Master.FindControl("lblLocalData"), Label).Text = Format(Date.Parse(pedido.DataEmissao), "dddd, dd MMMM, yyyy") & ", " & CType(Master.FindControl("lblMunicipio"), Label).Text & "."
                CType(Master.FindControl("lblOficialRegistrador"), Label).Text = CType(Master.FindControl("lblNomeOficial"), Label).Text


                Me.lblConjuges.Text = Me.getDados(CType(pedido.Documento, Casamento).Casal.Conjuge1) & "<br />&nbsp;&nbsp;"
                Me.lblConjuges.Text += Me.getDados(CType(pedido.Documento, Casamento).Casal.Conjuge2)

                '-- DATA DE NASCIMENTO ------------------
                lblDataCasa.Text = Utils.dataPorExtenso(pedido.Documento.DataRegistro)

                data = pedido.Documento.DataRegistro
                If data.Length.ToString = 10 Then
                    If data.Contains("/") Then
                        array = data.Split("/")
                        lblDia.Text = array(0)
                        lblMes.Text = array(1)
                        lblAno.Text = array(2)
                    Else
                        array = data.Split("-")
                        lblDia.Text = array(2)
                        lblMes.Text = array(1)
                        lblAno.Text = array(0)
                    End If
                End If

                '----------------------------------------

                Select Case CType(pedido.Documento, Casamento).Regime
                    Case Utils.Regime.COMUNHAO_DE_PARTICIPACAO_FINAL_NOS_AQUESTOS
                        lblRegime.Text = "Comunhão de Participação Final nos Aquestos."
                    Case Utils.Regime.COMUNHAO_DE_SEPARACAO_DE_BENS
                        lblRegime.Text = "Comunhão de Separação de Bens."
                    Case Utils.Regime.COMUNHAO_PARCIAL_DE_BENS
                        lblRegime.Text = "Comunhão Parcial de Bens."
                    Case Utils.Regime.COMUNHAO_UNIVERSAL_DE_BENS
                        lblRegime.Text = "Comunhão Universal de Bens."
                    Case Else
                        lblRegime.Text = "Regime."
                End Select

                lblNovosNomes.Text = ""
                If CType(pedido.Documento, Casamento).NovoNomeConjuge1 <> CType(pedido.Documento, Casamento).Casal.Conjuge1.Nome Then
                    lblNovosNomes.Text += CType(pedido.Documento, Casamento).NovoNomeConjuge1 & "<br />"
                End If

                If CType(pedido.Documento, Casamento).NovoNomeConjuge2 <> CType(pedido.Documento, Casamento).Casal.Conjuge2.Nome Then
                    lblNovosNomes.Text += CType(pedido.Documento, Casamento).NovoNomeConjuge2
                End If

                If lblNovosNomes.Text.Trim = String.Empty Then
                    lblNovosNomes.Text = "Não houve alterações."
                End If

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

        If Not cliente.Profissao Is Nothing Then
            str += IIf(cliente.Profissao.Trim = String.Empty, "sem profissão", cliente.Profissao) & ", "
        Else
            str += "sem profissão"
        End If


        If cliente.Sexo = "F" Then
            str += "filha de "
        Else
            str += "filho de "
        End If

        str += cliente.Filiacao.getPais & ". "

        'If cliente.Sexo = "F" Then
        '    str += "redidente e domiciliada em " & cliente.Endereco.CidadeEstado & "."
        'Else
        '    str += "redidente e domiciliado em " & cliente.Endereco.CidadeEstado & "."
        'End If



        Return str
    End Function
End Class
