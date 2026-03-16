<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearPedido.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Pedidos.CrearPedido" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Nuevo Pedido - Distribuidora Iumafis</title>
    <style>
        .detalle-table { width:100%; border-collapse:collapse; font-size:14px; }
        .detalle-table th { background:#6a1b9a; color:#fff; padding:10px 12px; text-align:left; }
        .detalle-table td { padding:8px 12px; border-bottom:1px solid #f0e6f8; vertical-align:middle; }
        .detalle-table tr:nth-child(even) td { background:#fdf6ff; }
        .total-bar { background:#f3e5f5; padding:14px 20px; border-radius:8px; display:flex; justify-content:space-between; align-items:center; margin-top:16px; }
        .total-bar .total-label { font-size:16px; color:#555; font-weight:600; }
        .total-bar .total-value { font-size:24px; font-weight:700; color:#6a1b9a; }
    </style>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-plus"></i> Nuevo Pedido</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / <a href="ListarPedidos.aspx">Pedidos</a> / Nuevo</div>
        </div>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert">
        <i class="fa fa-info-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-user"></i> Cliente</div>
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Seleccionar Cliente <span class="required">*</span></label>
                    <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCliente" InitialValue="" ErrorMessage="Seleccione un cliente." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-boxes"></i> Productos del Pedido</div>
            <asp:Button ID="btnAgregarFila" runat="server" Text="+ Agregar Producto" CssClass="btn btn-outline btn-sm" OnClick="btnAgregarFila_Click" CausesValidation="false" />
        </div>

        <div class="table-responsive">
            <table class="detalle-table">
                <thead>
                    <tr>
                        <th style="width:40%">Producto</th>
                        <th style="width:15%">Precio Unit.</th>
                        <th style="width:15%">Cantidad</th>
                        <th style="width:20%">Subtotal</th>
                        <th style="width:10%"></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDetalle" runat="server" OnItemCommand="rptDetalle_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_Changed" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" ReadOnly="true"
                                        style="background:#f5f5f5;" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" Text="1"
                                        AutoPostBack="true" OnTextChanged="txtCantidad_Changed" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubtotal" runat="server" CssClass="form-control" ReadOnly="true"
                                        style="background:#f5f5f5; font-weight:600;" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="btnEliminarFila" runat="server" CommandName="EliminarFila"
                                        CommandArgument='<%#Container.ItemIndex%>' CssClass="btn btn-danger btn-sm"
                                        CausesValidation="false"><i class="fa fa-times"></i></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>

        <div class="total-bar">
            <span class="total-label"><i class="fa fa-calculator"></i> Total del Pedido:</span>
            <span class="total-value">$<asp:Label ID="lblTotal" runat="server" Text="0.00" /></span>
        </div>
    </div>

    <div class="card">
        <div class="form-actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Crear Pedido" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <a href="ListarPedidos.aspx" class="btn btn-secondary"><i class="fa fa-arrow-left"></i> Cancelar</a>
        </div>
    </div>
</asp:Content>
