using Datos.Entidades;
using Negocio.Servicios;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Distribuidora_Iumafis.Pages.Pedidos
{
    public partial class CrearPedido : System.Web.UI.Page
    {
        private readonly PedidoNegocio pedidoSvc = new PedidoNegocio();
        private readonly ClienteNegocio clienteSvc = new ClienteNegocio();
        private readonly ProductoNegocio productoSvc = new ProductoNegocio();

        private List<FilaDetalle> Filas
        {
            get { return ViewState["Filas"] as List<FilaDetalle> ?? new List<FilaDetalle>(); }
            set { ViewState["Filas"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
                var filas = new List<FilaDetalle> { new FilaDetalle() };
                Filas = filas;
                BindDetalle();
            }
        }

        private void CargarClientes()
        {
            ddlCliente.Items.Clear();
            ddlCliente.Items.Add(new ListItem("-- Seleccione cliente --", ""));
            foreach (var c in clienteSvc.ObtenerTodos())
                ddlCliente.Items.Add(new ListItem(c.Nombre + " (" + c.TipoCliente + ")", c.Id.ToString()));
        }

        private void BindDetalle()
        {
            var productos = productoSvc.ObtenerTodos();
            rptDetalle.DataSource = Filas;
            rptDetalle.DataBind();

            foreach (RepeaterItem item in rptDetalle.Items)
            {
                var ddl = (DropDownList)item.FindControl("ddlProducto");
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("-- Seleccione --", "0"));
                foreach (var p in productos)
                    ddl.Items.Add(new ListItem(p.Nombre + " ($" + p.Precio.ToString("N2") + ")", p.Id.ToString()));

                var fila = Filas[item.ItemIndex];
                if (fila.ProductoId > 0)
                    ddl.SelectedValue = fila.ProductoId.ToString();

                ((TextBox)item.FindControl("txtPrecio")).Text = fila.Precio > 0 ? fila.Precio.ToString("N2") : "";
                ((TextBox)item.FindControl("txtCantidad")).Text = fila.Cantidad.ToString();
                ((TextBox)item.FindControl("txtSubtotal")).Text = fila.Subtotal > 0 ? fila.Subtotal.ToString("N2") : "";
            }
            ActualizarTotal();
        }

        protected void btnAgregarFila_Click(object sender, EventArgs e)
        {
            LeerFilasDesdeUI();
            var filas = Filas;
            filas.Add(new FilaDetalle());
            Filas = filas;
            BindDetalle();
        }

        protected void rptDetalle_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EliminarFila")
            {
                LeerFilasDesdeUI();
                int idx = int.Parse(e.CommandArgument.ToString());
                var filas = Filas;
                if (filas.Count > 1) filas.RemoveAt(idx);
                Filas = filas;
                BindDetalle();
            }
        }

        protected void ddlProducto_Changed(object sender, EventArgs e)
        {
            LeerFilasDesdeUI();
            var ddl = (DropDownList)sender;
            var item = (RepeaterItem)ddl.NamingContainer;
            int idx = item.ItemIndex;
            int prodId = int.Parse(ddl.SelectedValue);
            var filas = Filas;
            if (prodId > 0)
            {
                var p = productoSvc.ObtenerPorId(prodId);
                if (p != null)
                {
                    filas[idx].ProductoId = p.Id;
                    filas[idx].Precio = p.Precio;
                    filas[idx].Subtotal = p.Precio * filas[idx].Cantidad;
                }
            }
            Filas = filas;
            BindDetalle();
        }

        protected void txtCantidad_Changed(object sender, EventArgs e)
        {
            LeerFilasDesdeUI();
            var txt = (TextBox)sender;
            var item = (RepeaterItem)txt.NamingContainer;
            int idx = item.ItemIndex;
            var filas = Filas;
            if (int.TryParse(txt.Text, out int cant) && cant > 0)
            {
                filas[idx].Cantidad = cant;
                filas[idx].Subtotal = filas[idx].Precio * cant;
            }
            Filas = filas;
            BindDetalle();
        }

        private void LeerFilasDesdeUI()
        {
            var filas = Filas;
            foreach (RepeaterItem item in rptDetalle.Items)
            {
                if (item.ItemIndex >= filas.Count) break;
                var ddl = (DropDownList)item.FindControl("ddlProducto");
                var txtC = (TextBox)item.FindControl("txtCantidad");
                filas[item.ItemIndex].ProductoId = int.Parse(ddl.SelectedValue);
                int.TryParse(txtC.Text, out int c);
                filas[item.ItemIndex].Cantidad = c > 0 ? c : 1;
            }
            Filas = filas;
        }

        private void ActualizarTotal()
        {
            decimal total = 0;
            foreach (var f in Filas) total += f.Subtotal;
            lblTotal.Text = total.ToString("N2");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            LeerFilasDesdeUI();
            var filas = Filas;
            if (filas.Count == 0 || filas.TrueForAll(f => f.ProductoId == 0))
            {
                pnlAlerta.Visible = true;
                pnlAlerta.CssClass = "alert alert-danger";
                lblAlerta.Text = "Debe agregar al menos un producto al pedido.";
                return;
            }
            try
            {
                var detalles = new List<Datos.Entidades.DetallePedido>();
                decimal total = 0;
                foreach (var f in filas)
                {
                    if (f.ProductoId == 0) continue;
                    var subtotal = f.Precio * f.Cantidad;
                    total += subtotal;
                    detalles.Add(new Datos.Entidades.DetallePedido
                    {
                        ProductoId = f.ProductoId,
                        Cantidad = f.Cantidad,
                        PrecioUnitario = f.Precio,
                        Subtotal = subtotal
                    });
                }
                var pedido = new Datos.Entidades.Pedido
                {
                    ClienteId = int.Parse(ddlCliente.SelectedValue),
                    Estado = "pendiente",
                    Total = total,
                    Detalles = detalles
                };
                int nuevoId = pedidoSvc.Crear(pedido);
                Response.Redirect("DetallePedido.aspx?id=" + nuevoId + "&msg=creado");
            }
            catch (Exception ex)
            {
                pnlAlerta.Visible = true;
                pnlAlerta.CssClass = "alert alert-danger";
                lblAlerta.Text = "Error: " + ex.Message;
            }
        }

        [Serializable]
        public class FilaDetalle
        {
            public int ProductoId { get; set; }
            public decimal Precio { get; set; }
            public int Cantidad { get; set; } = 1;
            public decimal Subtotal { get; set; }
        }
    }
}