Imports Microsoft.VisualBasic
Imports Camadas.DAO
Imports Camadas.Dominio.Administrativo
Imports Excecoes
Imports Infraestrutura.Utils
Imports System.Data

Namespace Camadas.Negocio

    Public Class ClienteController
        Implements IClienteController

        Public Sub cadastrarCliente(ByRef cliente As Cliente) Implements IClienteController.cadastrarCliente
            Dim dao As IClienteDAO
            Dim idCliente As Integer = 0
            Dim u As Usuario

            Try

                DaoFactory.BeginTransaction()
                '--------------------------

                dao = DaoFactory.GetClienteDAO

                If cliente.Codigo = 0 Then '-- SE FOR IGUAL A ZERO, É PORQUE É UM NOVO CLIENTE
                    idCliente = dao.cadastrarCliente(cliente)
                    cliente.Codigo = idCliente

                Else '-- CASO CONTRÁRIO ATUALIZA
                    dao.atualizarCliente(cliente)
                End If

                If cliente.Gemeo.Codigo > 0 Then '-- SE FOR GÊMEOS, ATUALIZA O CAMPO FK0101GEMEO DO OUTRO IRMAO ---
                    dao.atualizarGemeo(cliente, cliente.Gemeo)
                End If

                '--------------------------
                DaoFactory.TransactionCommit()

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
        End Sub

        Public Function listarCliente(ByVal c As Cliente) As DataTable Implements IClienteController.listarCliente
            Dim dao As IClienteDAO

            Try

                dao = DaoFactory.GetClienteDAO
                Return dao.listarCliente(c)

            Catch ex As Exception
                Throw ex
            Finally
                dao = Nothing
                DaoFactory.CloseConnection()
            End Try
        End Function

    End Class

End Namespace