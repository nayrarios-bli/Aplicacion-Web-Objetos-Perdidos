using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Interfaces
{
    public interface IObjetoService
    {
        Task<IEnumerable<Objeto>> SearchObjetosAsync(
           string? texto,
           uint? idCategoria,
           uint? idLugarAlmacenamiento,
           string? lugarEncontrado,
           bool? soloDisponibles);
        Task<IEnumerable<Objeto>> GetAllObjetosAsync();
        Task<Objeto?> GetObjetoByIdAsync(uint id);
        Task<IEnumerable<Objeto>> GetAllObjetosByNombreAsync(string? texto, ObjetoQueryFilters? filters);
        Task<bool> ExistsCodigoAsync(string codigo);
        Task InsertObjeto(Objeto objeto);
        Task UpdateObjeto(Objeto objeto, uint idUsuarioAdmin);
        Task DeleteObjeto(uint id, uint idUsuarioAdmin);
    }
}
