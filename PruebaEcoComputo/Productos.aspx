<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Productos.aspx.vb" Inherits="PruebaEcoComputo.Productos" MasterPageFile="~/Site.Master" Title="Productos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding:5px; margin:10px; font-weight:bold;">
        <asp:label Id="LblTitulo" runat="server" Text="Productos" />
    </div>
    <div style="padding:5px; padding-left:0px;">
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table class="w-100">
                <tr>
                    <td style="text-align:left;">
                        <asp:Button id="BtnAgregar" runat="server" CssClass="btn btn-success" Text="+ Agregar Producto" PostBackUrl="~/Producto.aspx?Id=-1" />
                    </td>
                    <td style="display: flex; justify-content: flex-end;">
                        <asp:Panel runat="server" class="btn-group" role="group">
                            <asp:Label runat="server" Text="Excel:" style="padding-top:7px; margin-right:10px;"/>
                            <asp:Button ID="btnImportar" runat="server" class="btn btn-outline-success" Text="Importar" CommandArgument="Importar" OnClick="btnExcelOpciones_Click"/>
                            <asp:Button ID="btnExportar" runat="server" class="btn btn-outline-success" Text="Exportar" CommandArgument="Exportar" OnClick="btnExcelOpciones_Click"/>
                            <asp:Button ID="btnDescargar" runat="server" class="btn btn-outline-success" Text="Plantilla" CommandArgument="Plantilla" OnClick="btnExcelOpciones_Click"/>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-top: 10px;">
                        <asp:GridView ID="gvProductos" runat="server" Width="100%" CssClass="table table-striped" DataKeyNames="Id" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" EmptyDataText="No hay datos para mostrar" CellPadding="10">
                            <Columns>
                                <asp:BoundField DataField="Id" SortExpression="Id" HeaderText="Id" ItemStyle-HorizontalAlign="Center" Visible="False" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="Precio" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock" SortExpression="Stock"/>
                                <asp:TemplateField ShowHeader="False" HeaderText="Editar" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ActionHeader" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnEditar" Width="32px" Height="32px" runat="server" ImageUrl="~/img/edit.png" ToolTip="Seleccionar" CausesValidation="False" CommandName="Actualizar" CommandArgument='<%# Eval("Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False" HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ActionHeader" HeaderStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgBtnEliminar" Width="32px" Height="32px" runat="server" ImageUrl="~/img/eliminar.jpg" ToolTip="Seleccionar" CausesValidation="False" CommandName="Eliminar" CommandArgument='<%# Eval("Id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;">
                        <asp:Label runat="server" ID="LblError" CssClass="Error"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- POPUP CARGA ARCHIVO --%>
    <cc1:ModalPopupExtender ID="ventanaPopUp" runat="server" TargetControlID="BtnEnlace3" PopupDragHandleControlID="pnlPopup" PopupControlID="pnlPopup" CancelControlID="btnCancelarPopup" BackgroundCssClass="modalBackground"/>
    <asp:Panel ID="pnlPopup" runat="server" Width="700px" Height="150px" BorderStyle="Double" BorderColor="Black" BackColor="White" style="padding: 10px;">
        <table class="w-100">
            <tr>
                <td style="text-align:center;">
                    <asp:Label Text="Importar productos mediante Excel" runat="server" CssClass="modal-title" Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <td style="padding-top: 10px;">
                    <div class="input-group">
                        <asp:FileUpload ID="FUExcel" runat="server" CssClass="form-control"/>
                        <asp:Button ID="btnCargar" runat="server" class="btn btn-outline-success" Text="Cargar" OnClick="btnCargar_Click"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="d-grid gap-2 pt-2">
                    <asp:Button ID="btnCancelarPopup" CssClass="btn btn-primary" Text="Cancelar" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Button Style="display: none;" ID="BtnEnlace3" runat="server" Text="Button"/>
</asp:Content>