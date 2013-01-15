Imports Microsoft.VisualBasic
Imports Infraestrutura.Utils

Namespace Camadas.Dominio.Documentos

    <Serializable()> _
    Public Class Proclamas
        Inherits Documento

        Public Property CodigoCasamento() As Integer
        Public Property Edital() As Edital
        Public Property Casal() As Casal

        Public Sub New()
            _Casal = New Casal
            _Edital = New Edital
        End Sub

    End Class

End Namespace