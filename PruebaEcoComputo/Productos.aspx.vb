Imports System.ComponentModel.DataAnnotations
Imports System.IO
Imports OfficeOpenXml

Public Class Productos
    Inherits System.Web.UI.Page

    Dim oHelper As New DataAccess.DA
    Dim oUtilidades As New Utilidades

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                LblError.Text = String.Empty
                Dim Productos As DataTable = RecuperarProductos()
                Session("Productos") = Productos
                gvProductos.DataSource = Productos
                gvProductos.DataBind()
            Else
                LblError.Text = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub gvProductos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProductos.RowCommand
        Try
            Select Case e.CommandName
                Case "Actualizar"
                    Dim ID As String = CType(e.CommandSource, ImageButton).CommandArgument

                    Response.Redirect("Producto.aspx?Id=" & ID, False)
                Case "Eliminar"
                    Dim ID As String = CType(e.CommandSource, ImageButton).CommandArgument
                    oHelper.EliminarProducto(ID)
                    gvProductos.DataSource = RecuperarProductos()
                    gvProductos.DataBind()
            End Select
        Catch ex As System.Exception
            LblError.CssClass = "Error"
            Me.LblError.Text = ex.Message
        End Try
    End Sub

    Protected Function RecuperarProductos() As DataTable
        Dim Respuesta As DataTable
        Try
            Respuesta = oHelper.RecuperarProductos().Tables(0)
        Catch ex As Exception
            Throw ex
        End Try

        Return Respuesta
    End Function

    Protected Sub btnExcelOpciones_Click(sender As Object, e As EventArgs)
        Try
            Select Case CType(sender, Button).CommandArgument
                Case "Importar"
                    ventanaPopUp.Show()
                Case "Exportar"
                    Dim stringPath As String = "Downloads/EcoProducts.xlsx"
                    Dim ruta As String = Server.MapPath(stringPath)
                    Dim Productos As DataTable

                    If File.Exists(ruta) Then
                        File.Delete(ruta)
                    End If

                    If IsNothing(Session("Productos")) Then
                        Productos = oHelper.RecuperarProductos().Tables(0)
                    Else
                        Productos = Session("Productos")
                    End If

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial

                    Using pkg As New ExcelPackage(New FileInfo("EcoProducts.xlsx"))
                        Dim WS As ExcelWorksheet = pkg.Workbook.Worksheets.Add("Hoja 1")

                        WS.Cells.Item("A1").LoadFromDataTable(Productos, True)
                        WS.Cells.Item("C2:C" & (Productos.Rows.Count + 1)).Style.Numberformat.Format = "0.00"
                        WS.Cells.Item("D2:D" & (Productos.Rows.Count + 1)).Style.Numberformat.Format = "0"

                        Dim FI As New FileInfo(ruta)
                        pkg.SaveAs(FI)
                        Response.Redirect(stringPath, False)
                    End Using
                Case "Plantilla"
                    Response.Redirect("Downloads/CargaProductos.xlsx", False)
            End Select
        Catch ex As Exception
            LblError.CssClass = "Error"
            LblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnCargar_Click(sender As Object, e As EventArgs)
        Try
            Dim archivo As String = String.Empty
            Dim extension As String = String.Empty

            If FUExcel.HasFiles Then
                archivo = FUExcel.FileName
                extension = Path.GetExtension(archivo)

                If Not oUtilidades.ValidarExtension(extension.ToLower()) Then
                    Throw New Exception("El archivo debe ser de extensión .xlsx")
                End If

                Using st As New MemoryStream(FUExcel.FileBytes)

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial
                    Dim pkg As ExcelPackage = New ExcelPackage(st)
                    Dim NuevosProductos As DataTable = ExcelPackageToDataTable(pkg)

                    For Each prd As DataRow In NuevosProductos.Rows
                        oHelper.InsertarProducto(prd.Item("Nombre"), prd.Item("Precio"), prd.Item("Stock"))
                    Next

                    Dim Productos As DataTable = RecuperarProductos()
                    Session("Productos") = Productos
                    gvProductos.DataSource = Productos
                    gvProductos.DataBind()

                    LblError.CssClass = "Success"
                    LblError.Text = "Productos se han importado correctamente."
                End Using
            End If
        Catch ex As Exception
            LblError.CssClass = "Error"
            LblError.Text = ex.Message
        End Try
    End Sub

    Public Shared Function ExcelPackageToDataTable(ByVal excelPackage As ExcelPackage) As DataTable
        Try
            Dim dt As DataTable = New DataTable()
            Dim worksheet As ExcelWorksheet = excelPackage.Workbook.Worksheets(0)

            If worksheet.Dimension Is Nothing Then
                Return dt
            End If

            'columns
            For Each cell In worksheet.Cells(1, 1, 1, worksheet.Dimension.End.Column)
                dt.Columns.Add(cell.Text.Trim())
            Next

            'rows
            For r As Integer = 2 To worksheet.Dimension.End.Row
                Dim row = worksheet.Cells(r, 1, r, worksheet.Dimension.End.Column)
                Dim newRow As DataRow = dt.NewRow()

                For Each cell In row
                    newRow(cell.Start.Column - 1) = cell.Text
                Next

                dt.Rows.Add(newRow)
            Next

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class