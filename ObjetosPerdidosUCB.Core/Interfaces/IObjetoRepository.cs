
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IObjetoRepository
    {
        Task<IEnumerable<Objeto>> GetAllObjetosAsync();
        Task<Objeto> GetObjetoByIdAsync(int id);

        Task<Objeto> GetObjetoByNombreAsync(String nombre);

        Task InsertObjeto(Objeto objeto);

        Task UpdateObjeto(Objeto objeto);

        Task DeleteObjeto(Objeto objeto);
    }
}
