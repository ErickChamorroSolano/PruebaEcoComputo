Public Class Utilidades
    Public Shared HayMsjNotificacion As Boolean = False

    Public Function ValidarExtension(ByVal Ext As String) As Boolean
        Try
            Dim Ok As Boolean
            Select Case Ext
                Case ".xlsx"
                    Ok = True
                Case Else
                    Ok = False
            End Select
            Return Ok
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function
End Class
