Imports System.Linq
Imports System.Security.Cryptography
Imports Antlr.Runtime.Misc

Public Class Venta
    Inherits System.Web.UI.Page

    Dim oHelper As New DataAccess.DA
    Dim ListaProductos As New List(Of ClsProducto)
    Dim vSesion As String = "VentaProducto"
    Public Total As Decimal = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ViewState("EsProductoEncontrado") = False

                Dim Productos As DataTable = oHelper.RecuperarProductos().Tables(0)

                For Each item As DataRow In Productos.Rows
                    Dim prod As New ClsProducto With {
                        .Id = item.Item("Id"),
                        .Nombre = item.Item("Nombre"),
                        .Precio = item.Item("Precio"),
                        .Stock = item.Item("Stock")
                    }

                    ListaProductos.Add(prod)
                Next

                ListaProductos = (From prd In ListaProductos Order By prd.Nombre Select prd).ToList()
                Session("productos") = ListaProductos.ToList()
                ddlProductos.DataSource = ListaProductos
                ddlProductos.DataBind()

                CrearGrid()
            Else

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs)
        Try
            If Not CBool(ViewState("EsProductoEncontrado")) Then
                Throw New Exception("Debe ingresar un producto a vender.")
            End If

            If String.IsNullOrEmpty(txtCantidad.Text) Then
                Throw New Exception("Debe ingresar una cantidad.")
            ElseIf txtCantidad.Text = "0" Then
                Throw New Exception("La cantidad debe ser mayor que CERO.")
            End If

            If (CInt(txtStock.Text.Trim()) - CInt(txtCantidad.Text.Trim())) < 0 Then
                Throw New Exception("la cantidad excede a las existencias.")
            End If

            LoadGrid(ddlProductos.SelectedItem.Value, ddlProductos.SelectedItem.Text, txtPrecio.Text, txtCantidad.Text)
            ddlProductos.SelectedValue = "0"
            txtPrecio.Text = String.Empty
            txtStock.Text = String.Empty
            txtCantidad.Text = String.Empty
            txtCantidad.Enabled = False
            LblError.Text = String.Empty
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlProductos_DataBound(sender As Object, e As EventArgs)
        Try
            ddlProductos.Items.Insert(0, New ListItem("-- Seleccione --", "0"))
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub ddlProductos_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ListaProductos = Session("productos")
            If ddlProductos.SelectedValue <> "0" Then
                Dim TablaPro As New DataTable
                Try
                    TablaPro = Session(vSesion)
                    If TablaPro Is Nothing Then
                        Response.Redirect("~/Default.aspx")
                    End If
                Catch ex As System.Exception
                    Response.Redirect("~/Default.aspx")
                End Try

                Dim producto As New ClsProducto
                Dim CantidadObtenida As Integer = 0
                producto = (From prd In ListaProductos Where prd.Id = ddlProductos.SelectedValue Select prd).FirstOrDefault()

                txtPrecio.Text = producto.Precio

                For Each busqueda As DataRow In TablaPro.Rows
                    If busqueda("Id") = ddlProductos.SelectedItem.Value Then
                        CantidadObtenida = CInt(busqueda("Cantidad"))
                        Exit For
                    End If
                Next

                txtStock.Text = producto.Stock - CantidadObtenida
                txtCantidad.Enabled = True
                txtCantidad.Text = "1"
                ViewState("EsProductoEncontrado") = True


            Else
                txtPrecio.Text = String.Empty
                txtStock.Text = String.Empty
                txtCantidad.Text = String.Empty
                txtCantidad.Enabled = False
                ViewState("EsProductoEncontrado") = False
            End If
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Public Sub CrearGrid()
        Try
            Dim TablaPro As New DataTable(vSesion)
            Dim Col As New DataColumn

            Col = New DataColumn("Id", Type.GetType("System.String"))
            TablaPro.Columns.Add(Col)
            Col = New DataColumn("Nombre", Type.GetType("System.String"))
            TablaPro.Columns.Add(Col)
            Col = New DataColumn("Precio", Type.GetType("System.String"))
            TablaPro.Columns.Add(Col)
            Col = New DataColumn("Cantidad", Type.GetType("System.String"))
            TablaPro.Columns.Add(Col)
            Session(vSesion) = TablaPro
        Catch ex As System.Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Public Sub ClearGrid()
        Try
            Dim TablaPro As New DataTable
            Try
                TablaPro = Session(vSesion)
                If TablaPro Is Nothing Then
                    Response.Redirect("~/Default.aspx")
                End If
            Catch ex As System.Exception
                Response.Redirect("~/Default.aspx")
            End Try
            TablaPro.Rows.Clear()
            TablaPro.AcceptChanges()
            btnLimpiar.Visible = False
            btnComprar.Visible = False
            LblError.Text = String.Empty
            lblTotal.Visible = False
            Total = 0
        Catch ex As System.Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Public Sub LoadGrid(Id As Integer, Nombre As String, Precio As Decimal, Cantidad As Integer)
        Try
            Dim Fila As DataRow
            Dim TablaPro As New DataTable
            Try
                TablaPro = Session(vSesion)
                If TablaPro Is Nothing Then
                    Response.Redirect("~/Default.aspx")
                End If
            Catch ex As System.Exception
                Response.Redirect("~/Default.aspx")
            End Try

            Dim agregada As Boolean = False

            For Each busqueda As DataRow In TablaPro.Rows
                If busqueda("Id") = Id Then
                    Fila = TablaPro.NewRow()
                    Fila("Id") = Id
                    Fila("Nombre") = Nombre
                    Fila("Precio") = Precio.ToString()
                    Fila("Cantidad") = CInt(busqueda("Cantidad")) + Cantidad
                    TablaPro.Rows.Add(Fila)
                    TablaPro.Rows.Remove(busqueda)
                    agregada = True
                    Exit For
                End If
            Next

            If agregada = False Then

                Fila = TablaPro.NewRow()

                Fila("Id") = Id
                Fila("Nombre") = Nombre
                Fila("Precio") = Precio
                Fila("Cantidad") = Cantidad

                TablaPro.Rows.Add(Fila)
            End If

            btnComprar.Visible = True
            btnLimpiar.Visible = True
            gvVenta.DataSource = TablaPro
            gvVenta.DataBind()

            ViewState("EsProductoEncontrado") = False
            Dim x = gvVenta.Rows.Count
        Catch ex As System.Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub CalcularTotal() Handles gvVenta.DataBound
        Try
            Dim TablaPro As New DataTable
            Try
                TablaPro = Session(vSesion)
                If TablaPro Is Nothing Then
                    Response.Redirect("~/Default.aspx")
                End If
            Catch ex As System.Exception
                Response.Redirect("~/Default.aspx")
            End Try

            For Each itm As DataRow In TablaPro.Rows
                Total += CDec(itm.Item("Precio")) * CDec(itm.Item("Cantidad"))
            Next

            lblTotal.Visible = IIf(gvVenta.Rows.Count <= 0, False, True)
            lblTotal.Text = "Total: $" & Total.ToString()
            ViewState("Total") = Total
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub gvVenta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvVenta.RowCommand
        Try
            Dim TablaPro As New DataTable
            Select Case e.CommandName
                Case "Eliminar"
                    'obtener boton de acciones
                    Dim Imagen As ImageButton = CType(e.CommandSource, ImageButton)
                    'obtener celda de indice seleccionado
                    Dim Celda As DataControlFieldCell = CType(Imagen.Parent, DataControlFieldCell)
                    'obtener fila de indice seleccionado
                    Dim fila As GridViewRow = CType(Celda.Parent, GridViewRow)
                    'Cargar los datos de la tabla                    
                    Try
                        TablaPro = Session(vSesion)
                        If TablaPro Is Nothing Then
                            Response.Redirect("~/Default.aspx")
                        End If
                    Catch ex As System.Exception
                        Response.Redirect("~/Default.aspx")
                    End Try

                    'Eliminar fila
                    TablaPro.Rows.RemoveAt(fila.RowIndex)
                    'aceptar cambios?
                    TablaPro.AcceptChanges()
                    'asignar nuevos datos a la grilla
                    gvVenta.DataSource = TablaPro
                    'poblar nuevos datos a la grilla
                    gvVenta.DataBind()

                    ddlProductos.SelectedValue = "0"
                    txtPrecio.Text = String.Empty
                    txtStock.Text = String.Empty
                    txtCantidad.Text = String.Empty
                    txtCantidad.Enabled = False

                    'Ocultar boton de generar si no hay datos en la grilla
                    'en otras palabras, si elimina el único dato en la grilla...
                    If TablaPro.Rows.Count = 0 Then
                        btnComprar.Visible = False
                        'Limpiar grilla
                        ClearGrid()
                    Else
                        btnComprar.Visible = True
                    End If
            End Select
        Catch ex As System.Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnComprar_Click(sender As Object, e As EventArgs)
        Try
            Dim TablaPro As New DataTable
            Dim Recibo As Integer = -1
            Try
                TablaPro = Session(vSesion)
                If TablaPro Is Nothing Then
                    Response.Redirect("~/Default.aspx")
                End If
            Catch ex As System.Exception
                Response.Redirect("~/Default.aspx")
            End Try

            Recibo = oHelper.InsertarVenta(ViewState("Total"))

            If Not Recibo <= 0 Then
                For Each itm As DataRow In TablaPro.Rows
                    oHelper.InsertarVentaDetalle(Recibo, itm.Item("Id"), itm.Item("Cantidad"))
                Next
            End If

            'notificar mensaje antes de redirecionar
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Notificacion", "alert('" & "Venta realizada con éxito. Recibo: " & Recibo.ToString() & "');", True)
            btnLimpiar_Click(sender, e)
            'Response.Redirect("Default.aspx", False)
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnLimpiar_Click(sender As Object, e As EventArgs)
        Try
            ClearGrid()
            lblTotal.Visible = False
            btnComprar.Visible = False
            gvVenta.DataBind()
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub
End Class