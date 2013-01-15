Imports Microsoft.VisualBasic

Namespace Camadas.Dominio.Documentos

    <Serializable()> _
    Public Class CancelamentoNascimento
        Inherits Documento

        Public Property CodigoCancelamento() As Integer
        Public Property Motivo() As String
        Public Property Datado() As String
        Public Property NumeroLivro As String
        Public Property NumeroFolha As String
        Public Property NumeroTermo As String

    End Class

End Namespace
