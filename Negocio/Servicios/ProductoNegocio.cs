using Datos.DAOs;
using Datos.Entidades;
using System;
using System.Collections.Generic;

namespace Negocio.Servicios
{
    public class ProductoNegocio
    {
        private readonly ProductoDAO dao = new ProductoDAO();

        public List<Producto> ObtenerTodos()
        {
            return dao.ObtenerTodos();
        }

        public List<Producto> Buscar(string nombre, string categoria, string marca, decimal? precioMin, decimal? precioMax, bool? disponibilidad)
        {
            return dao.Buscar(nombre, categoria, marca, precioMin, precioMax, disponibilidad);
        }

        public Producto ObtenerPorId(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.");
            return dao.ObtenerPorId(id);
        }

        public void Guardar(Producto p)
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

        public List<string> ObtenerCategorias()
        {
            return dao.ObtenerCategorias();
        }

        private void Validar(Producto p)
        {
            if (string.IsNullOrWhiteSpace(p.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");
            if (p.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor que cero.");
            if (p.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");
        }
    }
}
