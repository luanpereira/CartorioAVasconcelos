Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Documentos
Imports Camadas.Dominio.Administrativo
Imports System.Data

Public Interface IDocumentoDAO
    Sub inserirDocumentoNascimento(ByVal nascimento As Nascimento)
    Function listarDocumentosByClienteID(ByVal cliente As Cliente) As DataTable
    Function listarTipoDocumento() As DataTable
    Function inserirNascimento(ByVal nascimento As Nascimento) As Integer
    Function inserirDocumento(ByVal nascimento As Nascimento) As Integer
    Function inserirPedido(ByVal pedido As Pedido) As Integer
    Function listarPedido(ByVal pedidoID As Integer) As Pedido
End Interface
