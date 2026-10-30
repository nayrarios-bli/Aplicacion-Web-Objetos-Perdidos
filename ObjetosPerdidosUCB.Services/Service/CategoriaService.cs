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
        private readonly IBaseRepository<Categoria> _categoriaRepository;
        private readonly ICategoriaRepository _categoriaQueryRepository;

        public CategoriaService(
            IBaseRepository<Categoria> categoriaRepository,
            ICategoriaRepository categoriaQueryRepository)
        {
            _categoriaRepository = categoriaRepository;
            _categoriaQueryRepository = categoriaQueryRepository;
        }

        public async Task<IEnumerable<Categoria>> GetAllCategoriasAsync(bool soloActivas)
        {
            return await _categoriaQueryRepository.GetAllCategoriasAsync(soloActivas);
        }

        public async Task<Categoria?> GetCategoriaByIdAsync(uint id)
        {
            return await _categoriaRepository.GetById(id);
        }

        public async Task InsertCategoria(Categoria categoria)
        {
            await _categoriaRepository.Add(categoria);
        }

        public async Task UpdateCategoria(Categoria categoria)
        {
            await _categoriaRepository.Update(categoria);
        }

        public async Task DeleteCategoria(uint id)
        {
            await _categoriaRepository.Delete(id);
        }
    }
}
