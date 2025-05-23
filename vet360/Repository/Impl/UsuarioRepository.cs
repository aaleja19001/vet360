using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Data;
using vet360.Models;
using System.Data.Entity;

namespace vet360.Repository.Impl
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Vet360Context _context;

        public UsuarioRepository(Vet360Context context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Mascotas)
                .ToList();
        }

        public Usuario GetById(int id)
        {
            return _context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Mascotas)
                .FirstOrDefault(u => u.Id == id);
        }

        public Usuario GetByEmail(string email)
        {
            return _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefault(u => u.Correo == email);
        }

        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Update(Usuario usuario)
        {
            var existing = _context.Usuarios.Find(usuario.Id);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(usuario);
                // Manejo de relaciones
                if (usuario.RolId.HasValue)
                {
                    existing.RolId = usuario.RolId;
                }
            }
        }

        public void Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Usuario> GetVeterinarios()
        {
            return _context.Usuarios
                .Include(u => u.Rol)  // Carga la relación Rol
                .Where(u => u.Rol.Nombre == "Veterinario")
                .ToList();
        }
    }
}