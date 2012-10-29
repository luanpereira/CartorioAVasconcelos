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

                DaoFactory.BeginTransaction()
                '--------------------------

                dao = DaoFactory.GetDocumentoDAO

                pedido.Documento.DataRegistro = Format(DateTime.Parse(pedido.Documento.DataRegistro), "yyyy-MM-dd")

                If pedido.Codigo = 0 Then 'INSERT
                    Select Case True
                        Case TypeOf pedido.Documento Is Nascimento
                            idResult = dao.inserirNascimento(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                            idResult = dao.inserirDocumentoNascimento(pedido.Documento)
                            pedido.Documento.Codigo = idResult

                        Case TypeOf pedido.Documento Is Casamento
                            Throw New NotImplementedException

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

                        Case TypeOf pedido.Documento Is Casamento
                            Throw New NotImplementedException

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
            Catch ex As UsuarioInvalidoException
                DaoFactory.TransactionRollback()
                Throw ex
            Catch ex As DAOException
                DaoFactory.TransactionRollback()
                Throw ex
            Catch ex As Exception
                DaoFactory.TransactionRollback()
                Throw New BusinessException(ex.Message)
            Finally
                dao = Nothing
                DaoFactory.CloseConnection()
            End Try
        End Function

        Public Function listarPedido(ByVal pedido As Dominio.Documentos.Pedido) As Dominio.Documentos.Pedido Implements IDocumentoController.listarPedido
            Dim dao As IDocumentoDAO

            Try

                dao = DaoFactory.GetDocumentoDAO

                Select Case True
                    Case TypeOf pedido.Documento Is Nascimento
                        Return dao.listarPedidoNascimento(pedido.Codigo)

                    Case TypeOf pedido.Documento Is Casamento
                        Return dao.listarPedidoCasamento(pedido.Codigo)

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
    End Class

End Namespace

