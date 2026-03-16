using Datos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Datos.DAOs
{
    public class ProductoDAO
    {
        public List<Producto> ObtenerTodos()
        {
            var lista = new List<Producto>();
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM productos ORDER BY nombre", conn);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(MapearProducto(dr));
                }
            }
            return lista;
        }

        public List<Producto> Buscar(string nombre, string categoria, string marca, decimal? precioMin, decimal? precioMax, bool? disponibilidad)
        {
            var lista = new List<Producto>();
            var sql = "SELECT * FROM productos WHERE 1=1";
            if (!string.IsNullOrEmpty(nombre)) sql += " AND nombre LIKE @nombre";
            if (!string.IsNullOrEmpty(categoria)) sql += " AND categoria = @categoria";
            if (!string.IsNullOrEmpty(marca)) sql += " AND marca LIKE @marca";
            if (precioMin.HasValue) sql += " AND precio >= @precioMin";
            if (precioMax.HasValue) sql += " AND precio <= @precioMax";
            if (disponibilidad.HasValue) sql += " AND disponibilidad = @disponibilidad";
            sql += " ORDER BY nombre";

            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(sql, conn);
                if (!string.IsNullOrEmpty(nombre)) cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
                if (!string.IsNullOrEmpty(categoria)) cmd.Parameters.AddWithValue("@categoria", categoria);
                if (!string.IsNullOrEmpty(marca)) cmd.Parameters.AddWithValue("@marca", "%" + marca + "%");
                if (precioMin.HasValue) cmd.Parameters.AddWithValue("@precioMin", precioMin.Value);
                if (precioMax.HasValue) cmd.Parameters.AddWithValue("@precioMax", precioMax.Value);
                if (disponibilidad.HasValue) cmd.Parameters.AddWithValue("@disponibilidad", disponibilidad.Value);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        lista.Add(MapearProducto(dr));
                }
            }
            return lista;
        }

        public Producto ObtenerPorId(int id)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM productos WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read()) return MapearProducto(dr);
                }
            }
            return null;
        }

        public void Insertar(Producto p)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"INSERT INTO productos (nombre,categoria,marca,descripcion,ingredientes,
                    beneficios,recomendaciones_uso,precio,stock,disponibilidad)
                    VALUES(@nombre,@categoria,@marca,@descripcion,@ingredientes,@beneficios,@recomendaciones,@precio,@stock,@disponibilidad)", conn);
                AgregarParametros(cmd, p);
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Producto p)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"UPDATE productos SET nombre=@nombre,categoria=@categoria,marca=@marca,
                    descripcion=@descripcion,ingredientes=@ingredientes,beneficios=@beneficios,
                    recomendaciones_uso=@recomendaciones,precio=@precio,stock=@stock,disponibilidad=@disponibilidad
                    WHERE id=@id", conn);
                AgregarParametros(cmd, p);
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM productos WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> ObtenerCategorias()
        {
            var lista = new List<string>();
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT DISTINCT categoria FROM productos WHERE categoria IS NOT NULL ORDER BY categoria", conn);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(dr.GetString(0));
                }
            }
            return lista;
        }

        private void AgregarParametros(MySqlCommand cmd, Producto p)
        {
            cmd.Parameters.AddWithValue("@nombre", p.Nombre);
            cmd.Parameters.AddWithValue("@categoria", p.Categoria ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@marca", p.Marca ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@descripcion", p.Descripcion ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ingredientes", p.Ingredientes ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@beneficios", p.Beneficios ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@recomendaciones", p.RecomendacionesUso ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@precio", p.Precio);
            cmd.Parameters.AddWithValue("@stock", p.Stock);
            cmd.Parameters.AddWithValue("@disponibilidad", p.Disponibilidad);
        }

        private Producto MapearProducto(MySqlDataReader dr)
        {
            return new Producto
            {
                Id = dr.GetInt32("id"),
                Nombre = dr.GetString("nombre"),
                Categoria = dr.IsDBNull(dr.GetOrdinal("categoria")) ? "" : dr.GetString("categoria"),
                Marca = dr.IsDBNull(dr.GetOrdinal("marca")) ? "" : dr.GetString("marca"),
                Descripcion = dr.IsDBNull(dr.GetOrdinal("descripcion")) ? "" : dr.GetString("descripcion"),
                Ingredientes = dr.IsDBNull(dr.GetOrdinal("ingredientes")) ? "" : dr.GetString("ingredientes"),
                Beneficios = dr.IsDBNull(dr.GetOrdinal("beneficios")) ? "" : dr.GetString("beneficios"),
                RecomendacionesUso = dr.IsDBNull(dr.GetOrdinal("recomendaciones_uso")) ? "" : dr.GetString("recomendaciones_uso"),
                Precio = dr.GetDecimal("precio"),
                Stock = dr.GetInt32("stock"),
                Disponibilidad = dr.GetBoolean("disponibilidad")
            };
        }
    }
}
