Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
Imports Dominio.Documentos
Imports System.Data.OleDb
Imports Excecoes
Imports Camadas.Dominio.Administrativo

Namespace Camadas.DAO
    Public Class DocumentoDAO
        Implements IDocumentoDAO

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

        Public Sub inserirDocumentoNascimento(ByVal nascimento As Dominio.Documentos.Nascimento) Implements IDocumentoDAO.inserirDocumentoNascimento

        End Sub

        Public Function listarDocumentosByClienteID(ByVal cliente As Dominio.Administrativo.Cliente) As System.Data.DataTable Implements IDocumentoDAO.listarDocumentosByClienteID
            Dim ds As New DataSet

            strSql = "  SELECT CT03CODIGO, CT01NOME, CT01CPF, CT03DATA, CT02NOME, CT03VIA, CT05NOME "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT05STATUS "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND FK0305STATUS=CT05CODIGO "
            strSql += "    AND FK0301SOLICITANTE = " & cliente.Codigo
            strSql += "        ORDER BY CT03DATA, CT02NOME "

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

        Public Function listarTipoDocumento() As System.Data.DataTable Implements IDocumentoDAO.listarTipoDocumento
            Dim ds As New DataSet

            strSql = "  SELECT 0 CT02DOCUMENTO, 'SELECIONE' CT02NOME "
            strSql += "  UNION "
            strSql += "  SELECT CT02DOCUMENTO, CT02NOME "
            strSql += "   FROM CT02TIPODOCUMENTO "
            strSql += "  WHERE CT02ATIVO=1 "

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

        Public Function inserirDocumento(ByVal nascimento As Dominio.Documentos.Nascimento) As Integer Implements IDocumentoDAO.inserirDocumento
            Dim result As Integer

            strSql = " INSERT INTO CT04DOCUMENTO (CT04CODIGO,FK0402TIPODOCUMENTO,FK0408NASCIMENTO) "
            strSql += " VALUES(NULL," & nascimento.TipoLivro & "," & nascimento.Codigo & ") "

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT04", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("DOCUMENTO JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Function inserirNascimento(ByVal nascimento As Dominio.Documentos.Nascimento) As Integer Implements IDocumentoDAO.inserirNascimento
            Dim result As Integer

            strSql = " INSERT INTO CT08NASCIMENTO (CT08CODIGO,CT08DECLARANTE,CT08MATERNIDADE,CT08HORARIO) "
            strSql += " VALUES(NULL,'" & nascimento.Declarante & "','" & nascimento.Maternidade & "','" & nascimento.Horario & "')"

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT08", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("NASCIMENTO JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Function inserirPedido(ByVal pedido As Dominio.Documentos.Pedido) As Integer Implements IDocumentoDAO.inserirPedido
            Dim result As Integer

            strSql = " INSERT INTO CT03PEDIDO (CT03CODIGO,FK0301SOLICITANTE,FK0304DOCUMENTO,CT03DATA,FK0305STATUS,CT03VIA,CT03AVERBACAO,CT03SERVENTIA,CT03ACERVO,CT03ATRIBUICAO,CT03ANOREGISTRO,CT03NUMEROLIVRO,CT03NUMEROFOLHA,CT03NUMEROTERMO) "
            strSql += " VALUES(NULL," & pedido.Solicitante.Codigo & "," & pedido.Documento.Codigo & ",NOW(),6,1,'" & pedido.Averbacao & "'," & pedido.Matricula.Serventia & "," & pedido.Matricula.Acervo & "," & pedido.Matricula.Atribuicao & "," & pedido.Matricula.AnoRegistro & "," & pedido.Matricula.NumeroLivro & "," & pedido.Matricula.NumeroFolha & "," & pedido.Matricula.NumeroTermo & ")"

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT03", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("PEDIDO JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Function listarPedido(ByVal pedidoID As Integer) As Dominio.Documentos.Pedido Implements IDocumentoDAO.listarPedido
            Dim pedido As Dominio.Documentos.Pedido
            Dim nascimento As Dominio.Documentos.Nascimento
            Dim gemeo As Cliente

            strSql = "  SELECT * "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT05STATUS, CT98CIDADE, CT08NASCIMENTO "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND FK0305STATUS=CT05CODIGO "
            strSql += "    AND FK0198CIDADEUF=CT98CODIGO "
            strSql += "    AND CT08CODIGO=FK0408NASCIMENTO "
            strSql += "    AND CT03CODIGO = " & pedidoID
            strSql += "        ORDER BY CT03DATA, CT02NOME "

            Try
                cmd = conn.CreateCommand
                cmd.CommandText = strSql
                dr = cmd.ExecuteReader

                While dr.Read
                    pedido = New Dominio.Documentos.Pedido

                    pedido.Averbacao = dr.Item("CT03AVERBACAO").ToString
                    pedido.Solicitante.Codigo = dr.Item("FK0301SOLICITANTE").ToString
                    pedido.Solicitante.Nome = dr.Item("CT01NOME").ToString
                    pedido.Solicitante.Filiacao.NomePai = dr.Item("CT01PAI").ToString
                    pedido.Solicitante.Filiacao.NomeMae = dr.Item("CT01MAE").ToString
                    pedido.Solicitante.Filiacao.NomeAvoPaterno1 = dr.Item("CT01AVOPATERNO1").ToString
                    pedido.Solicitante.Filiacao.NomeAvoPaterno2 = dr.Item("CT01AVOPATERNO2").ToString
                    pedido.Solicitante.Filiacao.NomeAvoMaterno1 = dr.Item("CT01AVOMATERNO1").ToString
                    pedido.Solicitante.Filiacao.NomeAvoMaterno2 = dr.Item("CT01AVOMATERNO2").ToString
                    pedido.Solicitante.DataNascimento = dr.Item("CT01DATANASCIMENTO").ToString.Substring(0, 9)
                    pedido.Solicitante.Sexo = dr.Item("CT01SEXO").ToString

                    gemeo = New Cliente
                    gemeo.Nome = ""
                    pedido.Solicitante.Gemeo = gemeo

                    pedido.Matricula.Acervo = dr.Item("CT03ACERVO").ToString
                    pedido.Matricula.AnoRegistro = dr.Item("CT03ANOREGISTRO").ToString
                    pedido.Matricula.Atribuicao = dr.Item("CT03ATRIBUICAO").ToString
                    pedido.Matricula.NumeroFolha = dr.Item("CT03NUMEROFOLHA").ToString
                    pedido.Matricula.NumeroLivro = dr.Item("CT03NUMEROLIVRO").ToString
                    pedido.Matricula.NumeroTermo = dr.Item("CT03NUMEROTERMO").ToString
                    pedido.Matricula.Serventia = dr.Item("CT03SERVENTIA").ToString
                    pedido.Matricula.TipoLivro = dr.Item("CT02DOCUMENTO").ToString

                    nascimento = New Dominio.Documentos.Nascimento
                    nascimento.Horario = dr.Item("CT08HORARIO").ToString
                    nascimento.Declarante = IIf(dr.Item("CT08DECLARANTE").ToString = "P", "O Pai", "A Mãe")
                    nascimento.Maternidade = dr.Item("CT08MATERNIDADE").ToString
                    nascimento.TipoLivro = dr.Item("CT02DOCUMENTO").ToString
                    nascimento.Cidade = dr.Item("CT98NOME").ToString
                    pedido.Documento = nascimento

                    pedido.Documento.DataRegistro = dr.Item("CT03DATA").ToString.Substring(0, 9)

                End While

                dr.Close()

                Return pedido

            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function
    End Class
End Namespace