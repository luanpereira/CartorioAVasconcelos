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

            strSql = "  SELECT CT01CODIGO, CT03CODIGO, CT01NOME, CT01CPF, CT03DATACRIACAO, CT02TIPOLIVRO, CT02NOME, CT03VIA, CT05NOME "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT05STATUS "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND FK0305STATUS=CT05CODIGO "
            strSql += "    AND FK0301SOLICITANTE = " & cliente.Codigo
            strSql += "        ORDER BY CT03DATACRIACAO DESC, CT02NOME "

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

            strSql = "  SELECT 0 CT02DOCUMENTO, 0 CT02TIPOLIVRO, '... SELECIONE' CT02NOME "
            strSql += "  UNION "
            strSql += "  SELECT CT02DOCUMENTO, CT02TIPOLIVRO, CT02NOME "
            strSql += "   FROM CT02TIPODOCUMENTO "
            strSql += "  WHERE CT02ATIVO=1 "
            strSql += "     ORDER BY CT02NOME "

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

            strSql = " INSERT INTO CT03PEDIDO (CT03CODIGO,FK0301SOLICITANTE,FK0304DOCUMENTO,CT03DATAEMISSAO,FK0305STATUS,CT03VIA,CT03AVERBACAO,CT03SERVENTIA,CT03ACERVO,CT03ATRIBUICAO,CT03ANOREGISTRO,CT03NUMEROLIVRO,CT03NUMEROFOLHA,CT03NUMEROTERMO,CT03DATACRIACAO) "
            strSql += " VALUES(NULL," & pedido.Solicitante.Codigo & "," & pedido.Documento.Codigo & ","

            If Not pedido.DataEmissao Is Nothing Then
                strSql += "'" & pedido.DataEmissao & "',"
            Else
                strSql += "null,"
            End If

            strSql += "6,1,"

            If Not pedido.Averbacao Is Nothing Then
                strSql += "'" & pedido.Averbacao.ToUpper & "',"
            Else
                strSql += "null,"
            End If

            If Not pedido.Matricula.Serventia Is Nothing Then
                strSql += pedido.Matricula.Serventia & ","
            Else
                strSql += "null,"
            End If

            If Not pedido.Matricula.Acervo Is Nothing Then
                strSql += pedido.Matricula.Acervo & ","
            Else
                strSql += "null,"
            End If

            If Not pedido.Matricula.Atribuicao Is Nothing Then
                strSql += pedido.Matricula.Atribuicao & ","
            Else
                strSql += "null,"
            End If

            strSql += pedido.Matricula.AnoRegistro & ","

            If Not pedido.Matricula.NumeroLivro Is Nothing Then
                strSql += pedido.Matricula.NumeroLivro & ","
            Else
                strSql += "null,"
            End If

            If Not pedido.Matricula.NumeroFolha Is Nothing Then
                strSql += pedido.Matricula.NumeroFolha & ","
            Else
                strSql += "null,"
            End If

            If Not pedido.Matricula.NumeroTermo Is Nothing Then
                strSql += pedido.Matricula.NumeroTermo & ","
            Else
                strSql += "null,"
            End If

            strSql += "now())"

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
            strSql += "        ORDER BY CT03DATAEMISSAO, CT02NOME "

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

                    If dr.Item("CT03DATAEMISSAO").ToString = String.Empty Then
                        pedido.DataEmissao = ""
                    Else
                        pedido.DataEmissao = dr.Item("CT03DATAEMISSAO").ToString.Substring(0, 10)
                    End If

                    If dr.Item("CT04DATAREGISTRO").ToString = String.Empty Then
                        pedido.Documento.DataRegistro = ""
                    Else
                        pedido.Documento.DataRegistro = dr.Item("CT04DATAREGISTRO").ToString.Substring(0, 10)
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

            strSql = "  UPDATE CT03PEDIDO SET FK0305STATUS=" & pedido.Status & ",CT03AVERBACAO='"

            If pedido.Averbacao Is Nothing Then
                strSql += ""
            Else
                strSql += pedido.Averbacao.ToUpper()
            End If

            strSql += "',CT03SERVENTIA='" & IIf(pedido.Matricula.Serventia Is Nothing, "", pedido.Matricula.Serventia)
            strSql += "',CT03ACERVO='" & IIf(pedido.Matricula.Acervo Is Nothing, "", pedido.Matricula.Acervo) & "',CT03ATRIBUICAO='" & IIf(pedido.Matricula.Atribuicao Is Nothing, "", pedido.Matricula.Atribuicao) & "',CT03ANOREGISTRO=" & pedido.Matricula.AnoRegistro
            strSql += ",CT03NUMEROLIVRO='" & IIf(pedido.Matricula.NumeroLivro Is Nothing, "", pedido.Matricula.NumeroLivro) & "',CT03NUMEROFOLHA='" & IIf(pedido.Matricula.NumeroFolha Is Nothing, "", pedido.Matricula.NumeroFolha)
            strSql += "',CT03NUMEROTERMO='" & IIf(pedido.Matricula.NumeroTermo Is Nothing, "", pedido.Matricula.NumeroTermo) & "', "
            strSql += " CT03DATAEMISSAO='" & pedido.DataEmissao & "' "
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
            Dim pedido As Dominio.Documentos.Pedido = Nothing
            Dim casamento As Dominio.Documentos.Casamento

            strSql = "  SELECT *, CAST(CT03NUMEROFOLHA AS CHAR) NUMFOLHA, CAST(CT03NUMEROLIVRO AS CHAR) NUMLIVRO, CAST(CT03NUMEROTERMO AS CHAR) NUMTERMO, CAST(CT03ACERVO AS CHAR) NUMACERVO, CAST(CT03SERVENTIA AS CHAR) NUMSERVENTIA, "

            strSql += "  (SELECT CT06CODIGO FROM CT06CONJUGE WHERE FK0706CASAL=CT06CODIGO ) CASALID, "
            strSql += "  (SELECT CT01CODIGO FROM CT06CONJUGE,CT01CLIENTE WHERE FK0706CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE1 ) CODIGOCONJUGE1, "
            strSql += "  (SELECT CT01NOME FROM CT06CONJUGE,CT01CLIENTE WHERE FK0706CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE1 ) NOMECONJUGE1, "
            strSql += "  (SELECT CT01CODIGO FROM CT06CONJUGE,CT01CLIENTE WHERE FK0706CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE2 ) CODIGOCONJUGE2, "
            strSql += "  (SELECT CT01NOME FROM CT06CONJUGE,CT01CLIENTE WHERE FK0706CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE2 ) NOMECONJUGE2,"

            strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198NATURAL=CT98CODIGO ) NOMECIDADENATURAL, "
            strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198NATURAL=CT98CODIGO ) NOMEESTADONATURAL "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT07CASAMENTO "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND CT07CODIGO=FK0407CASAMENTO "
            strSql += "    AND CT03CODIGO = " & pedidoID
            strSql += "        ORDER BY CT03DATAEMISSAO, CT02NOME "

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
                    pedido.Solicitante.Cpf = dr.Item("CT01CPF").ToString
                    pedido.Solicitante.Rg = dr.Item("CT01RG").ToString
                    pedido.Solicitante.Natural.Nome = dr.Item("NOMECIDADENATURAL").ToString
                    pedido.Solicitante.Natural.Estado.Nome = dr.Item("NOMEESTADONATURAL").ToString
                    pedido.Solicitante.EstadoCivil = dr.Item("CT01ESTADOCIVIL").ToString

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

                    casamento = New Dominio.Documentos.Casamento
                    casamento.Codigo = dr.Item("CT07CODIGO")
                    casamento.NovoNomeConjuge1 = dr.Item("CT07NOVONOMECONJUGE1").ToString
                    casamento.NovoNomeConjuge2 = dr.Item("CT07NOVONOMECONJUGE2").ToString
                    casamento.Regime = dr.Item("CT07REGIME").ToString

                    casamento.Casal.Codigo = dr.Item("CASALID").ToString
                    casamento.Casal.Conjuge1.Codigo = dr.Item("CODIGOCONJUGE1").ToString
                    casamento.Casal.Conjuge1.Nome = dr.Item("NOMECONJUGE1").ToString

                    casamento.Casal.Conjuge2.Codigo = dr.Item("CODIGOCONJUGE2").ToString
                    casamento.Casal.Conjuge2.Nome = dr.Item("NOMECONJUGE2").ToString

                    casamento.TipoLivro = dr.Item("CT02DOCUMENTO").ToString

                    pedido.Documento = casamento

                    If dr.Item("CT03DATAEMISSAO").ToString = String.Empty Then
                        pedido.DataEmissao = ""
                    Else
                        pedido.DataEmissao = dr.Item("CT03DATAEMISSAO").ToString.Substring(0, 10)
                    End If

                    If dr.Item("CT04DATAREGISTRO").ToString = String.Empty Then
                        pedido.Documento.DataRegistro = ""
                    Else
                        pedido.Documento.DataRegistro = dr.Item("CT04DATAREGISTRO").ToString.Substring(0, 10)
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

        Public Function listarPedidoObito(ByVal pedidoID As Integer) As Dominio.Documentos.Pedido Implements IDocumentoDAO.listarPedidoObito
            Dim pedido As Dominio.Documentos.Pedido = Nothing
            Dim obito As Dominio.Documentos.Obito

            strSql = "  SELECT *, CAST(CT03NUMEROFOLHA AS CHAR) NUMFOLHA, CAST(CT03NUMEROLIVRO AS CHAR) NUMLIVRO, CAST(CT03NUMEROTERMO AS CHAR) NUMTERMO, CAST(CT03ACERVO AS CHAR) NUMACERVO, CAST(CT03SERVENTIA AS CHAR) NUMSERVENTIA, "
            'strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198CIDADEUF=CT98CODIGO ) NOMECIDADE, "
            'strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198CIDADEUF=CT98CODIGO ) NOMEESTADO, "
            strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198NATURAL=CT98CODIGO ) NOMECIDADENATURAL, "
            strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198NATURAL=CT98CODIGO ) NOMEESTADONATURAL "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT09OBITO, CT12COR "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND CT09CODIGO=FK0409OBITO "
            strSql += "    AND CT12CODIGO=FK0912COR "
            strSql += "    AND CT03CODIGO = " & pedidoID
            strSql += "        ORDER BY CT03DATAEMISSAO, CT02NOME "

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
                    pedido.Solicitante.Cpf = dr.Item("CT01CPF").ToString
                    pedido.Solicitante.Rg = dr.Item("CT01RG").ToString
                    pedido.Solicitante.Natural.Nome = dr.Item("NOMECIDADENATURAL").ToString
                    pedido.Solicitante.Natural.Estado.Nome = dr.Item("NOMEESTADONATURAL").ToString
                    pedido.Solicitante.EstadoCivil = dr.Item("CT01ESTADOCIVIL").ToString

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
                    obito.DataObito = dr.Item("CT09DATAOBITO").ToString.Substring(0, 10)
                    obito.Cor.Codigo = dr.Item("CT12CODIGO").ToString
                    obito.Cor.Nome = dr.Item("CT12NOME").ToString
                    pedido.Documento = obito

                    If dr.Item("CT03DATAEMISSAO").ToString = String.Empty Then
                        pedido.DataEmissao = ""
                    Else
                        pedido.DataEmissao = dr.Item("CT03DATAEMISSAO").ToString.Substring(0, 10)
                    End If

                    If dr.Item("CT04DATAREGISTRO").ToString = String.Empty Then
                        pedido.Documento.DataRegistro = ""
                    Else
                        pedido.Documento.DataRegistro = dr.Item("CT04DATAREGISTRO").ToString.Substring(0, 10)
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

        Public Function inserirCasamento(ByVal casamento As Dominio.Documentos.Casamento) As Integer Implements IDocumentoDAO.inserirCasamento
            Dim result As Integer

            strSql = " INSERT INTO CT07CASAMENTO (CT07CODIGO,CT07NOVONOMECONJUGE1,CT07NOVONOMECONJUGE2,CT07REGIME,FK0706CASAL) "
            strSql += " VALUES(NULL,'" & casamento.NovoNomeConjuge1.ToUpper & "','" & casamento.NovoNomeConjuge2.ToUpper & "'," & casamento.Regime & "," & casamento.Casal.Codigo & ") "

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT07", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("CASAMENTO JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Function inserirDocumentoCasamento(ByVal casamento As Dominio.Documentos.Casamento) As Integer Implements IDocumentoDAO.inserirDocumentoCasamento
            Dim result As Integer

            strSql = " INSERT INTO CT04DOCUMENTO (CT04CODIGO,FK0402TIPODOCUMENTO,FK0407CASAMENTO,CT04DATAREGISTRO) "
            strSql += " VALUES(NULL," & casamento.TipoLivro & "," & casamento.Codigo & ",'" & casamento.DataRegistro & "') "

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

        Public Function inserirDocumentoNascimento(ByVal nascimento As Dominio.Documentos.Nascimento) As Integer Implements IDocumentoDAO.inserirDocumentoNascimento
            Dim result As Integer

            strSql = " INSERT INTO CT04DOCUMENTO (CT04CODIGO,FK0402TIPODOCUMENTO,FK0408NASCIMENTO,CT04DATAREGISTRO) "
            strSql += " VALUES(NULL," & nascimento.TipoLivro & "," & nascimento.Codigo & ",'" & nascimento.DataRegistro & "') "

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

            strSql = " INSERT INTO CT04DOCUMENTO (CT04CODIGO,FK0402TIPODOCUMENTO,FK0409OBITO,CT04DATAREGISTRO) "
            strSql += " VALUES(NULL," & obito.TipoLivro & "," & obito.Codigo & ",'" & obito.DataRegistro & "') "

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

            strSql = " INSERT INTO CT09OBITO (CT09CODIGO,CT09DATAOBITO,CT09LOCAL,CT09CAUSAMORTE,CT09DECLARANTE,CT09MEDICO,CT09SEPULTAMENTO,CT09HORARIO,FK0912COR) "
            strSql += " VALUES(NULL,'" & obito.DataObito & "','" & obito.Local & "','" & obito.CausaMorte & "','" & obito.Declarante & "','" & obito.Medico & "','" & obito.Sepultamento & "','" & obito.Horario & "'," & obito.Cor.Codigo & ") "

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
            strSql = "  UPDATE CT07CASAMENTO SET CT07NOVONOMECONJUGE1 ='" & casamento.NovoNomeConjuge1.ToUpper & "', CT07NOVONOMECONJUGE2 = '" & casamento.NovoNomeConjuge2.ToUpper & "', CT07REGIME =" & casamento.Regime & " "
            strSql += " WHERE CT07CODIGO = " & casamento.Codigo

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()

                '===========LOG===========
                Seguranca.GravarLog(usuario, "U", "CT07", strSql)
                '=========================

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("CASAMENTO JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Sub

        Public Sub atualizarObito(ByVal obito As Dominio.Documentos.Obito) Implements IDocumentoDAO.atualizarObito
            strSql = "  UPDATE CT09OBITO SET CT09DECLARANTE='" & obito.Declarante & "', CT09LOCAL= '" & obito.Local & "', CT09CAUSAMORTE='" & obito.CausaMorte & "', FK0912COR=" & obito.Cor.Codigo & ", "
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

        Public Function listarCor() As System.Data.DataTable Implements IDocumentoDAO.listarCor
            Dim ds As New DataSet

            strSql = "  SELECT 0 CT12CODIGO, 'SELECIONE' CT12NOME "
            strSql += "  UNION "
            strSql += "  SELECT CT12CODIGO, CT12NOME "
            strSql += "   FROM CT12COR "

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

        Public Function inserirCasal(ByVal casal As Dominio.Documentos.Casal) As Integer Implements IDocumentoDAO.inserirCasal
            Dim result As Integer

            strSql = " INSERT INTO CT06CONJUGE (CT06CODIGO,FK0601CONJUGE1,FK0601CONJUGE2) "
            strSql += " VALUES(NULL," & casal.Conjuge1.Codigo & "," & casal.Conjuge2.Codigo & ") "

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT06", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("CASAL JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Function listarDocumentos() As System.Data.DataTable Implements IDocumentoDAO.listarDocumentos
            Dim ds As New DataSet

            strSql = "  SELECT CT01CODIGO, CT03CODIGO, CT01NOME, CT01CPF, CT03DATACRIACAO, CT02TIPOLIVRO, CT02NOME, CT03VIA, CT05NOME "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT05STATUS "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND FK0305STATUS=CT05CODIGO "
            strSql += "        ORDER BY CT03DATACRIACAO DESC, CT02NOME LIMIT 16 "

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

        Public Sub atualizarCancelamentoNascimento(ByVal cancelNascimento As Dominio.Documentos.CancelamentoNascimento) Implements IDocumentoDAO.atualizarCancelamentoNascimento

            strSql = "  UPDATE CT10CANCELAMENTONASCIMENTO SET CT10MOTIVO='" & cancelNascimento.Motivo & "', "
            strSql += " CT10DATADO='" & cancelNascimento.Datado & "', CT10FOLHAS ='" & cancelNascimento.NumeroFolha & "', "
            strSql += " CT10LIVRO='" & cancelNascimento.NumeroLivro & "', CT10TERMO ='" & cancelNascimento.NumeroTermo & "' "
            strSql += " WHERE CT10CODIGO = " & cancelNascimento.Codigo

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()

                '===========LOG===========
                Seguranca.GravarLog(usuario, "U", "CT10", strSql)
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

        Public Function inserirCancelamentoNascimento(ByVal cancelNascimento As Dominio.Documentos.CancelamentoNascimento) As Integer Implements IDocumentoDAO.inserirCancelamentoNascimento
            Dim result As Integer

            strSql = " INSERT INTO CT10CANCELAMENTONASCIMENTO (CT10CODIGO,CT10MOTIVO,CT10DATADO,CT10FOLHAS,CT10LIVRO,CT10TERMO) "
            strSql += " VALUES(NULL,'" & cancelNascimento.Motivo & "','" & cancelNascimento.Datado & "'," & cancelNascimento.NumeroFolha & "," & cancelNascimento.NumeroLivro & "," & cancelNascimento.NumeroTermo & ")"

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT10", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("CANCELAMENTO DE REGISTRO DE NASCIMENTO JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Function inserirDocumentoCancelamentoNascimento(ByVal cancelNascimento As Dominio.Documentos.CancelamentoNascimento) As Integer Implements IDocumentoDAO.inserirDocumentoCancelamentoNascimento
            Dim result As Integer

            strSql = " INSERT INTO CT04DOCUMENTO (CT04CODIGO,FK0402TIPODOCUMENTO,FK0410CANCELAMENTONASC,CT04DATAREGISTRO) "
            strSql += " VALUES(NULL," & cancelNascimento.TipoLivro & "," & cancelNascimento.Codigo & ",'" & cancelNascimento.DataRegistro & "') "

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

        Public Function listarPedidoCancelamentoNascimento(ByVal pedidoID As Integer) As Dominio.Documentos.Pedido Implements IDocumentoDAO.listarPedidoCancelamentoNascimento
            Dim pedido As Dominio.Documentos.Pedido = Nothing
            Dim nascimento As Dominio.Documentos.CancelamentoNascimento

            strSql = "  SELECT *, CAST(CT10FOLHAS AS CHAR) NUMFOLHA, CAST(CT10LIVRO AS CHAR) NUMLIVRO, CAST(CT10TERMO AS CHAR) NUMTERMO, CAST(CT03ACERVO AS CHAR) NUMACERVO, CAST(CT03SERVENTIA AS CHAR) NUMSERVENTIA, "
            'strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198CIDADEUF=CT98CODIGO ) NOMECIDADE, "
            'strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198CIDADEUF=CT98CODIGO ) NOMEESTADO, "
            strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198NATURAL=CT98CODIGO ) NOMECIDADENATURAL, "
            strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198NATURAL=CT98CODIGO ) NOMEESTADONATURAL "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT10CANCELAMENTONASCIMENTO "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND CT10CODIGO=FK0410CANCELAMENTONASC "
            strSql += "    AND CT03CODIGO = " & pedidoID
            strSql += "        ORDER BY CT03DATAEMISSAO, CT02NOME "

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

                    pedido.Matricula.Acervo = dr.Item("NUMACERVO").ToString
                    pedido.Matricula.AnoRegistro = dr.Item("CT03ANOREGISTRO").ToString
                    pedido.Matricula.Atribuicao = dr.Item("CT03ATRIBUICAO").ToString
                    pedido.Matricula.NumeroFolha = dr.Item("NUMFOLHA").ToString
                    pedido.Matricula.NumeroLivro = dr.Item("NUMLIVRO").ToString
                    pedido.Matricula.NumeroTermo = dr.Item("NUMTERMO").ToString
                    pedido.Matricula.Serventia = dr.Item("NUMSERVENTIA").ToString
                    pedido.Matricula.TipoLivro = dr.Item("CT02DOCUMENTO").ToString

                    nascimento = New Dominio.Documentos.CancelamentoNascimento
                    nascimento.Codigo = dr.Item("CT10CODIGO")
                    nascimento.NumeroFolha = dr.Item("NUMFOLHA") 'IIf(dr.Item("CT08DECLARANTE").ToString = "P", "O Pai", "A Mãe")
                    nascimento.NumeroLivro = dr.Item("NUMLIVRO").ToString
                    nascimento.NumeroTermo = dr.Item("NUMTERMO").ToString
                    nascimento.TipoLivro = dr.Item("CT02DOCUMENTO").ToString
                    nascimento.Datado = dr.Item("CT10DATADO").ToString
                    nascimento.Motivo = dr.Item("CT10MOTIVO").ToString
                    pedido.Documento = nascimento

                    If dr.Item("CT03DATAEMISSAO").ToString = String.Empty Then
                        pedido.DataEmissao = ""
                    Else
                        pedido.DataEmissao = dr.Item("CT03DATAEMISSAO").ToString.Substring(0, 10)
                    End If

                    If dr.Item("CT04DATAREGISTRO").ToString = String.Empty Then
                        pedido.Documento.DataRegistro = ""
                    Else
                        pedido.Documento.DataRegistro = dr.Item("CT04DATAREGISTRO").ToString.Substring(0, 10)
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

        Public Sub atualizarCasamentoReligioso(ByVal casamento As Dominio.Documentos.CasamentoReligioso) Implements IDocumentoDAO.atualizarCasamentoReligioso
            strSql = "  UPDATE CT13CASAMENTORELIGIOSO SET CT13NOVONOMECONJUGE1 ='" & casamento.NovoNomeConjuge1.ToUpper & "', CT13NOVONOMECONJUGE2 = '" & casamento.NovoNomeConjuge2.ToUpper & "', CT13REGIME =" & casamento.Regime & " "
            strSql += " WHERE CT13CODIGO = " & casamento.Codigo

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()

                '===========LOG===========
                Seguranca.GravarLog(usuario, "U", "CT13", strSql)
                '=========================

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("CASAMENTO RELIGIOSO JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Sub

        Public Function inserirCasamentoReligioso(ByVal casamento As Dominio.Documentos.CasamentoReligioso) As Integer Implements IDocumentoDAO.inserirCasamentoReligioso
            Dim result As Integer

            strSql = " INSERT INTO CT13CASAMENTORELIGIOSO (CT13CODIGO,CT13NOVONOMECONJUGE1,CT13NOVONOMECONJUGE2,CT13REGIME,FK1306CASAL) "
            strSql += " VALUES(NULL,'" & casamento.NovoNomeConjuge1.ToUpper & "','" & casamento.NovoNomeConjuge2.ToUpper & "'," & casamento.Regime & "," & casamento.Casal.Codigo & ") "

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT13", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("CASAMENTO RELIGIOSO JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Function inserirDocumentoCasamentoReligioso(ByVal casamento As Dominio.Documentos.CasamentoReligioso) As Integer Implements IDocumentoDAO.inserirDocumentoCasamentoReligioso
            Dim result As Integer

            strSql = " INSERT INTO CT04DOCUMENTO (CT04CODIGO,FK0402TIPODOCUMENTO,FK0413CASAMENTORELIGIOSO,CT04DATAREGISTRO) "
            strSql += " VALUES(NULL," & casamento.TipoLivro & "," & casamento.Codigo & ",'" & casamento.DataRegistro & "') "

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

        Public Function listarPedidoCasamentoReligioso(ByVal pedidoID As Integer) As Dominio.Documentos.Pedido Implements IDocumentoDAO.listarPedidoCasamentoReligioso
            Dim pedido As Dominio.Documentos.Pedido = Nothing
            Dim casamento As Dominio.Documentos.CasamentoReligioso

            strSql = "  SELECT *, CAST(CT03NUMEROFOLHA AS CHAR) NUMFOLHA, CAST(CT03NUMEROLIVRO AS CHAR) NUMLIVRO, CAST(CT03NUMEROTERMO AS CHAR) NUMTERMO, CAST(CT03ACERVO AS CHAR) NUMACERVO, CAST(CT03SERVENTIA AS CHAR) NUMSERVENTIA, "

            strSql += "  (SELECT CT06CODIGO FROM CT06CONJUGE WHERE FK1306CASAL=CT06CODIGO ) CASALID, "
            strSql += "  (SELECT CT01CODIGO FROM CT06CONJUGE,CT01CLIENTE WHERE FK1306CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE1 ) CODIGOCONJUGE1, "
            strSql += "  (SELECT CT01NOME FROM CT06CONJUGE,CT01CLIENTE WHERE FK1306CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE1 ) NOMECONJUGE1, "
            strSql += "  (SELECT CT01CODIGO FROM CT06CONJUGE,CT01CLIENTE WHERE FK1306CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE2 ) CODIGOCONJUGE2, "
            strSql += "  (SELECT CT01NOME FROM CT06CONJUGE,CT01CLIENTE WHERE FK1306CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE2 ) NOMECONJUGE2,"

            strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198NATURAL=CT98CODIGO ) NOMECIDADENATURAL, "
            strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198NATURAL=CT98CODIGO ) NOMEESTADONATURAL "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT13CASAMENTORELIGIOSO "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND CT13CODIGO=FK0413CASAMENTORELIGIOSO "
            strSql += "    AND CT03CODIGO = " & pedidoID
            strSql += "        ORDER BY CT03DATAEMISSAO, CT02NOME "

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
                    pedido.Solicitante.Cpf = dr.Item("CT01CPF").ToString
                    pedido.Solicitante.Rg = dr.Item("CT01RG").ToString
                    pedido.Solicitante.Natural.Nome = dr.Item("NOMECIDADENATURAL").ToString
                    pedido.Solicitante.Natural.Estado.Nome = dr.Item("NOMEESTADONATURAL").ToString
                    pedido.Solicitante.EstadoCivil = dr.Item("CT01ESTADOCIVIL").ToString

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

                    casamento = New Dominio.Documentos.CasamentoReligioso
                    casamento.Codigo = dr.Item("CT13CODIGO")
                    casamento.NovoNomeConjuge1 = dr.Item("CT13NOVONOMECONJUGE1").ToString
                    casamento.NovoNomeConjuge2 = dr.Item("CT13NOVONOMECONJUGE2").ToString
                    casamento.Regime = dr.Item("CT13REGIME").ToString

                    casamento.Casal.Codigo = dr.Item("CASALID").ToString
                    casamento.Casal.Conjuge1.Codigo = dr.Item("CODIGOCONJUGE1").ToString
                    casamento.Casal.Conjuge1.Nome = dr.Item("NOMECONJUGE1").ToString

                    casamento.Casal.Conjuge2.Codigo = dr.Item("CODIGOCONJUGE2").ToString
                    casamento.Casal.Conjuge2.Nome = dr.Item("NOMECONJUGE2").ToString

                    casamento.TipoLivro = dr.Item("CT02DOCUMENTO").ToString

                    pedido.Documento = casamento

                    If dr.Item("CT03DATAEMISSAO").ToString = String.Empty Then
                        pedido.DataEmissao = ""
                    Else
                        pedido.DataEmissao = dr.Item("CT03DATAEMISSAO").ToString.Substring(0, 10)
                    End If

                    If dr.Item("CT04DATAREGISTRO").ToString = String.Empty Then
                        pedido.Documento.DataRegistro = ""
                    Else
                        pedido.Documento.DataRegistro = dr.Item("CT04DATAREGISTRO").ToString.Substring(0, 10)
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

        Public Sub atualizarHabilitacao(ByVal casamento As Dominio.Documentos.Habilitacao) Implements IDocumentoDAO.atualizarHabilitacao
            strSql = "  UPDATE CT11HABILITACAO SET CT11NOVONOMECONJUGE1 ='" & casamento.NovoNomeConjuge1.ToUpper & "', CT11NOVONOMECONJUGE2 = '" & casamento.NovoNomeConjuge2.ToUpper & "', CT11REGIME =" & casamento.Regime & " "
            strSql += " WHERE CT11CODIGO = " & casamento.Codigo

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()

                '===========LOG===========
                Seguranca.GravarLog(usuario, "U", "CT11", strSql)
                '=========================

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("HABILITACAO JÁ CADASTRADA NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Sub

        Public Sub atualizarProclamas(ByVal casamento As Dominio.Documentos.Proclamas) Implements IDocumentoDAO.atualizarProclamas
            strSql = "  UPDATE CT14PROCLAMAS SET CT14NUMEROEDITAL = " & casamento.Edital.Numero & ", CT14ANOEDITAL = " & casamento.Edital.Ano & ""
            strSql += " WHERE CT14CODIGO = " & casamento.Codigo

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()

                '===========LOG===========
                Seguranca.GravarLog(usuario, "U", "CT14", strSql)
                '=========================

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("PROCLAMAS JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Sub

        Public Function inserirDocumentoHabilitacao(ByVal casamento As Dominio.Documentos.Habilitacao) As Integer Implements IDocumentoDAO.inserirDocumentoHabilitacao
            Dim result As Integer

            strSql = " INSERT INTO CT04DOCUMENTO (CT04CODIGO,FK0402TIPODOCUMENTO,FK0411HABILITACAO,CT04DATAREGISTRO) "
            strSql += " VALUES(NULL," & casamento.TipoLivro & "," & casamento.Codigo & ",'" & casamento.DataRegistro & "') "

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

        Public Function inserirDocumentoProclamas(ByVal casamento As Dominio.Documentos.Proclamas) As Integer Implements IDocumentoDAO.inserirDocumentoProclamas
            Dim result As Integer

            strSql = " INSERT INTO CT04DOCUMENTO (CT04CODIGO,FK0402TIPODOCUMENTO,FK0414PROCLAMAS,CT04DATAREGISTRO) "
            strSql += " VALUES(NULL," & casamento.TipoLivro & "," & casamento.Codigo & ",'" & casamento.DataRegistro & "') "

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

        Public Function inserirHabilitacao(ByVal casamento As Dominio.Documentos.Habilitacao) As Integer Implements IDocumentoDAO.inserirHabilitacao
            Dim result As Integer

            strSql = " INSERT INTO CT11HABILITACAO (CT11CODIGO,CT11NOVONOMECONJUGE1,CT11NOVONOMECONJUGE2,CT11REGIME,FK1106CASAL) "
            strSql += " VALUES(NULL,'" & casamento.NovoNomeConjuge1.ToUpper & "','" & casamento.NovoNomeConjuge2.ToUpper & "'," & casamento.Regime & "," & casamento.Casal.Codigo & ") "

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT11", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("HABILITACAO JÁ CADASTRADA NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Function inserirProclamas(ByVal casamento As Dominio.Documentos.Proclamas) As Integer Implements IDocumentoDAO.inserirProclamas
            Dim result As Integer

            strSql = " INSERT INTO CT14PROCLAMAS (CT14CODIGO,CT14NUMEROEDITAL,CT14ANOEDITAL,FK1406CASAL) "
            strSql += " VALUES(NULL," & casamento.Edital.Numero & "," & casamento.Edital.Ano & "," & casamento.Casal.Codigo & ") "

            Try
                cmd = conn.CreateCommand
                cmd.Transaction = DaoFactory.GetCurrentTransaction
                cmd.CommandText = strSql
                cmd.ExecuteNonQuery()
                result = cmd.LastInsertedId

                '===========LOG===========
                Seguranca.GravarLog(usuario, "I", "CT14", strSql)
                '=========================

                Return result

            Catch ex As UsuarioInvalidoException
                Throw New UsuarioInvalidoException(ex.Message)
            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As MySqlException
                If ex.Number = 1062 Then
                    Throw New DAOException("PROCLAMAS JÁ CADASTRADO NO SISTEMA!")
                Else
                    Throw New DAOException(ex.Message)
                End If
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function

        Public Function listarPedidoHabilitacao(ByVal pedidoID As Integer) As Dominio.Documentos.Pedido Implements IDocumentoDAO.listarPedidoHabilitacao
            Dim pedido As Dominio.Documentos.Pedido = Nothing
            Dim casamento As Dominio.Documentos.Habilitacao

            strSql = "  SELECT *, CAST(CT03NUMEROFOLHA AS CHAR) NUMFOLHA, CAST(CT03NUMEROLIVRO AS CHAR) NUMLIVRO, CAST(CT03NUMEROTERMO AS CHAR) NUMTERMO, CAST(CT03ACERVO AS CHAR) NUMACERVO, CAST(CT03SERVENTIA AS CHAR) NUMSERVENTIA, "

            strSql += "  (SELECT CT06CODIGO FROM CT06CONJUGE WHERE FK1106CASAL=CT06CODIGO ) CASALID, "
            strSql += "  (SELECT CT01CODIGO FROM CT06CONJUGE,CT01CLIENTE WHERE FK1106CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE1 ) CODIGOCONJUGE1, "
            strSql += "  (SELECT CT01NOME FROM CT06CONJUGE,CT01CLIENTE WHERE FK1106CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE1 ) NOMECONJUGE1, "
            strSql += "  (SELECT CT01CODIGO FROM CT06CONJUGE,CT01CLIENTE WHERE FK1106CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE2 ) CODIGOCONJUGE2, "
            strSql += "  (SELECT CT01NOME FROM CT06CONJUGE,CT01CLIENTE WHERE FK1106CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE2 ) NOMECONJUGE2,"

            strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198NATURAL=CT98CODIGO ) NOMECIDADENATURAL, "
            strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198NATURAL=CT98CODIGO ) NOMEESTADONATURAL "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT11HABILITACAO "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND CT11CODIGO=FK0411HABILITACAO "
            strSql += "    AND CT03CODIGO = " & pedidoID
            strSql += "        ORDER BY CT03DATAEMISSAO, CT02NOME "

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
                    pedido.Solicitante.Cpf = dr.Item("CT01CPF").ToString
                    pedido.Solicitante.Rg = dr.Item("CT01RG").ToString
                    pedido.Solicitante.Natural.Nome = dr.Item("NOMECIDADENATURAL").ToString
                    pedido.Solicitante.Natural.Estado.Nome = dr.Item("NOMEESTADONATURAL").ToString
                    pedido.Solicitante.EstadoCivil = dr.Item("CT01ESTADOCIVIL").ToString

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

                    casamento = New Dominio.Documentos.Habilitacao
                    casamento.Codigo = dr.Item("CT11CODIGO")
                    casamento.NovoNomeConjuge1 = dr.Item("CT11NOVONOMECONJUGE1").ToString
                    casamento.NovoNomeConjuge2 = dr.Item("CT11NOVONOMECONJUGE2").ToString
                    casamento.Regime = dr.Item("CT11REGIME").ToString

                    casamento.Casal.Codigo = dr.Item("CASALID").ToString
                    casamento.Casal.Conjuge1.Codigo = dr.Item("CODIGOCONJUGE1").ToString
                    casamento.Casal.Conjuge1.Nome = dr.Item("NOMECONJUGE1").ToString

                    casamento.Casal.Conjuge2.Codigo = dr.Item("CODIGOCONJUGE2").ToString
                    casamento.Casal.Conjuge2.Nome = dr.Item("NOMECONJUGE2").ToString

                    casamento.TipoLivro = dr.Item("CT02DOCUMENTO").ToString

                    pedido.Documento = casamento

                    If dr.Item("CT03DATAEMISSAO").ToString = String.Empty Then
                        pedido.DataEmissao = ""
                    Else
                        pedido.DataEmissao = dr.Item("CT03DATAEMISSAO").ToString.Substring(0, 10)
                    End If

                    If dr.Item("CT04DATAREGISTRO").ToString = String.Empty Then
                        pedido.Documento.DataRegistro = ""
                    Else
                        pedido.Documento.DataRegistro = dr.Item("CT04DATAREGISTRO").ToString.Substring(0, 10)
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

        Public Function listarPedidoProclamas(ByVal pedidoID As Integer) As Dominio.Documentos.Pedido Implements IDocumentoDAO.listarPedidoProclamas
            Dim pedido As Dominio.Documentos.Pedido = Nothing
            Dim casamento As Dominio.Documentos.Proclamas

            strSql = "  SELECT *, CAST(CT03NUMEROFOLHA AS CHAR) NUMFOLHA, CAST(CT03NUMEROLIVRO AS CHAR) NUMLIVRO, CAST(CT03NUMEROTERMO AS CHAR) NUMTERMO, CAST(CT03ACERVO AS CHAR) NUMACERVO, CAST(CT03SERVENTIA AS CHAR) NUMSERVENTIA, "

            strSql += "  (SELECT CT06CODIGO FROM CT06CONJUGE WHERE FK1406CASAL=CT06CODIGO ) CASALID, "
            strSql += "  (SELECT CT01CODIGO FROM CT06CONJUGE,CT01CLIENTE WHERE FK1406CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE1 ) CODIGOCONJUGE1, "
            strSql += "  (SELECT CT01NOME FROM CT06CONJUGE,CT01CLIENTE WHERE FK1406CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE1 ) NOMECONJUGE1, "
            strSql += "  (SELECT CT01CODIGO FROM CT06CONJUGE,CT01CLIENTE WHERE FK1406CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE2 ) CODIGOCONJUGE2, "
            strSql += "  (SELECT CT01NOME FROM CT06CONJUGE,CT01CLIENTE WHERE FK1406CASAL=CT06CODIGO AND CT01CODIGO=FK0601CONJUGE2 ) NOMECONJUGE2,"

            strSql += "  (SELECT CT98NOME FROM CT98CIDADE WHERE FK0198NATURAL=CT98CODIGO ) NOMECIDADENATURAL, "
            strSql += "  (SELECT CT99SIGLA FROM CT98CIDADE,CT99ESTADO WHERE CT99CODIGO=FK9899ESTADO AND FK0198NATURAL=CT98CODIGO ) NOMEESTADONATURAL "
            strSql += "   FROM CT01CLIENTE, CT02TIPODOCUMENTO, CT03PEDIDO, CT04DOCUMENTO, CT14PROCLAMAS "
            strSql += "  WHERE FK0304DOCUMENTO=CT04CODIGO "
            strSql += "    AND FK0301SOLICITANTE=CT01CODIGO "
            strSql += "    AND FK0402TIPODOCUMENTO=CT02DOCUMENTO "
            strSql += "    AND CT14CODIGO=FK0414PROCLAMAS "
            strSql += "    AND CT03CODIGO = " & pedidoID
            strSql += "        ORDER BY CT03DATAEMISSAO, CT02NOME "

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
                    pedido.Solicitante.Cpf = dr.Item("CT01CPF").ToString
                    pedido.Solicitante.Rg = dr.Item("CT01RG").ToString
                    pedido.Solicitante.Natural.Nome = dr.Item("NOMECIDADENATURAL").ToString
                    pedido.Solicitante.Natural.Estado.Nome = dr.Item("NOMEESTADONATURAL").ToString
                    pedido.Solicitante.EstadoCivil = dr.Item("CT01ESTADOCIVIL").ToString

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

                    casamento = New Dominio.Documentos.Proclamas
                    casamento.Codigo = dr.Item("CT14CODIGO")

                    casamento.Casal.Codigo = dr.Item("CASALID").ToString
                    casamento.Casal.Conjuge1.Codigo = dr.Item("CODIGOCONJUGE1").ToString
                    casamento.Casal.Conjuge1.Nome = dr.Item("NOMECONJUGE1").ToString

                    casamento.Casal.Conjuge2.Codigo = dr.Item("CODIGOCONJUGE2").ToString
                    casamento.Casal.Conjuge2.Nome = dr.Item("NOMECONJUGE2").ToString

                    casamento.TipoLivro = dr.Item("CT02DOCUMENTO").ToString

                    casamento.Edital.Ano = dr.Item("CT14ANOEDITAL")
                    casamento.Edital.Numero = dr.Item("CT14NUMEROEDITAL")

                    pedido.Documento = casamento

                    If dr.Item("CT03DATAEMISSAO").ToString = String.Empty Then
                        pedido.DataEmissao = ""
                    Else
                        pedido.DataEmissao = dr.Item("CT03DATAEMISSAO").ToString.Substring(0, 10)
                    End If

                    If dr.Item("CT04DATAREGISTRO").ToString = String.Empty Then
                        pedido.Documento.DataRegistro = ""
                    Else
                        pedido.Documento.DataRegistro = dr.Item("CT04DATAREGISTRO").ToString.Substring(0, 10)
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

        Public Function listarUltimoEdital() As Dominio.Documentos.Edital Implements IDocumentoDAO.listarUltimoEdital
            Dim edital As Dominio.Documentos.Edital = Nothing

            strSql = "  select CT14NUMEROEDITAL,CT14ANOEDITAL "
            strSql += "   from CT14PROCLAMAS "
            strSql += "        order by CT14ANOEDITAL DESC, CT14NUMEROEDITAL DESC LIMIT 1 "

            Try
                cmd = conn.CreateCommand
                cmd.CommandText = strSql
                dr = cmd.ExecuteReader

                While dr.Read
                    edital = New Dominio.Documentos.Edital

                    edital.Ano = dr.Item("CT14ANOEDITAL")
                    edital.Numero = dr.Item("CT14NUMEROEDITAL")
                End While

                dr.Close()

                Return edital

            Catch ex As OleDbException
                Throw New DAOException(ex.Message)
            Catch ex As Exception
                Throw New DAOException(ex.Message)
            End Try
        End Function
    End Class
End Namespace