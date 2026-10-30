using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Interfaces
{
    public interface ILugarAlmacenamientoService
    {
        Task<IEnumerable<LugarAlmacenamiento>> GetAllLugaresAsync(bool soloActivos);
        Task<LugarAlmacenamiento?> GetLugarByIdAsync(uint id);
        Task InsertLugarAlmacenamiento(LugarAlmacenamiento objeto);
        Task UpdateLugarAlmacenamiento(LugarAlmacenamiento objeto);
        Task DeleteLugarAlmacenamiento(uint id);
    }
}