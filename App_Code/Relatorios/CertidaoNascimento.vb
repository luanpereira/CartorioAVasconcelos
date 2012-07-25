Imports DataDynamics.ActiveReports
Imports DataDynamics.ActiveReports.Document
Imports Camadas.Dominio.Documentos

Namespace Camadas.Relatorios

    Public Class CertidaoNascimento
        Inherits DataDynamics.ActiveReports.ActiveReport

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
        Private WithEvents label5 As DataDynamics.ActiveReports.Label
        Private WithEvents textBox2 As DataDynamics.ActiveReports.TextBox
        Private WithEvents txtNascimento As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox1 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label1 As DataDynamics.ActiveReports.Label
        Private WithEvents txtDia As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox5 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label2 As DataDynamics.ActiveReports.Label
        Private WithEvents txtMes As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox7 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label3 As DataDynamics.ActiveReports.Label
        Private WithEvents txtAno As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox9 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label4 As DataDynamics.ActiveReports.Label
        Private WithEvents txtHora As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox11 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label6 As DataDynamics.ActiveReports.Label
        Private WithEvents txtMunicipio As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox13 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label7 As DataDynamics.ActiveReports.Label
        Private WithEvents txtMunicipioReg As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox15 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label8 As DataDynamics.ActiveReports.Label
        Private WithEvents txtLocalNascimento As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox17 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label9 As DataDynamics.ActiveReports.Label
        Private WithEvents txtSexo As DataDynamics.ActiveReports.TextBox
        Private WithEvents label12 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label10 As DataDynamics.ActiveReports.Label
        Private WithEvents txtFiliacao As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox19 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label11 As DataDynamics.ActiveReports.Label
        Private WithEvents txtAvos As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox21 As DataDynamics.ActiveReports.TextBox
        Private WithEvents label13 As DataDynamics.ActiveReports.Label
        Private WithEvents txtGemeo As DataDynamics.ActiveReports.TextBox
        Private WithEvents label14 As DataDynamics.ActiveReports.Label
        Private WithEvents txtNomeGemeo As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox24 As DataDynamics.ActiveReports.TextBox
        Private WithEvents textBox26 As DataDynamics.ActiveReports.TextBox
        Private WithEvents txtDataRegistro As DataDynamics.ActiveReports.TextBox
        Private WithEvents label15 As DataDynamics.ActiveReports.Label

