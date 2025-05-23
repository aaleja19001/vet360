using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Services
{
    public interface IUsuarioService
    {
        List<Usuario> ListarTodos();
        Usuario ObtenerPorId(int id);
        Usuario ObtenerPorEmail(string email);
        void Crear(Usuario usuario);
        void Actualizar(Usuario usuario);
        void Eliminar(int id);
        bool ValidarCredenciales(string email, string contraseña);

        IEnumerable<Usuario> ObtenerVeterinarios();
    }
}
