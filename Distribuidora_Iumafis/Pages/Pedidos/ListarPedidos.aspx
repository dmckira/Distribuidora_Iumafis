<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarPedidos.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Pedidos.ListarPedidos" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Pedidos - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-shopping-cart"></i> Gestión de Pedidos</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / Pedidos</div>
        </div>
        <a href="CrearPedido.aspx" class="btn btn-primary"><i class="fa fa-plus"></i> Nuevo Pedido</a>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert alert-success">
        <i class="fa fa-check-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-search"></i> Filtrar Pedidos</div>
        </div>
        <div class="search-bar">
            <div class="form-group">
                <label class="form-label">Estado</label>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">-- Todos --</asp:ListItem>
                    <asp:ListItem Value="pendiente">Pendiente</asp:ListItem>
                    <asp:ListItem Value="en preparacion">En Preparación</asp:ListItem>
                    <asp:ListItem Value="enviado">Enviado</asp:ListItem>
                    <asp:ListItem Value="entregado">Entregado</asp:ListItem>
                    <asp:ListItem Value="cancelado">Cancelado</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label class="form-label">Desde</label>
                <asp:TextBox ID="txtDesde" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="form-group">
                <label class="form-label">Hasta</label>
                <asp:TextBox ID="txtHasta" runat="server" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="form-group" style="align-self:flex-end;">
                <asp:Button ID="btnBuscar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" style="margin-left:6px;" />
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-list"></i> Listado de Pedidos</div>
            <span class="text-muted">Total: <asp:Label ID="lblTotal" runat="server" Text="0" /> pedidos</span>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvPedidos" runat="server" CssClass="data-table" AutoGenerateColumns="false"
                EmptyDataText="" OnRowCommand="gvPedidos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="# Pedido" ItemStyle-Width="80px" />
                    <asp:BoundField DataField="ClienteNombre" HeaderText="Cliente" />
                    <asp:BoundField DataField="FechaPedido" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class='<%# GetBadgeEstado(Eval("Estado").ToString()) %>'>
                                <%#Eval("Estado")%>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C2}" />
                    <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="250px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDetalle" runat="server" CommandName="Detalle" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-info btn-sm"><i class="fa fa-eye"></i> Ver</asp:LinkButton>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-warning btn-sm"><i class="fa fa-edit"></i> Estado</asp:LinkButton>
                            <asp:LinkButton ID="btnPago" runat="server" CommandName="Pago" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-success btn-sm"><i class="fa fa-credit-card"></i> Pago</asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Eliminar este pedido?');"><i class="fa fa-trash"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblVacio" runat="server" Visible="false" CssClass="empty-state">
                <br/><i class="fa fa-shopping-cart" style="font-size:48px;color:#ddd;"></i><br/>No se encontraron pedidos.
            </asp:Label>
        </div>
    </div>
</asp:Content>
