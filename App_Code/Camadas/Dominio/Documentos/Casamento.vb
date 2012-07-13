Imports Microsoft.VisualBasic
Imports Infraestrutura.Utils

Namespace Camadas.Dominio.Documentos

    Public Class Casamento
        Inherits Documento

        Public Property CodigoCasamento() As Integer
        Public Property Juiz() As String
        Public Property NovoNomeEla() As String
        Public Property NovoNomeEle() As String
        Public Property Regime() As Regime
        Public Property Casal() As Casal

        Public Sub New()
            _Casal = New Casal
        End Sub
    End Class

End Namespace
