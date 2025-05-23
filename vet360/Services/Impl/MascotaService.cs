using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vet360.Models;
using vet360.Repository;
using vet360.Repository.Impl;

namespace vet360.Services.Impl
{
    public class MascotaService : IMascotaService
    {
        private readonly IMascotaRepository _repo;

        public MascotaService(IMascotaRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Mascota> ObtenerTodas() => _repo.GetAll();

        public Mascota ObtenerPorId(int id) => _repo.GetById(id);

        public void Crear(Mascota mascota)
        {
            _repo.Add(mascota);
            _repo.Save();
        }

        public void Editar(Mascota mascota)
        {

            _repo.Update(mascota);
            _repo.Save();
        }

        public void Eliminar(int id)
        {
            _repo.Delete(id);
            _repo.Save();
        }

        public IEnumerable<Mascota> ObtenerPorUsuario(int usuarioId)
        {
            return _repo.ObtenerPorUsuario(usuarioId);
        }
    }
}