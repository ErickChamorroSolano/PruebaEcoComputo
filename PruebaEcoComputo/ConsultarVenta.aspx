<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConsultarVenta.aspx.vb" Inherits="PruebaEcoComputo.ConsultarVenta" MasterPageFile="~/Site.Master" Title="Consultar Venta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding:5px; margin:10px; font-weight:bold;">
        <asp:label Id="LblTitulo" runat="server" Text="Consultar Venta" />
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlConsultarVenta" runat="server" CssClass="panelForm" Width="500px">
                <table class="w-100">
                    <%-- VENTA --%>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Venta:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtRecibo" runat="server" Width="100%" CssClass="form-control" placeholder="Ingrese numero de recibo"/>
                            <cc1:FilteredTextBoxExtender ID="fteRecibo" runat="server" TargetControlID="txtRecibo" FilterType="Numbers" />
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
                            <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar" OnClick="btnBuscar_Click"/>
                            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-outline-primary" Text="Cancelar" PostBackUrl="~/Default.aspx" UseSubmitBehavior="false"/>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- POPUP RECIBO --%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <cc1:ModalPopupExtender ID="ventanaPopUp" runat="server" TargetControlID="BtnEnlace3" PopupDragHandleControlID="pnlPopup" PopupControlID="pnlPopup" CancelControlID="btnCancelarPopup" BackgroundCssClass="modalBackground"/>
            <asp:Panel ID="pnlPopup" runat="server" CssClass="panelPopup" BorderStyle="Double" BorderColor="Black" BackColor="DarkGray">
                <div id="divPrnt" class="divPrint">
                    <asp:Panel ID="pnlReciboVenta" runat="server" BorderStyle="None" CssClass="withShadow" Style="Width: auto; Height: auto; background: white; overflow-y: hidden; resize: none; margin: 20px 0px; border-radius: 3px; padding: 15px 20px 50px 15px;">
                        <asp:Label ID="TxtReciboVenta" Text="text" runat="server" style="font-family: Consolas, sans-serif; font-size: 11pt; color: black; white-space: pre;"/>
                    </asp:Panel>
                </div>
                <div class="divPrintAcciones">
                    <asp:Button ID="btnImprimir" CssClass="btn btn-primary" Visible="true" Text="Imprimir" runat="server" OnClientClick="PrintPartOfPage('divPrnt')" />
                    &nbsp;
                    <asp:Button ID="btnCancelarPopup" CssClass="btn btn-primary" Visible="true" Text="Cancelar" runat="server" />
                </div>
            </asp:Panel>
            <asp:Button Style="display: none;" ID="BtnEnlace3" runat="server" Text="Button"/>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function PrintPartOfPage(dvprintid) {
            var prtContent = document.getElementById(dvprintid);
            var WinPrint = window.open('', '', 'left=100,top=100,width=800,height=800');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
    </script>
</asp:Content>

