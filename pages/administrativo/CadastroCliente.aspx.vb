Imports Excecoes
Imports Camadas.Negocio
Imports Infraestrutura.Utils
Imports Camadas.Dominio.Administrativo
Imports System.Data

Partial Class pages_administrativo_CadastroCliente
    Inherits System.Web.UI.Page

    Private controller As IClienteController = New ClienteController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim id As Integer
        Dim c As Cliente

        If Not IsPostBack Then
            Me.txtNome.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtEndereco.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtEmail.Attributes.Add("onkeypress", "return ValidarEntrada(event, '7')")
            Me.txtRg.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")
            Me.txtCPF.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")
            Me.txtTelefoneFixo.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")
            Me.txtCelular.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")
            Me.txtDataNascimento.Attributes.Add("onkeypress", "return ValidarEntrada(event, '1')")
            Me.txtCPF.Attributes.Add("onblur", "return ValidaCPF(this);")
            Me.txtPai.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtMae.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtAvoPaterno1.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtAvoPaterno2.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtAvoMaterno1.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtAvoMaterno2.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")
            Me.txtProfissão.Attributes.Add("onkeypress", "return ValidarEntrada(event, '3')")

            '--LISTAR ESTADOS --------------
            drpUF.DataValueField = "CT99CODIGO"
            drpUF.DataTextField = "CT99NOME"
            drpUF.DataSource = ListarEstados()
            drpUF.DataBind()
            drpUF.Items.Add(New ListItem("Selecione...", 0))
            drpUF.SelectedValue = 0

            drpCidade.Items.Add(New ListItem("Selecione o Estado...", 0))

            drpUfNatural.DataValueField = "CT99CODIGO"
            drpUfNatural.DataTextField = "CT99NOME"
            drpUfNatural.DataSource = ListarEstados()
            drpUfNatural.DataBind()
            drpUfNatural.Items.Add(New ListItem("Selecione...", 0))
            drpUfNatural.SelectedValue = 0

            drpCidadeNatural.Items.Add(New ListItem("Selecione o Estado...", 0))
            '-------------------------------

            '--LISTAR COMBO DOS GÊMEOS -------------
            c = New Cliente
            drpGemeo.DataTextField = "CT01NOME"
            drpGemeo.DataValueField = "CT01CODIGO"
            drpGemeo.DataSource = controller.listarCliente(c)
            drpGemeo.DataBind()

            drpGemeo.Items.Add(New ListItem("Não", 0))
            drpGemeo.SelectedValue = 0
            '---------------------------------------

            Try
                id = Integer.Parse(Request.QueryString("id"))
                If id > 0 Then
                    ViewState("idCliente") = id
                    Me.listarDadosCliente(id)
                Else
                    ViewState("idCliente") = 0
                End If

            Catch ex As Exception
                ViewState("idCliente") = 0
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('ERRO NO ID. " & ex.Message.Replace("'", "") & "'); history.back()", True)
            End Try

        End If

    End Sub

    Private Sub listarDadosCliente(ByVal id As Integer)
        Dim c As Camadas.Dominio.Administrativo.Cliente
        Dim dtb As DataTable

        Try
            c = New Camadas.Dominio.Administrativo.Cliente
            c.Codigo = id

            dtb = controller.listarCliente(c)

            txtNome.Text = dtb.Rows(0).Item("CT01NOME").ToString

            txtCPF.Text = dtb.Rows(0).Item("CT01CPF").ToString
            txtRg.Text = dtb.Rows(0).Item("CT01RG").ToString
            txtEndereco.Text = dtb.Rows(0).Item("CT01ENDERECO").ToString
            drpUF.SelectedValue = dtb.Rows(0).Item("CODIGO_UF").ToString
            drpUF_SelectedIndexChanged(Nothing, Nothing)
            drpCidade.SelectedValue = dtb.Rows(0).Item("FK0198CIDADEUF").ToString
            'txtDataNascimento.Text = dtb.Rows(0).Item("CT01DATANASCIMENTO").ToString
            txtTelefoneFixo.Text = dtb.Rows(0).Item("CT01FONEFIXO").ToString
            txtCelular.Text = dtb.Rows(0).Item("CT01CELULAR").ToString
            txtEmail.Text = dtb.Rows(0).Item("CT01EMAIL").ToString
            txtDataNascimento.Text = Format(DateTime.Parse(dtb.Rows(0).Item("CT01DATANASCIMENTO").ToString), "dd/MM/yyyy")

            drpEstadoCivil.SelectedValue = dtb.Rows(0).Item("CT01ESTADOCIVIL").ToString
            drpSexo.SelectedValue = dtb.Rows(0).Item("CT01SEXO").ToString
            drpUfNatural.SelectedValue = dtb.Rows(0).Item("CODIGO_UF_NATURAL").ToString
            drpUfNatural_SelectedIndexChanged(Nothing, Nothing)
            drpCidadeNatural.SelectedValue = dtb.Rows(0).Item("CIDADE_NATURAL").ToString
            txtProfissão.Text = dtb.Rows(0).Item("CT01PROFISSAO").ToString
            txtPai.Text = dtb.Rows(0).Item("CT01PAI").ToString
            txtMae.Text = dtb.Rows(0).Item("CT01MAE").ToString
            txtAvoPaterno1.Text = dtb.Rows(0).Item("CT01AVOPATERNO1").ToString
            txtAvoPaterno2.Text = dtb.Rows(0).Item("CT01AVOPATERNO2").ToString
            txtAvoMaterno1.Text = dtb.Rows(0).Item("CT01AVOMATERNO1").ToString
            txtAvoMaterno2.Text = dtb.Rows(0).Item("CT01AVOMATERNO2").ToString
            drpGemeo.SelectedValue = dtb.Rows(0).Item("FK0101GEMEO").ToString

        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try
    End Sub

    Protected Sub btnSalvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvar.Click
        Dim cliente As Camadas.Dominio.Administrativo.Cliente
        Dim gemeo As Camadas.Dominio.Administrativo.Cliente

        Try

            If txtDataNascimento.Text = String.Empty Then
                Throw New BusinessException("O CAMPO DATA DE NASCIMENTO É OBRIGATÓRIO")
            End If

            If txtNome.Text = String.Empty Then Throw New BusinessException("O CAMPO NOME É OBRIGATÓRIO.")
            If txtPai.Text = String.Empty Then Throw New BusinessException("O CAMPO Pai É OBRIGATÓRIO.")
            If txtMae.Text = String.Empty Then Throw New BusinessException("O CAMPO Mãe É OBRIGATÓRIO.")
            If drpSexo.SelectedValue = "0" Then Throw New BusinessException("O CAMPO Sexo É OBRIGATÓRIO.")
            If drpGemeo.SelectedValue = ViewState("idCliente") And ViewState("idCliente") > 0 Then Throw New BusinessException("OPERAÇÃO CANCELADA. O IRMÃO GÊMEO É O PRÓPRIO CLIENTE. VERIFIQUE")

            cliente = New Camadas.Dominio.Administrativo.Cliente
            cliente.Codigo = ViewState("idCliente")
            cliente.Nome = txtNome.Text.ToUpper
            cliente.Cpf = txtCPF.Text
            cliente.Rg = txtRg.Text
            cliente.Endereco.Logradouro = txtEndereco.Text.ToUpper
            cliente.Endereco.Cidade.Codigo = drpCidade.SelectedValue
            cliente.Endereco.Cidade.Nome = drpCidade.SelectedItem.Text
            cliente.Endereco.Cidade.Estado.Codigo = drpUF.SelectedValue
            cliente.Endereco.Cidade.Estado.Nome = drpUF.SelectedItem.Text
            cliente.Contato.FoneResidencial = txtTelefoneFixo.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "")
            cliente.Contato.FoneCelular = txtCelular.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "")
            cliente.Contato.Email = txtEmail.Text.ToLower
            cliente.DataNascimento = Format(DateTime.Parse(txtDataNascimento.Text), "yyyy-MM-dd")
            cliente.EstadoCivil = drpEstadoCivil.SelectedValue
            cliente.Sexo = drpSexo.SelectedValue
            cliente.Natural.Codigo = drpCidadeNatural.SelectedValue
            cliente.Natural.Nome = drpCidadeNatural.SelectedItem.Text
            cliente.Profissao = txtProfissão.Text.ToUpper
            cliente.Filiacao.NomePai = txtPai.Text.ToUpper
            cliente.Filiacao.NomeMae = txtMae.Text.ToUpper
            cliente.Filiacao.NomeAvoPaterno1 = txtAvoPaterno1.Text.ToUpper
            cliente.Filiacao.NomeAvoPaterno2 = txtAvoPaterno2.Text.ToUpper
            cliente.Filiacao.NomeAvoMaterno1 = txtAvoMaterno1.Text.ToUpper
            cliente.Filiacao.NomeAvoMaterno2 = txtAvoMaterno2.Text.ToUpper

            gemeo = New Cliente
            gemeo.Codigo = drpGemeo.SelectedValue
            cliente.Gemeo = gemeo

            controller.cadastrarCliente(cliente)

            If Not Session("gerenciarDocumento") Is Nothing Then
                Response.Redirect("~/pages/documentacao/Gerenciar.aspx?cliente=" & cliente.Codigo)
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('CLIENTE ATUALIZADO COM SUCESSO.'); history.back();", True)
            End If

        Catch ex As UsuarioInvalidoException
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
            Response.Redirect("~/pages/Login.aspx")
        Catch ex As BusinessException
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "Mensagem", "Mensagem('" & ex.Message.Replace("'", "") & "');", True)
        End Try

    End Sub

    Protected Sub drpUF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpUF.SelectedIndexChanged
        Try
            drpCidade.Items.Clear()
            drpCidade.DataValueField = "CT98CODIGO"
            drpCidade.DataTextField = "CT98NOME"
            drpCidade.DataSource = ListarCidades(drpUF.SelectedValue)
            drpCidade.DataBind()
            drpCidade.Items.Add(New ListItem("Selecione a Cidade...", 0))
            drpCidade.SelectedValue = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/pages/administrativo/ConsultarCliente.aspx")
    End Sub

    Protected Sub drpUfNatural_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpUfNatural.SelectedIndexChanged
        Try
            drpCidadeNatural.Items.Clear()
            drpCidadeNatural.DataValueField = "CT98CODIGO"
            drpCidadeNatural.DataTextField = "CT98NOME"
            drpCidadeNatural.DataSource = ListarCidades(drpUfNatural.SelectedValue)
            drpCidadeNatural.DataBind()
            drpCidadeNatural.Items.Add(New ListItem("Selecione a Cidade...", 0))
            drpCidadeNatural.SelectedValue = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
