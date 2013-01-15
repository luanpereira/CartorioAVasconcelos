Imports Microsoft.VisualBasic
Imports Camadas.DAO
Imports Excecoes
Imports Camadas.Dominio.Documentos

Namespace Camadas.Negocio

    Public Class DocumentoController
        Implements IDocumentoController

        Public Function listarDocumentosByClienteID(ByVal cliente As Dominio.Administrativo.Cliente) As System.Data.DataTable Implements IDocumentoController.listarDocumentosByClienteID
            Dim dao As IDocumentoDAO

            Try
                If cliente.Codigo = 0 Then Throw New CampoObrigatorioException("O CÓDIGO DO CLIENTE É OBRIGATÓRIO.")

                dao = DaoFactory.GetDocumentoDAO
                Return dao.listarDocumentosByClienteID(cliente)

            Catch ex As CampoObrigatorioException
                Throw ex
            Catch ex As Exception
                Throw ex
            Finally
                dao = Nothing
                DaoFactory.CloseConnection()
            End Try
        End Function

        Public Function listarTipoDocumento() As System.Data.DataTable Implements IDocumentoController.listarTipoDocumento
            Dim dao As IDocumentoDAO

            Try

                dao = DaoFactory.GetDocumentoDAO
                Return dao.listarTipoDocumento

            Catch ex As CampoObrigatorioException
                Throw ex
            Catch ex As Exception
                Throw ex
            Finally
                dao = Nothing
                DaoFactory.CloseConnection()
            End Try
        End Function

        Public Function solicitarDocumento(ByVal pedido As Dominio.Documentos.Pedido) As Integer Implements IDocumentoController.solicitarDocumento
            Dim dao As IDocumentoDAO
            Dim idNascimento As Integer = 0
            Dim idResult As Integer = 0
            Dim u As Usuario

            Try
                If pedido.Documento.TipoLivro = 0 Then Throw New BusinessException("TIPO LIVRO OBRIGATÓRIO.")

                DaoFactory.BeginTransaction()
                '--------------------------

                dao = DaoFactory.GetDocumentoDAO

                pedido.Documento.DataRegistro = Format(DateTime.Parse(pedido.Documento.DataRegistro), "yyyy-MM-dd")

                If pedido.DataEmissao.Contains("/") Then
                    If Format(Now.Date, "dd/MM/yyyy") = pedido.DataEmissao Then
                        pedido.DataEmissao = Format(DateTime.Now, "yyyy-MM-dd H:m:s")
                    Else
                        pedido.DataEmissao = Format(DateTime.Parse(pedido.DataEmissao), "yyyy-MM-dd")
                    End If
                Else
                    If Format(Now.Date, "yyyy-MM-dd") = pedido.DataEmissao Then
                        pedido.DataEmissao = Format(DateTime.Now, "yyyy-MM-dd H:m:s")
                    End If
                End If

                If pedido.Codigo = 0 Then 'INSERT
                    Select Case True
                        Case TypeOf pedido.Documento Is Nascimento
                            idResult = dao.inserirNascimento(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                            idResult = dao.inserirDocumentoNascimento(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                        Case TypeOf pedido.Documento Is CancelamentoNascimento
                            CType(pedido.Documento, CancelamentoNascimento).Datado = Format(DateTime.Parse(CType(pedido.Documento, CancelamentoNascimento).Datado), "yyyy-MM-dd")

                            idResult = dao.inserirCancelamentoNascimento(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                            idResult = dao.inserirDocumentoCancelamentoNascimento(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                        Case TypeOf pedido.Documento Is Casamento
                            idResult = dao.inserirCasal(CType(pedido.Documento, Casamento).Casal)
                            CType(pedido.Documento, Casamento).Casal.Codigo = idResult

                            idResult = dao.inserirCasamento(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                            idResult = dao.inserirDocumentoCasamento(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                        Case TypeOf pedido.Documento Is CasamentoReligioso
                            idResult = dao.inserirCasal(CType(pedido.Documento, CasamentoReligioso).Casal)
                            CType(pedido.Documento, CasamentoReligioso).Casal.Codigo = idResult

                            idResult = dao.inserirCasamentoReligioso(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                            idResult = dao.inserirDocumentoCasamentoReligioso(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                        Case TypeOf pedido.Documento Is Habilitacao
                            idResult = dao.inserirCasal(CType(pedido.Documento, Habilitacao).Casal)
                            CType(pedido.Documento, Habilitacao).Casal.Codigo = idResult

                            idResult = dao.inserirHabilitacao(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                            idResult = dao.inserirDocumentoHabilitacao(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                        Case TypeOf pedido.Documento Is Proclamas
                            idResult = dao.inserirCasal(CType(pedido.Documento, Proclamas).Casal)
                            CType(pedido.Documento, Proclamas).Casal.Codigo = idResult

                            CType(pedido.Documento, Proclamas).Edital = Me.gerarNumeroEditalProclamas(CType(pedido.Documento, Proclamas).Edital.Ano)

                            idResult = dao.inserirProclamas(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                            idResult = dao.inserirDocumentoProclamas(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                        Case TypeOf pedido.Documento Is Obito
                            idResult = dao.inserirObito(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                            idResult = dao.inserirDocumentoObito(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                        Case Else
                            Throw New CampoObrigatorioException("TIPO DE DOCUMENTO NÃO DEFINIDO.")
                    End Select


                    idResult = dao.inserirPedido(pedido)
                Else 'UPDATE
                    pedido.Status = 6

                    Select Case True
                        Case TypeOf pedido.Documento Is Nascimento
                            dao.atualizarNascimento(pedido.Documento)

                        Case TypeOf pedido.Documento Is CancelamentoNascimento
                            CType(pedido.Documento, CancelamentoNascimento).Datado = Format(DateTime.Parse(CType(pedido.Documento, CancelamentoNascimento).Datado), "yyyy-MM-dd")

                            dao.atualizarCancelamentoNascimento(pedido.Documento)

                        Case TypeOf pedido.Documento Is Casamento
                            dao.atualizarCasamento(pedido.Documento)

                        Case TypeOf pedido.Documento Is Habilitacao
                            dao.atualizarHabilitacao(pedido.Documento)

                        Case TypeOf pedido.Documento Is Proclamas
                            dao.atualizarProclamas(pedido.Documento)

                        Case TypeOf pedido.Documento Is CasamentoReligioso
                            dao.atualizarCasamentoReligioso(pedido.Documento)

                        Case TypeOf pedido.Documento Is Obito
                            dao.atualizarObito(pedido.Documento)

                        Case Else
                            Throw New CampoObrigatorioException("TIPO DE DOCUMENTO NÃO DEFINIDO.")
                    End Select


                    dao.atualizarPedido(pedido)
                End If

                '--------------------------
                DaoFactory.TransactionCommit()

                Return idResult
            Catch ex As BusinessException
                DaoFactory.TransactionRollback()
                Throw ex
            Catch ex As UsuarioInvalidoException
                DaoFactory.TransactionRollback()
                Throw ex
            Catch ex As DAOException
                DaoFactory.TransactionRollback()
                Throw ex
            Catch ex As Exception
                DaoFactory.TransactionRollback()
                If ex.Message = "String was not recognized as a valid DateTime." Then
                    Throw New BusinessException("DATA INVÁLIDA. TENTE NOVAMENTE.")
                Else
                    Throw New BusinessException(ex.Message)
                End If
            Finally
                dao = Nothing
                DaoFactory.CloseConnection()
            End Try
        End Function

        Public Function listarPedido(ByVal pedido As Dominio.Documentos.Pedido) As Dominio.Documentos.Pedido Implements IDocumentoController.listarPedido
            Dim dao As IDocumentoDAO
            Dim daoCliente As IClienteDAO

            Dim returnPedido As Pedido = Nothing

            Try

                dao = DaoFactory.GetDocumentoDAO
                daoCliente = DaoFactory.GetClienteDAO

                returnPedido = New Pedido

                Select Case True
                    Case TypeOf pedido.Documento Is Nascimento
                        Return dao.listarPedidoNascimento(pedido.Codigo)

                    Case TypeOf pedido.Documento Is CancelamentoNascimento
                        Return dao.listarPedidoCancelamentoNascimento(pedido.Codigo)

                    Case TypeOf pedido.Documento Is Casamento
                        returnPedido = dao.listarPedidoCasamento(pedido.Codigo)
                        CType(returnPedido.Documento, Casamento).Casal.Conjuge1 = daoCliente.listarClassCliente(CType(returnPedido.Documento, Casamento).Casal.Conjuge1)
                        CType(returnPedido.Documento, Casamento).Casal.Conjuge2 = daoCliente.listarClassCliente(CType(returnPedido.Documento, Casamento).Casal.Conjuge2)

                        Return returnPedido

                    Case TypeOf pedido.Documento Is CasamentoReligioso
                        returnPedido = dao.listarPedidoCasamentoReligioso(pedido.Codigo)
                        CType(returnPedido.Documento, CasamentoReligioso).Casal.Conjuge1 = daoCliente.listarClassCliente(CType(returnPedido.Documento, CasamentoReligioso).Casal.Conjuge1)
                        CType(returnPedido.Documento, CasamentoReligioso).Casal.Conjuge2 = daoCliente.listarClassCliente(CType(returnPedido.Documento, CasamentoReligioso).Casal.Conjuge2)

                        Return returnPedido

                    Case TypeOf pedido.Documento Is Habilitacao
                        returnPedido = dao.listarPedidoHabilitacao(pedido.Codigo)
                        CType(returnPedido.Documento, Habilitacao).Casal.Conjuge1 = daoCliente.listarClassCliente(CType(returnPedido.Documento, Habilitacao).Casal.Conjuge1)
                        CType(returnPedido.Documento, Habilitacao).Casal.Conjuge2 = daoCliente.listarClassCliente(CType(returnPedido.Documento, Habilitacao).Casal.Conjuge2)

                        Return returnPedido

                    Case TypeOf pedido.Documento Is Proclamas
                        returnPedido = dao.listarPedidoProclamas(pedido.Codigo)
                        CType(returnPedido.Documento, Proclamas).Casal.Conjuge1 = daoCliente.listarClassCliente(CType(returnPedido.Documento, Proclamas).Casal.Conjuge1)
                        CType(returnPedido.Documento, Proclamas).Casal.Conjuge2 = daoCliente.listarClassCliente(CType(returnPedido.Documento, Proclamas).Casal.Conjuge2)

                        Return returnPedido

                    Case TypeOf pedido.Documento Is Obito
                        Return dao.listarPedidoObito(pedido.Codigo)

                    Case Else
                        Throw New CampoObrigatorioException("TIPO DE DOCUMENTO NÃO DEFINIDO.")
                End Select

            Catch ex As CampoObrigatorioException
                Throw ex
            Catch ex As Exception
                Throw ex
            Finally
                dao = Nothing
                DaoFactory.CloseConnection()
            End Try
        End Function

        Public Function listarCor() As System.Data.DataTable Implements IDocumentoController.listarCor
            Dim dao As IDocumentoDAO

            Try

                dao = DaoFactory.GetDocumentoDAO
                Return dao.listarCor

            Catch ex As CampoObrigatorioException
                Throw ex
            Catch ex As Exception
                Throw ex
            Finally
                dao = Nothing
                DaoFactory.CloseConnection()
            End Try
        End Function

        Public Function listarDocumentos() As System.Data.DataTable Implements IDocumentoController.listarDocumentos
            Dim dao As IDocumentoDAO

            Try

                dao = DaoFactory.GetDocumentoDAO
                Return dao.listarDocumentos()

            Catch ex As CampoObrigatorioException
                Throw ex
            Catch ex As Exception
                Throw ex
            Finally
                dao = Nothing
                DaoFactory.CloseConnection()
            End Try
        End Function

        Public Function gerarNumeroEditalProclamas(ByVal anoAtual As Integer) As Dominio.Documentos.Edital Implements IDocumentoController.gerarNumeroEditalProclamas
            'Dim dao As IDocumentoDAO
            Dim edital As Edital

            Try
                edital = New Edital

                'dao = DaoFactory.GetDocumentoDAO
                edital = Me.listarUltimoEdital()

                If anoAtual = edital.Ano Then
                    edital.Numero += 1
                Else
                    edital.Numero = 1
                    edital.Ano = anoAtual
                End If

                Return edital
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function listarUltimoEdital() As Dominio.Documentos.Edital Implements IDocumentoController.listarUltimoEdital
            Dim dao As IDocumentoDAO

            Try

                dao = DaoFactory.GetDocumentoDAO
                Return dao.listarUltimoEdital()

            Catch ex As Exception
                Throw ex
            Finally
                dao = Nothing
                'DaoFactory.CloseConnection()
            End Try
        End Function
    End Class

End Namespace

