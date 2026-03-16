using Datos.Entidades;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Clientes
{
    public partial class EditarCliente : System.Web.UI.Page
    {
        private readonly ClienteNegocio svc = new ClienteNegocio();
        private readonly PedidoNegocio pedidoSvc = new PedidoNegocio();
        private int ClienteId => int.TryParse(Request.QueryString["id"], out int id) ? id : 0;
        private bool ModoHistorial => Request.QueryString["modo"] == "historial";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ClienteId == 0) { Response.Redirect("ListarClientes.aspx"); return; }
            if (!IsPostBack) CargarCliente();
        }

        private void CargarCliente()
        {
            var c = svc.ObtenerPorId(ClienteId);
            if (c == null) { Response.Redirect("ListarClientes.aspx"); return; }

            if (ModoHistorial)
            {
                lblTitulo.Text = "<i class='fa fa-history'></i> Historial de Compras";
                lblSubtitulo.Text = "Historial";
                pnlFormulario.Visible = false;
                pnlHistorial.Visible = true;
                lblClienteNombre.Text = c.Nombre;
                lblClienteTipo.Text = c.TipoCliente;
                lblClienteTel.Text = c.Telefono;

                var pedidos = pedidoSvc.ObtenerPorCliente(ClienteId);
                gvHistorial.DataSource = pedidos;
                gvHistorial.DataBind();
                lblHistorialVacio.Visible = pedidos.Count == 0;
            }
            else
            {
                txtNombre.Text = c.Nombre;
                ddlTipoCliente.SelectedValue = c.TipoCliente ?? "";
                txtTelefono.Text = c.Telefono;
                txtEmail.Text = c.Email;
                txtDireccion.Text = c.Direccion;
            }
        }

        protected string GetBadgeEstado(string estado)
        {
            switch (estado)
            {
                case "entregado": return "badge badge-success";
                case "cancelado": return "badge badge-danger";
                case "enviado": return "badge badge-info";
                case "en preparacion": return "badge badge-warning";
                default: return "badge badge-secondary";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                var c = new Cliente
                {
                    Id = ClienteId,
                    Nombre = txtNombre.Text.Trim(),
                    TipoCliente = ddlTipoCliente.SelectedValue,
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };
                svc.Guardar(c);
                Response.Redirect("ListarClientes.aspx?msg=editado");
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