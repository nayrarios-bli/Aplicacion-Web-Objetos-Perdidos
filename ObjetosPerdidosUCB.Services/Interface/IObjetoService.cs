using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Interfaces
{
    public interface IObjetoService
    {
        Task<IEnumerable<Objeto>> GetAllObjetosAsync();
        Task<ObjetoDto?> GetDetalleAsync(uint id);
        Task<IEnumerable<Objeto>> GetAllObjetosByNombreAsync(ObjetoQueryFilters? filters);
        Task<bool> ExistsCodigoAsync(string codigo);
        Task InsertObjeto(Objeto objeto);
        Task UpdateObjeto(Objeto objeto);
        Task DeleteObjeto(uint id);
    }
}
