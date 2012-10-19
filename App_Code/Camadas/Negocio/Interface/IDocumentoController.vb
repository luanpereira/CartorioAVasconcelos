Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Administrativo
Imports System.Data
Imports Camadas.Dominio.Documentos

Public Interface IDocumentoController
    Function listarDocumentosByClienteID(ByVal cliente As Cliente) As DataTable
    Function listarTipoDocumento() As DataTable
    Function solicitarDocumento(ByVal pedido As Pedido) As Integer
    Function listarPedido(ByVal pedidoID As Integer) As Pedido
End Interface
