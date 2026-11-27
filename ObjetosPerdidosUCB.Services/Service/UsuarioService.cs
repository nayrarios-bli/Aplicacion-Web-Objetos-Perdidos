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
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _unitOfWork.UsuarioRepository.GetAll();
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(uint id)
        {
            return await _unitOfWork.UsuarioRepository.GetById(id);
        }

        public async Task<Usuario?> GetUsuarioByCorreoAsync(string correo)
        {
            return await _unitOfWork.UsuarioRepository.GetUsuarioByCorreoAsync(correo);
        }

        public async Task InsertUsuario(Usuario usuario)
        {
            await _unitOfWork.UsuarioRepository.Add(usuario);
        }

        public async Task UpdateUsuario(Usuario usuario)
        {
            await _unitOfWork.UsuarioRepository.Update(usuario);
        }

        public async Task DeleteUsuario(uint id)
        {
            await _unitOfWork.UsuarioRepository.Delete(id);
        }
        public async Task ValidarAdministradorAsync(uint idUsuario)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetById(idUsuario);

            if (usuario == null || !usuario.Estado)
                throw new BusinessException("El usuario no existe o está inactivo.", HttpStatusCode.NotFound);

            if (!string.Equals(usuario.Rol, "administrador", StringComparison.OrdinalIgnoreCase))
                throw new BusinessException(
                    "Solo los administradores autorizados pueden realizar esta operación.",
                    HttpStatusCode.Forbidden);
        }
    }
}
