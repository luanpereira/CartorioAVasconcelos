Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Administrativo

Namespace Camadas.Dominio.Documentos

    Public Class Casal
        Public Property Codigo() As Integer
        Public Property Esposo() As Cliente
        Public Property Esposa() As Cliente

        Public Sub New()
            _Esposo = New Cliente
            _Esposa = New Cliente
        End Sub
    End Class

End Namespace

