Imports Microsoft.VisualBasic

Namespace Camadas.Dominio.Documentos

    Public Class CancelamentoNascimento
        Inherits Documento

        Public Property CodigoCancelamento() As Integer
        Public Property Motivo() As String
        Public Property Mandado() As String
        Public Property Datado() As String

    End Class

End Namespace
