using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Repository
{
    public interface IHorarioRepository 
    {
        IEnumerable<Horario> ObtenerHorariosDisponibles();
        IEnumerable<DateTime> ObtenerFechasDisponibles(int veterinarioId, DateTime desde);
        Horario ObtenerPorId(int id);
        void GuardarCambios();

        IEnumerable<Horario> ObtenerHorariosDisponiblesPorVeterinarioYFecha(int veterinarioId, DateTime fecha);
    }
}
