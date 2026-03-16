using Datos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Datos.DAOs
{
    public class UsuarioDAO
    {
        public Usuario Autenticar(string nombreUsuario, string password)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM usuarios WHERE usuario=@usuario AND password=@password", conn);
                cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@password", password);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read()) return MapearUsuario(dr);
                }
            }
            return null;
        }

        public List<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM usuarios ORDER BY nombre", conn);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) lista.Add(MapearUsuario(dr));
                }
            }
            return lista;
        }

        public Usuario ObtenerPorId(int id)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT * FROM usuarios WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read()) return MapearUsuario(dr);
                }
            }
            return null;
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT COUNT(*) FROM usuarios WHERE usuario=@usuario", conn);
                cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public void Insertar(Usuario u)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"INSERT INTO usuarios (nombre,usuario,password,email)
                    VALUES(@nombre,@usuario,@password,@email)", conn);
                AgregarParametros(cmd, u);
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(Usuario u)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"UPDATE usuarios SET nombre=@nombre,usuario=@usuario,
                    email=@email WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@nombre", u.Nombre);
                cmd.Parameters.AddWithValue("@usuario", u.NombreUsuario);
                cmd.Parameters.AddWithValue("@email", u.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@id", u.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void CambiarPassword(int id, string nuevaPassword)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("UPDATE usuarios SET password=@password WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@password", nuevaPassword);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (var conn = ConexionMySQL.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("DELETE FROM usuarios WHERE id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private void AgregarParametros(MySqlCommand cmd, Usuario u)
        {
            cmd.Parameters.AddWithValue("@nombre", u.Nombre);
            cmd.Parameters.AddWithValue("@usuario", u.NombreUsuario);
            cmd.Parameters.AddWithValue("@password", u.Password);
            cmd.Parameters.AddWithValue("@email", u.Email ?? (object)DBNull.Value);
        }

        private Usuario MapearUsuario(MySqlDataReader dr)
        {
            return new Usuario
            {
                Id = dr.GetInt32("id"),
                Nombre = dr.GetString("nombre"),
                NombreUsuario = dr.GetString("usuario"),
                Password = dr.GetString("password"),
                Email = dr.IsDBNull(dr.GetOrdinal("email")) ? "" : dr.GetString("email"),
                FechaRegistro = dr.IsDBNull(dr.GetOrdinal("fecha_registro")) ? DateTime.Now : dr.GetDateTime("fecha_registro")
            };
        }
    }
}
