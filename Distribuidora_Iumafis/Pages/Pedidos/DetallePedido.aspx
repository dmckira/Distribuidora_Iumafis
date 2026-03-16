<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetallePedido.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Pedidos.DetallePedido" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Detalle Pedido - Distribuidora Iumafis</title>
    <style>
        @media print {
            .navbar, .page-header .btn, .form-actions, .footer { display:none !important; }
            .card { box-shadow:none; border:1px solid #ddd; }
        }
        .comprobante-header { text-align:center; padding:20px; border-bottom:2px solid #9c27b0; margin-bottom:20px; }
        .comprobante-header h2 { color:#6a1b9a; font-size:22px; }
        .comprobante-header p { color:#888; font-size:13px; }
        .info-grid { display:grid; grid-template-columns:1fr 1fr; gap:16px; margin-bottom:20px; }
        .info-item { background:#faf4fc; padding:12px 16px; border-radius:8px; }
        .info-item .label { font-size:11px; text-transform:uppercase; color:#9c27b0; font-weight:700; margin-bottom:4px; }
        .info-item .value { font-size:15px; color:#333; font-weight:600; }
        .total-row { background:#6a1b9a; color:#fff; font-weight:700; }
        .total-row td { padding:14px !important; font-size:16px; }
    </style>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-file-alt"></i> Comprobante de Pedido</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / <a href="ListarPedidos.aspx">Pedidos</a> / Detalle</div>
        </div>
        <div style="display:flex;gap:8px;">
            <button onclick="window.print()" class="btn btn-info"><i class="fa fa-print"></i> Imprimir</button>
            <a href="EditarPedido.aspx?id=<%=PedidoId%>" class="btn btn-warning"><i class="fa fa-edit"></i> Cambiar Estado</a>
            <a href="../Pagos/CrearPago.aspx?pedidoId=<%=PedidoId%>" class="btn btn-success"><i class="fa fa-credit-card"></i> Registrar Pago</a>
            <a href="ListarPedidos.aspx" class="btn btn-secondary"><i class="fa fa-arrow-left"></i> Volver</a>
        </div>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert alert-success">
        <i class="fa fa-check-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <div class="card" id="comprobante">
        <div class="comprobante-header">
            <h2><i class="fa fa-spa"></i> Distribuidora Iumafis</h2>
            <p>Distribuidora</p>
        </div>

        <div class="info-grid">
            <div class="info-item">
                <div class="label">Número de Pedido</div>
                <div class="value">#<asp:Label ID="lblPedidoId" runat="server" /></div>
            </div>
            <div class="info-item">
                <div class="label">Fecha del Pedido</div>
                <div class="value"><asp:Label ID="lblFecha" runat="server" /></div>
            </div>
            <div class="info-item">
                <div class="label">Cliente</div>
                <div class="value"><asp:Label ID="lblCliente" runat="server" /></div>
            </div>
            <div class="info-item">
                <div class="label">Estado</div>
                <div class="value"><asp:Label ID="lblEstado" runat="server" /></div>
            </div>
        </div>

        <div class="table-responsive">
            <table class="data-table">
                <thead>
                    <tr>
                        <th>Producto</th>
                        <th>Precio Unit.</th>
                        <th>Cantidad</th>
                        <th>Subtotal</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDetalles" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ProductoNombre")%></td>
                                <td><%#string.Format("{0:C2}", Eval("PrecioUnitario"))%></td>
                                <td><%#Eval("Cantidad")%></td>
                                <td><%#string.Format("{0:C2}", Eval("Subtotal"))%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr class="total-row">
                        <td colspan="3" style="text-align:right;">TOTAL:</td>
                        <td><asp:Label ID="lblTotal" runat="server" /></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-credit-card"></i> Pagos Registrados</div>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvPagos" runat="server" CssClass="data-table" AutoGenerateColumns="false" EmptyDataText="">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="TipoPago" HeaderText="Tipo de Pago" />
                    <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C2}" />
                    <asp:BoundField DataField="FechaPago" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField DataField="Cuotas" HeaderText="Cuotas" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblPagosVacio" runat="server" Visible="false" CssClass="text-muted" Text="No hay pagos registrados para este pedido." style="display:block;padding:12px;" />
        </div>
    </div>
</asp:Content>
