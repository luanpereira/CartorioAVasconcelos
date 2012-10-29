Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Documentos
Imports Camadas.Dominio.Administrativo
Imports System.Data

Public Interface IDocumentoDAO
    Function listarDocumentosByClienteID(ByVal cliente As Cliente) As DataTable
    Function listarTipoDocumento() As DataTable

    Function inserirNascimento(ByVal nascimento As Nascimento) As Integer
    Function inserirCasamento(ByVal casamento As Casamento) As Integer
    Function inserirObito(ByVal obito As Obito) As Integer

    Function inserirDocumentoNascimento(ByVal nascimento As Nascimento) As Integer
    Function inserirDocumentoCasamento(ByVal casamento As Casamento) As Integer
    Function inserirDocumentoObito(ByVal obito As Obito) As Integer

    Function inserirPedido(ByVal pedido As Pedido) As Integer

    Sub atualizarNascimento(ByVal nascimento As Nascimento)
    Sub atualizarCasamento(ByVal casamento As Casamento)
    Sub atualizarObito(ByVal obito As Obito)
    Sub atualizarPedido(ByVal pedido As Pedido)

    Function listarPedidoNascimento(ByVal pedidoID As Integer) As Pedido
    Function listarPedidoObito(ByVal pedidoID As Integer) As Pedido
    Function listarPedidoCasamento(ByVal pedidoID As Integer) As Pedido
End Interface
