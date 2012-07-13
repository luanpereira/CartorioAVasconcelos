Imports Microsoft.VisualBasic

Namespace Camadas.Dominio.Documentos

    Public Class Habilitacao
        Inherits Documento

        Public Property CodigoHabilitacao() As Integer
        Public Property Casal() As Casal

        Public Sub New()
            _Casal = New Casal
        End Sub

    End Class

End Namespace
