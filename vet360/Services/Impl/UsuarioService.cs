using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Models;
using vet360.Repository;

namespace vet360.Services.Impl
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public List<Usuario> ListarTodos()
        {
            return _usuarioRepository.GetAll().ToList();
        }

        public Usuario ObtenerPorId(int id)
        {
            return _usuarioRepository.GetById(id);
        }

        public Usuario ObtenerPorEmail(string email)
        {
            return _usuarioRepository.GetByEmail(email);
        }

        public void Crear(Usuario usuario)
        {
            _usuarioRepository.Add(usuario);
            _usuarioRepository.Save();
        }

        public void Actualizar(Usuario usuario)
        {
            _usuarioRepository.Update(usuario);
            _usuarioRepository.Save();
        }

        public void Eliminar(int id)
        {
            _usuarioRepository.Delete(id);
            _usuarioRepository.Save();
        }

        public bool ValidarCredenciales(string email, string contraseña)
        {
            var usuario = _usuarioRepository.GetByEmail(email);
            return usuario != null && usuario.Contraseña == contraseña;
        }

        public IEnumerable<Usuario> ObtenerVeterinarios()
        {
            return _usuarioRepository.GetVeterinarios();
        }
    }
}