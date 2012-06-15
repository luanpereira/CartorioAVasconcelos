Imports Camadas.Dominio
Imports Microsoft.VisualBasic
Imports Infraestrutura.Utils

Namespace Camadas.Dominio.Administrativo

    Public Class Cliente

        Public Property Codigo() As Integer
        Public Property Nome() As String
        Public Property Cpf() As String
        Public Property Rg() As String
        Public Property Sexo() As Short
        Public Property EstadoCivil() As Short
        Public Property CodigoUsuario() As Integer
        Public Property Endereco() As New Endereco
        Public Property Contato() As New Contato
        Public Property Senha() As String
        Public Property DataNascimento() As String
        Public Property isAcessoWeb() As Boolean
        Public Property QuantidadeDispensadores() As Integer
        Public Property TipoPessoa() As eTipoPessoa
        Public Property PessoaFisica() As PessoaFisica
        Public Property TipoCliente() As eTipoCliente

    End Class

End Namespace