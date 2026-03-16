<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarClientes.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Clientes.ListarClientes" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Clientes - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-users"></i> Gestión de Clientes</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / Clientes</div>
        </div>
        <a href="CrearCliente.aspx" class="btn btn-primary"><i class="fa fa-user-plus"></i> Nuevo Cliente</a>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert alert-success">
        <i class="fa fa-check-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-search"></i> Buscar Clientes</div>
        </div>
        <div class="search-bar">
            <div class="form-group">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="txtBusNombre" runat="server" CssClass="form-control" placeholder="Nombre del cliente..." />
            </div>
            <div class="form-group">
                <label class="form-label">Tipo de Cliente</label>
                <asp:DropDownList ID="ddlTipoCliente" runat="server" CssClass="form-control">
                    <asp:ListItem Value="">-- Todos --</asp:ListItem>
                    <asp:ListItem Value="Persona">Persona</asp:ListItem>
                    <asp:ListItem Value="Establecimiento">Establecimiento</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label class="form-label">Teléfono</label>
                <asp:TextBox ID="txtBusTelefono" runat="server" CssClass="form-control" placeholder="Teléfono..." />
            </div>
            <div class="form-group">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtBusEmail" runat="server" CssClass="form-control" placeholder="Email..." />
            </div>
            <div class="form-group" style="align-self:flex-end;">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" style="margin-left:6px;" />
            </div>
        </div>
    </div>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-list"></i> Listado de Clientes</div>
            <span class="text-muted">Total: <asp:Label ID="lblTotal" runat="server" Text="0" /> clientes</span>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="gvClientes" runat="server" CssClass="data-table" AutoGenerateColumns="false"
                EmptyDataText="" OnRowCommand="gvClientes_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                            <span class='<%# Eval("TipoCliente").ToString()=="Establecimiento" ? "badge badge-purple" : "badge badge-info" %>'>
                                <i class='<%# Eval("TipoCliente").ToString()=="Establecimiento" ? "fa fa-store" : "fa fa-user" %>'></i>
                                <%#Eval("TipoCliente")%>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                    <asp:BoundField DataField="FechaRegistro" HeaderText="Registro" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="230px">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnHistorial" runat="server" CommandName="Historial" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-info btn-sm"><i class="fa fa-history"></i> Historial</asp:LinkButton>
                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-warning btn-sm"><i class="fa fa-edit"></i> Editar</asp:LinkButton>
                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%#Eval("Id")%>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Eliminar este cliente?');"><i class="fa fa-trash"></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblVacio" runat="server" Visible="false" CssClass="empty-state">
                <br/><i class="fa fa-users" style="font-size:48px;color:#ddd;"></i><br/>No se encontraron clientes.
            </asp:Label>
        </div>
    </div>
</asp:Content>
