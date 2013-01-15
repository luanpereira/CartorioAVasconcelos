Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Documentos
Imports Camadas.Dominio.Administrativo
Imports System.Data

Public Interface IDocumentoDAO
    Function listarDocumentosByClienteID(ByVal cliente As Cliente) As DataTable
    Function listarDocumentos() As DataTable
    Function listarTipoDocumento() As DataTable
    Function listarCor() As DataTable

    Function inserirNascimento(ByVal nascimento As Nascimento) As Integer
    Function inserirCancelamentoNascimento(ByVal cancelNascimento As CancelamentoNascimento) As Integer
    Function inserirHabilitacao(ByVal casamento As Habilitacao) As Integer
    Function inserirProclamas(ByVal casamento As Proclamas) As Integer
    Function inserirCasamento(ByVal casamento As Casamento) As Integer
    Function inserirCasamentoReligioso(ByVal casamento As CasamentoReligioso) As Integer
    Function inserirCasal(ByVal casal As Casal) As Integer
    Function inserirObito(ByVal obito As Obito) As Integer

    Function inserirDocumentoNascimento(ByVal nascimento As Nascimento) As Integer
    Function inserirDocumentoCancelamentoNascimento(ByVal cancelNascimento As CancelamentoNascimento) As Integer
    Function inserirDocumentoHabilitacao(ByVal casamento As Habilitacao) As Integer
    Function inserirDocumentoProclamas(ByVal casamento As Proclamas) As Integer
    Function inserirDocumentoCasamento(ByVal casamento As Casamento) As Integer
    Function inserirDocumentoCasamentoReligioso(ByVal casamento As CasamentoReligioso) As Integer
    Function inserirDocumentoObito(ByVal obito As Obito) As Integer

    Function inserirPedido(ByVal pedido As Pedido) As Integer

    Sub atualizarNascimento(ByVal nascimento As Nascimento)
    Sub atualizarCancelamentoNascimento(ByVal cancelNascimento As CancelamentoNascimento)
    Sub atualizarHabilitacao(ByVal casamento As Habilitacao)
    Sub atualizarProclamas(ByVal casamento As Proclamas)
    Sub atualizarCasamento(ByVal casamento As Casamento)
    Sub atualizarCasamentoReligioso(ByVal casamento As CasamentoReligioso)
    Sub atualizarObito(ByVal obito As Obito)
    Sub atualizarPedido(ByVal pedido As Pedido)

    Function listarPedidoNascimento(ByVal pedidoID As Integer) As Pedido
    Function listarPedidoCancelamentoNascimento(ByVal pedidoID As Integer) As Pedido
    Function listarPedidoObito(ByVal pedidoID As Integer) As Pedido
    Function listarPedidoHabilitacao(ByVal pedidoID As Integer) As Pedido
    Function listarPedidoProclamas(ByVal pedidoID As Integer) As Pedido
    Function listarPedidoCasamento(ByVal pedidoID As Integer) As Pedido
    Function listarPedidoCasamentoReligioso(ByVal pedidoID As Integer) As Pedido

    Function listarUltimoEdital() As Edital
End Interface
