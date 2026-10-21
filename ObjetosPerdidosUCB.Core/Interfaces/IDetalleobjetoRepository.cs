using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IDetalleobjetoRepository
    {
        Task<IEnumerable<Detalleobjeto>> GetAllDetalleobjetosAsync();

        Task<Detalleobjeto> GetDetalleobjetoByIdAsync(int id);

        Task InsertDetalleobjeto(Detalleobjeto detalleObjeto);

        Task UpdateDetalleobjeto(Detalleobjeto detalleobjeto);

        Task DeleteDetalleobjeto(Detalleobjeto detalleobjeto);
    }
}
