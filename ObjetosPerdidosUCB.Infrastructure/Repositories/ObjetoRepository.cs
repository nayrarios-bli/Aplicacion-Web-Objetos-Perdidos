using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;
using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.DTOs;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class ObjetoRepository : IObjetoRepository
    {
        private readonly ObjetosPerdidosContext _objetosPerdidosContext;

        public ObjetoRepository(ObjetosPerdidosContext objetosPerdidosContext)
        {
            _objetosPerdidosContext = objetosPerdidosContext;
        }
        public async Task<IEnumerable<Objeto>> GetAllObjetosAsync()
        {
            var objetos = await _objetosPerdidosContext.Objetos.ToListAsync();
            return objetos;
        }
        public async Task<Objeto> GetObjetoByNombreAsync(string nombre)
        {
            var objeto = await _objetosPerdidosContext.Objetos.FirstOrDefaultAsync(x => x.Nombre == nombre);
            return objeto;
        }
        public async Task InsertObjeto(Objeto objeto)
        {
            _objetosPerdidosContext.Objetos.Add(objeto);
            await _objetosPerdidosContext.SaveChangesAsync();
        }

        public async Task UpdateObjeto(Objeto objeto)
        {
            _objetosPerdidosContext.Objetos.Update(objeto);
            await _objetosPerdidosContext.SaveChangesAsync();
        }
        public async Task DeleteObjeto(Objeto objeto)
        {
            _objetosPerdidosContext.Objetos.Remove(objeto);
            await _objetosPerdidosContext.SaveChangesAsync();
        }
    }
}
