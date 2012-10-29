Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Administrativo

Namespace Camadas.Dominio.Documentos

    Public Class Pedido
        Public Property Codigo() As Integer
        Public Property Documento() As Documento
        Public Property Solicitante() As Cliente
        Public Property Matricula() As Matricula
        Public Property Averbacao() As String
        Public Property Status() As Integer

        Public Sub New()
            _Matricula = New Matricula
            _Solicitante = New Cliente
        End Sub
    End Class

End Namespace