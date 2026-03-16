<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarProductos.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Productos.ListarProductos" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Productos - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-box"></i> Gestión de Productos</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / Productos</div>
        </div>
        <a href="CrearProducto.aspx" class="btn btn-primary"><i class="fa fa-plus"></i> Nuevo Producto</a>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert alert-success">
        <i class="fa fa-check-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-search"></i> Buscar Productos</div>
        </div>
        <div class="search-bar">
            <div class="form-group">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="txtBusNombre" runat="server" CssClass="form-control" placeholder="Buscar por nombre..." />
            </div>
            <div class="form-group">
                <label class="form-label">Categoría</label>
                <asp:DropDownList ID="ddlBusCategoria" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">-- Todas --</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label class="form-label">Marca</label>
                <asp:TextBox ID="txtBusMarca" runat="server" CssClass="form-control" placeholder="Marca..." />
            </div>
            <div class="form-group">
                <label class="form-label">Precio Mín.</label>
                <asp:TextBox ID="txtPrecioMin" runat="server" CssClass="form-control" placeholder="0.00" />
            </div>
            <div class="form-group">
                <label class="form-label">Precio Máx.</label>
                <asp:TextBox ID="txtPrecioMax" runat="server" CssClass="form-control" placeholder="9999.99" />
            </div>
            <div class="form-group">
                <label class="form-label">Disponibilidad</label>
                <asp:DropDownList ID="ddlDisponibilidad" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">-- Todas --</asp:ListItem>
                    <asp:ListItem Value="1">Disponible</asp:ListItem>
                    <asp:ListItem Value="0">No disponible</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group" style="align-self:flex-end;">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" style="margin-left:6px;" />
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-list"></i> Listado de Productos</div>
            <span class="text-muted">Total: <asp:Label ID="lblTotal" runat="server" Text="0" /> productos</span>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvProductos" runat="server" CssClass="data-table" AutoGenerateColumns="false"
                EmptyDataText="" OnRowCommand="gvProductos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                    <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C2}" />
                    <asp:BoundField DataField="Stock" HeaderText="Stock" />
                    <asp:TemplateField HeaderText="Disponibilidad">
                        <ItemTemplate>
                            <span class='<%# (bool)Eval("Disponibilidad") ? "badge badge-success" : "badge badge-danger" %>'>
                                <%# (bool)Eval("Disponibilidad") ? "Disponible" : "No disponible" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDetalle" runat="server" CommandName="Detalle" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-info btn-sm"><i class="fa fa-eye"></i> Ver</asp:LinkButton>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-warning btn-sm"><i class="fa fa-edit"></i> Editar</asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Eliminar este producto?');"><i class="fa fa-trash"></i> Eliminar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblVacio" runat="server" Visible="false" CssClass="empty-state">
                <br/><i class="fa fa-box-open" style="font-size:48px;color:#ddd;"></i><br/>No se encontraron productos.
            </asp:Label>
        </div>
    </div>
</asp:Content>
