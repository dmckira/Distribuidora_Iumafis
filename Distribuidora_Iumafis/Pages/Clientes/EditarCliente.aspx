<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarCliente.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Clientes.EditarCliente" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Cliente - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><asp:Label ID="lblTitulo" runat="server" Text="Editar Cliente" /></div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / <a href="ListarClientes.aspx">Clientes</a> / <asp:Label ID="lblSubtitulo" runat="server" Text="Editar" /></div>
        </div>
        <a href="ListarClientes.aspx" class="btn btn-secondary"><i class="fa fa-arrow-left"></i> Volver</a>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert">
        <i class="fa fa-info-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <asp:Panel ID="pnlFormulario" runat="server">
        <div class="card">
            <div class="card-header">
                <div class="card-title"><i class="fa fa-user"></i> Datos del Cliente</div>
            </div>
            <div class="form-row">
                <div class="form-col">
                    <div class="form-group">
                        <label class="form-label">Nombre Completo <span class="required">*</span></label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="Obligatorio." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                    </div>
                </div>
                <div class="form-col">
                    <div class="form-group">
                        <label class="form-label">Tipo de Cliente</label>
                        <asp:DropDownList ID="ddlTipoCliente" runat="server" CssClass="form-control">
                            <asp:ListItem Value="">-- Seleccionar --</asp:ListItem>
                            <asp:ListItem Value="Persona">Persona</asp:ListItem>
                            <asp:ListItem Value="Establecimiento">Establecimiento</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-row">
                <div class="form-col">
                    <div class="form-group">
                        <label class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-col">
                    <div class="form-group">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail"
                            ValidationExpression="^[^@]+@[^@]+\.[^@]+$"
                            ErrorMessage="Email inválido." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="form-label">Dirección</label>
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" />
            </div>
            <asp:Panel ID="pnlAcciones" runat="server" CssClass="form-actions">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
                <a href="ListarClientes.aspx" class="btn btn-secondary"><i class="fa fa-times"></i> Cancelar</a>
            </asp:Panel>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlHistorial" runat="server" Visible="false">
        <div class="card">
            <div class="card-header">
                <div class="card-title"><i class="fa fa-history"></i> Historial de Compras</div>
            </div>
            <div style="margin-bottom:16px; padding:16px; background:#faf4fc; border-radius:8px;">
                <strong>Cliente:</strong> <asp:Label ID="lblClienteNombre" runat="server" /> &nbsp;&nbsp;
                <strong>Tipo:</strong> <asp:Label ID="lblClienteTipo" runat="server" /> &nbsp;&nbsp;
                <strong>Tel:</strong> <asp:Label ID="lblClienteTel" runat="server" />
            </div>
            <div class="table-responsive">
                <asp:GridView ID="gvHistorial" runat="server" CssClass="data-table" AutoGenerateColumns="false" EmptyDataText="">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Pedido #" ItemStyle-Width="80px" />
                        <asp:BoundField DataField="FechaPedido" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='<%# GetBadgeEstado(Eval("Estado").ToString()) %>'>
                                    <%#Eval("Estado")%>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C2}" />
                        <asp:TemplateField HeaderText="Acciones" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <a href='../Pedidos/DetallePedido.aspx?id=<%#Eval("Id")%>' class="btn btn-info btn-sm"><i class="fa fa-eye"></i> Ver</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblHistorialVacio" runat="server" Visible="false" CssClass="empty-state">
                    <br/><i class="fa fa-shopping-cart" style="font-size:48px;color:#ddd;"></i><br/>Este cliente no tiene pedidos registrados.
                </asp:Label>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
