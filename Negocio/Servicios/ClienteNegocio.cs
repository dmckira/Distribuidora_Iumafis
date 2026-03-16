using Datos.DAOs;
using Datos.Entidades;
using System;
using System.Collections.Generic;

namespace Negocio.Servicios
{
    public class ClienteNegocio
    {
        private readonly ClienteDAO dao = new ClienteDAO();

        public List<Cliente> ObtenerTodos()
        {
            return dao.ObtenerTodos();
        }

        public List<Cliente> Buscar(string nombre, string tipoCliente, string telefono, string email)
        {
            return dao.Buscar(nombre, tipoCliente, telefono, email);
        }

        public Cliente ObtenerPorId(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.");
            return dao.ObtenerPorId(id);
        }

        public void Guardar(Cliente c)
        {
            Validar(c);
            if (c.Id == 0)
                dao.Insertar(c);
            else
                dao.Actualizar(c);
        }

        public void Eliminar(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.");
            dao.Eliminar(id);
        }

        private void Validar(Cliente c)
        {
            if (string.IsNullOrWhiteSpace(c.Nombre))
                throw new ArgumentException("El nombre del cliente es obligatorio.");
        }
    }
}
