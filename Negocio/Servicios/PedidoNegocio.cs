using Datos.DAOs;
using Datos.Entidades;
using System;
using System.Collections.Generic;

namespace Negocio.Servicios
{
    public class PedidoNegocio
    {
        private readonly PedidoDAO dao = new PedidoDAO();

        public List<Pedido> ObtenerTodos()
        {
            return dao.ObtenerTodos();
        }

        public List<Pedido> Buscar(int? clienteId, string estado, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return dao.Buscar(clienteId, estado, fechaDesde, fechaHasta);
        }

        public Pedido ObtenerPorId(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.");
            return dao.ObtenerPorId(id);
        }

        public int Crear(Pedido p)
        {
            Validar(p);
            return dao.Insertar(p);
        }

        public void ActualizarEstado(int id, string estado)
        {
            var estadosValidos = new[] { "pendiente", "en preparacion", "enviado", "entregado", "cancelado" };
            if (Array.IndexOf(estadosValidos, estado) < 0)
                throw new ArgumentException("Estado inválido.");
            dao.ActualizarEstado(id, estado);
        }

        public void Eliminar(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.");
            dao.Eliminar(id);
        }

        public List<Pedido> ObtenerPorCliente(int clienteId)
        {
            return dao.ObtenerPorCliente(clienteId);
        }

        private void Validar(Pedido p)
        {
            if (p.ClienteId <= 0)
                throw new ArgumentException("Debe seleccionar un cliente.");
            if (p.Detalles == null || p.Detalles.Count == 0)
                throw new ArgumentException("El pedido debe tener al menos un producto.");
            foreach (var d in p.Detalles)
            {
                if (d.Cantidad <= 0)
                    throw new ArgumentException("La cantidad debe ser mayor que cero.");
                if (d.PrecioUnitario <= 0)
                    throw new ArgumentException("El precio unitario debe ser mayor que cero.");
            }
        }
    }
}
