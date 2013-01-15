Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Administrativo
Imports System.Data

Public Interface IClienteDAO
    Function cadastrarCliente(ByVal cliente As Cliente) As Integer
    Sub atualizarCliente(ByVal cliente As Cliente)

    Function listarCliente(ByVal cliente As Cliente) As DataTable
    Function listarClassCliente(ByVal cliente As Cliente) As Cliente

    Sub atualizarGemeo(ByVal gemeo1 As Cliente, ByVal gemeo2 As Cliente)
End Interface
