Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document
Imports Camadas.Dominio.Documentos

Namespace Camadas.Relatorios

    Public Class Documento
        Inherits DataDynamics.ActiveReports.ActiveReport

        Public Property SubRelDocumento() As Object
        'Public Property Matricula() As String
        'Public Property Nome() As String
        'Public Property Averbacao() As String
        Public Property Pedido() As Pedido

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call
        End Sub

        'Form overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region " ActiveReports Designer generated code "
        'NOTE: The following procedure is required by the ActiveReports Designer
        'It can be modified using the ActiveReports Designer.
        'Do not modify it using the code editor.
        Private WithEvents Detail1 As DataDynamics.ActiveReports.Detail
        Private WithEvents reportHeader1 As DataDynamics.ActiveReports.ReportHeader
        Private WithEvents picture1 As DataDynamics.ActiveReports.Picture
        Private WithEvents label1 As DataDynamics.ActiveReports.Label
        Private WithEvents lblTipoDocumento As DataDynamics.ActiveReports.Label
        Private WithEvents label2 As DataDynamics.ActiveReports.Label
        Private WithEvents lblNome As DataDynamics.ActiveReports.Label
        Private WithEvents label3 As DataDynamics.ActiveReports.Label
        Private WithEvents lblMatricula As DataDynamics.ActiveReports.Label
        Private WithEvents subRel As DataDynamics.ActiveReports.SubReport
        Private WithEvents textBox2 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label12 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label4 As DataDynamics.ActiveReports.Label
        Private WithEvents lblNomeOficial As DataDynamics.ActiveReports.Label
        Private WithEvents textBox1 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label6 As DataDynamics.ActiveReports.Label
        Private WithEvents lblLocalData As DataDynamics.ActiveReports.Label
        Private WithEvents label8 As DataDynamics.ActiveReports.Label
        Private WithEvents txtAverbacao As DataDynamics.ActiveReports.TextBox
        Private WithEvents label5 As DataDynamics.ActiveReports.Label
        Private WithEvents lblNomeOficio As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox4 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label7 As DataDynamics.ActiveReports.Label
        Private WithEvents lblOficialReg As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox6 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label9 As DataDynamics.ActiveReports.Label
        Private WithEvents lblMunicipioDF As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox8 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label10 As DataDynamics.ActiveReports.Label
        Private WithEvents lblEndereco As DataDynamics.ActiveReports.TextBox
        Private WithEvents reportFooter1 As DataDynamics.ActiveReports.ReportFooter
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Global.Resources.Documento))
            Me.Detail1 = New DataDynamics.ActiveReports.Detail()
            Me.subRel = New DataDynamics.ActiveReports.SubReport()
            Me.reportHeader1 = New DataDynamics.ActiveReports.ReportHeader()
            Me.picture1 = New DataDynamics.ActiveReports.Picture()
            Me.label1 = New DataDynamics.ActiveReports.Label()
            Me.lblTipoDocumento = New DataDynamics.ActiveReports.Label()
            Me.label2 = New DataDynamics.ActiveReports.Label()
            Me.lblNome = New DataDynamics.ActiveReports.Label()
            Me.label3 = New DataDynamics.ActiveReports.Label()
            Me.lblMatricula = New DataDynamics.ActiveReports.Label()
            Me.reportFooter1 = New DataDynamics.ActiveReports.ReportFooter()
            Me.textBox2 = New DataDynamics.ActiveReports.TextBox()
            Me.label12 = New DataDynamics.ActiveReports.TextBox()
            Me.label4 = New DataDynamics.ActiveReports.Label()
            Me.lblNomeOficial = New DataDynamics.ActiveReports.Label()
            Me.textBox1 = New DataDynamics.ActiveReports.TextBox()
            Me.label6 = New DataDynamics.ActiveReports.Label()
            Me.lblLocalData = New DataDynamics.ActiveReports.Label()
            Me.label8 = New DataDynamics.ActiveReports.Label()
            Me.txtAverbacao = New DataDynamics.ActiveReports.TextBox()
            Me.label5 = New DataDynamics.ActiveReports.Label()
            Me.lblNomeOficio = New DataDynamics.ActiveReports.TextBox()
            Me.textBox4 = New DataDynamics.ActiveReports.TextBox()
            Me.label7 = New DataDynamics.ActiveReports.Label()
            Me.lblOficialReg = New DataDynamics.ActiveReports.TextBox()
            Me.textBox6 = New DataDynamics.ActiveReports.TextBox()
            Me.label9 = New DataDynamics.ActiveReports.Label()
            Me.lblMunicipioDF = New DataDynamics.ActiveReports.TextBox()
            Me.textBox8 = New DataDynamics.ActiveReports.TextBox()
            Me.label10 = New DataDynamics.ActiveReports.Label()
            Me.lblEndereco = New DataDynamics.ActiveReports.TextBox()
            CType(Me.picture1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.lblTipoDocumento, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.lblNome, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.lblMatricula, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label12, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label4, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.lblNomeOficial, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label6, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.lblLocalData, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label8, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtAverbacao, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label5, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.lblNomeOficio, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox4, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label7, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.lblOficialReg, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox6, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label9, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.lblMunicipioDF, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox8, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label10, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.lblEndereco, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'Detail1
            '
            Me.Detail1.ColumnSpacing = 0.0!
            Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.subRel})
            Me.Detail1.Height = 0.614583373!
            Me.Detail1.Name = "Detail1"
            '
            'subRel
            '
            Me.subRel.CloseBorder = False
            Me.subRel.Height = 0.51000011!
            Me.subRel.Left = 0.0400000103!
            Me.subRel.Name = "subRel"
            Me.subRel.Report = Nothing
            Me.subRel.ReportName = "subReport1"
            Me.subRel.Top = 0.0!
            Me.subRel.Width = 6.36499977!
            '
            'reportHeader1
            '
            Me.reportHeader1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.picture1, Me.label1, Me.lblTipoDocumento, Me.label2, Me.lblNome, Me.label3, Me.lblMatricula})
            Me.reportHeader1.Height = 2.45833302!
            Me.reportHeader1.Name = "reportHeader1"
            '
            'picture1
            '
            Me.picture1.Height = 1.10500002!
            Me.picture1.HyperLink = Nothing
            Me.picture1.ImageData = CType(resources.GetObject("picture1.ImageData"), System.IO.Stream)
            Me.picture1.Left = 2.69199991!
            Me.picture1.Name = "picture1"
            Me.picture1.Top = 0.0!
            Me.picture1.Width = 1.11500001!
            '
            'label1
            '
            Me.label1.Height = 0.166666701!
            Me.label1.HyperLink = Nothing
            Me.label1.Left = 1.79104197!
            Me.label1.Name = "label1"
            Me.label1.Style = "font-size: 9pt; font-weight: bold; text-align: center"
            Me.label1.Text = "REGISTRO CIVIL DAS PESSOAS NATURAIS"
            Me.label1.Top = 1.11699998!
            Me.label1.Width = 2.91691709!
            '
            'lblTipoDocumento
            '
            Me.lblTipoDocumento.Height = 0.270833403!
            Me.lblTipoDocumento.HyperLink = Nothing
            Me.lblTipoDocumento.Left = 1.22350001!
            Me.lblTipoDocumento.Name = "lblTipoDocumento"
            Me.lblTipoDocumento.Style = "font-size: 14pt; font-weight: bold; text-align: center"
            Me.lblTipoDocumento.Text = " [Tipo de Documento]"
            Me.lblTipoDocumento.Top = 1.32700002!
            Me.lblTipoDocumento.Width = 4.05200005!
            '
            'label2
            '
            Me.label2.Height = 0.166666701!
            Me.label2.HyperLink = Nothing
            Me.label2.Left = 1.79104197!
            Me.label2.Name = "label2"
            Me.label2.Style = "font-size: 9pt; font-weight: bold; text-align: center"
            Me.label2.Text = "Nome"
            Me.label2.Top = 1.62800002!
            Me.label2.Width = 2.91691709!
            '
            'lblNome
            '
            Me.lblNome.DataField = "Solicitante.Nome"
            Me.lblNome.Height = 0.2079999!
            Me.lblNome.HyperLink = Nothing
            Me.lblNome.Left = 1.08299994!
            Me.lblNome.Name = "lblNome"
            Me.lblNome.Style = "font-size: 12pt; font-weight: bold; text-align: center"
            Me.lblNome.Text = "[Nome da pessoa]"
            Me.lblNome.Top = 1.79499996!
            Me.lblNome.Width = 4.33300114!
            '
            'label3
            '
            Me.label3.Height = 0.166666701!
            Me.label3.HyperLink = Nothing
            Me.label3.Left = 1.79104197!
            Me.label3.Name = "label3"
            Me.label3.Style = "font-size: 9pt; font-weight: bold; text-align: center"
            Me.label3.Text = "Matrícula"
            Me.label3.Top = 2.06550002!
            Me.label3.Width = 2.91691709!
            '
            'lblMatricula
            '
            Me.lblMatricula.Height = 0.2079999!
            Me.lblMatricula.HyperLink = Nothing
            Me.lblMatricula.Left = 1.08299994!
            Me.lblMatricula.Name = "lblMatricula"
            Me.lblMatricula.Style = "font-size: 12pt; font-weight: bold; text-align: center"
            Me.lblMatricula.Text = "99999999 9999 9 99999 999 9999999 99"
            Me.lblMatricula.Top = 2.23250008!
            Me.lblMatricula.Width = 4.33300114!
            '
            'reportFooter1
            '
            Me.reportFooter1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.textBox2, Me.label12, Me.label4, Me.lblNomeOficial, Me.textBox1, Me.label6, Me.lblLocalData, Me.label8, Me.txtAverbacao, Me.label5, Me.lblNomeOficio, Me.textBox4, Me.label7, Me.lblOficialReg, Me.textBox6, Me.label9, Me.lblMunicipioDF, Me.textBox8, Me.label10, Me.lblEndereco})
            Me.reportFooter1.Height = 2.68741703!
            Me.reportFooter1.Name = "reportFooter1"
            '
            'textBox2
            '
            Me.textBox2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox2.Height = 0.349000007!
            Me.textBox2.Left = 0.0399999991!
            Me.textBox2.Name = "textBox2"
            Me.textBox2.Text = Nothing
            Me.textBox2.Top = 0.70599997!
            Me.textBox2.Width = 2.74000001!
            '
            'label12
            '
            Me.label12.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.label12.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.label12.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.label12.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.label12.Height = 0.648000002!
            Me.label12.Left = 0.0399999991!
            Me.label12.Name = "label12"
            Me.label12.Text = Nothing
            Me.label12.Top = 0.0!
            Me.label12.Width = 6.36499977!
            '
            'label4
            '
            Me.label4.Height = 0.166666701!
            Me.label4.HyperLink = Nothing
            Me.label4.Left = 0.0800000131!
            Me.label4.Name = "label4"
            Me.label4.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label4.Text = "Observações / Averbações"
            Me.label4.Top = 0.0320000015!
            Me.label4.Width = 1.89600003!
            '
            'lblNomeOficial
            '
            Me.lblNomeOficial.Height = 0.166666701!
            Me.lblNomeOficial.HyperLink = Nothing
            Me.lblNomeOficial.Left = 2.92499995!
            Me.lblNomeOficial.Name = "lblNomeOficial"
            Me.lblNomeOficial.Style = "font-size: 9pt; font-weight: bold; text-align: center"
            Me.lblNomeOficial.Text = "[Nome do Oficial]"
            Me.lblNomeOficial.Top = 2.34450006!
            Me.lblNomeOficial.Width = 3.48000002!
            '
            'textBox1
            '
            Me.textBox1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox1.Height = 0.210999995!
            Me.textBox1.Left = 2.92499995!
            Me.textBox1.Name = "textBox1"
            Me.textBox1.Text = Nothing
            Me.textBox1.Top = 2.13400006!
            Me.textBox1.Width = 3.48000002!
            '
            'label6
            '
            Me.label6.Height = 0.166666701!
            Me.label6.HyperLink = Nothing
            Me.label6.Left = 3.71700001!
            Me.label6.Name = "label6"
            Me.label6.Style = "font-size: 9pt; font-style: italic; font-weight: bold; text-align: center"
            Me.label6.Text = "Oficial Registrador"
            Me.label6.Top = 2.50300002!
            Me.label6.Width = 1.89600003!
            '
            'lblLocalData
            '
            Me.lblLocalData.Height = 0.166666701!
            Me.lblLocalData.HyperLink = Nothing
            Me.lblLocalData.Left = 2.93499994!
            Me.lblLocalData.Name = "lblLocalData"
            Me.lblLocalData.Style = "font-size: 9pt; font-weight: bold; text-align: center"
            Me.lblLocalData.Text = "[Local e data]"
            Me.lblLocalData.Top = 1.00750005!
            Me.lblLocalData.Width = 3.48000002!
            '
            'label8
            '
            Me.label8.Height = 0.178000003!
            Me.label8.HyperLink = Nothing
            Me.label8.Left = 2.93499994!
            Me.label8.Name = "label8"
            Me.label8.Style = "font-size: 9pt; font-weight: bold; text-align: center"
            Me.label8.Text = "O Conteúdo da certidão é verdadeiro. Dou fé."
            Me.label8.Top = 0.777499974!
            Me.label8.Width = 3.49000001!
            '
            'txtAverbacao
            '
            Me.txtAverbacao.Height = 0.374999911!
            Me.txtAverbacao.Left = 0.0800000131!
            Me.txtAverbacao.Name = "txtAverbacao"
            Me.txtAverbacao.Style = "font-size: 12pt"
            Me.txtAverbacao.Text = "texto"
            Me.txtAverbacao.Top = 0.219000101!
            Me.txtAverbacao.Width = 6.26000023!
            '
            'label5
            '
            Me.label5.Height = 0.166666701!
            Me.label5.HyperLink = Nothing
            Me.label5.Left = 0.0800000131!
            Me.label5.Name = "label5"
            Me.label5.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label5.Text = "Nome do Ofício"
            Me.label5.Top = 0.715499997!
            Me.label5.Width = 1.89600003!
            '
            'lblNomeOficio
            '
            Me.lblNomeOficio.Height = 0.200000003!
            Me.lblNomeOficio.Left = 0.0800000131!
            Me.lblNomeOficio.Name = "lblNomeOficio"
            Me.lblNomeOficio.Style = "font-size: 10pt"
            Me.lblNomeOficio.Text = "texto"
            Me.lblNomeOficio.Top = 0.855000019!
            Me.lblNomeOficio.Width = 2.61199999!
            '
            'textBox4
            '
            Me.textBox4.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox4.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox4.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox4.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox4.Height = 0.418500006!
            Me.textBox4.Left = 0.0399999991!
            Me.textBox4.Name = "textBox4"
            Me.textBox4.Text = Nothing
            Me.textBox4.Top = 1.1645!
            Me.textBox4.Width = 2.74000001!
            '
            'label7
            '
            Me.label7.Height = 0.166666701!
            Me.label7.HyperLink = Nothing
            Me.label7.Left = 0.0800000131!
            Me.label7.Name = "label7"
            Me.label7.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label7.Text = "Oficial Registrador"
            Me.label7.Top = 1.17400002!
            Me.label7.Width = 1.89600003!
            '
            'lblOficialReg
            '
            Me.lblOficialReg.Height = 0.200000003!
            Me.lblOficialReg.Left = 0.0800000131!
            Me.lblOficialReg.Name = "lblOficialReg"
            Me.lblOficialReg.Style = "font-size: 10pt"
            Me.lblOficialReg.Text = "texto"
            Me.lblOficialReg.Top = 1.31299996!
            Me.lblOficialReg.Width = 2.61199999!
            '
            'textBox6
            '
            Me.textBox6.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox6.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox6.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox6.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox6.Height = 0.418500006!
            Me.textBox6.Left = 0.0399999991!
            Me.textBox6.Name = "textBox6"
            Me.textBox6.Text = Nothing
            Me.textBox6.Top = 1.62450004!
            Me.textBox6.Width = 2.74000001!
            '
            'label9
            '
            Me.label9.Height = 0.166666701!
            Me.label9.HyperLink = Nothing
            Me.label9.Left = 0.0800000131!
            Me.label9.Name = "label9"
            Me.label9.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label9.Text = "Município / DF"
            Me.label9.Top = 1.63399994!
            Me.label9.Width = 1.89600003!
            '
            'lblMunicipioDF
            '
            Me.lblMunicipioDF.Height = 0.200000003!
            Me.lblMunicipioDF.Left = 0.0800000131!
            Me.lblMunicipioDF.Name = "lblMunicipioDF"
            Me.lblMunicipioDF.Style = "font-size: 10pt"
            Me.lblMunicipioDF.Text = "texto"
            Me.lblMunicipioDF.Top = 1.773!
            Me.lblMunicipioDF.Width = 2.61199999!
            '
            'textBox8
            '
            Me.textBox8.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox8.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox8.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox8.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox8.Height = 0.583499789!
            Me.textBox8.Left = 0.0399999991!
            Me.textBox8.Name = "textBox8"
            Me.textBox8.Text = Nothing
            Me.textBox8.Top = 2.08649993!
            Me.textBox8.Width = 2.74000001!
            '
            'label10
            '
            Me.label10.Height = 0.166666701!
            Me.label10.HyperLink = Nothing
            Me.label10.Left = 0.0800000131!
            Me.label10.Name = "label10"
            Me.label10.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label10.Text = "Endereço"
            Me.label10.Top = 2.09599996!
            Me.label10.Width = 1.89600003!
            '
            'lblEndereco
            '
            Me.lblEndereco.Height = 0.371000201!
            Me.lblEndereco.Left = 0.0800000131!
            Me.lblEndereco.Name = "lblEndereco"
            Me.lblEndereco.Style = "font-size: 10pt"
            Me.lblEndereco.Text = "texto"
            Me.lblEndereco.Top = 2.2349999!
            Me.lblEndereco.Width = 2.61199999!
            '
            'Documento
            '
            Me.MasterReport = False
            Me.PageSettings.PaperHeight = 11.0!
            Me.PageSettings.PaperWidth = 8.5!
            Me.PrintWidth = 6.47749996!
            Me.ScriptLanguage = "VB.NET"
            Me.Sections.Add(Me.reportHeader1)
            Me.Sections.Add(Me.Detail1)
            Me.Sections.Add(Me.reportFooter1)
            Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                        "l; font-size: 10pt; color: Black; ddo-char-set: 204", "Normal"))
            Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"))
            Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                        "lic", "Heading2", "Normal"))
            Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"))
            CType(Me.picture1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.lblTipoDocumento, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.lblNome, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.lblMatricula, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label12, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label4, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.lblNomeOficial, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label6, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.lblLocalData, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label8, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtAverbacao, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label5, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.lblNomeOficio, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox4, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label7, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.lblOficialReg, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox6, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label9, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.lblMunicipioDF, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox8, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label10, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.lblEndereco, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region

        Private Sub Documento_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart

            ' CONFIGURAÇÃO DE PÁGINA ---------------------------
            Me.PageSettings.DefaultPaperSize = False
            Me.PageSettings.DefaultPaperSource = False

            Me.PageSettings.Margins.Top = 0.1F
            Me.PageSettings.Margins.Bottom = 0.1F
            Me.PageSettings.Margins.Left = 0.2F
            Me.PageSettings.Margins.Right = 0.2F
            '---------------------------------------------------

            Select True
                Case TypeOf SubRelDocumento Is Camadas.Relatorios.CertidaoNascimento
                    Me.lblTipoDocumento.Text = "CERTIDÃO DE NASCIMENTO"
                Case Else

            End Select

            lblNome.Text = Pedido.Solicitante.Nome
            lblMatricula.Text = Pedido.Matricula.getMatricula

            Me.subRel.Report = SubRelDocumento

            txtAverbacao.Text = IIf(Pedido.Averbacao = String.Empty, "Nenhuma.", Pedido.Averbacao)
            lblNomeOficio.Text = "Cartório Agostinho Vasconcelos"
            lblNomeOficial.Text = "Enoch Ribeiro de Vasconcelos"
            lblOficialReg.Text = lblNomeOficial.Text
            lblMunicipioDF.Text = "São Luís - MA"
            lblEndereco.Text = "Centro comercial da Cohab – Anil"
            lblLocalData.Text = Format(DateTime.Now, "dddd, dd MMMM, yyyy") & ", " & lblMunicipioDF.Text & "."

        End Sub
    End Class

End Namespace