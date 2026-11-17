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
        //private readonly IBaseRepository<LugarAlmacenamiento> _lugarRepository;
        //private readonly ILugarAlmacenamientoRepository _lugarQueryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LugarAlmacenadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LugarAlmacenamiento>> GetAllLugaresAsync(bool soloActivos)
        {
            return await _unitOfWork.LugarAlmacenamientoRepository.GetAllLugaresAsync(soloActivos);
        }

        public async Task<LugarAlmacenamiento?> GetLugarByIdAsync(uint id)
        {
            return await _unitOfWork.LugarAlmacenamientoRepository.GetById(id);
        }

        public async Task InsertLugarAlmacenamiento(LugarAlmacenamiento lugar)
        {
            await _unitOfWork.LugarAlmacenamientoRepository.Add(lugar);
        }

        public async Task UpdateLugarAlmacenamiento(LugarAlmacenamiento lugar)
        {
            await _unitOfWork.LugarAlmacenamientoRepository.Update(lugar);
        }

        public async Task DeleteLugarAlmacenamiento(uint id)
        {
            await _unitOfWork.LugarAlmacenamientoRepository.Delete(id);
        }
    }
}
