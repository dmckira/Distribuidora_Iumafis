<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearPago.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Pagos.CrearPago" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Registrar Pago - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-credit-card"></i> Registrar Pago</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / <a href="ListarPagos.aspx">Pagos</a> / Nuevo</div>
        </div>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert">
        <i class="fa fa-info-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <asp:Panel ID="pnlInfoPedido" runat="server" Visible="false">
        <div class="alert alert-info" style="margin-bottom:16px;">
            <i class="fa fa-info-circle"></i>
            Pedido #<asp:Label ID="lblPedidoInfo" runat="server" /> &mdash;
            Cliente: <asp:Label ID="lblClienteInfo" runat="server" /> &mdash;
            Total: $<asp:Label ID="lblTotalInfo" runat="server" />
        </div>
    </asp:Panel>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-dollar-sign"></i> Datos del Pago</div>
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Pedido <span class="required">*</span></label>
                    <asp:DropDownList ID="ddlPedido" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPedido" InitialValue="" ErrorMessage="Seleccione un pedido." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
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
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipoPago" InitialValue="" ErrorMessage="Seleccione tipo de pago." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Monto <span class="required">*</span></label>
                    <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control" placeholder="0.00" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMonto" ErrorMessage="El monto es obligatorio." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                    <asp:RangeValidator runat="server" ControlToValidate="txtMonto" MinimumValue="0.01" MaximumValue="9999999" Type="Double" ErrorMessage="Monto invalido." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Cuotas</label>
                    <asp:TextBox ID="txtCuotas" runat="server" CssClass="form-control" Text="1" />
                </div>
            </div>
        </div>
        <div class="form-actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Registrar Pago" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <a href="ListarPagos.aspx" class="btn btn-secondary"><i class="fa fa-arrow-left"></i> Cancelar</a>
        </div>
    </div>
</asp:Content>
