using Datos.DAOs;
using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Negocio.Servicios
{
    public class UsuarioNegocio
    {
        private readonly UsuarioDAO dao = new UsuarioDAO();

        public Usuario Autenticar(string nombreUsuario, string password)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Usuario y contraseña son obligatorios.");
            string hash = HashPassword(password);
            return dao.Autenticar(nombreUsuario, hash);
        }

        public List<Usuario> ObtenerTodos()
        {
            return dao.ObtenerTodos();
        }

        public Usuario ObtenerPorId(int id)
        {
            return dao.ObtenerPorId(id);
        }

        public void Registrar(Usuario u)
        {
            if (string.IsNullOrWhiteSpace(u.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(u.NombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");
            if (string.IsNullOrWhiteSpace(u.Password))
                throw new ArgumentException("La contraseña es obligatoria.");
            if (u.Password.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.");
            if (dao.ExisteUsuario(u.NombreUsuario))
                throw new ArgumentException("El nombre de usuario ya está en uso.");
            u.Password = HashPassword(u.Password);
            dao.Insertar(u);
        }

        public void Actualizar(Usuario u)
        {
            if (string.IsNullOrWhiteSpace(u.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            dao.Actualizar(u);
        }

        public void CambiarPassword(int id, string passwordActual, string nuevaPassword)
        {
            var u = dao.ObtenerPorId(id);
            if (u == null) throw new ArgumentException("Usuario no encontrado.");
            if (u.Password != HashPassword(passwordActual))
                throw new ArgumentException("La contraseña actual es incorrecta.");
            if (nuevaPassword.Length < 6)
                throw new ArgumentException("La nueva contraseña debe tener al menos 6 caracteres.");
            dao.CambiarPassword(id, HashPassword(nuevaPassword));
        }

        public void Eliminar(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.");
            dao.Eliminar(id);
        }

        public static string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder();
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
