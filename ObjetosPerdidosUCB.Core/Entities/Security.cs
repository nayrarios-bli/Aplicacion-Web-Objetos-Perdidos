using ObjetosPerdidosUCB.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Core.Entities
{
    public partial class Security : BaseEntity
    {
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public RoleType Role { get; set; }
    }
}
