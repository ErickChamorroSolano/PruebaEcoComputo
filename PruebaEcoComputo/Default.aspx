<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="PruebaEcoComputo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Prueba EcoComputo</h1>
            <p class="lead">By Erick Chamorro</p>
        </section>

        <div class="row">
            <section class="col-md-8" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Seguimiento de Ventas</h2>
                <p>
                    Debes crear un programa en Visual Basic para el seguimiento de ventas en una tienda. El programa debe realizar lo siguiente:
                    <br />
                    Definir una clase llamada Producto con los siguientes campos:
                    <br />
                    <br />
                    ID (entero): un identificador único para el producto.<br />
                    Nombre (cadena): el nombre del producto.<br />
                    Precio (decimal): el precio unitario del producto.<br />
                    Stock (entero): la cantidad de existencias del producto.
                    <br />
                    <br />
                    Implementar una clase principal GestorVentas que tenga una lista de productos y permita realizar las siguientes operaciones:
                    <br />
                    <br />
                    Agregar un producto a la lista.<br />
                    Mostrar la lista de productos con su información.<br />
                    Realizar una venta, que debe reducir la cantidad de existencias del producto vendido.<br />
                    Calcular e imprimir el total de ventas realizadas.<br />
                    Salir del programa.<br />
                    Utiliza una estructura de datos adecuada para almacenar la lista de productos y asegúrate de que el programa maneje la existencia de productos correctamente.<br />
                    <br />
                    <br />
                    Proporciona una opción para guardar y cargar la información de productos en un archivo
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="Options" aria-orientation="vertical">
                <h3 id="OptionsTitle">¿Que desea hacer?</h3>
                <br />
                <br />
                <asp:Button ID="btnProductos" runat="server" CssClass="btn btn-primary btn-lg" Text="Ver Productos" PostBackUrl="~/Productos.aspx"/>
                <br />
                <br />
                <asp:Button ID="btnVentas" runat="server" CssClass="btn btn-success btn-lg" Text="Realizar una venta" PostBackUrl="~/Venta.aspx"/>
            </section>
        </div>
    </main>

</asp:Content>
