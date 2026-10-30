using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ObjetosPerdidosContext _context;
        private readonly DbSet<T> _entities;
        public BaseRepository(ObjetosPerdidosContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public async Task Add(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(uint id)
        {
            var entity = await GetById(id);
            if (entity == null)
                return;
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T?> GetById(uint id)
        {
            return await _entities.FirstAsync(x => x.Id == id);
        }

        public async Task Update(T entity)
        {

            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}