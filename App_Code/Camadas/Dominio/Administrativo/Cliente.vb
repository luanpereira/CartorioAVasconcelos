Imports Camadas.Dominio
Imports Microsoft.VisualBasic
Imports Infraestrutura.Utils

Namespace Camadas.Dominio.Administrativo

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

    End Class

End Namespace