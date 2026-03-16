using Datos.Entidades;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Productos
{
    public partial class EditarProducto : System.Web.UI.Page
    {
        private readonly ProductoNegocio svc = new ProductoNegocio();
        private int ProductoId => int.TryParse(Request.QueryString["id"], out int id) ? id : 0;
        private bool ModoVer => Request.QueryString["modo"] == "ver";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ProductoId == 0) { Response.Redirect("ListarProductos.aspx"); return; }
            if (!IsPostBack) CargarProducto();
        }

        private void CargarProducto()
        {
            var p = svc.ObtenerPorId(ProductoId);
            if (p == null) { Response.Redirect("ListarProductos.aspx"); return; }

            txtNombre.Text = p.Nombre;
            txtCategoria.Text = p.Categoria;
            txtMarca.Text = p.Marca;
            txtPrecio.Text = p.Precio.ToString("F2");
            txtStock.Text = p.Stock.ToString();
            txtDescripcion.Text = p.Descripcion;
            txtIngredientes.Text = p.Ingredientes;
            txtBeneficios.Text = p.Beneficios;
            txtRecomendaciones.Text = p.RecomendacionesUso;
            ddlDisponibilidad.SelectedValue = p.Disponibilidad ? "true" : "false";

            if (ModoVer)
            {
                lblTitulo.Text = "<i class='fa fa-eye'></i> Detalle del Producto";
                lblSubtitulo.Text = "Ver";
                txtNombre.ReadOnly = txtCategoria.ReadOnly = txtMarca.ReadOnly = true;
                txtPrecio.ReadOnly = txtStock.ReadOnly = txtDescripcion.ReadOnly = true;
                txtIngredientes.ReadOnly = txtBeneficios.ReadOnly = txtRecomendaciones.ReadOnly = true;
                ddlDisponibilidad.Enabled = false;
                pnlAcciones.Visible = false;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                var p = new Producto
                {
                    Id = ProductoId,
                    Nombre = txtNombre.Text.Trim(),
                    Categoria = txtCategoria.Text.Trim(),
                    Marca = txtMarca.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    Ingredientes = txtIngredientes.Text.Trim(),
                    Beneficios = txtBeneficios.Text.Trim(),
                    RecomendacionesUso = txtRecomendaciones.Text.Trim(),
                    Precio = decimal.Parse(txtPrecio.Text),
                    Stock = int.TryParse(txtStock.Text, out int st) ? st : 0,
                    Disponibilidad = ddlDisponibilidad.SelectedValue == "true"
                };
                svc.Guardar(p);
                Response.Redirect("ListarProductos.aspx?msg=editado");
            }
            catch (Exception ex)
            {
                pnlAlerta.Visible = true;
                lblAlerta.Text = "Error: " + ex.Message;
            }
        }
    }
}