Imports Microsoft.VisualBasic
Imports Infraestrutura.Utils

Namespace Camadas.Dominio.Documentos

    <Serializable()> _
    Public Class Edital
        Public Property Ano() As Integer
        Public Property Numero() As Integer

        Public Overrides Function ToString() As String
            Return _Numero & "/" & _Ano
        End Function
    End Class

End Namespace