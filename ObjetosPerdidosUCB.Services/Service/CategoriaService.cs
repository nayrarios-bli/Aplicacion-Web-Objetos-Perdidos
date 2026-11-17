using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Categoria>> GetAllCategoriasAsync(bool soloActivas)
        {
            return await _unitOfWork.CategoriaRepository.GetAllCategoriasAsync(soloActivas);
        }

        public async Task<Categoria?> GetCategoriaByIdAsync(uint id)
        {
            return await _unitOfWork.CategoriaRepository.GetById(id);
        }

        public async Task InsertCategoria(Categoria categoria)
        {
            await _unitOfWork.CategoriaRepository.Add(categoria);
        }

        public async Task UpdateCategoria(Categoria categoria)
        {
            await _unitOfWork.CategoriaRepository.Update(categoria);
        }

        public async Task DeleteCategoria(uint id)
        {
            await _unitOfWork.CategoriaRepository.Delete(id);
        }
    }
}
