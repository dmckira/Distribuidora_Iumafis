<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarProducto.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Productos.EditarProducto" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Producto - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><asp:Label ID="lblTitulo" runat="server" Text="Editar Producto" /></div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / <a href="ListarProductos.aspx">Productos</a> / <asp:Label ID="lblSubtitulo" runat="server" Text="Editar" /></div>
        </div>
        <a href="ListarProductos.aspx" class="btn btn-secondary"><i class="fa fa-arrow-left"></i> Volver</a>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert alert-danger">
        <i class="fa fa-exclamation-circle"></i>
        <asp:Label ID="lblAlerta" runat="server" />
    </asp:Panel>

    <div class="card">
        <div class="card-header">
            <div class="card-title"><i class="fa fa-box"></i> Datos del Producto</div>
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Nombre <span class="required">*</span></label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="Obligatorio." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Categoría</label>
                    <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Marca</label>
                    <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" />
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Precio <span class="required">*</span></label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPrecio" ErrorMessage="Obligatorio." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                    <asp:RangeValidator runat="server" ControlToValidate="txtPrecio" MinimumValue="0" MaximumValue="999999" Type="Double" ErrorMessage="Precio inválido." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Stock</label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Disponibilidad</label>
                    <asp:DropDownList ID="ddlDisponibilidad" runat="server" CssClass="form-control">
                        <asp:ListItem Value="true">Disponible</asp:ListItem>
                        <asp:ListItem Value="false">No disponible</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label class="form-label">Descripción</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Ingredientes</label>
                    <asp:TextBox ID="txtIngredientes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Beneficios</label>
                    <asp:TextBox ID="txtBeneficios" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Recomendaciones de Uso</label>
                    <asp:TextBox ID="txtRecomendaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlAcciones" runat="server" CssClass="form-actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <a href="ListarProductos.aspx" class="btn btn-secondary"><i class="fa fa-times"></i> Cancelar</a>
        </asp:Panel>
    </div>
</asp:Content>
