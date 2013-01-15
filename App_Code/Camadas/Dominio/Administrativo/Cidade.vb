Imports Microsoft.VisualBasic

Namespace Camadas.Dominio.Administrativo

    <Serializable()> _
    Public Class Cidade
        Public Property Codigo() As Integer
        Public Property Nome() As String
        Public Property Estado() As New Estado

        Public Sub New()
            _Estado = New Estado
        End Sub

        Public Shadows Function ToString() As String
            Return _Nome & "-" & _Estado.Nome
        End Function

    End Class

End Namespace
