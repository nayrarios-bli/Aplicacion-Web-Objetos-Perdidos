using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> GetAllCategoriasAsync(bool soloActivas);
        Task<Categoria> GetCategoriaByIdAsync(uint id);
        Task InsertCategoria(Categoria objeto);
        Task UpdateCategoria(Categoria objeto);
        Task DeleteCategoria(uint objeto);
    }
}