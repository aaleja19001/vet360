using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vet360.Models
{
    public class Horario
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; } // es el veterinoo
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public virtual Usuario Veterinario { get; set; }
    }
}