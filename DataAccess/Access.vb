Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common
Imports System.IO
Imports System.Configuration
Imports System.Web
Imports System
Imports System.Net
Imports System.Data.SqlClient

Public Class DA

    Dim ConnectionStringName As String = "PruebaDBContext"

#Region "Producto"
    Public Function RecuperarProductos() As DataSet
        Try
            Dim factory As DatabaseProviderFactory = New DatabaseProviderFactory()
            Dim DB As Database = factory.Create(ConnectionStringName)
            Dim DatabaseCommand As DbCommand = DB.GetStoredProcCommand("RecuperarProductos")

            Using connection As DbConnection = DB.CreateConnection()
                connection.Open()

                Try
                    Return DB.ExecuteDataSet(DatabaseCommand)
                Catch ex As Exception
                    Throw ex
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RecuperarProductoPorID(Id As String) As DataSet
        Try
            Dim factory As DatabaseProviderFactory = New DatabaseProviderFactory()
            Dim DB As Database = factory.Create(ConnectionStringName)
            Dim DatabaseCommand As DbCommand = DB.GetStoredProcCommand("RecuperarProductoPorID")

            DB.AddInParameter(DatabaseCommand, "Id", DbType.Int32, Id)

            Using connection As DbConnection = DB.CreateConnection()
                connection.Open()

                Try
                    Return DB.ExecuteDataSet(DatabaseCommand)
                Catch ex As Exception
                    Throw ex
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub InsertarProducto(Nombre As String, Precio As Decimal, Stock As Integer)
        Try
            Dim factory As New DatabaseProviderFactory()
            Dim DB As Database = factory.Create(ConnectionStringName)
            Dim DatabaseCommand As DbCommand = DB.GetStoredProcCommand("InsertarProducto")

            DB.AddInParameter(DatabaseCommand, "Nombre", DbType.String, Nombre)
            DB.AddInParameter(DatabaseCommand, "Precio", DbType.Decimal, Precio)
            DB.AddInParameter(DatabaseCommand, "Stock", DbType.Int32, Stock)

            Using connection As DbConnection = DB.CreateConnection()
                connection.Open()

                Try
                    DB.ExecuteNonQuery(DatabaseCommand)
                Catch ex As Exception
                    Throw ex
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ModificarProducto(Id As Integer, Nombre As String, Precio As Decimal, Stock As Integer)
        Try
            Dim factory As New DatabaseProviderFactory()
            Dim DB As Database = factory.Create(ConnectionStringName)
            Dim DatabaseCommand As DbCommand = DB.GetStoredProcCommand("ModificarProducto")

            DB.AddInParameter(DatabaseCommand, "Id", DbType.Int32, Id)
            DB.AddInParameter(DatabaseCommand, "Nombre", DbType.String, Nombre)
            DB.AddInParameter(DatabaseCommand, "Precio", DbType.Decimal, Precio)
            DB.AddInParameter(DatabaseCommand, "Stock", DbType.Int32, Stock)

            Using connection As DbConnection = DB.CreateConnection()
                connection.Open()

                Try
                    DB.ExecuteNonQuery(DatabaseCommand)
                Catch ex As Exception
                    Throw ex
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub EliminarProducto(Id As String)
        Try
            Dim factory As DatabaseProviderFactory = New DatabaseProviderFactory()
            Dim DB As Database = factory.Create(ConnectionStringName)
            Dim DatabaseCommand As DbCommand = DB.GetStoredProcCommand("EliminarProducto")

            DB.AddInParameter(DatabaseCommand, "Id", DbType.Int32, Id)

            Using connection As DbConnection = DB.CreateConnection()
                connection.Open()

                Try
                    DB.ExecuteNonQuery(DatabaseCommand)
                Catch ex As Exception
                    Throw ex
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Venta"
    Public Function InsertarVenta(ValorTotal As Decimal) As Integer
        Try
            Dim factory As New DatabaseProviderFactory()
            Dim DB As Database = factory.Create(ConnectionStringName)
            Dim DatabaseCommand As DbCommand = DB.GetStoredProcCommand("InsertarVenta")

            DB.AddInParameter(DatabaseCommand, "ValorTotal", DbType.Decimal, ValorTotal)

            Using connection As DbConnection = DB.CreateConnection()
                connection.Open()

                Try
                    Return DB.ExecuteScalar(DatabaseCommand)
                Catch ex As Exception
                    Throw ex
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub InsertarVentaDetalle(Recibo As Integer, IdProducto As Integer, Cantidad As Integer)
        Try
            Dim factory As New DatabaseProviderFactory()
            Dim DB As Database = factory.Create(ConnectionStringName)
            Dim DatabaseCommand As DbCommand = DB.GetStoredProcCommand("InsertarVentaDetalle")

            DB.AddInParameter(DatabaseCommand, "Recibo", DbType.Int32, Recibo)
            DB.AddInParameter(DatabaseCommand, "IdProducto", DbType.Int32, IdProducto)
            DB.AddInParameter(DatabaseCommand, "Cantidad", DbType.Int32, Cantidad)

            Using connection As DbConnection = DB.CreateConnection()
                connection.Open()

                Try
                    DB.ExecuteNonQuery(DatabaseCommand)
                Catch ex As Exception
                    Throw ex
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function RecuperarVentaYDetalle(Recibo As Integer) As DataSet
        Try
            Dim factory As DatabaseProviderFactory = New DatabaseProviderFactory()
            Dim DB As Database = factory.Create(ConnectionStringName)
            Dim DatabaseCommand As DbCommand = DB.GetStoredProcCommand("RecuperarVenta")

            DB.AddInParameter(DatabaseCommand, "Recibo", DbType.Int32, Recibo)

            Using connection As DbConnection = DB.CreateConnection()
                connection.Open()

                Try
                    Return DB.ExecuteDataSet(DatabaseCommand)
                Catch ex As Exception
                    Throw ex
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class
