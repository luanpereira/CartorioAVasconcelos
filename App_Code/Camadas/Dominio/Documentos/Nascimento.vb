Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Administrativo

Namespace Camadas.Dominio.Documentos

    Public Class Nascimento
        Inherits Documento

        Public Property CodigoNascimento() As Integer
        Public Property Declarante() As Char
        Public Property Maternidade() As String
        Public Property Filiacao() As Filiacao
        Public Property Horario() As String
        Public Property Cidade() As String

        Public Sub New()
            _Filiacao = New Filiacao
        End Sub

    End Class

End Namespace

