Imports System.Runtime

Public Class ConsultarVenta
    Inherits System.Web.UI.Page

    Dim oHelper As New DataAccess.DA

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrEmpty(txtRecibo.Text.Trim()) Then
                Throw New Exception("Debe ingresar un recibo.")
            End If

            Dim Respuesta As String = ImpresoraRecibo.ImprimirRecibo(txtRecibo.Text.Trim())

            TxtReciboVenta.Text = Respuesta
            ventanaPopUp.Show()
            LblError.Text = String.Empty
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub
End Class