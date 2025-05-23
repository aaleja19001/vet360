using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vet360.Models
{
    public class Servicio
    {
        public int Id { get; set; }
        public string NombreServicioVet { get; set; } 
        public decimal Precio { get; set; }

        public virtual ICollection<Cita> Citas { get; set; }
    }
}