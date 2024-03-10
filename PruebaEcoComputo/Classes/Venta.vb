Public Class ClsVenta
    Private _Recibo As Integer
    Public Property Recibo() As Integer
        Get
            Return _Recibo
        End Get
        Set(ByVal value As Integer)
            _Recibo = value
        End Set
    End Property

    Private _Fecha As DateTime
    Public Property Fecha() As DateTime
        Get
            Return _Fecha
        End Get
        Set(ByVal value As DateTime)
            _Fecha = value
        End Set
    End Property
End Class

Public Class VentaProducto
    Private _Recibo As Integer
    Public Property Recibo() As Integer
        Get
            Return _Recibo
        End Get
        Set(ByVal value As Integer)
            _Recibo = value
        End Set
    End Property

    Private _IdProducto As Integer
    Public Property IdProducto() As Integer
        Get
            Return _IdProducto
        End Get
        Set(ByVal value As Integer)
            _IdProducto = value
        End Set
    End Property

    Private _NombreProducto As String
    Public Property NombreProducto() As String
        Get
            Return _NombreProducto
        End Get
        Set(ByVal value As String)
            _NombreProducto = value
        End Set
    End Property

    Private _PrecioProducto As Decimal
    Public Property PrecioProducto() As Decimal
        Get
            Return _PrecioProducto
        End Get
        Set(ByVal value As Decimal)
            _PrecioProducto = value
        End Set
    End Property

    Private _Cantidad As Integer
    Public Property Cantidad() As Integer
        Get
            Return _Cantidad
        End Get
        Set(ByVal value As Integer)
            _Cantidad = value
        End Set
    End Property
End Class
