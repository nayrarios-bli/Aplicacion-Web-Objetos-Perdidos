using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Excepctions;
using ObjetosPerdidosUCB.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Interfaces
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IBaseRepository<Usuario> _usuarioRepository;
        private readonly IUsuarioRepository _usuarioQueryRepository;

        public UsuarioService(
            IBaseRepository<Usuario> usuarioRepository,
            IUsuarioRepository usuarioQueryRepository)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioQueryRepository = usuarioQueryRepository;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _usuarioRepository.GetAll();
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(uint id)
        {
            return await _usuarioRepository.GetById(id);
        }

        public async Task<Usuario?> GetUsuarioByCorreoAsync(string correo)
        {
            return await _usuarioQueryRepository.GetUsuarioByCorreoAsync(correo);
        }

        public async Task InsertUsuario(Usuario usuario)
        {
            await _usuarioRepository.Add(usuario);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            await _usuarioRepository.Update(usuario);
        }

        public async Task DeleteUsuario(uint id)
        {
            await _usuarioRepository.Delete(id);
        }
        public async Task ValidarAdministradorAsync(uint idUsuario)
        {
            var usuario = await _usuarioRepository.GetById(idUsuario);

            if (usuario == null || !usuario.Estado)
                throw new BusinessException("El usuario no existe o está inactivo.", HttpStatusCode.NotFound);

            if (!string.Equals(usuario.Rol, "administrador", StringComparison.OrdinalIgnoreCase))
                throw new BusinessException(
                    "Solo los administradores autorizados pueden realizar esta operación.",
                    HttpStatusCode.Forbidden);
        }

    }
}
