Imports Microsoft.VisualBasic
Imports Camadas.Dominio.Administrativo

Namespace Camadas.Dominio.Documentos

    <Serializable()> _
    Public Class Obito
        Inherits Documento

        Public Property CodigoObito() As Integer
        Public Property DataObito() As String
        Public Property Local() As String
        Public Property Cidade() As Cidade
        Public Property CausaMorte() As String
        Public Property Declarante() As String
        Public Property Medico() As String
        Public Property Sepultamento() As String
        Public Property Cor() As Cor


        Public Sub New()
            _Cidade = New Cidade
            _Cor = New Cor
        End Sub
    End Class

End Namespace
