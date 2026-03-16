using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Pagos
{
    public partial class ListarPagos : System.Web.UI.Page
    {
        private readonly PagoNegocio svc = new PagoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPagos();
                if (Request.QueryString["msg"] == "creado")
                    MostrarAlerta("Pago registrado exitosamente.", "alert-success");
                else if (Request.QueryString["msg"] == "editado")
                    MostrarAlerta("Pago actualizado exitosamente.", "alert-success");
                else if (Request.QueryString["msg"] == "eliminado")
                    MostrarAlerta("Pago eliminado exitosamente.", "alert-success");
            }
        }

        private void CargarPagos()
        {
            DateTime? desde = null, hasta = null;
            if (DateTime.TryParse(txtDesde.Text, out DateTime d)) desde = d;
            if (DateTime.TryParse(txtHasta.Text, out DateTime h)) hasta = h;
            var lista = svc.Buscar(null, ddlTipoPago.SelectedValue, desde, hasta);
            gvPagos.DataSource = lista;
            gvPagos.DataBind();
            lblTotal.Text = lista.Count.ToString();
            lblMontoTotal.Text = lista.Sum(p => p.Monto).ToString("N2");
            lblVacio.Visible = lista.Count == 0;
        }

        protected void btnBuscar_Click(object sender, EventArgs e) => CargarPagos();

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlTipoPago.SelectedIndex = 0;
            txtDesde.Text = txtHasta.Text = "";
            CargarPagos();
        }

        protected void gvPagos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Ver" || e.CommandName == "Editar")
                Response.Redirect("EditarPago.aspx?id=" + id + (e.CommandName == "Ver" ? "&modo=ver" : ""));
            else if (e.CommandName == "Eliminar")
            {
                try
                {
                    svc.Eliminar(id);
                    MostrarAlerta("Pago eliminado exitosamente.", "alert-success");
                    CargarPagos();
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