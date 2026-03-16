<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarPago.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Pagos.EditarPago" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Pago - Distribuidora Iumafis</title>
    <style>
        @media print {
            .navbar, .page-header .btn, .form-actions, .footer { display:none !important; }
        }
    </style>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><asp:Label ID="lblTitulo" runat="server" Text="Editar Pago" /></div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / <a href="ListarPagos.aspx">Pagos</a> / <asp:Label ID="lblSubtitulo" runat="server" Text="Editar" /></div>
        </div>
        <div style="display:flex;gap:8px;">
            <asp:Panel ID="pnlBtnImprimir" runat="server" Visible="false">
                <button onclick="window.print()" class="btn btn-info"><i class="fa fa-print"></i> Imprimir Comprobante</button>
            </asp:Panel>
            <a href="ListarPagos.aspx" class="btn btn-secondary"><i class="fa fa-arrow-left"></i> Volver</a>
        </div>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert">
        <i class="fa fa-info-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-dollar-sign"></i> Datos del Pago</div>
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Pedido</label>
                    <asp:DropDownList ID="ddlPedido" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Cliente</label>
                    <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" ReadOnly="true" style="background:#f5f5f5;" />
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Tipo de Pago <span class="required">*</span></label>
                    <asp:DropDownList ID="ddlTipoPago" runat="server" CssClass="form-control">
                        <asp:ListItem Value="">-- Seleccionar --</asp:ListItem>
                        <asp:ListItem Value="Efectivo">Efectivo</asp:ListItem>
                        <asp:ListItem Value="Tarjeta">Tarjeta</asp:ListItem>
                        <asp:ListItem Value="Transferencia">Transferencia</asp:ListItem>
                        <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                        <asp:ListItem Value="Credito">Credito</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoPago" InitialValue="" ErrorMessage="Seleccione tipo." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Monto <span class="required">*</span></label>
                    <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMonto" ErrorMessage="Obligatorio." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                    <asp:RangeValidator runat="server" ControlToValidate="txtMonto" MinimumValue="0" MaximumValue="9999999" Type="Double" ErrorMessage="Invalido." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Cuotas</label>
                    <asp:TextBox ID="txtCuotas" runat="server" CssClass="form-control" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <label class="form-label">Fecha de Pago</label>
            <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" ReadOnly="true" style="background:#f5f5f5;" />
        </div>
        <asp:Panel ID="pnlAcciones" runat="server" CssClass="form-actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <a href="ListarPagos.aspx" class="btn btn-secondary"><i class="fa fa-times"></i> Cancelar</a>
        </asp:Panel>
    </div>
</asp:Content>
