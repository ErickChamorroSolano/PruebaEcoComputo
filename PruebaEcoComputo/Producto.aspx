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
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Nombre:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" Width="100%" MaxLength="30"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Precio:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrecio" runat="server" Width="100%" MaxLength="20"/>
                            <cc1:FilteredTextBoxExtender ID="ftePrecio" runat="server" TargetControlID="txtPrecio" FilterType="Custom,Numbers" FilterMode="ValidChars" ValidChars=".," />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Stock:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtStock" runat="server" Width="100%" MaxLength="3" TextMode="Number"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-top: 10px; padding-bottom: 10px;">
                            <asp:Label runat="server" ID="LblError" CssClass="Error" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding-top: 10px;">
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click"/>
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" PostBackUrl="~/Productos.aspx" UseSubmitBehavior="false"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>