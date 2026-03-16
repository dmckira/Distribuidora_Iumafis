using Negocio.Servicios;
using System;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Productos
{
    public partial class ListarProductos : System.Web.UI.Page
    {
        private readonly ProductoNegocio svc = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarProductos();
                if (Request.QueryString["msg"] == "creado")
                    MostrarAlerta("Producto creado exitosamente.", "alert-success");
                else if (Request.QueryString["msg"] == "editado")
                    MostrarAlerta("Producto actualizado exitosamente.", "alert-success");
                else if (Request.QueryString["msg"] == "eliminado")
                    MostrarAlerta("Producto eliminado exitosamente.", "alert-success");
            }
        }

        private void CargarCategorias()
        {
            ddlBusCategoria.Items.Clear();
            ddlBusCategoria.Items.Add(new ListItem("-- Todas --", ""));
            foreach (var cat in svc.ObtenerCategorias())
                ddlBusCategoria.Items.Add(new ListItem(cat, cat));
        }

        private void CargarProductos()
        {
            decimal? precioMin = null, precioMax = null;
            if (decimal.TryParse(txtPrecioMin.Text, out decimal pm)) precioMin = pm;
            if (decimal.TryParse(txtPrecioMax.Text, out decimal px)) precioMax = px;
            bool? disp = null;
            if (ddlDisponibilidad.SelectedValue == "1") disp = true;
            else if (ddlDisponibilidad.SelectedValue == "0") disp = false;

            var lista = svc.Buscar(txtBusNombre.Text.Trim(), ddlBusCategoria.SelectedValue,
                txtBusMarca.Text.Trim(), precioMin, precioMax, disp);

            gvProductos.DataSource = lista;
            gvProductos.DataBind();
            lblTotal.Text = lista.Count.ToString();
            lblVacio.Visible = lista.Count == 0;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusNombre.Text = "";
            txtBusMarca.Text = "";
            txtPrecioMin.Text = "";
            txtPrecioMax.Text = "";
            ddlBusCategoria.SelectedIndex = 0;
            ddlDisponibilidad.SelectedIndex = 0;
            CargarProductos();
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Detalle")
                Response.Redirect("EditarProducto.aspx?id=" + id + "&modo=ver");
            else if (e.CommandName == "Editar")
                Response.Redirect("EditarProducto.aspx?id=" + id);
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    svc.Eliminar(id);
                    MostrarAlerta("Producto eliminado exitosamente.", "alert-success");
                    CargarProductos();
                }
                catch (Exception ex)
                {
                    MostrarAlerta("Error al eliminar: " + ex.Message, "alert-danger");
                }
            }
        }

        private void MostrarAlerta(string mensaje, string tipo)
        {
            pnlAlerta.Visible = true;
            pnlAlerta.CssClass = "alert " + tipo;
            lblAlerta.Text = mensaje;
        }
    }
}
