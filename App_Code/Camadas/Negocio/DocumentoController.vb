Imports Microsoft.VisualBasic
Imports Camadas.DAO
Imports Excecoes

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

                'If cliente.Codigo = 0 Then '-- SE FOR IGUAL A ZERO, É PORQUE É UM NOVO CLIENTE
                '    idCliente = dao.cadastrarCliente(cliente)
                '    cliente.Codigo = idCliente

                'Else '-- CASO CONTRÁRIO ATUALIZA
                '    dao.atualizarCliente(cliente)
                'End If

                'If cliente.Gemeo.Codigo > 0 Then '-- SE FOR GÊMEOS, ATUALIZA O CAMPO FK0101GEMEO DO OUTRO IRMAO ---
                '    dao.atualizarGemeo(cliente, cliente.Gemeo)
                'End If

                idResult = dao.inserirNascimento(pedido.Documento)
                pedido.Documento.Codigo = idResult

                idResult = dao.inserirDocumento(pedido.Documento)
                pedido.Documento.Codigo = idResult

                idResult = dao.inserirPedido(pedido)

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

        Public Function listarPedido(ByVal pedidoID As Integer) As Dominio.Documentos.Pedido Implements IDocumentoController.listarPedido
            Dim dao As IDocumentoDAO

            Try

                dao = DaoFactory.GetDocumentoDAO
                Return dao.listarPedido(pedidoID)

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

