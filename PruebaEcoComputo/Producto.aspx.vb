Public Class Producto
    Inherits System.Web.UI.Page

    Dim oHelper As New DataAccess.DA

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Request.QueryString.Count > 0 Then
                    ViewState("IdProducto") = Request.QueryString("Id")

                    If ViewState("IdProducto") = "-1" Then
                        btnAceptar.Text = "Agregar"
                    Else
                        btnAceptar.Text = "Editar"
                        RecuperarProducto(ViewState("IdProducto"))
                    End If
                Else
                    Response.Redirect("Productos.aspx", True)
                End If
            End If
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub RecuperarProducto(Id As String)
        Try
            Dim Producto As DataSet = oHelper.RecuperarProductoPorID(Id)

            If Producto.Tables.Count > 0 Then
                If Producto.Tables(0).Rows.Count > 0 Then
                    For Each item As DataRow In Producto.Tables(0).Rows
                        txtNombre.Text = item.Item("Nombre").ToString()
                        txtPrecio.Text = item.Item("Precio").ToString().Replace(",", ".")
                        txtStock.Text = item.Item("Stock").ToString()
                    Next
                Else
                    Throw New Exception("No existe producto con el ID recibido.")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs)
        Try
            LblError.Text = String.Empty
            Dim Nombre As String = txtNombre.Text.Trim()
            Dim Precio As Decimal = 0
            Dim Stock As Integer = 0

            If String.IsNullOrEmpty(Nombre) Then
                Throw New Exception("Debe ingresar un nombre de producto.")
            End If

            If Not String.IsNullOrEmpty(txtPrecio.Text.Trim()) Then
                If Not IsNumeric(txtPrecio.Text.Trim()) Then
                    Throw New Exception("Debe digitar un valor valido para el PRECIO.")
                ElseIf txtPrecio.Text = "0" Then
                    Throw New Exception("El valor debe ser mayor que CERO.")
                Else
                    Precio = txtPrecio.Text.Trim()
                End If
            Else
                Throw New Exception("Debe ingresar un valor para el PRECIO.")
            End If

            If Not String.IsNullOrEmpty(txtStock.Text.Trim()) Then
                If Not IsNumeric(txtStock.Text.Trim()) Then
                    Throw New Exception("Debe digitar un valor valido para el PRECIO.")
                ElseIf txtStock.Text = "0" Then
                    Throw New Exception("El stock debe ser mayor que CERO.")
                Else
                    Stock = txtStock.Text.Trim()
                End If
            Else
                Throw New Exception("Debe ingresar un valor para el Stock.")
            End If

            If ViewState("IdProducto") = "-1" Then
                oHelper.InsertarProducto(Nombre, Precio, Stock)
            Else
                oHelper.ModificarProducto(ViewState("Id"), Nombre, Precio, Stock)
            End If
            Response.Redirect("Productos.aspx", False)
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub
End Class