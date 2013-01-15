Imports Microsoft.VisualBasic
Imports Infraestrutura.Utils

Namespace Camadas.Dominio.Documentos

    <Serializable()> _
    Public Class Habilitacao
        Inherits Documento

        Public Property CodigoCasamento() As Integer
        Public Property Juiz() As String
        Public Property NovoNomeConjuge1() As String
        Public Property NovoNomeConjuge2() As String
        Public Property Regime() As Regime
        Public Property Casal() As Casal

        Public Sub New()
            _Casal = New Casal
        End Sub

    End Class

End Namespace
