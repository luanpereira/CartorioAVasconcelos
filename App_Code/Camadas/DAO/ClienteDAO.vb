Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Administrativo
Imports System.Data
Imports System.Data.OleDb
Imports Infraestrutura.Utils
Imports Excecoes
Imports MySql.Data.MySqlClient

Namespace Camadas.DAO

    Public Class ClienteDAO
        Implements IClienteDAO

        Private conn As MySqlConnection
        Private cmd As MySqlCommand
        Private dr As IDataReader
        Private strSql As String
        Private adpt As IDbDataAdapter

        Private _paramSql As MySqlParameter

        'OBTÉM O USUÁRIO QUE ESTÁ NA SESSÃO
        Private session As HttpSessionState = HttpContext.Current.Session
        Private usuario As Usuario = CType(session("usuario"), Usuario)

        Public Sub New(ByVal _conn As IDbConnection)
            conn = _conn
        End Sub

        Public Function cadastrarCliente(ByVal cliente As Cliente) As Integer Implements IClienteDAO.cadastrarCliente
            Dim result As Integer

            strSql = " INSERT INTO CT01CLIENTE (CT01NOME,CT01CPF,CT01RG,FK0198NATURAL,CT01ENDERECO,FK0198CIDADEUF,CT01PROFISSAO,CT01EMAIL,CT01FONEFIXO,CT01CELULAR,CT01SEXO,CT01DATANASCIMENTO,CT01ESTADOCIVIL,CT01PAI,CT01MAE,CT01AVOPATERNO1,CT01AVOPATERNO2,CT01AVOMATERNO1,CT01AVOMATERNO2,FK0101GEMEO) "
            strSql += " VALUES('" & cliente.Nome & "'," & IIf(cliente.Cpf = String.Empty, "NULL", "'" & cliente.Cpf & "'") & ",'" & cliente.Rg & "'," & IIf(cliente.Natural.Codigo = 0, "NULL", cliente.Natural.Codigo) & ",'" & cliente.Endereco.Logradouro & "',"
            strSql += IIf(cliente.Endereco.Cidade.Codigo = 0, "NULL", cliente.Endereco.Cidade.Codigo) & ",'" & cliente.Profissao & "','" & cliente.Contato.Email & "','" & cliente.Contato.FoneResidencial & "','"
            strSql += cliente.Contato.FoneCelular & "','" & cliente.Sexo & "','" & cliente.DataNascimento & "','" & cliente.EstadoCivil & "','"
            strSql += cliente.Filiacao.NomePai & "','" & cliente.Filiacao.NomeMae & "','" & cliente.Filiacao.NomeAvoPaterno1 & "','" & cliente.Filiacao.NomeAvoPaterno2 & "','"
            strSql += cliente.Filiacao.NomeAvoMaterno1 & "','" & cliente.Filiacao.NomeAvoMaterno2 & "', " & IIf(cliente.Gemeo.Codigo > 0, cliente.Gemeo.Codigo, "NULL") & ")"

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT01", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("CLIENTE COM O CPF JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Sub atualizarCliente(ByVal cliente As Cliente) Implements IClienteDAO.atualizarCliente

            strSql = "  UPDATE CT01CLIENTE SET CT01NOME='" & cliente.Nome & "', CT01CPF=" & IIf(cliente.Cpf = String.Empty, "NULL", "'" & cliente.Cpf & "'") & ", CT01RG='" & cliente.Rg & "', "
            strSql += IIf(cliente.Natural.Codigo > 0, "FK0198NATURAL=" & cliente.Natural.Codigo & ", ", "") & " CT01ENDERECO='" & cliente.Endereco.Logradouro & "', "
            strSql += IIf(cliente.Endereco.Cidade.Codigo > 0, "FK0198CIDADEUF=" & cliente.Endereco.Cidade.Codigo & ", ", "") & " CT01PROFISSAO='" & cliente.Profissao & "', "
            strSql += " CT01EMAIL='" & cliente.Contato.Email & "',CT01FONEFIXO='" & cliente.Contato.FoneResidencial & "',CT01CELULAR='" & cliente.Contato.FoneCelular & "', "
            strSql += " CT01SEXO='" & cliente.Sexo & "',CT01DATANASCIMENTO='" & cliente.DataNascimento & "', CT01ESTADOCIVIL='" & cliente.EstadoCivil & "',CT01PAI='" & cliente.Filiacao.NomePai & "', "
            strSql += " CT01MAE='" & cliente.Filiacao.NomeMae & "',CT01AVOPATERNO1='" & cliente.Filiacao.NomeAvoPaterno1 & "',CT01AVOPATERNO2='" & cliente.Filiacao.NomeAvoPaterno2 & "', "
            strSql += " CT01AVOMATERNO1='" & cliente.Filiacao.NomeAvoMaterno1 & "',CT01AVOMATERNO2='" & cliente.Filiacao.NomeAvoMaterno2 & "', FK0101GEMEO=" & IIf(cliente.Gemeo.Codigo > 0, cliente.Gemeo.Codigo, "NULL")
            strSql += " WHERE CT01CODIGO = " & cliente.Codigo

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()

                '===========LOG===========
                Seguranca.GravarLog(usuario, "U", "CT01", strSql)
                '=========================

            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Sub

        Public Function listarCliente(ByVal c As Cliente) As DataTable Implements IClienteDAO.listarCliente
            Dim ds As New DataSet

            strSql = "  SELECT *, "
            strSql += "        CONCAT(CT01CELULAR,'/',CT01FONEFIXO) AS TELEFONE, "
            strSql += "        (SELECT CT99CODIGO FROM CT99ESTADO, CT98CIDADE WHERE FK9899ESTADO=CT99CODIGO AND CT98CODIGO=FK0198CIDADEUF) AS CODIGO_UF, "
            strSql += "        (SELECT CT99SIGLA FROM CT99ESTADO, CT98CIDADE WHERE FK9899ESTADO=CT99CODIGO AND CT98CODIGO=FK0198CIDADEUF) AS SIGLA_UF, "
            strSql += "        (SELECT CT98NOME FROM CT98CIDADE WHERE CT98CODIGO=FK0198CIDADEUF) AS CIDADE, "
            strSql += "        (SELECT CT99CODIGO FROM CT99ESTADO, CT98CIDADE WHERE FK9899ESTADO=CT99CODIGO AND CT98CODIGO=FK0198NATURAL) AS CODIGO_UF_NATURAL, "
            strSql += "        (SELECT CT99SIGLA FROM CT99ESTADO, CT98CIDADE WHERE FK9899ESTADO=CT99CODIGO AND CT98CODIGO=FK0198NATURAL) AS SIGLA_UF_NATURAL, "
            strSql += "        (SELECT CT98CODIGO FROM CT98CIDADE WHERE CT98CODIGO=FK0198NATURAL) AS CIDADE_NATURAL, "
            strSql += "        (SELECT CT98NOME FROM CT98CIDADE WHERE CT98CODIGO=FK0198NATURAL) AS NOME_CIDADE_NATURAL, "
            strSql += "        (SELECT CT01NOME FROM CT01CLIENTE WHERE CT01CODIGO=C.FK0101GEMEO) AS NOME_GEMEO "
            strSql += "    FROM CT01CLIENTE AS C "
            strSql += "   WHERE 1=1 "

            If c.Codigo > 0 Then strSql += " AND C.CT01CODIGO = " & c.Codigo

            If Not c.Nome Is Nothing AndAlso Not c.Nome.Trim = String.Empty Then strSql += " AND C.CT01NOME LIKE '%" & c.Nome & "%' "
            If Not c.Cpf Is Nothing AndAlso Not c.Cpf.Trim = String.Empty Then strSql += " AND C.CT01CPF LIKE '%" & c.Cpf & "%' "


            strSql += "  ORDER BY CT01NOME "

            Try
                adpt = DaoFactory.GetDataAdapter
                cmd = conn.CreateCommand
                cmd.CommandText = strSql
                adpt.SelectCommand = cmd
                adpt.Fill(ds)

                Return ds.Tables(0)
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Sub atualizarGemeo(ByVal gemeo1 As Dominio.Administrativo.Cliente, ByVal gemeo2 As Dominio.Administrativo.Cliente) Implements IClienteDAO.atualizarGemeo
            strSql = "  UPDATE CT01CLIENTE SET FK0101GEMEO=" & IIf(gemeo1.Codigo > 0, gemeo1.Codigo, "NULL")
            strSql += " WHERE CT01CODIGO = " & gemeo2.Codigo

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()

                '===========LOG===========
                Seguranca.GravarLog(usuario, "U", "CT01", strSql)
                '=========================

            Catch ex As UsuarioInvalidoException
                Throw New DAOException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Sub

        Public Function listarClassCliente(ByVal cliente As Dominio.Administrativo.Cliente) As Dominio.Administrativo.Cliente Implements IClienteDAO.listarClassCliente
            Dim returnCliente As Cliente = Nothing

            strSql = "  SELECT *, "
            strSql += "        CONCAT(CT01CELULAR,'/',CT01FONEFIXO) AS TELEFONE, "
            strSql += "        (SELECT CT99CODIGO FROM CT99ESTADO, CT98CIDADE WHERE FK9899ESTADO=CT99CODIGO AND CT98CODIGO=FK0198CIDADEUF) AS CODIGO_UF, "
            strSql += "        (SELECT CT99SIGLA FROM CT99ESTADO, CT98CIDADE WHERE FK9899ESTADO=CT99CODIGO AND CT98CODIGO=FK0198CIDADEUF) AS SIGLA_UF, "
            strSql += "        (SELECT CT98CODIGO FROM CT98CIDADE WHERE CT98CODIGO=FK0198CIDADEUF) AS CODIGO_CIDADE, "
            strSql += "        (SELECT CT98NOME FROM CT98CIDADE WHERE CT98CODIGO=FK0198CIDADEUF) AS CIDADE, "
            strSql += "        (SELECT CT99CODIGO FROM CT99ESTADO, CT98CIDADE WHERE FK9899ESTADO=CT99CODIGO AND CT98CODIGO=FK0198NATURAL) AS CODIGO_UF_NATURAL, "
            strSql += "        (SELECT CT99SIGLA FROM CT99ESTADO, CT98CIDADE WHERE FK9899ESTADO=CT99CODIGO AND CT98CODIGO=FK0198NATURAL) AS SIGLA_UF_NATURAL, "
            strSql += "        (SELECT CT98CODIGO FROM CT98CIDADE WHERE CT98CODIGO=FK0198NATURAL) AS CIDADE_NATURAL, "
            strSql += "        (SELECT CT98NOME FROM CT98CIDADE WHERE CT98CODIGO=FK0198NATURAL) AS NOME_CIDADE_NATURAL, "
            strSql += "        (SELECT CT01NOME FROM CT01CLIENTE WHERE CT01CODIGO=C.FK0101GEMEO) AS NOME_GEMEO "
            strSql += "    FROM CT01CLIENTE AS C "
            strSql += "   WHERE 1=1 "

            If cliente.Codigo > 0 Then strSql += " AND C.CT01CODIGO = " & cliente.Codigo

            If Not cliente.Nome Is Nothing AndAlso Not cliente.Nome.Trim = String.Empty Then strSql += " AND C.CT01NOME LIKE '%" & cliente.Nome & "%' "
            If Not cliente.Cpf Is Nothing AndAlso Not cliente.Cpf.Trim = String.Empty Then strSql += " AND C.CT01CPF LIKE '%" & cliente.Cpf & "%' "


            strSql += "  ORDER BY CT01NOME "

            Try
                cmd = conn.CreateCommand
                cmd.CommandText = strSql
                dr = cmd.ExecuteReader

                While dr.Read
                    returnCliente = New Cliente

                    returnCliente.Codigo = dr.Item("CT01CODIGO")
                    returnCliente.DataNascimento = dr.Item("CT01DATANASCIMENTO").ToString
                    returnCliente.Endereco.Logradouro = dr.Item("CT01ENDERECO").ToString
                    returnCliente.Endereco.Cidade.Codigo = IIf(dr.Item("CODIGO_CIDADE").ToString = String.Empty, 0, dr.Item("CODIGO_CIDADE").ToString)
                    returnCliente.Endereco.Cidade.Nome = dr.Item("CIDADE").ToString
                    returnCliente.Endereco.Cidade.Estado.Codigo = IIf(dr.Item("CODIGO_UF").ToString = String.Empty, 0, dr.Item("CODIGO_UF").ToString)
                    returnCliente.Endereco.Cidade.Estado.Nome = dr.Item("SIGLA_UF").ToString

                    returnCliente.Natural.Codigo = IIf(dr.Item("CIDADE_NATURAL").ToString = String.Empty, 0, dr.Item("CIDADE_NATURAL").ToString)
                    returnCliente.Natural.Nome = dr.Item("NOME_CIDADE_NATURAL").ToString
                    returnCliente.Natural.Estado.Codigo = IIf(dr.Item("CODIGO_UF_NATURAL").ToString = String.Empty, 0, dr.Item("CODIGO_UF_NATURAL").ToString)
                    returnCliente.Natural.Estado.Nome = dr.Item("SIGLA_UF_NATURAL").ToString

                    returnCliente.EstadoCivil = dr.Item("CT01ESTADOCIVIL").ToString
                    returnCliente.Filiacao.NomePai = dr.Item("CT01PAI").ToString
                    returnCliente.Filiacao.NomeMae = dr.Item("CT01MAE").ToString

                    returnCliente.Nome = dr.Item("CT01NOME").ToString
                    returnCliente.Profissao = dr.Item("CT01PROFISSAO").ToString
                    returnCliente.Sexo = dr.Item("CT01SEXO").ToString
                    
                End While

                dr.Close()

                Return returnCliente

            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class

End Namespace