using Datos.DAOs;
using Datos.Entidades;
using System;
using System.Collections.Generic;

namespace Negocio.Servicios
{
    public class PagoNegocio
    {
        private readonly PagoDAO dao = new PagoDAO();

        public List<Pago> ObtenerTodos()
        {
            return dao.ObtenerTodos();
        }

        public List<Pago> Buscar(int? clienteId, string tipoPago, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            return dao.Buscar(clienteId, tipoPago, fechaDesde, fechaHasta);
        }

        public Pago ObtenerPorId(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.");
            return dao.ObtenerPorId(id);
        }

        public void Guardar(Pago p)
        {
            Validar(p);
            if (p.Id == 0)
                dao.Insertar(p);
            else
                dao.Actualizar(p);
        }

        public void Eliminar(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.");
            dao.Eliminar(id);
        }

        public List<Pago> ObtenerPorPedido(int pedidoId)
        {
            return dao.ObtenerPorPedido(pedidoId);
        }

        private void Validar(Pago p)
        {
            if (p.PedidoId <= 0)
                throw new ArgumentException("Debe asociar el pago a un pedido válido.");
            if (p.Monto <= 0)
                throw new ArgumentException("El monto del pago debe ser mayor que cero.");
            if (p.Cuotas < 1)
                throw new ArgumentException("Las cuotas deben ser al menos 1.");
        }
    }
}
