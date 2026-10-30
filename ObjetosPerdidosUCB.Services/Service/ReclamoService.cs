using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Excepctions;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ObjetosPerdidosUCB.Services.Service
{
    public class ReclamoService : IReclamoService
    {
        private readonly IBaseRepository<Reclamo> _reclamoRepository;
        private readonly IReclamoRepository _reclamoQueryRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IObjetoService _objetoService;

        public ReclamoService(
            IBaseRepository<Reclamo> reclamoRepository,
            IReclamoRepository reclamoQueryRepository,
            IUsuarioService usuarioService,
            IObjetoService objetoService)
        {
            _reclamoRepository = reclamoRepository;
            _reclamoQueryRepository = reclamoQueryRepository;
            _usuarioService = usuarioService;
            _objetoService = objetoService;
        }

        public async Task<IEnumerable<Reclamo>> GetAllReclamosAsync()
        {
            return await _reclamoQueryRepository.GetAllReclamosAsync();
        }

        public async Task<bool> ExistsPendienteAsync(uint idObjeto, uint idUsuario)
        {
            return await _reclamoQueryRepository.ExistsPendienteAsync(idObjeto, idUsuario);
        }

        public async Task<Reclamo?> GetReclamoByIdAsync(uint id)
        {
            return await _reclamoRepository.GetById(id);
        }

        public async Task InsertReclamo(Reclamo reclamo)
        {
            if (string.IsNullOrWhiteSpace(reclamo.Justificacion))
                throw new BusinessException("La justificación del reclamo es obligatoria.");

            var usuario = await _usuarioService.GetUsuarioByIdAsync(reclamo.IdUsuario);
            if (usuario == null || !usuario.Estado)
                throw new BusinessException("El usuario no existe o está inactivo.");

            var objeto = await _objetoService.GetObjetoByIdAsync(reclamo.IdObjeto);
            if (objeto == null)
                throw new BusinessException("El objeto no existe o no está disponible.", HttpStatusCode.NotFound);

            if (await _reclamoQueryRepository.ExistsPendienteAsync(reclamo.IdObjeto, reclamo.IdUsuario))
                throw new BusinessException("Ya tienes un reclamo pendiente para este objeto.", HttpStatusCode.Conflict);

            reclamo.Estado = "pendiente";
            reclamo.FechaEmitida = DateTime.Now;

            await _reclamoRepository.Add(reclamo);
        }

        public async Task UpdateReclamo(Reclamo reclamo, uint idUsuarioAdmin)
        {
            await _usuarioService.ValidarAdministradorAsync(idUsuarioAdmin);

            var existente = await _reclamoRepository.GetById(reclamo.Id);
            if (existente == null)
                throw new BusinessException("El reclamo no existe.", HttpStatusCode.NotFound);

            existente.Estado = reclamo.Estado;
            existente.Observacion = reclamo.Observacion;
            existente.IdRevisor = idUsuarioAdmin;
            existente.FechaRevisada = DateTime.Now;

            await _reclamoRepository.Update(existente);
        }

        public async Task MarcarEntregadoAsync(uint idReclamo, uint idUsuarioAdmin)
        {
            await _usuarioService.ValidarAdministradorAsync(idUsuarioAdmin);

            var reclamo = await _reclamoRepository.GetById(idReclamo);
            if (reclamo == null)
                throw new BusinessException("El reclamo no existe.", HttpStatusCode.NotFound);

            if (reclamo.Estado != "aprobado" || reclamo.IdRevisor == null)
                throw new BusinessException(
                    "El administrador debe verificar que el reclamante es el propietario antes de entregar el objeto.");

            var objeto = await _objetoService.GetObjetoByIdAsync(reclamo.IdObjeto);
            if (objeto == null)
                throw new BusinessException("El objeto no existe o ya no está disponible.", HttpStatusCode.NotFound);

            objeto.Estado = false;
            await _objetoService.UpdateObjeto(objeto, idUsuarioAdmin);

            reclamo.Estado = "entregado";
            await _reclamoRepository.Update(reclamo);
        }
        public async Task DeleteReclamo(uint id)
        {
            await _reclamoRepository.Delete(id);
        }

       
    }
}
