using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vet360.Models;

namespace vet360.Services
{
    public interface ICitaService
    {
        List<Cita> ListarTodas();
        Cita ObtenerPorId(int id);
        List<Cita> ObtenerPorUsuario(int usuarioId);
        List<Cita> ObtenerPorMascota(int mascotaId);
        void Crear(Cita cita);
        void Actualizar(Cita cita);
        void Cancelar(int id);
        void Confirmar(int id);

        IEnumerable<Cita> ObtenerCitasPorVeterinario(int idVeterinario, DateTime desde, DateTime hasta);
        IEnumerable<DateTime> ObtenerFechasDisponibles(int veterinarioId);
        IEnumerable<Horario> ObtenerHorariosDisponibles();
        bool AgendarCita(Cita cita);
        IEnumerable<Mascota> ObtenerMascotasPorCliente(int clienteId);


    }
}
