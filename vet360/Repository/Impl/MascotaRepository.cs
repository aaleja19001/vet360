using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using vet360.Data;
using vet360.Models;

namespace vet360.Repository.Impl
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly Vet360Context _context;

        public MascotaRepository(Vet360Context context)
        {
            _context = context;
        }

        public IEnumerable<Mascota> GetAll() => _context.Mascotas.ToList();

        public Mascota GetById(int id) => _context.Mascotas.Find(id);

        public void Add(Mascota mascota)
        {
            _context.Mascotas.Add(mascota);
        }

        public void Update(Mascota mascota)
        {
            // _context.Entry(mascota).State = EntityState.Modified;
            // Eliminar cualquier entidad de Mascota rastreada previamente con el mismo Id
            var mascotaExistente = _context.Mascotas.Local.FirstOrDefault(m => m.Id == mascota.Id);
            if (mascotaExistente != null)
            {
                // Si ya está rastreada, elimina la instancia duplicada
                _context.Entry(mascotaExistente).State = EntityState.Detached;
            }

            // Adjuntar la entidad
            _context.Mascotas.Attach(mascota);

            var entry = _context.Entry(mascota);

            // Recorremos los nombres de las propiedades (EF6)
            foreach (var propertyName in entry.CurrentValues.PropertyNames)
            {
                if (propertyName != nameof(Mascota.UsuarioId))
                {
                    entry.Property(propertyName).IsModified = true;
                }
            }

            //System.Diagnostics.Debug.WriteLine("el usuario es: . " + mascota.Usuario);
           // _context.Entry(mascota).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var mascota = _context.Mascotas.Find(id);
            if (mascota != null)
                _context.Mascotas.Remove(mascota);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Mascota> ObtenerPorUsuario(int usuarioId)
        {
            System.Diagnostics.Debug.WriteLine("mascotas: id usuario. " + usuarioId);
            return _context.Mascotas
                           .Where(m => m.UsuarioId == usuarioId)
                           .ToList();
        }
    }
}