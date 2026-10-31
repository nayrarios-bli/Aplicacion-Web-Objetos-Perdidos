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
    public class ObjetoService : IObjetoService
    {
        private readonly IBaseRepository<Objeto> _objetoRepository;
        private readonly IObjetoRepository _objetoQueryRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly ICategoriaService _categoriaService;
        private readonly ILugarAlmacenamientoService _lugarService;


        public ObjetoService(
            IBaseRepository<Objeto> objetoRepository,
            IObjetoRepository objetoQueryRepository,
            IUsuarioService usuarioService,
            ICategoriaService categoriaService,
            ILugarAlmacenamientoService lugarService)
        {
            _objetoRepository = objetoRepository;
            _objetoQueryRepository = objetoQueryRepository;
            _usuarioService = usuarioService;
            _categoriaService = categoriaService;
            _lugarService = lugarService;
        }

        public async Task<IEnumerable<Objeto>> GetAllObjetosAsync()
        {
            var objetos = await _objetoRepository.GetAll();
            return objetos.Where(o => o.Estado);
        }

        public async Task<Objeto?> GetObjetoByIdAsync(uint id)
        {
            var objeto = await _objetoQueryRepository.GetObjetoByIdAsync(id);
            if (objeto == null || !objeto.Estado)
                return null;
            return objeto;
        }
        public async Task<IEnumerable<Objeto>> SearchObjetosAsync(
            string? texto, uint? idCategoria, uint? idLugarAlmacenamiento,
            string? lugarEncontrado, bool? soloDisponibles)
        {
            return await _objetoQueryRepository.SearchObjetosAsync(
                texto, idCategoria, idLugarAlmacenamiento, lugarEncontrado, true);
        }
        public async Task<bool> ExistsCodigoAsync(string codigo)
        { 
            return await _objetoQueryRepository.ExistsCodigoAsync(codigo);
        }
        public async Task InsertObjeto(Objeto objeto)
        {
            if (objeto.Detalle == null)
                throw new BusinessException("El detalle del objeto es obligatorio.");

            await _usuarioService.ValidarAdministradorAsync(objeto.Detalle.IdUsuarioRegistro);
            ValidarCamposObligatorios(objeto);
            await ValidarReferenciasAsync(objeto);

            if (await _objetoQueryRepository.ExistsCodigoAsync(objeto.Codigo))
                throw new BusinessException($"Ya existe un objeto con el código {objeto.Codigo}.", HttpStatusCode.Conflict);

            objeto.Estado = true;
            await _objetoRepository.Add(objeto);
        }
        public async Task UpdateObjeto(Objeto objeto, uint idUsuarioAdmin)
        {
            await _usuarioService.ValidarAdministradorAsync(idUsuarioAdmin);

            var existente = await _objetoQueryRepository.GetObjetoParaActualizarAsync(objeto.Id);
            if (existente == null)
                throw new BusinessException("El objeto no existe.", HttpStatusCode.NotFound);

            if (objeto.Detalle == null)
                throw new BusinessException("El detalle del objeto es obligatorio.");

            ValidarCamposObligatorios(objeto);
            await ValidarReferenciasAsync(objeto);

            if (await _objetoQueryRepository.ExistsCodigoAsync(objeto.Codigo, objeto.Id))
                throw new BusinessException(
                    $"Ya existe otro objeto con el código {objeto.Codigo}.",
                    HttpStatusCode.Conflict);

            // Objeto
            existente.Codigo = objeto.Codigo;
            existente.Nombre = objeto.Nombre;
            existente.Descripcion = objeto.Descripcion;
            existente.Imagen = objeto.Imagen;
            existente.IdCategoria = objeto.IdCategoria;

            // Detalle (si por algún motivo no existía, se crea)
            existente.Detalle ??= new Detalleobjeto { IdObjeto = existente.Id };
            existente.Detalle.LugarEncontrado = objeto.Detalle.LugarEncontrado;
            existente.Detalle.FechaEncontrada = objeto.Detalle.FechaEncontrada;
            existente.Detalle.FechaEntrega = objeto.Detalle.FechaEntrega;
            existente.Detalle.DetalleVerificacion = objeto.Detalle.DetalleVerificacion;
            existente.Detalle.IdLugarAlmacenamiento = objeto.Detalle.IdLugarAlmacenamiento;
            existente.Detalle.IdUsuarioRegistro = objeto.Detalle.IdUsuarioRegistro;

            await _objetoQueryRepository.UpdateObjetoAsync(existente);
        }
        public async Task DeleteObjeto(uint id, uint idUsuarioAdmin)
        {
            await _usuarioService.ValidarAdministradorAsync(idUsuarioAdmin);

            var existente = await _objetoQueryRepository.GetObjetoByIdAsync(id);
            if (existente == null)
                throw new BusinessException("El objeto no existe.", HttpStatusCode.NotFound);

            await _objetoRepository.Delete(id);
        }

        private static void ValidarCamposObligatorios(Objeto objeto)
        {
            if (string.IsNullOrWhiteSpace(objeto.Nombre))
                throw new BusinessException("El nombre del objeto es obligatorio.");
            if (string.IsNullOrWhiteSpace(objeto.Codigo))
                throw new BusinessException("El código del objeto es obligatorio.");
            if (string.IsNullOrWhiteSpace(objeto.Descripcion))
                throw new BusinessException("La descripción del objeto es obligatoria.");
            if (objeto.IdCategoria == 0)
                throw new BusinessException("La categoría es obligatoria.");
            if (objeto.Detalle == null || objeto.Detalle.IdLugarAlmacenamiento == 0)
                throw new BusinessException("El lugar de almacenamiento es obligatorio.");
        }

        private async Task ValidarReferenciasAsync(Objeto objeto)
        {
            var categoria = await _categoriaService.GetCategoriaByIdAsync(objeto.IdCategoria);
            if (categoria == null || !categoria.Estado)
                throw new BusinessException("La categoría no existe o está inactiva.");

            var lugar = await _lugarService.GetLugarByIdAsync(objeto.Detalle!.IdLugarAlmacenamiento);
            if (lugar == null || !lugar.Estado)
                throw new BusinessException("El lugar de almacenamiento no existe o está inactivo.");
        }

    }
}

