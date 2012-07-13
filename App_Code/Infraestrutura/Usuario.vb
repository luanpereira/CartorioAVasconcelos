
Imports System.Collections.Generic
Imports Camadas.Dominio
Imports Seguranca
Imports Camadas.Dominio.Administrativo
Imports Infraestrutura.Utils

Public Class Usuario

    Public Property Codigo() As Integer
    Public Property Usuario() As String
    Public Property Senha() As String
    Public Property Nome() As String
    Public Property NivelAcesso() As String
    Public Property UltimoAcesso() As String
    Public Property Ativo() As Boolean

End Class

