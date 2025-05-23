using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Models;

namespace vet360.Models
{
    public class Usuario
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public virtual Rol Rol { get; set; }

        public virtual ICollection<Mascota> Mascotas { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }

        public int? RolId { get; set; }

        public virtual ICollection<HistorialMedico> HistorialesMedicosRegistrados { get; set; }
    }
}