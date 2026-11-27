using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Excepctions;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Services.Interfaces;
using ObjetosPerdidosUCB.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using ObjetosPerdidosUCB.Core.DTOs;

namespace ObjetosPerdidosUCB.Services.Service
{
    public class ObjetoService : IObjetoService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ICategoriaService _categoriaService;
        private readonly ILugarAlmacenamientoService _lugarService;

        public readonly IUnitOfWork _unitOfWork;


        public ObjetoService(IUnitOfWork unitOfWork,
            IUsuarioService usuarioService,
            ICategoriaService categoriaService,
            ILugarAlmacenamientoService lugarService)
        {
            _unitOfWork = unitOfWork;
            _usuarioService = usuarioService;
            _categoriaService = categoriaService;
            _lugarService = lugarService;
        }

        public async Task<IEnumerable<Objeto>> GetAllObjetosAsync()
        {
            var objetos = await _unitOfWork.ObjetoRepository.GetAll();
            return objetos.Where(o => o.Estado);
        }

        public async Task<ObjetoDto?> GetDetalleAsync(uint id)
        {
            var objeto = await _unitOfWork.ObjetoRepository.GetObjetoByIdAsync(id);
            if (objeto == null || !objeto.Estado)
                return null;
            return objeto;
        }
        public async Task<bool> ExistsCodigoAsync(string codigo)
        { 
            return await _unitOfWork.ObjetoRepository.ExistsCodigoAsync(codigo);
        }
        public async Task InsertObjeto(Objeto objeto)
        {
            if (objeto.Detalle == null)
                throw new BusinessException("El detalle del objeto es obligatorio.");

            await _usuarioService.ValidarAdministradorAsync(objeto.Detalle.IdUsuarioRegistro);
            ValidarCamposObligatorios(objeto);
            await ValidarReferenciasAsync(objeto);

            if (await _unitOfWork.ObjetoRepository.ExistsCodigoAsync(objeto.Codigo))
                throw new BusinessException($"Ya existe un objeto con el código {objeto.Codigo}.", HttpStatusCode.Conflict);

            objeto.Estado = true;
            await _unitOfWork.ObjetoRepository.Add(objeto);
        }
        public async Task UpdateObjeto(Objeto objeto)
        {
            //ya no necesitamos validar el usuario manualmente
            //await _usuarioService.ValidarAdministradorAsync(idUsuarioAdmin);

            var existente = await _unitOfWork.ObjetoRepository.GetObjetoParaActualizarAsync(objeto.Id);
            if (existente == null)
                throw new BusinessException("El objeto no existe.", HttpStatusCode.NotFound);

            if (objeto.Detalle == null)
                throw new BusinessException("El detalle del objeto es obligatorio.");

            ValidarCamposObligatorios(objeto);
            await ValidarReferenciasAsync(objeto);

            if (await _unitOfWork.ObjetoRepository.ExistsCodigoAsync(objeto.Codigo, objeto.Id))
                throw new BusinessException(
                    $"Ya existe otro objeto con el código {objeto.Codigo}.",
                    HttpStatusCode.Conflict);

            // Objeto
            existente.Codigo = objeto.Codigo;
            existente.Nombre = objeto.Nombre;
            existente.Descripcion = objeto.Descripcion;
            existente.Imagen = objeto.Imagen;
            existente.IdCategoria = objeto.IdCategoria;
            existente.Estado = objeto.Estado;

            // Detalle (si por algún motivo no existía, se crea)
            existente.Detalle ??= new Detalleobjeto { IdObjeto = existente.Id };
            existente.Detalle.LugarEncontrado = objeto.Detalle.LugarEncontrado;
            existente.Detalle.FechaEncontrada = objeto.Detalle.FechaEncontrada;
            existente.Detalle.FechaEntrega = objeto.Detalle.FechaEntrega;
            existente.Detalle.DetalleVerificacion = objeto.Detalle.DetalleVerificacion;
            existente.Detalle.IdLugarAlmacenamiento = objeto.Detalle.IdLugarAlmacenamiento;
            existente.Detalle.IdUsuarioRegistro = objeto.Detalle.IdUsuarioRegistro;

            await _unitOfWork.ObjetoRepository.UpdateObjetoAsync(existente);
        }
        public async Task DeleteObjeto(uint id)
        {
            //await _usuarioService.ValidarAdministradorAsync(idUsuarioAdmin);

            var existente = await _unitOfWork.ObjetoRepository.GetObjetoByIdAsync(id);
            if (existente == null)
                throw new BusinessException("El objeto no existe.", HttpStatusCode.NotFound);

            await _unitOfWork.ObjetoRepository.Delete(id);
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

        public async Task<IEnumerable<Objeto>> GetAllObjetosByNombreAsync(ObjetoQueryFilters? filters)
        {
            filters ??= new ObjetoQueryFilters();
            var objetos = await _unitOfWork.ObjetoRepository.GetObjetosParaFiltrarAsync();

            if (filters.categoria != null)
            {
                objetos = objetos.Where(x => x.IdCategoriaNavigation.Nombre == filters.categoria);
            }
            if (filters.lugarAlmacenamiento != null)
            {
                objetos = objetos.Where(x => x.Detalle.IdLugarAlmacenamientoNavigation.Nombre == filters.lugarAlmacenamiento);
            }
            if (filters.lugarEncontrado != null)
            {
                objetos = objetos.Where(x => x.Detalle.LugarEncontrado.ToLower()
                    .Contains(filters.lugarEncontrado.ToLower()));
            }
            if (filters.soloDisponibles != null)
            {
                objetos = objetos.Where(x => x.Estado == filters.soloDisponibles);
            }
            /*if (filters.Date != null)
            {
                string fechaAux = Procesos.ParseFechaFlexible(filters.Date);
                if (fechaAux != null)
                {
                    objetos = objetos.Where(x => x.Date.ToShortDateString() ==
                        Convert.ToDateTime(fechaAux).ToShortDateString());

                }
            }*/ // para añadir filtro por fecha

            return objetos;
        }
    }
}

