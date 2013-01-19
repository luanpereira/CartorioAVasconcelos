Imports Camadas.Dominio
Imports Microsoft.VisualBasic
Imports Infraestrutura.Utils

Namespace Camadas.Dominio.Administrativo

    <Serializable()> _
    Public Class Cliente

        Public Property Codigo() As Integer
        Public Property Nome() As String
        Public Property Cpf() As String
        Public Property Rg() As String
        Public Property Sexo() As Char
        Public Property EstadoCivil() As Char
        Public Property Profissao() As String
        Public Property Natural() As Cidade
        Public Property Endereco() As Endereco
        Public Property Contato() As Contato
        Public Property Senha() As String
        Public Property DataNascimento() As String
        Public Property isAcessoWeb() As Boolean
        Public Property QuantidadeDispensadores() As Integer
        Public Property Filiacao() As Filiacao
        Public Property Gemeo() As Cliente

        Public Sub New()
            _Filiacao = New Filiacao
            _Contato = New Contato
            _Endereco = New Endereco
            _Natural = New Cidade
            '_Gemeo = New Cliente
        End Sub

        Public ReadOnly Property getIdade(Optional ByVal data As String = "") As String
            Get
                Dim dias As Single
                Dim anos As Integer
                Dim meses As Integer
                Dim resto As Integer

                dias = DateDiff("d", Convert.ToDateTime(_DataNascimento), IIf(data = String.Empty, Now, data))
                anos = Int(dias / 365)
                resto = dias Mod 365
                meses = Int(resto / 30)
                dias = resto Mod 30

                If anos > 1 Then
                    Return anos & " anos"
                ElseIf anos = 1 Then
                    Return anos & " ano"
                ElseIf meses > 1 Then
                    Return meses & " meses"
                ElseIf meses = 1 Then
                    Return meses & " mês"
                ElseIf dias > 1 Then
                    Return dias & " dias"
                ElseIf dias = 1 Then
                    Return dias & " dia"
                Else
                    Return ""
                End If

            End Get
        End Property
    End Class

End Namespace