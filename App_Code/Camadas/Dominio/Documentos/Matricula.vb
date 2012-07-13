Imports Microsoft.VisualBasic
Imports Infraestrutura.Utils

Namespace Camadas.Dominio.Documentos

    Public Class Matricula
        Private _matricula As String = ""

        Public Property Serventia As String
        Public Property Acervo As String
        Public Property Atribuicao As String
        Public Property AnoRegistro As Integer
        Public Property NumeroLivro As String
        Public Property NumeroFolha As String
        Public Property NumeroTermo As String
        Public Property TipoLivro As TipoLivro

        Public Sub New()

        End Sub

        Public Sub New(ByVal Serventia As String, ByVal Acervo As String, ByVal Atribuicao As String, ByVal AnoRegistro As Integer, _
                       ByVal NumeroLivro As String, ByVal NumeroFolha As String, ByVal NumeroTermo As String, ByVal TipoLivro As TipoLivro)

            _Serventia = Serventia
            _Acervo = Acervo
            _Atribuicao = Atribuicao
            _AnoRegistro = AnoRegistro
            _NumeroLivro = NumeroLivro
            _NumeroFolha = NumeroFolha
            _NumeroTermo = NumeroTermo
            _TipoLivro = TipoLivro

        End Sub

        Public Function getMatricula() As String

            _matricula = _Serventia & _Acervo & _Atribuicao & " " & _AnoRegistro & " " & _TipoLivro & " " & _NumeroLivro & " " & _NumeroFolha & " " & _NumeroTermo
            _matricula += " " & Me.getDV

            If _matricula.Replace(" ", "").Length = 32 Then
                Return _matricula
            Else
                Return "Erro na geração da matrícula. Verifique os dados digitados."
            End If
        End Function

        Private Function getDV() As Int16
            Dim d1 = 0, d2 As Int16 = 0
            Dim mat As String = _matricula.Replace(" ", "")

            d1 = Me.calcularDX(mat)
            mat = String.Concat(mat, d1)

            d2 = Me.calcularDX(mat)

            Return d1 & d2
        End Function

        Private Function calcularDX(ByVal numero As String) As Int16
            Dim lista_mult As New List(Of String)
            Dim soma As Long = 0
            Dim resto As Int16

            lista_mult = Me.multiplica(numero, numero.Length + 1)
            soma = Me.somatoria(lista_mult) * 10

            resto = soma Mod 11

            If resto = 10 Then resto = 1

            Return resto
        End Function

        Private Function multiplica(ByVal numero As String, ByVal mult As Long) As List(Of String)
            Dim lista_mult As New List(Of String)

            For i As Integer = 0 To numero.Length - 1
                lista_mult.Add(Long.Parse(numero.Substring(i, 1)) * mult)
                mult -= 1
            Next

            Return lista_mult
        End Function

        Private Function somatoria(ByVal lista_mult As List(Of String)) As Long
            Dim soma As Long = 0

            For i As Integer = 0 To lista_mult.Count - 1
                soma += Long.Parse(lista_mult.Item(i))
            Next

            Return soma
        End Function

    End Class

End Namespace
