<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearProducto.aspx.cs" Inherits="Distribuidora_Iumafis.Pages.Productos.CrearProducto" MasterPageFile="~/Site.master" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Nuevo Producto - Distribuidora Iumafis</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <div>
            <div class="page-title"><i class="fa fa-plus"></i> Nuevo Producto</div>
            <div class="breadcrumb"><a href="../../default.aspx">Inicio</a> / <a href="ListarProductos.aspx">Productos</a> / Nuevo</div>
        </div>
    </div>

    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert">
        <i class="fa fa-info-circle"></i>
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
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre del producto" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="Obligatorio." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Categoría</label>
                    <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control" placeholder="Ej: Cuidado facial" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Marca</label>
                    <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" placeholder="Marca del producto" />
                </div>
            </div>
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Precio <span class="required">*</span></label>
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="0.00" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPrecio" ErrorMessage="Obligatorio." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                    <asp:RangeValidator runat="server" ControlToValidate="txtPrecio" MinimumValue="0" MaximumValue="999999" Type="Double" ErrorMessage="Precio inválido." ForeColor="Red" Display="Dynamic" Font-Size="12px" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Stock</label>
                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" Text="0" />
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
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Descripción del producto" />
        </div>
        <div class="form-row">
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Ingredientes</label>
                    <asp:TextBox ID="txtIngredientes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Lista de ingredientes" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Beneficios</label>
                    <asp:TextBox ID="txtBeneficios" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Beneficios del producto" />
                </div>
            </div>
            <div class="form-col">
                <div class="form-group">
                    <label class="form-label">Recomendaciones de Uso</label>
                    <asp:TextBox ID="txtRecomendaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Modo de uso y recomendaciones" />
                </div>
            </div>
        </div>
        <div class="form-actions">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Producto" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            <a href="ListarProductos.aspx" class="btn btn-secondary"><i class="fa fa-arrow-left"></i> Cancelar</a>
        </div>
    </div>
</asp:Content>
