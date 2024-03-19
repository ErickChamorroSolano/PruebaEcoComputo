<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Producto.aspx.vb" Inherits="PruebaEcoComputo.Producto" MasterPageFile="~/Site.Master" Title="Producto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding:5px; margin:10px; font-weight:bold;">
        <asp:label Id="LblTitulo" runat="server" Text="Producto" />
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlProducto" runat="server" CssClass="panelForm" Width="400px">
                <table class="w-100">
                    <%-- NOMBRE --%>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Nombre:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Width="100%" MaxLength="30"/>
                        </td>
                    </tr>
                    <%-- PRECIO --%>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Precio:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" Width="100%" MaxLength="20"/>
                            <cc1:FilteredTextBoxExtender ID="ftePrecio" runat="server" TargetControlID="txtPrecio" FilterType="Custom,Numbers" FilterMode="ValidChars" ValidChars=".," />
                        </td>
                    </tr>
                    <%-- STOCK --%>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Stock:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" Width="100%" MaxLength="3" TextMode="Number"/>
                        </td>
                    </tr>
                    <%-- ERROR LABEL --%>
                    <tr>
                        <td colspan="2" style="padding-top: 10px; padding-bottom: 10px;">
                            <asp:Label runat="server" ID="LblError" CssClass="Error" />
                        </td>
                    </tr>
                    <%-- BOTONES --%>
                    <tr>
                        <td colspan="2" style="padding-top: 10px;">
                            <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-primary" Text="Aceptar" OnClick="btnAceptar_Click"/>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-primary" PostBackUrl="~/Productos.aspx" UseSubmitBehavior="false"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>