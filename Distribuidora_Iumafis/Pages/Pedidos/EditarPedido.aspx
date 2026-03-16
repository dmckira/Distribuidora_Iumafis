<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarPedido.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Pedidos.EditarPedido" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Editar Pedido - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-edit"></i> Cambiar Estado del Pedido</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / <a href="ListarPedidos.aspx">Pedidos</a> / Editar</div>
        </div>
        <a href="ListarPedidos.aspx" class="btn btn-secondary"><i class="fa fa-arrow-left"></i> Volver</a>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert">
        <i class="fa fa-info-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-shopping-cart"></i> Información del Pedido</div>
        </div>
        <div style="display:grid; grid-template-columns:1fr 1fr 1fr; gap:16px; margin-bottom:24px;">
            <div style="background:#faf4fc; padding:14px 18px; border-radius:8px;">
                <div style="font-size:11px; text-transform:uppercase; color:#9c27b0; font-weight:700; margin-bottom:4px;">Pedido #</div>
                <div style="font-size:18px; font-weight:700; color:#333;"><asp:Label ID="lblId" runat="server" /></div>
            </div>
            <div style="background:#faf4fc; padding:14px 18px; border-radius:8px;">
                <div style="font-size:11px; text-transform:uppercase; color:#9c27b0; font-weight:700; margin-bottom:4px;">Cliente</div>
                <div style="font-size:15px; font-weight:600; color:#333;"><asp:Label ID="lblCliente" runat="server" /></div>
            </div>
            <div style="background:#faf4fc; padding:14px 18px; border-radius:8px;">
                <div style="font-size:11px; text-transform:uppercase; color:#9c27b0; font-weight:700; margin-bottom:4px;">Total</div>
                <div style="font-size:18px; font-weight:700; color:#2e7d32;"><asp:Label ID="lblTotal" runat="server" /></div>
            </div>
        </div>

        <div class="form-group" style="max-width:400px;">
            <label class="form-label">Estado del Pedido <span class="required">*</span></label>
            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                <asp:ListItem Value="pendiente">Pendiente</asp:ListItem>
                <asp:ListItem Value="en preparacion">En Preparación</asp:ListItem>
                <asp:ListItem Value="enviado">Enviado</asp:ListItem>
                <asp:ListItem Value="entregado">Entregado</asp:ListItem>
                <asp:ListItem Value="cancelado">Cancelado</asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="form-actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Actualizar Estado" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <a href="DetallePedido.aspx?id=<%=PedidoId%>" class="btn btn-info"><i class="fa fa-eye"></i> Ver Detalle</a>
            <a href="ListarPedidos.aspx" class="btn btn-secondary"><i class="fa fa-times"></i> Cancelar</a>
        </div>
    </div>
</asp:Content>
