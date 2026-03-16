<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarPagos.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Pagos.ListarPagos" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Pagos - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-credit-card"></i> Gestión de Pagos</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / Pagos</div>
        </div>
        <a href="CrearPago.aspx" class="btn btn-primary"><i class="fa fa-plus"></i> Registrar Pago</a>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert alert-success">
        <i class="fa fa-check-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-search"></i> Filtrar Pagos</div>
        </div>
        <div class="search-bar">
            <div class="form-group">
                <label class="form-label">Tipo de Pago</label>
                <asp:DropDownList ID="ddlTipoPago" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">-- Todos --</asp:ListItem>
                    <asp:ListItem Value="Efectivo">Efectivo</asp:ListItem>
                    <asp:ListItem Value="Tarjeta">Tarjeta</asp:ListItem>
                    <asp:ListItem Value="Transferencia">Transferencia</asp:ListItem>
                    <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                    <asp:ListItem Value="Crédito">Crédito</asp:ListItem>
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
            <div class="card-title"><i class="fa fa-list"></i> Listado de Pagos</div>
            <span class="text-muted">Total: <asp:Label ID="lblTotal" runat="server" Text="0" /> pagos &nbsp;|&nbsp; Monto total: $<asp:Label ID="lblMontoTotal" runat="server" Text="0.00" /></span>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvPagos" runat="server" CssClass="data-table" AutoGenerateColumns="false"
                EmptyDataText="" OnRowCommand="gvPagos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-Width="60px" />
                    <asp:BoundField DataField="PedidoId" HeaderText="Pedido #" ItemStyle-Width="80px" />
                    <asp:BoundField DataField="ClienteNombre" HeaderText="Cliente" />
                    <asp:TemplateField HeaderText="Tipo de Pago">
                        <ItemTemplate>
                            <span class="badge badge-info"><i class="fa fa-credit-card"></i> <%#Eval("TipoPago")%></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C2}" />
                    <asp:BoundField DataField="FechaPago" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:BoundField DataField="Cuotas" HeaderText="Cuotas" />
                    <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnVer" runat="server" CommandName="Ver" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-info btn-sm"><i class="fa fa-eye"></i> Ver</asp:LinkButton>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-warning btn-sm"><i class="fa fa-edit"></i> Editar</asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Eliminar este pago?');"><i class="fa fa-trash"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblVacio" runat="server" Visible="false" CssClass="empty-state">
                <br/><i class="fa fa-credit-card" style="font-size:48px;color:#ddd;"></i><br/>No se encontraron pagos.
            </asp:Label>
        </div>
    </div>
</asp:Content>
