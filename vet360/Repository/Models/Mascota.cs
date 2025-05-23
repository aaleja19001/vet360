using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vet360.Models
{
    public class Mascota
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public int Edad { get; set; }
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }
        public virtual ICollection<HistorialMedico> Historiales { get; set; }

        public virtual ICollection<HistorialMedico> HistorialesMedicos { get; set; }
    }
}