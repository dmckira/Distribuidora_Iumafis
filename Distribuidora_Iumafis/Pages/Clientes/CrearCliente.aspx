<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearCliente.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Clientes.CrearCliente" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Nuevo Cliente - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-user-plus"></i> Nuevo Cliente</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / <a href="ListarClientes.aspx">Clientes</a> / Nuevo</div>
        </div>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert">
        <i class="fa fa-info-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-user"></i> Datos del Cliente</div>
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Nombre Completo <span class="required">*</span></label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre completo" />
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
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Teléfono de contacto" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="correo@ejemplo.com" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail"
                        ValidationExpression="^[^@]+@[^@]+\.[^@]+$"
                        ErrorMessage="Email inválido." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
        </div>
        <div class="form-group">
            <label class="form-label">Dirección</label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Dirección completa" />
        </div>
        <div class="form-actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cliente" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <a href="ListarClientes.aspx" class="btn btn-secondary"><i class="fa fa-arrow-left"></i> Cancelar</a>
        </div>
    </div>
</asp:Content>
