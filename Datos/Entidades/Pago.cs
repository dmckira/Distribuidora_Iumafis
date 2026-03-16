using System;

namespace Datos.Entidades
{
    public class Pago
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public string ClienteNombre { get; set; }
        public string TipoPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public int Cuotas { get; set; }
    }
}
