using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.Interfaces
{
    public interface ISecurityRepository : IBaseRepository<Security>
    {
        Task<Security> GetLoginByCredentials(UserLogin login);
    }
}
