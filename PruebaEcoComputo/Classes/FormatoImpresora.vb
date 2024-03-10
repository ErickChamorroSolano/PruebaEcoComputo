Public Class FormatoImpresora
    Public Shared Function SeccionarParrafo(ByVal texto As String, Optional EsArmPos As Boolean = False) As List(Of String)
        Dim Lineas As List(Of String) = New List(Of String)
        Dim linea As String = texto
        If EsArmPos Then
            Dim Posicion As Integer = 32

            While Not String.IsNullOrEmpty(texto)
                texto = texto.Trim()
                If Posicion + 1 < texto.Length Then
                    linea = texto.Substring(0, Posicion)
                    If Not texto.Chars(Posicion + 1).Equals(" ") Then
                        Dim posUltEspacio = linea.LastIndexOf(" ")
                        If posUltEspacio < 0 Then
                            linea += " "
                        End If
                        Lineas.Add(texto.Substring(0, linea.LastIndexOf(" ")))
                        texto = texto.Substring(linea.LastIndexOf(" "))

                    Else
                        Lineas.Add(linea)
                        If Posicion + 1 > texto.Length Then
                            texto = ""
                        Else
                            texto = texto.Substring(Posicion + 1)
                        End If
                    End If
                Else
                    Lineas.Add(texto)
                    texto = ""
                End If
            End While
        Else
            Dim Posicion As Integer = 40

            While Not String.IsNullOrEmpty(texto)
                texto = texto.Trim()
                If Posicion + 1 < texto.Length Then
                    linea = texto.Substring(0, Posicion)
                    If Not texto.Chars(Posicion + 1).Equals(" ") Then
                        Dim posUltEspacio = linea.LastIndexOf(" ")
                        If posUltEspacio < 0 Then
                            linea += " "
                        End If
                        Lineas.Add(texto.Substring(0, linea.LastIndexOf(" ")))
                        texto = texto.Substring(linea.LastIndexOf(" "))
                    Else
                        Lineas.Add(linea)
                        If Posicion + 1 > texto.Length Then
                            texto = ""
                        Else
                            texto = texto.Substring(Posicion + 1)
                        End If
                    End If
                Else
                    Lineas.Add(texto)
                    texto = ""
                End If
            End While
        End If

        Return Lineas
    End Function

    Public Shared Function CentrarTexto(ByVal ancho As Int32, ByVal texto As String) As String
        If texto.Length > ancho Then
            texto = texto.Substring(0, ancho)
        End If
        Return Space((ancho - texto.Length) \ 2) & texto
    End Function

    Public Shared Function DobleColumnaTotal(ByVal texto1 As String, ByVal texto2 As String, Optional EsArmPos As Boolean = False) As String
        If EsArmPos Then
            If texto2.Length + texto1.Length > 32 Then
                If texto1.Length > 32 Then
                    texto1 = texto1.Substring(0, 32)
                End If
                texto2 = texto2.Substring(0, 32 - texto1.Length - 1)
            End If
            Return texto1 & Space(32 - texto1.Length - texto2.Length) & texto2
        Else
            If texto2.Length + texto1.Length > 40 Then

                texto2 = texto2.Substring(0, 40 - texto1.Length - 1)
            End If
            Return texto1 & Space(40 - texto1.Length - texto2.Length) & texto2
        End If
    End Function

    Public Shared Function TextAlign(text As String, length As Integer, align As Char, Optional fill As System.Nullable(Of Char) = Nothing) As String

        Try
            Dim t As String = text.Trim()
            Dim n As Integer = text.Length, d As Integer, i As Integer = 0
            Dim charFill As Char

            If (fill Is Nothing) Then
                charFill = " "c
            Else
                charFill = CChar(fill)
            End If

            If n > length Then
                t = t.Substring(0, length - 2) & ".."
            Else
                Select Case align
                    Case "L"c, "l"c
                        t = t.PadRight(length, charFill)
                        Exit Select
                    Case "C"c, "c"c
                        d = Int(CDbl((length - n) / CDbl(2)))
                        If ((length - n) Mod 2 <> 0) Then
                            i = 1
                        Else
                            i = 0
                        End If
                        t = (New [String](charFill, d) & t) & New [String](charFill, d + i)
                        Exit Select
                    Case "R"c, "r"c
                        t = t.PadLeft(length, charFill)
                        Exit Select
                End Select
            End If

            Return t
        Catch : Throw
        End Try
    End Function

    Public Class Tickete
        Dim Texto As New StringBuilder()
        Public Sub WriteLine(ByVal Text As String)
            Texto.AppendLine(Text)
        End Sub

        Public Function RecuperarTexto() As String
            Return Texto.ToString()
        End Function
    End Class
End Class
