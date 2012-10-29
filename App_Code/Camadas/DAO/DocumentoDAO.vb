Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Data
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

        Public Function listarDocumentosByClienteID(ByVal cliente As Dominio.Administrativo.Cliente) As System.Data.DataTable Implements IDocumentoDAO.listarDocumentosByClienteID
            Dim ds As New DataSet

            strSql = "  SELECT CT01CODIGO, CT03CODIGO, CT01NOME, CT01CPF, CT03DATA, CT02NOME, CT03VIA, CT05NOME "
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

            strSql = " INSERT INTO CT03PEDIDO (CT03CODIGO,FK0301SOLICITANTE,FK0304DOCUMENTO,CT03DATA,FK0305STATUS,CT03VIA,CT03AVERBACAO,CT03SERVENTIA,CT03ACERVO,CT03ATRIBUICAO,CT03ANOREGISTRO,CT03NUMEROLIVRO,CT03NUMEROFOLHA,CT03NUMEROTERMO,CT03DATAREGISTRO) "
            strSql += " VALUES(NULL," & pedido.Solicitante.Codigo & "," & pedido.Documento.Codigo & ",NOW(),6,1,'" & pedido.Averbacao & "'," & pedido.Matricula.Serventia & "," & pedido.Matricula.Acervo & "," & pedido.Matricula.Atribuicao & "," & pedido.Matricula.AnoRegistro & "," & pedido.Matricula.NumeroLivro & "," & pedido.Matricula.NumeroFolha & "," & pedido.Matricula.NumeroTermo & ",'" & pedido.Documento.DataRegistro & "')"

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

        Public Function listarPedidoNascimento(ByVal pedidoID As Integer) As Dominio.Documentos.Pedido Implements IDocumentoDAO.listarPedidoNascimento
            Dim pedido As Dominio.Documentos.Pedido = Nothing
            Dim nascimento As Dominio.Documentos.Nascimento
            Dim gemeo As Cliente

            strSql = "  SELECT *, CAST(CT03NUMEROFOLHA AS CHAR) NUMFOLHA, CAST(CT03NUMEROLIVRO AS CHAR) NUMLIVRO, CAST(CT03NUMEROTERMO AS CHAR) NUMTERMO, CAST(CT03ACERVO AS CHAR) NUMACERVO, CAST(CT03SERVENTIA AS CHAR) NUMSERVENTIA, "
            'strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198CIDADEUF=CT98CODIGO ) NOMECIDADE, "
            'strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198CIDADEUF=CT98CODIGO ) NOMEESTADO, "
            strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198NATURAL=CT98CODIGO ) NOMECIDADENATURAL, "
            strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198NATURAL=CT98CODIGO ) NOMEESTADONATURAL, "
            strSql += "  (SELECT GEMEO2.CT01NOME FROM CT01CLIENTE AS GEMEO2 WHERE GEMEO2.CT01CODIGO=GEMEO1.FK0101GEMEO) GEMEO "
            strSql += "   FROM CT01CLIENTE AS GEMEO1, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT08NASCIMENTO "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=GEMEO1.CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
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

                    If dr.Item("CT01DATANASCIMENTO").ToString = String.Empty Then
                        pedido.Solicitante.DataNascimento = ""
                    Else
                        pedido.Solicitante.DataNascimento = dr.Item("CT01DATANASCIMENTO").ToString.Substring(0, 10)
                    End If

                    pedido.Solicitante.Sexo = dr.Item("CT01SEXO").ToString

                    gemeo = New Cliente
                    gemeo.Nome = IIf(IsDBNull(dr.Item("GEMEO")), "NÃO", dr.Item("GEMEO"))
                    pedido.Solicitante.Gemeo = gemeo

                    pedido.Matricula.Acervo = dr.Item("NUMACERVO").ToString
                    pedido.Matricula.AnoRegistro = dr.Item("CT03ANOREGISTRO").ToString
                    pedido.Matricula.Atribuicao = dr.Item("CT03ATRIBUICAO").ToString
                    pedido.Matricula.NumeroFolha = dr.Item("NUMFOLHA").ToString
                    pedido.Matricula.NumeroLivro = dr.Item("NUMLIVRO").ToString
                    pedido.Matricula.NumeroTermo = dr.Item("NUMTERMO").ToString
                    pedido.Matricula.Serventia = dr.Item("NUMSERVENTIA").ToString
                    pedido.Matricula.TipoLivro = dr.Item("CT02DOCUMENTO").ToString

                    nascimento = New Dominio.Documentos.Nascimento
                    nascimento.Codigo = dr.Item("CT08CODIGO")
                    nascimento.Horario = dr.Item("CT08HORARIO").ToString
                    nascimento.Declarante = dr.Item("CT08DECLARANTE") 'IIf(dr.Item("CT08DECLARANTE").ToString = "P", "O Pai", "A Mãe")
                    nascimento.Maternidade = dr.Item("CT08MATERNIDADE").ToString
                    nascimento.TipoLivro = dr.Item("CT02DOCUMENTO").ToString
                    nascimento.Cidade = dr.Item("NOMECIDADENATURAL").ToString & " - " & dr.Item("NOMEESTADONATURAL").ToString
                    pedido.Documento = nascimento

                    If dr.Item("CT03DATAREGISTRO").ToString = String.Empty Then
                        pedido.Documento.DataRegistro = ""
                    Else
                        pedido.Documento.DataRegistro = dr.Item("CT03DATAREGISTRO").ToString.Substring(0, 10)
                    End If

                End While

                dr.Close()

                Return pedido

            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Sub atualizarNascimento(ByVal nascimento As Dominio.Documentos.Nascimento) Implements IDocumentoDAO.atualizarNascimento

            strSql = "  UPDATE CT08NASCIMENTO SET CT08DECLARANTE='" & nascimento.Declarante & "', "
            strSql += " CT08MATERNIDADE='" & nascimento.Maternidade & "', CT08HORARIO='" & nascimento.Horario & "' "
            strSql += " WHERE CT08CODIGO = " & nascimento.Codigo

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()

                '===========LOG===========
                Seguranca.GravarLog(usuario, "U", "CT08", strSql)
                '=========================

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
        End Sub

        Public Sub atualizarPedido(ByVal pedido As Dominio.Documentos.Pedido) Implements IDocumentoDAO.atualizarPedido

            strSql = "  UPDATE CT03PEDIDO SET FK0305STATUS=" & pedido.Status & ",CT03AVERBACAO='" & pedido.Averbacao & "',CT03SERVENTIA='" & pedido.Matricula.Serventia & "',CT03ACERVO='" & pedido.Matricula.Acervo & "',CT03ATRIBUICAO='" & pedido.Matricula.Atribuicao & "',CT03ANOREGISTRO=" & pedido.Matricula.AnoRegistro & ",CT03NUMEROLIVRO='" & pedido.Matricula.NumeroLivro & "',CT03NUMEROFOLHA='" & pedido.Matricula.NumeroFolha & "',CT03NUMEROTERMO='" & pedido.Matricula.NumeroTermo & "', "
            strSql += " CT03DATAREGISTRO='" & pedido.Documento.DataRegistro & "' "
            strSql += " WHERE CT03CODIGO = " & pedido.Codigo

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()

                '===========LOG===========
                Seguranca.GravarLog(usuario, "U", "CT03", strSql)
                '=========================

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
        End Sub

        Public Function listarPedidoCasamento(ByVal pedidoID As Integer) As Dominio.Documentos.Pedido Implements IDocumentoDAO.listarPedidoCasamento

        End Function

        Public Function listarPedidoObito(ByVal pedidoID As Integer) As Dominio.Documentos.Pedido Implements IDocumentoDAO.listarPedidoObito
            Dim pedido As Dominio.Documentos.Pedido = Nothing
            Dim obito As Dominio.Documentos.Obito

            strSql = "  SELECT *, CAST(CT03NUMEROFOLHA AS CHAR) NUMFOLHA, CAST(CT03NUMEROLIVRO AS CHAR) NUMLIVRO, CAST(CT03NUMEROTERMO AS CHAR) NUMTERMO, CAST(CT03ACERVO AS CHAR) NUMACERVO, CAST(CT03SERVENTIA AS CHAR) NUMSERVENTIA, "
            'strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198CIDADEUF=CT98CODIGO ) NOMECIDADE, "
            'strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198CIDADEUF=CT98CODIGO ) NOMEESTADO, "
            strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198NATURAL=CT98CODIGO ) NOMECIDADENATURAL, "
            strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198NATURAL=CT98CODIGO ) NOMEESTADONATURAL "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT09OBITO "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND CT09CODIGO=FK0408OBITO "
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

                    If dr.Item("CT01DATANASCIMENTO").ToString = String.Empty Then
                        pedido.Solicitante.DataNascimento = ""
                    Else
                        pedido.Solicitante.DataNascimento = dr.Item("CT01DATANASCIMENTO").ToString.Substring(0, 10)
                    End If

                    pedido.Solicitante.Sexo = dr.Item("CT01SEXO").ToString

                    pedido.Matricula.Acervo = dr.Item("NUMACERVO").ToString
                    pedido.Matricula.AnoRegistro = dr.Item("CT03ANOREGISTRO").ToString
                    pedido.Matricula.Atribuicao = dr.Item("CT03ATRIBUICAO").ToString
                    pedido.Matricula.NumeroFolha = dr.Item("NUMFOLHA").ToString
                    pedido.Matricula.NumeroLivro = dr.Item("NUMLIVRO").ToString
                    pedido.Matricula.NumeroTermo = dr.Item("NUMTERMO").ToString
                    pedido.Matricula.Serventia = dr.Item("NUMSERVENTIA").ToString
                    pedido.Matricula.TipoLivro = dr.Item("CT02DOCUMENTO").ToString

                    obito = New Dominio.Documentos.Obito
                    obito.Codigo = dr.Item("CT09CODIGO")
                    obito.Horario = dr.Item("CT09HORARIO").ToString
                    obito.Declarante = dr.Item("CT09DECLARANTE") 'IIf(dr.Item("CT08DECLARANTE").ToString = "P", "O Pai", "A Mãe")
                    obito.Local = dr.Item("CT09LOCAL").ToString
                    obito.TipoLivro = dr.Item("CT02DOCUMENTO").ToString
                    obito.Medico = dr.Item("CT09MEDICO").ToString
                    obito.Sepultamento = dr.Item("CT09SEPULTAMENTO").ToString
                    obito.CausaMorte = dr.Item("CT09CAUSAMORTE").ToString
                    obito.DataObito = dr.Item("CT09DATAOBITO").ToString
                    pedido.Documento = obito

                    If dr.Item("CT03DATAREGISTRO").ToString = String.Empty Then
                        pedido.Documento.DataRegistro = ""
                    Else
                        pedido.Documento.DataRegistro = dr.Item("CT03DATAREGISTRO").ToString.Substring(0, 10)
                    End If

                End While

                dr.Close()

                Return pedido

            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Function inserirCasamento(ByVal nascimento As Dominio.Documentos.Casamento) As Integer Implements IDocumentoDAO.inserirCasamento

        End Function

        Public Function inserirDocumentoCasamento(ByVal casamento As Dominio.Documentos.Casamento) As Integer Implements IDocumentoDAO.inserirDocumentoCasamento

        End Function

        Public Function inserirDocumentoNascimento(ByVal nascimento As Dominio.Documentos.Nascimento) As Integer Implements IDocumentoDAO.inserirDocumentoNascimento
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

        Public Function inserirDocumentoObito(ByVal obito As Dominio.Documentos.Obito) As Integer Implements IDocumentoDAO.inserirDocumentoObito
            Dim result As Integer

            strSql = " INSERT INTO CT04DOCUMENTO (CT04CODIGO,FK0402TIPODOCUMENTO,FK0408OBITO) "
            strSql += " VALUES(NULL," & obito.TipoLivro & "," & obito.Codigo & ") "

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

        Public Function inserirObito(ByVal obito As Dominio.Documentos.Obito) As Integer Implements IDocumentoDAO.inserirObito
            Dim result As Integer

            strSql = " INSERT INTO CT09OBITO (CT09CODIGO,CT09DATAOBITO,CT09LOCAL,CT09CAUSAMORTE,CT09DECLARANTE,CT09MEDICO,CT09SEPULTAMENTO) "
            strSql += " VALUES(NULL,'" & obito.DataObito & "','" & obito.Local & "','" & obito.CausaMorte & "','" & obito.Declarante & "','" & obito.Medico & "','" & obito.Sepultamento & "') "

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT09", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("ÓBITO JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Sub atualizarCasamento(ByVal casamento As Dominio.Documentos.Casamento) Implements IDocumentoDAO.atualizarCasamento

        End Sub

        Public Sub atualizarObito(ByVal obito As Dominio.Documentos.Obito) Implements IDocumentoDAO.atualizarObito
            strSql = "  UPDATE CT08NASCIMENTO SET CT09DECLARANTE='" & obito.Declarante & "', CT09LOCAL= '" & obito.Local & "', CT09CAUSAMORTE='" & obito.CausaMorte & "', "
            strSql += " CT09DATAOBITO='" & obito.DataObito & "', CT09HORARIO='" & obito.Horario & "', CT09MEDICO='" & obito.Medico & "', CT09SEPULTAMENTO='" & obito.Sepultamento & "' "
            strSql += " WHERE CT09CODIGO = " & obito.Codigo

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()

                '===========LOG===========
                Seguranca.GravarLog(usuario, "U", "CT09", strSql)
                '=========================

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("ÓBITO JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Sub
    End Class
End Namespace