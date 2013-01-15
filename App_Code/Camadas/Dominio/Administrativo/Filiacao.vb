Imports Microsoft.VisualBasic

Namespace Camadas.Dominio.Administrativo

    <Serializable()> _
    Public Class Filiacao
        Public Property NomePai() As String
        Public Property NomeMae() As String

        Public Property NomeAvoPaterno1() As String
        Public Property NomeAvoPaterno2() As String

        Public Property NomeAvoMaterno1() As String
        Public Property NomeAvoMaterno2() As String


        Public ReadOnly Property getAvos() As String
            Get
                Dim str As String = ""

                If Not _NomeAvoPaterno1 = String.Empty And Not _NomeAvoPaterno2 = String.Empty Then
                    str += "Paternos: " & _NomeAvoPaterno1.ToUpper & " e " & _NomeAvoPaterno2.ToUpper
                ElseIf _NomeAvoPaterno1 = String.Empty And _NomeAvoPaterno2 = String.Empty Then
                    str += ""
                ElseIf _NomeAvoPaterno1 = String.Empty Then
                    str += "Paterno: " & _NomeAvoPaterno2.ToUpper
                ElseIf _NomeAvoPaterno2 = String.Empty Then
                    str += "Paterno: " & _NomeAvoPaterno1.ToUpper
                End If

                If Not str = String.Empty Then
                    str += ".<BR />&nbsp;&nbsp;"
                End If

                If Not _NomeAvoMaterno1 = String.Empty And Not _NomeAvoMaterno2 = String.Empty Then
                    str += "Maternos: " & _NomeAvoMaterno1.ToUpper & " e " & _NomeAvoMaterno2.ToUpper
                ElseIf _NomeAvoMaterno1 = String.Empty And _NomeAvoMaterno2 = String.Empty Then
                    str += ""
                ElseIf _NomeAvoMaterno1 = String.Empty Then
                    str += "Materno: " & _NomeAvoMaterno2.ToUpper
                ElseIf _NomeAvoMaterno2 = String.Empty Then
                    str += "Materno: " & _NomeAvoMaterno1.ToUpper
                End If

                Return str
            End Get
        End Property

        Public ReadOnly Property getPais() As String
            Get
                If Not _NomePai Is Nothing AndAlso Not _NomeMae Is Nothing Then
                    If Not _NomePai = String.Empty And Not _NomeMae = String.Empty Then
                        Return _NomePai.ToUpper & " e " & _NomeMae.ToUpper
                    ElseIf _NomePai = String.Empty Then
                        Return _NomeMae.ToUpper
                    ElseIf _NomeMae = String.Empty Then
                        Return _NomePai.ToUpper
                    Else
                        Return ""
                    End If
                Else
                    Return ""
                End If
            End Get
        End Property
    End Class

End Namespace