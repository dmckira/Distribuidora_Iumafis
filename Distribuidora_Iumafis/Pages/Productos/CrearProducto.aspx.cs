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
    public partial class CrearProducto : System.Web.UI.Page
    {
        private readonly ProductoNegocio svc = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                var p = new Producto
                {
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
                Response.Redirect("ListarProductos.aspx?msg=creado");
            }
            catch (Exception ex)
            {
                pnlAlerta.Visible = true;
                pnlAlerta.CssClass = "alert alert-danger";
                lblAlerta.Text = "Error: " + ex.Message;
            }
        }
    }
}