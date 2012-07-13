Imports Microsoft.VisualBasic

Namespace Camadas.Dominio.Administrativo

    Public Class Filiacao
        Public Property NomePai() As String
        Public Property NomeMae() As String

        Public Property NomeAvoPaterno1() As String
        Public Property NomeAvoPaterno2() As String

        Public Property NomeAvoMaterno1() As String
        Public Property NomeAvoMaterno2() As String


        Public ReadOnly Property getAvos() As String
            Get
                Return "Paternos: " & _NomeAvoPaterno1.ToUpper & " e " & _NomeAvoPaterno2.ToUpper & ". <br />Maternos: " & _NomeAvoMaterno1.ToUpper & " e " & _NomeAvoMaterno2.ToUpper & "."
            End Get
        End Property

        Public ReadOnly Property getPais() As String
            Get
                Return _NomePai.ToUpper & " e " & _NomeMae.ToUpper & "."
            End Get
        End Property
    End Class

End Namespace