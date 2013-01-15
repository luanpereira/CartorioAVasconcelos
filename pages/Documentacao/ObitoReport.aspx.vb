Imports System.Configuration.ConfigurationManager
Imports Camadas.Dominio.Documentos
Imports Infraestrutura

Partial Class pages_Documentacao_ObitoReport
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim pedido As Pedido
        Dim obito As Obito
        Dim data, estadoCivilIdade As String
        Dim array As String()
        Dim dt As DateTime
        Dim ts As TimeSpan
        Dim idade As Integer

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
                CType(Master.FindControl("lblLocalData"), Label).Text = Format(Date.Parse(pedido.DataEmissao), "dddd, dd MMMM, yyyy") & ", " & CType(Master.FindControl("lblMunicipio"), Label).Text & "."
                CType(Master.FindControl("lblOficialRegistrador"), Label).Text = CType(Master.FindControl("lblNomeOficial"), Label).Text

                obito = New Obito
                obito = pedido.Documento

                Me.lblSexo.Text = IIf(pedido.Solicitante.Sexo = "M", "Masculino", "Feminino")
                Me.lblCor.Text = obito.Cor.Nome

                Select Case pedido.Solicitante.EstadoCivil
                    Case "0"
                        estadoCivilIdade = "SEM ESTADO CIVIL"
                    Case "C"
                        estadoCivilIdade = IIf(pedido.Solicitante.Sexo = "F", "CASADA", "CASADO")
                    Case "S"
                        estadoCivilIdade = IIf(pedido.Solicitante.Sexo = "F", "SOLTEIRA", "SOLTEIRO")
                    Case "V"
                        estadoCivilIdade = IIf(pedido.Solicitante.Sexo = "F", "VIÚVA", "VIÚVO")
                    Case "U"
                        estadoCivilIdade = "UNIÃO ESTÁVEL"
                    Case Else
                        estadoCivilIdade = "NÃO DEFINIDO"
                End Select

                dt = pedido.Solicitante.DataNascimento
                ts = DateTime.Today.Subtract(dt)
                idade = New DateTime(ts.Ticks).ToString("yy")

                If idade > 1 Then
                    estadoCivilIdade += ", " & idade & " anos."
                ElseIf idade = 1 Then
                    estadoCivilIdade += ", " & idade & " ano."
                Else
                    idade = New DateTime(ts.Ticks).ToString("MM")
                    If idade > 1 Then
                        estadoCivilIdade += ", " & idade & " meses."
                    ElseIf idade = 1 Then
                        estadoCivilIdade += ", " & idade & " mês."
                    Else
                        idade = New DateTime(ts.Ticks).ToString("dd")
                        If idade > 1 Then
                            estadoCivilIdade += ", " & idade & " dias."
                        Else
                            estadoCivilIdade += ", " & idade & " dia."
                        End If
                    End If
                End If

                Me.lblEstadoCivilIdade.Text = estadoCivilIdade
                Me.lblNatural.Text = pedido.Solicitante.Natural.ToString

                'If Not pedido.Solicitante.Cpf Is Nothing And pedido.Solicitante.Cpf.Trim = String.Empty And Not pedido.Solicitante.Rg Is Nothing And pedido.Solicitante.Rg.Trim = String.Empty Then
                'Me.lblDocId.Text = "SEM DOCUMENTO."
                'Else
                If Not pedido.Solicitante.Cpf Is Nothing AndAlso Not pedido.Solicitante.Cpf.Trim = String.Empty Then
                    Me.lblDocId.Text = "CPF " & pedido.Solicitante.Cpf
                ElseIf Not pedido.Solicitante.Rg Is Nothing AndAlso Not pedido.Solicitante.Rg.Trim = String.Empty Then
                    Me.lblDocId.Text = "RG " & pedido.Solicitante.Rg
                Else
                    Me.lblDocId.Text = "SEM DOCUMENTO."
                End If
                'End If

                lblFiliacao.Text = pedido.Solicitante.Filiacao.getPais

                lblDataHorra.Text = Utils.dataPorExtenso(obito.DataObito) & " às " & obito.Horario.Substring(0, 2) & "H " & obito.Horario.Substring(3, 2) & "Min"
                lblLocal.Text = obito.Local
                lblCausaMorte.Text = obito.CausaMorte
                lblSepultamento.Text = obito.Sepultamento
                lblDeclarante.Text = Mid(obito.Declarante, 1, 18) 'IIf(obito.Declarante.Length > 17, obito.Declarante.Substring(0, 18), obito.Declarante)
                lblMedico.Text = obito.Medico

                Session.Remove("pedido")
            Else
                Response.Write("<h1>DOCUMENTO NÃO ENCONTRADO. VERIFIQUE NOVAMENTE.</h1>")
            End If
        End If
    End Sub
End Class
