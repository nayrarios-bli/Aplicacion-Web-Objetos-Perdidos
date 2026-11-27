using Microsoft.EntityFrameworkCore.Storage;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ObjetosPerdidosContext _context;
        public readonly IUsuarioRepository? _usuarioRespository;
        public readonly IReclamoRepository? _reclamoRespository;
        public readonly IObjetoRepository? _objetoRepository;
        public readonly ICategoriaRepository? _categoriaRepository;
        public readonly ILugarAlmacenamientoRepository? _lugarAlmacenamientoRepository;
        private readonly ISecurityRepository _securityRepository;
        private readonly IDapperContext _dapper;
        private IDbContextTransaction? _efTransaction;

        public UnitOfWork(ObjetosPerdidosContext context, IDapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public IUsuarioRepository UsuarioRepository =>
            _usuarioRespository ?? new UsuarioRepository(_context);

        public IReclamoRepository ReclamoRepository =>
            _reclamoRespository ?? new ReclamoRepository(_context);
        public IObjetoRepository ObjetoRepository =>
            _objetoRepository ?? new ObjetoRepository(_context , _dapper);

        public ICategoriaRepository CategoriaRepository =>
            _categoriaRepository?? new CategoriaRepository(_context);

        public ILugarAlmacenamientoRepository LugarAlmacenamientoRepository =>
            _lugarAlmacenamientoRepository?? new LugarAlmacenamientoRepository(_context);

        public ISecurityRepository SecurityRepository =>
            _securityRepository ?? new SecurityRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _efTransaction?.Dispose();
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
