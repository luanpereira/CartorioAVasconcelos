Namespace Camadas.Dominio.Administrativo

    <Serializable()> _
    Public Class Endereco

        Public Property Cep() As String

        Public Property Logradouro() As String

        Public Property Bairro() As String

        Public Property Cidade() As New Cidade

        Public ReadOnly Property Completo() As String
            Get
                Return _Logradouro & ", " & _Bairro & " " & _Cidade.Nome & "-" & _Cidade.Estado.Nome
            End Get
        End Property

        Public ReadOnly Property CidadeEstado() As String
            Get
                Return _Cidade.Nome & "-" & _Cidade.Estado.Nome
            End Get
        End Property


    End Class

End Namespace