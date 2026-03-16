using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Clientes
{
    public partial class ListarClientes : System.Web.UI.Page
    {
        private readonly ClienteNegocio svc = new ClienteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
                if (Request.QueryString["msg"] == "creado")
                    MostrarAlerta("Cliente creado exitosamente.", "alert-success");
                else if (Request.QueryString["msg"] == "editado")
                    MostrarAlerta("Cliente actualizado exitosamente.", "alert-success");
                else if (Request.QueryString["msg"] == "eliminado")
                    MostrarAlerta("Cliente eliminado exitosamente.", "alert-success");
            }
        }

        private void CargarClientes()
        {
            var lista = svc.Buscar(txtBusNombre.Text.Trim(), ddlTipoCliente.SelectedValue,
                txtBusTelefono.Text.Trim(), txtBusEmail.Text.Trim());
            gvClientes.DataSource = lista;
            gvClientes.DataBind();
            lblTotal.Text = lista.Count.ToString();
            lblVacio.Visible = lista.Count == 0;
        }

        protected void btnBuscar_Click(object sender, EventArgs e) => CargarClientes();

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusNombre.Text = txtBusTelefono.Text = txtBusEmail.Text = "";
            ddlTipoCliente.SelectedIndex = 0;
            CargarClientes();
        }

        protected void gvClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Historial")
                Response.Redirect("EditarCliente.aspx?id=" + id + "&modo=historial");
            else if (e.CommandName == "Editar")
                Response.Redirect("EditarCliente.aspx?id=" + id);
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    svc.Eliminar(id);
                    MostrarAlerta("Cliente eliminado exitosamente.", "alert-success");
                    CargarClientes();
                }
                catch (Exception ex)
                {
                    MostrarAlerta("Error al eliminar: " + ex.Message, "alert-danger");
                }
            }
        }

        private void MostrarAlerta(string msg, string tipo)
        {
            pnlAlerta.Visible = true;
            pnlAlerta.CssClass = "alert " + tipo;
            lblAlerta.Text = msg;
        }
    }
}