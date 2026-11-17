using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUsuarioRepository UsuarioRepository { get; }
        IReclamoRepository ReclamoRepository { get; }
        IObjetoRepository ObjetoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        ILugarAlmacenamientoRepository LugarAlmacenamientoRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
