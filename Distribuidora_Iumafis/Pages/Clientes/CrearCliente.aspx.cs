using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos.Entidades;
using Negocio.Servicios;

namespace Distribuidora_Iumafis.Pages.Clientes
{
    public partial class CrearCliente : System.Web.UI.Page
    {
        private readonly ClienteNegocio svc = new ClienteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            try
            {
                var c = new Cliente
                {
                    Nombre = txtNombre.Text.Trim(),
                    TipoCliente = ddlTipoCliente.SelectedValue,
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                };
                svc.Guardar(c);
                Response.Redirect("ListarClientes.aspx?msg=creado");
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