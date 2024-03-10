Public Class ClsProducto
    Private _Id As Integer
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    Private _Nombre As String
    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property

    Private _Precio As Decimal
    Public Property Precio() As Decimal
        Get
            Return _Precio
        End Get
        Set(ByVal value As Decimal)
            _Precio = value
        End Set
    End Property

    Private _Stock As Integer
    Public Property Stock() As Integer
        Get
            Return _Stock
        End Get
        Set(ByVal value As Integer)
            _Stock = value
        End Set
    End Property
End Class
