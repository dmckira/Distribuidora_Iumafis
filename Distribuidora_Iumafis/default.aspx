<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Distribuidora_Iumafis._default" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Dashboard - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-tachometer-alt"></i> Dashboard</div>
            <div class="breadcrumb">Inicio / Panel de Control</div>
        </div>
    </div>

    <div class="stats-grid">
        <div class="stat-card purple">
            <div class="stat-icon"><i class="fa fa-box"></i></div>
            <div class="stat-info">
                <div class="stat-value"><asp:Label ID="lblTotalProductos" runat="server" Text="0" /></div>
                <div class="stat-label">Productos Activos</div>
            </div>
        </div>
        <div class="stat-card green">
            <div class="stat-icon"><i class="fa fa-users"></i></div>
            <div class="stat-info">
                <div class="stat-value"><asp:Label ID="lblTotalClientes" runat="server" Text="0" /></div>
                <div class="stat-label">Clientes Registrados</div>
            </div>
        </div>
        <div class="stat-card orange">
            <div class="stat-icon"><i class="fa fa-shopping-cart"></i></div>
            <div class="stat-info">
                <div class="stat-value"><asp:Label ID="lblPedidosPendientes" runat="server" Text="0" /></div>
                <div class="stat-label">Pedidos en Proceso</div>
            </div>
        </div>
        <div class="stat-card blue">
            <div class="stat-icon"><i class="fa fa-dollar-sign"></i></div>
            <div class="stat-info">
                <div class="stat-value">$<asp:Label ID="lblIngresosMes" runat="server" Text="0.00" /></div>
                <div class="stat-label">Ingresos del Mes</div>
            </div>
        </div>
    </div>

    <div style="display:grid; grid-template-columns:1fr 1fr; gap:24px; flex-wrap:wrap;">
        <div class="card">
            <div class="card-header">
                <div class="card-title"><i class="fa fa-trophy"></i> Top Productos</div>
                <a href="Pages/Reportes/Reportes.aspx" class="btn btn-outline btn-sm"><i class="fa fa-chart-bar"></i> Ver Reporte</a>
            </div>
            <asp:Repeater ID="rptTopProductos" runat="server">
                <HeaderTemplate>
                    <div style="display:flex; flex-direction:column; gap:10px;">
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="display:flex; align-items:center; gap:12px; padding:10px; background:#faf4fc; border-radius:8px;">
                        <div style="width:32px; height:32px; background:#9c27b0; border-radius:50%; color:#fff; display:flex; align-items:center; justify-content:center; font-weight:700; font-size:13px; flex-shrink:0;"><%#Container.ItemIndex+1%></div>
                        <div style="flex:1; font-size:14px; color:#333;"><%#Container.DataItem%></div>
                    </div>
                </ItemTemplate>
                <FooterTemplate></div></FooterTemplate>
            </asp:Repeater>
            <asp:Label ID="lblNoTopProductos" runat="server" Text="" CssClass="text-muted" />
        </div>

        <div class="card">
            <div class="card-header">
                <div class="card-title"><i class="fa fa-star"></i> Mejores Clientes</div>
                <a href="Pages/Reportes/Reportes.aspx" class="btn btn-outline btn-sm"><i class="fa fa-chart-bar"></i> Ver Reporte</a>
            </div>
            <asp:Repeater ID="rptTopClientes" runat="server">
                <HeaderTemplate>
                    <div style="display:flex; flex-direction:column; gap:10px;">
                </HeaderTemplate>
                <ItemTemplate>
                    <div style="display:flex; align-items:center; gap:12px; padding:10px; background:#faf4fc; border-radius:8px;">
                        <div style="width:32px; height:32px; background:#6a1b9a; border-radius:50%; color:#fff; display:flex; align-items:center; justify-content:center; font-weight:700; font-size:13px; flex-shrink:0;"><%#Container.ItemIndex+1%></div>
                        <div style="flex:1; font-size:14px; color:#333;"><%#Container.DataItem%></div>
                    </div>
                </ItemTemplate>
                <FooterTemplate></div></FooterTemplate>
            </asp:Repeater>
            <asp:Label ID="lblNoTopClientes" runat="server" Text="" CssClass="text-muted" />
        </div>
    </div>

    <div class="card" style="margin-top:24px;">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-bolt"></i> Accesos Rápidos</div>
        </div>
        <div style="display:flex; gap:14px; flex-wrap:wrap;">
            <a href="Pages/Pedidos/CrearPedido.aspx" class="btn btn-primary"><i class="fa fa-plus"></i> Nuevo Pedido</a>
            <a href="Pages/Clientes/CrearCliente.aspx" class="btn btn-success"><i class="fa fa-user-plus"></i> Nuevo Cliente</a>
            <a href="Pages/Productos/CrearProducto.aspx" class="btn btn-info"><i class="fa fa-plus"></i> Nuevo Producto</a>
            <a href="Pages/Pagos/CrearPago.aspx" class="btn btn-warning"><i class="fa fa-credit-card"></i> Registrar Pago</a>
            <a href="Pages/Reportes/Reportes.aspx" class="btn btn-secondary"><i class="fa fa-chart-bar"></i> Ver Informes</a>
        </div>
    </div>
</asp:Content>