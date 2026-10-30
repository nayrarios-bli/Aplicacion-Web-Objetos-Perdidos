using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Service
{
    public class LugarAlmacenadoService : ILugarAlmacenamientoService
    {
        private readonly IBaseRepository<LugarAlmacenamiento> _lugarRepository;
        private readonly ILugarAlmacenamientoRepository _lugarQueryRepository;

        public LugarAlmacenadoService(
            IBaseRepository<LugarAlmacenamiento> lugarRepository,
            ILugarAlmacenamientoRepository lugarQueryRepository)
        {
            _lugarRepository = lugarRepository;
            _lugarQueryRepository = lugarQueryRepository;
        }

        public async Task<IEnumerable<LugarAlmacenamiento>> GetAllLugaresAsync(bool soloActivos)
        {
            return await _lugarQueryRepository.GetAllLugaresAsync(soloActivos);
        }

        public async Task<LugarAlmacenamiento?> GetLugarByIdAsync(uint id)
        {
            return await _lugarRepository.GetById(id);
        }

        public async Task InsertLugarAlmacenamiento(LugarAlmacenamiento lugar)
        {
            await _lugarRepository.Add(lugar);
        }

        public async Task UpdateLugarAlmacenamiento(LugarAlmacenamiento lugar)
        {
            await _lugarRepository.Update(lugar);
        }

        public async Task DeleteLugarAlmacenamiento(uint id)
        {
            await _lugarRepository.Delete(id);
        }
    }
}
