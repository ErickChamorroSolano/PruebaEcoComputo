<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Venta.aspx.vb" Inherits="PruebaEcoComputo.Venta" MasterPageFile="~/Site.Master" Title="Venta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding:5px; margin:10px; font-weight:bold;">
        <asp:label Id="LblTitulo" runat="server" Text="Venta" />
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlVenta" runat="server" CssClass="panelForm" Width="500px">
                <table class="w-100">
                    <%-- PRODUCTO ID --%>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Producto:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProductos" CssClass="form-select" runat="server" Width="100%" AutoPostBack="true" DataTextField="Nombre" DataValueField="Id" OnDataBound="ddlProductos_DataBound" OnSelectedIndexChanged="ddlProductos_SelectedIndexChanged"/>
                        </td>
                    </tr>
                    <%-- PRECIO --%>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Precio:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtPrecio" runat="server" Width="100%" CssClass="form-control" MaxLength="20" Enabled="false"/>
                        </td>
                    </tr>
                    <%-- STOCK --%>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Stock:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtStock" runat="server" Width="100%" CssClass="form-control" MaxLength="3" Enabled="false"/>
                        </td>
                    </tr>
                    <%-- CANTIDAD --%>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Cantidad:" />
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtCantidad" runat="server" Width="100%" CssClass="form-control" MaxLength="3" Enabled="false"/>
                            <cc1:FilteredTextBoxExtender ID="fteCantidad" runat="server" TargetControlID="txtCantidad" FilterType="Custom, Numbers" FilterMode="ValidChars" ValidChars=".,"/>
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
                            <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAceptar_Click"/>
                            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-outline-primary" Text="Cancelar" PostBackUrl="~/Default.aspx" UseSubmitBehavior="false"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <br />

            <%-- TABLA VENTAPRODUCTO --%>
            <asp:Panel ID="pnlTabla" runat="server" Width="100%">
                <asp:Table ID="tblVenta" runat="server" Width="700px">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Button ID="btnComprar" runat="server" CssClass="btn btn-warning" Text="Comprar" Visible="false" OnClick="btnComprar_Click"/>
                            &nbsp;
                            <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-danger" Text="Limpiar" Visible="false" OnClick="btnLimpiar_Click"/>
                        </asp:TableCell>
                        <asp:TableCell style="text-align: right;">
                            <asp:Label ID="lblTotal" runat="server" CssClass="form-label" Visible="false" Font-Size="Large" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" style="padding-top:5px;">
                            <asp:GridView ID="gvVenta" runat="server" Width="700px" CssClass="table table-primary table-hover table-striped" DataKeyNames="Id" AutoPostBack="True" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Id" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" SortExpression="Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" Text='<%# Eval("Id")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNombre" Text='<%# Eval("Nombre")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Precio" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrecio" Text='<%# "$ " & Eval("Precio")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantidad" Text='<%# Eval("Cantidad")%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                        <ItemTemplate>        
                                            <asp:ImageButton ID="ibtnEliminar" runat="server" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id")%>' ImageUrl="~/img/eliminar.jpg" ToolTip="Eliminar dato" Width="32px" Height="32px" OnClientClick=" return confirm('¿Está seguro que desea eliminar este registro?');"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>