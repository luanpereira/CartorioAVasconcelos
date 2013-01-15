Imports Microsoft.VisualBasic
Imports Infraestrutura.Utils

Namespace Camadas.Dominio.Documentos

    <Serializable()> _
    Public Class Documento
        Public Property Codigo() As Integer
        Public Property Nome() As String
        Public Property Descricao() As String
        Public Property Valor() As Double
        Public Property TipoLivro() As TipoLivro
        Public Property DataRegistro() As String
        Public Property Horario() As String

    End Class

End Namespace