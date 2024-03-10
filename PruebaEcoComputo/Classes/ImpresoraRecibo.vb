Imports System.Globalization
Imports System.IO
Imports System.Runtime.Remoting.Messaging

Public Class ImpresoraRecibo
    Inherits FormatoImpresora

    Public Shared Function ImprimirRecibo(Recibo As Integer) As String
        Dim Respuesta As String = ""

        Try
            Dim OHelper As New DataAccess.DA
            Dim Parrafo As List(Of String)
            Dim Ancho As Short = 40
            Dim SeparadorGuion As String = StrDup(Ancho, "-")
            Dim Existe As Boolean = False
            Dim TotalVenta As Decimal = 0

            Dim FechaHora As DateTime

            'COMIENZA EL TICKETE
            Dim SW As New Tickete
            Dim ODatos As DataSet = OHelper.RecuperarVentaYDetalle(Recibo)
            If ODatos.Tables.Count > 0 Then
                For Each Encabezado As DataRow In ODatos.Tables(0).Rows
                    FechaHora = Encabezado.Item("FechaHora")
                    TotalVenta = Encabezado.Item("ValorTotal")
                Next

                Parrafo = SeccionarParrafo("EcoComputo", False)
                For Each Linea As String In Parrafo
                    SW.WriteLine(CentrarTexto(Ancho, Linea))
                Next
                Parrafo.Clear()

                SW.WriteLine(SeparadorGuion)
                SW.WriteLine(CentrarTexto(Ancho, "INFORMACION DE VENTA"))
                SW.WriteLine(SeparadorGuion)

                SW.WriteLine("Recibo: " & Recibo)
                SW.WriteLine(DobleColumnaTotal("Fecha:", FechaHora))

                SW.WriteLine(SeparadorGuion)
                SW.WriteLine(CentrarTexto(Ancho, "Nombre del producto y/o servicio"))
                SW.WriteLine(SeparadorGuion)

                'DETALLES DE PRODUCTO
                For Each Detalle As DataRow In ODatos.Tables(1).Rows
                    SW.WriteLine("Producto: " & Detalle.Item("Nombre"))
                    SW.WriteLine(TextAlign("Codigo", 10, "L") & TextAlign("Cantidad", 8, "R") & TextAlign("Precio", 10, "R") & TextAlign("Total", 12, "R"))
                    SW.WriteLine(TextAlign(Detalle.Item("Id"), 10, "L") & TextAlign(Detalle.Item("Cantidad"), 8, "R") & TextAlign(CDec(Detalle.Item("Precio")).ToString("C2"), 10, "R") & TextAlign(CDec(Detalle.Item("Valor")).ToString("C2"), 12, "R"))
                    SW.WriteLine(SeparadorGuion)
                Next

                SW.WriteLine(DobleColumnaTotal("TOTAL ", TotalVenta.ToString("C2")))

                Respuesta = SW.RecuperarTexto()
            End If
        Catch ex As Data.DataException
            'LogFallas.ReportarError(ex, "ImprimirReciboCanastilla5UVT", "Recibo: " & Recibo.ToString(), "ImpresionRecibos")
            Throw ex
        Catch ex As Data.SqlClient.SqlException
            'LogFallas.ReportarError(ex, "ImprimirReciboCanastilla5UVT", "Recibo: " & Recibo.ToString(), "ImpresionRecibos")
            Throw ex
        Catch ex As IO.IOException
            'LogFallas.ReportarError(ex, "ImprimirReciboCanastilla5UVT", "Recibo: " & Recibo.ToString(), "ImpresionRecibos")
            Throw ex
        Catch ex As System.Exception
            'LogFallas.ReportarError(ex, "ImprimirReciboCanastilla5UVT", "Recibo: " & Recibo.ToString(), "ImpresionRecibos")
            Throw ex
        End Try

        Return Respuesta
    End Function
End Class
