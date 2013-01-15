Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Administrativo

Namespace Camadas.Dominio.Documentos

    <Serializable()> _
    Public Class Casal
        Public Property Codigo() As Integer
        Public Property Conjuge1() As Cliente
        Public Property Conjuge2() As Cliente

        Public Sub New()
            _Conjuge1 = New Cliente
            _Conjuge2 = New Cliente
        End Sub
    End Class

End Namespace