#Region " ActiveReports Designer generated code "
        'NOTE: The following procedure is required by the ActiveReports Designer
        'It can be modified using the ActiveReports Designer.
        'Do not modify it using the code editor.
        Private WithEvents Detail1 As DataDynamics.ActiveReports.Detail
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Global.Resources.CertidaoNascimento))
            Me.Detail1 = New DataDynamics.ActiveReports.Detail()
            Me.textBox26 = New DataDynamics.ActiveReports.TextBox()
            Me.textBox24 = New DataDynamics.ActiveReports.TextBox()
            Me.textBox2 = New DataDynamics.ActiveReports.TextBox()
            Me.label5 = New DataDynamics.ActiveReports.Label()
            Me.txtNascimento = New DataDynamics.ActiveReports.TextBox()
            Me.textBox1 = New DataDynamics.ActiveReports.TextBox()
            Me.label1 = New DataDynamics.ActiveReports.Label()
            Me.txtDia = New DataDynamics.ActiveReports.TextBox()
            Me.textBox5 = New DataDynamics.ActiveReports.TextBox()
            Me.label2 = New DataDynamics.ActiveReports.Label()
            Me.txtMes = New DataDynamics.ActiveReports.TextBox()
            Me.textBox7 = New DataDynamics.ActiveReports.TextBox()
            Me.label3 = New DataDynamics.ActiveReports.Label()
            Me.txtAno = New DataDynamics.ActiveReports.TextBox()
            Me.textBox9 = New DataDynamics.ActiveReports.TextBox()
            Me.label4 = New DataDynamics.ActiveReports.Label()
            Me.txtHora = New DataDynamics.ActiveReports.TextBox()
            Me.textBox11 = New DataDynamics.ActiveReports.TextBox()
            Me.label6 = New DataDynamics.ActiveReports.Label()
            Me.txtMunicipio = New DataDynamics.ActiveReports.TextBox()
            Me.textBox13 = New DataDynamics.ActiveReports.TextBox()
            Me.label7 = New DataDynamics.ActiveReports.Label()
            Me.txtMunicipioReg = New DataDynamics.ActiveReports.TextBox()
            Me.textBox15 = New DataDynamics.ActiveReports.TextBox()
            Me.label8 = New DataDynamics.ActiveReports.Label()
            Me.txtLocalNascimento = New DataDynamics.ActiveReports.TextBox()
            Me.textBox17 = New DataDynamics.ActiveReports.TextBox()
            Me.label9 = New DataDynamics.ActiveReports.Label()
            Me.txtSexo = New DataDynamics.ActiveReports.TextBox()
            Me.label12 = New DataDynamics.ActiveReports.TextBox()
            Me.label10 = New DataDynamics.ActiveReports.Label()
            Me.txtFiliacao = New DataDynamics.ActiveReports.TextBox()
            Me.textBox19 = New DataDynamics.ActiveReports.TextBox()
            Me.label11 = New DataDynamics.ActiveReports.Label()
            Me.txtAvos = New DataDynamics.ActiveReports.TextBox()
            Me.textBox21 = New DataDynamics.ActiveReports.TextBox()
            Me.label13 = New DataDynamics.ActiveReports.Label()
            Me.txtGemeo = New DataDynamics.ActiveReports.TextBox()
            Me.label14 = New DataDynamics.ActiveReports.Label()
            Me.txtNomeGemeo = New DataDynamics.ActiveReports.TextBox()
            Me.txtDataRegistro = New DataDynamics.ActiveReports.TextBox()
            Me.label15 = New DataDynamics.ActiveReports.Label()
            CType(Me.textBox26, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox24, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label5, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtNascimento, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtDia, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox5, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label2, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtMes, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox7, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label3, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtAno, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox9, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label4, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtHora, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox11, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label6, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtMunicipio, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox13, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label7, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtMunicipioReg, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox15, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label8, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtLocalNascimento, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox17, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label9, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtSexo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label12, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label10, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtFiliacao, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox19, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label11, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtAvos, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.textBox21, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label13, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtGemeo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label14, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtNomeGemeo, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.txtDataRegistro, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.label15, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'Detail1
            '
            Me.Detail1.ColumnSpacing = 0.0!
            Me.Detail1.Controls.AddRange(New DataDynamics.ActiveReports.ARControl() {Me.textBox26, Me.textBox24, Me.textBox2, Me.label5, Me.txtNascimento, Me.textBox1, Me.label1, Me.txtDia, Me.textBox5, Me.label2, Me.txtMes, Me.textBox7, Me.label3, Me.txtAno, Me.textBox9, Me.label4, Me.txtHora, Me.textBox11, Me.label6, Me.txtMunicipio, Me.textBox13, Me.label7, Me.txtMunicipioReg, Me.textBox15, Me.label8, Me.txtLocalNascimento, Me.textBox17, Me.label9, Me.txtSexo, Me.label12, Me.label10, Me.txtFiliacao, Me.textBox19, Me.label11, Me.txtAvos, Me.textBox21, Me.label13, Me.txtGemeo, Me.label14, Me.txtNomeGemeo, Me.txtDataRegistro, Me.label15})
            Me.Detail1.Height = 3.9745!
            Me.Detail1.Name = "Detail1"
            '
            'textBox26
            '
            Me.textBox26.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox26.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox26.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox26.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox26.Height = 0.4185!
            Me.textBox26.Left = 0.0!
            Me.textBox26.Name = "textBox26"
            Me.textBox26.Text = Nothing
            Me.textBox26.Top = 3.514!
            Me.textBox26.Width = 4.761!
            '
            'textBox24
            '
            Me.textBox24.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox24.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox24.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox24.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox24.Height = 0.4185!
            Me.textBox24.Left = 0.6180001!
            Me.textBox24.Name = "textBox24"
            Me.textBox24.Text = Nothing
            Me.textBox24.Top = 3.0435!
            Me.textBox24.Width = 5.869!
            '
            'textBox2
            '
            Me.textBox2.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox2.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox2.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox2.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox2.Height = 0.349!
            Me.textBox2.Left = 0.0!
            Me.textBox2.Name = "textBox2"
            Me.textBox2.Text = Nothing
            Me.textBox2.Top = 0.01!
            Me.textBox2.Width = 4.761!
            '
            'label5
            '
            Me.label5.Height = 0.1666667!
            Me.label5.HyperLink = Nothing
            Me.label5.Left = 0.04!
            Me.label5.Name = "label5"
            Me.label5.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label5.Text = "Data de nascimemnto"
            Me.label5.Top = 0.01950003!
            Me.label5.Width = 1.896!
            '
            'txtNascimento
            '
            Me.txtNascimento.Height = 0.2!
            Me.txtNascimento.Left = 0.04!
            Me.txtNascimento.Name = "txtNascimento"
            Me.txtNascimento.Style = "font-size: 12pt"
            Me.txtNascimento.Text = "texto"
            Me.txtNascimento.Top = 0.158!
            Me.txtNascimento.Width = 4.666!
            '
            'textBox1
            '
            Me.textBox1.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox1.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox1.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox1.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox1.Height = 0.348!
            Me.textBox1.Left = 4.835!
            Me.textBox1.Name = "textBox1"
            Me.textBox1.Text = Nothing
            Me.textBox1.Top = 0.011!
            Me.textBox1.Width = 0.4690003!
            '
            'label1
            '
            Me.label1.Height = 0.1666667!
            Me.label1.HyperLink = Nothing
            Me.label1.Left = 4.875!
            Me.label1.Name = "label1"
            Me.label1.Style = "font-size: 8pt; font-weight: normal; text-align: center"
            Me.label1.Text = "Dia"
            Me.label1.Top = 0.02050003!
            Me.label1.Width = 0.3749999!
            '
            'txtDia
            '
            Me.txtDia.Height = 0.2!
            Me.txtDia.Left = 4.875!
            Me.txtDia.Name = "txtDia"
            Me.txtDia.Style = "font-size: 12pt; text-align: center"
            Me.txtDia.Text = "00"
            Me.txtDia.Top = 0.159!
            Me.txtDia.Width = 0.375!
            '
            'textBox5
            '
            Me.textBox5.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox5.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox5.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox5.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox5.Height = 0.4185!
            Me.textBox5.Left = 5.385!
            Me.textBox5.Name = "textBox5"
            Me.textBox5.Text = Nothing
            Me.textBox5.Top = 0.011!
            Me.textBox5.Width = 0.4690003!
            '
            'label2
            '
            Me.label2.Height = 0.1666667!
            Me.label2.HyperLink = Nothing
            Me.label2.Left = 5.425!
            Me.label2.Name = "label2"
            Me.label2.Style = "font-size: 8pt; font-weight: normal; text-align: center"
            Me.label2.Text = "Mês"
            Me.label2.Top = 0.02049991!
            Me.label2.Width = 0.3749999!
            '
            'txtMes
            '
            Me.txtMes.Height = 0.2!
            Me.txtMes.Left = 5.425!
            Me.txtMes.Name = "txtMes"
            Me.txtMes.Style = "font-size: 12pt; text-align: center"
            Me.txtMes.Text = "00"
            Me.txtMes.Top = 0.159!
            Me.txtMes.Width = 0.375!
            '
            'textBox7
            '
            Me.textBox7.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox7.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox7.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox7.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox7.Height = 0.4185!
            Me.textBox7.Left = 5.927!
            Me.textBox7.Name = "textBox7"
            Me.textBox7.Text = Nothing
            Me.textBox7.Top = 0.00950011!
            Me.textBox7.Width = 0.563!
            '
            'label3
            '
            Me.label3.Height = 0.1666667!
            Me.label3.HyperLink = Nothing
            Me.label3.Left = 5.967!
            Me.label3.Name = "label3"
            Me.label3.Style = "font-size: 8pt; font-weight: normal; text-align: center"
            Me.label3.Text = "Ano"
            Me.label3.Top = 0.01900002!
            Me.label3.Width = 0.469!
            '
            'txtAno
            '
            Me.txtAno.Height = 0.2!
            Me.txtAno.Left = 5.967!
            Me.txtAno.Name = "txtAno"
            Me.txtAno.Style = "font-size: 12pt; text-align: center"
            Me.txtAno.Text = "0000"
            Me.txtAno.Top = 0.158!
            Me.txtAno.Width = 0.4689999!
            '
            'textBox9
            '
            Me.textBox9.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox9.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox9.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox9.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox9.Height = 0.4185!
            Me.textBox9.Left = 0.0!
            Me.textBox9.Name = "textBox9"
            Me.textBox9.Text = Nothing
            Me.textBox9.Top = 0.49!
            Me.textBox9.Width = 1.0!
            '
            'label4
            '
            Me.label4.Height = 0.1666667!
            Me.label4.HyperLink = Nothing
            Me.label4.Left = 0.04000044!
            Me.label4.Name = "label4"
            Me.label4.Style = "font-size: 8pt; font-weight: normal; text-align: center"
            Me.label4.Text = "Hora"
            Me.label4.Top = 0.4994999!
            Me.label4.Width = 0.9069996!
            '
            'txtHora
            '
            Me.txtHora.Height = 0.2!
            Me.txtHora.Left = 0.04!
            Me.txtHora.Name = "txtHora"
            Me.txtHora.Style = "font-size: 12pt; text-align: center"
            Me.txtHora.Text = "texto"
            Me.txtHora.Top = 0.638!
            Me.txtHora.Width = 0.9069996!
            '
            'textBox11
            '
            Me.textBox11.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox11.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox11.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox11.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox11.Height = 0.4185!
            Me.textBox11.Left = 1.087!
            Me.textBox11.Name = "textBox11"
            Me.textBox11.Text = Nothing
            Me.textBox11.Top = 0.491!
            Me.textBox11.Width = 5.403!
            '
            'label6
            '
            Me.label6.Height = 0.1666667!
            Me.label6.HyperLink = Nothing
            Me.label6.Left = 1.137!
            Me.label6.Name = "label6"
            Me.label6.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label6.Text = "Município de Nascimento e Unidade da Federação"
            Me.label6.Top = 0.501!
            Me.label6.Width = 2.875!
            '
            'txtMunicipio
            '
            Me.txtMunicipio.Height = 0.2!
            Me.txtMunicipio.Left = 1.137!
            Me.txtMunicipio.Name = "txtMunicipio"
            Me.txtMunicipio.Style = "font-size: 12pt; text-align: left"
            Me.txtMunicipio.Text = "texto"
            Me.txtMunicipio.Top = 0.6390001!
            Me.txtMunicipio.Width = 5.299!
            '
            'textBox13
            '
            Me.textBox13.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox13.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox13.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox13.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox13.Height = 0.4185!
            Me.textBox13.Left = 0.00000004097819!
            Me.textBox13.Name = "textBox13"
            Me.textBox13.Text = Nothing
            Me.textBox13.Top = 0.9725!
            Me.textBox13.Width = 1.55!
            '
            'label7
            '
            Me.label7.Height = 0.1666667!
            Me.label7.HyperLink = Nothing
            Me.label7.Left = 0.04!
            Me.label7.Name = "label7"
            Me.label7.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label7.Text = "Município de Registro e UF"
            Me.label7.Top = 0.9820001!
            Me.label7.Width = 1.459!
            '
            'txtMunicipioReg
            '
            Me.txtMunicipioReg.Height = 0.2!
            Me.txtMunicipioReg.Left = 0.04!
            Me.txtMunicipioReg.Name = "txtMunicipioReg"
            Me.txtMunicipioReg.Style = "font-size: 12pt"
            Me.txtMunicipioReg.Text = "texto"
            Me.txtMunicipioReg.Top = 1.111!
            Me.txtMunicipioReg.Width = 1.459!
            '
            'textBox15
            '
            Me.textBox15.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox15.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox15.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox15.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox15.Height = 0.4185!
            Me.textBox15.Left = 1.615!
            Me.textBox15.Name = "textBox15"
            Me.textBox15.Text = Nothing
            Me.textBox15.Top = 0.973!
            Me.textBox15.Width = 4.05!
            '
            'label8
            '
            Me.label8.Height = 0.1666667!
            Me.label8.HyperLink = Nothing
            Me.label8.Left = 1.643!
            Me.label8.Name = "label8"
            Me.label8.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label8.Text = "Local de Nascimento"
            Me.label8.Top = 0.992!
            Me.label8.Width = 2.396!
            '
            'txtLocalNascimento
            '
            Me.txtLocalNascimento.Height = 0.2!
            Me.txtLocalNascimento.Left = 1.643!
            Me.txtLocalNascimento.Name = "txtLocalNascimento"
            Me.txtLocalNascimento.Style = "font-size: 12pt"
            Me.txtLocalNascimento.Text = "texto"
            Me.txtLocalNascimento.Top = 1.13!
            Me.txtLocalNascimento.Width = 3.97!
            '
            'textBox17
            '
            Me.textBox17.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox17.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox17.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox17.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox17.Height = 0.4185!
            Me.textBox17.Left = 5.719!
            Me.textBox17.Name = "textBox17"
            Me.textBox17.Text = Nothing
            Me.textBox17.Top = 0.964!
            Me.textBox17.Width = 0.7680001!
            '
            'label9
            '
            Me.label9.Height = 0.1666667!
            Me.label9.HyperLink = Nothing
            Me.label9.Left = 5.75!
            Me.label9.Name = "label9"
            Me.label9.Style = "font-size: 8pt; font-weight: normal; text-align: center"
            Me.label9.Text = "Sexo"
            Me.label9.Top = 0.983!
            Me.label9.Width = 0.6860003!
            '
            'txtSexo
            '
            Me.txtSexo.Height = 0.2!
            Me.txtSexo.Left = 5.75!
            Me.txtSexo.Name = "txtSexo"
            Me.txtSexo.Style = "font-size: 12pt; text-align: center"
            Me.txtSexo.Text = "texto"
            Me.txtSexo.Top = 1.112!
            Me.txtSexo.Width = 0.6859999!
            '
            'label12
            '
            Me.label12.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.label12.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.label12.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.label12.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.label12.Height = 0.586!
            Me.label12.Left = 3.72529E-9!
            Me.label12.Name = "label12"
            Me.label12.Text = Nothing
            Me.label12.Top = 1.469!
            Me.label12.Width = 6.487!
            '
            'label10
            '
            Me.label10.Height = 0.1666667!
            Me.label10.HyperLink = Nothing
            Me.label10.Left = 0.04!
            Me.label10.Name = "label10"
            Me.label10.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label10.Text = " Filiação"
            Me.label10.Top = 1.501!
            Me.label10.Width = 1.896!
            '
            'txtFiliacao
            '
            Me.txtFiliacao.Height = 0.3649999!
            Me.txtFiliacao.Left = 0.04!
            Me.txtFiliacao.Name = "txtFiliacao"
            Me.txtFiliacao.Style = "font-size: 12pt"
            Me.txtFiliacao.Text = "texto"
            Me.txtFiliacao.Top = 1.638!
            Me.txtFiliacao.Width = 6.396!
            '
            'textBox19
            '
            Me.textBox19.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox19.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox19.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox19.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox19.Height = 0.8464999!
            Me.textBox19.Left = 3.72529E-9!
            Me.textBox19.Name = "textBox19"
            Me.textBox19.Text = Nothing
            Me.textBox19.Top = 2.1255!
            Me.textBox19.Width = 6.487!
            '
            'label11
            '
            Me.label11.Height = 0.1666667!
            Me.label11.HyperLink = Nothing
            Me.label11.Left = 0.04!
            Me.label11.Name = "label11"
            Me.label11.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label11.Text = "Avós"
            Me.label11.Top = 2.1575!
            Me.label11.Width = 1.896!
            '
            'txtAvos
            '
            Me.txtAvos.Height = 0.6249999!
            Me.txtAvos.Left = 0.04!
            Me.txtAvos.Name = "txtAvos"
            Me.txtAvos.Style = "font-size: 12pt"
            Me.txtAvos.Text = "texto"
            Me.txtAvos.Top = 2.294!
            Me.txtAvos.Width = 6.396!
            '
            'textBox21
            '
            Me.textBox21.Border.BottomStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox21.Border.LeftStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox21.Border.RightStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox21.Border.TopStyle = DataDynamics.ActiveReports.BorderLineStyle.Solid
            Me.textBox21.Height = 0.4185!
            Me.textBox21.Left = 0.0!
            Me.textBox21.Name = "textBox21"
            Me.textBox21.Text = Nothing
            Me.textBox21.Top = 3.0415!
            Me.textBox21.Width = 0.563!
            '
            'label13
            '
            Me.label13.Height = 0.1666667!
            Me.label13.HyperLink = Nothing
            Me.label13.Left = 0.04000044!
            Me.label13.Name = "label13"
            Me.label13.Style = "font-size: 8pt; font-weight: normal; text-align: center"
            Me.label13.Text = "Gêmeo"
            Me.label13.Top = 3.051!
            Me.label13.Width = 0.469!
            '
            'txtGemeo
            '
            Me.txtGemeo.Height = 0.2!
            Me.txtGemeo.Left = 0.04!
            Me.txtGemeo.Name = "txtGemeo"
            Me.txtGemeo.Style = "font-size: 12pt; text-align: center"
            Me.txtGemeo.Text = "texto"
            Me.txtGemeo.Top = 3.19!
            Me.txtGemeo.Width = 0.4689999!
            '
            'label14
            '
            Me.label14.Height = 0.1666667!
            Me.label14.HyperLink = Nothing
            Me.label14.Left = 0.668!
            Me.label14.Name = "label14"
            Me.label14.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label14.Text = "Nome e Matrícula Gêmeo"
            Me.label14.Top = 3.0535!
            Me.label14.Width = 2.875!
            '
            'txtNomeGemeo
            '
            Me.txtNomeGemeo.Height = 0.2!
            Me.txtNomeGemeo.Left = 0.668!
            Me.txtNomeGemeo.Name = "txtNomeGemeo"
            Me.txtNomeGemeo.Style = "font-size: 12pt"
            Me.txtNomeGemeo.Text = "texto"
            Me.txtNomeGemeo.Top = 3.192!
            Me.txtNomeGemeo.Width = 5.768!
            '
            'txtDataRegistro
            '
            Me.txtDataRegistro.Height = 0.2!
            Me.txtDataRegistro.Left = 0.04!
            Me.txtDataRegistro.Name = "txtDataRegistro"
            Me.txtDataRegistro.Style = "font-size: 12pt"
            Me.txtDataRegistro.Text = "texto"
            Me.txtDataRegistro.Top = 3.662!
            Me.txtDataRegistro.Width = 4.666001!
            '
            'label15
            '
            Me.label15.Height = 0.1666667!
            Me.label15.HyperLink = Nothing
            Me.label15.Left = 0.04!
            Me.label15.Name = "label15"
            Me.label15.Style = "font-size: 8pt; font-weight: normal; text-align: left"
            Me.label15.Text = "Data do registro"
            Me.label15.Top = 3.5235!
            Me.label15.Width = 1.896!
            '
            'CertidaoNascimento
            '
            Me.MasterReport = False
            Me.PageSettings.PaperHeight = 11.0!
            Me.PageSettings.PaperWidth = 8.5!
            Me.ScriptLanguage = "VB.NET"
            Me.Sections.Add(Me.Detail1)
            Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" & _
                        "l; font-size: 10pt; color: Black; ddo-char-set: 204", "Normal"))
            Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"))
            Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" & _
                        "lic", "Heading2", "Normal"))
            Me.StyleSheet.Add(New DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"))
            CType(Me.textBox26, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox24, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label5, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtNascimento, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtDia, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox5, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label2, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtMes, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox7, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label3, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtAno, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox9, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label4, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtHora, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox11, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label6, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtMunicipio, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox13, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label7, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtMunicipioReg, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox15, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label8, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtLocalNascimento, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox17, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label9, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtSexo, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label12, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label10, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtFiliacao, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox19, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label11, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtAvos, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.textBox21, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label13, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtGemeo, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label14, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtNomeGemeo, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.txtDataRegistro, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.label15, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region


        Private Sub CertidaoNascimento_ReportStart(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ReportStart
            txtFiliacao.Text = Pedido.Solicitante.Filiacao.getPais
            txtAvos.Text = Pedido.Solicitante.Filiacao.getAvos

            Me.txtNascimento.Text = WeekdayName(5)
        End Sub
    End Class

End Namespace