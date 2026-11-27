using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Api.Controllers.Response;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Enums;
using ObjetosPerdidosUCB.Core.Excepctions;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Core.QueryFilters;
using ObjetosPerdidosUCB.Services.Interfaces;
using ObjetosPerdidosUCB.Services.Validators.Objetos;
using System.Net;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetoController : ControllerBase
    {
        private readonly IObjetoService _objetoService;
        private readonly IUsuarioService _usuarioService;
        private readonly ICategoriaService _categoriaService;
        private readonly ILugarAlmacenamientoService _lugarService;
        private readonly IMapper _mapper;
        private readonly CrearObjetoDtoValidator _crearValidator;
        private readonly ActualizarObjetoDtoValidator _actualizarValidator;
        private readonly ObjetoDtoValidator _objetoValidator;

        public ObjetoController(
            IObjetoService objetoService,
            IUsuarioService usuarioService,
            ICategoriaService categoriaService,
            ILugarAlmacenamientoService lugarService,
            IMapper mapper,
            CrearObjetoDtoValidator crearValidator,
            ObjetoDtoValidator objetoValidator,
            ActualizarObjetoDtoValidator actualizarValidator)
        {
            _objetoService = objetoService;
            _usuarioService = usuarioService;
            _categoriaService = categoriaService;
            _lugarService = lugarService;
            _mapper = mapper;
            _objetoValidator = objetoValidator;
            _crearValidator = crearValidator;
            _actualizarValidator = actualizarValidator;
        }

        /// <summary>
        /// CU-03 Buscar objetos (y CU-06 filtrar por lugar de almacenamiento).
        /// Ejemplo: GET api/Objeto?texto=laptop&amp;idCategoria=1&amp;soloDisponibles=true
        /// </summary>
        [HttpGet("dto/mapper/buscar")]
        public async Task<IActionResult> FiltrarObjetos([FromQuery] ObjetoQueryFilters? filters)
        {
            var objetos = await _objetoService.GetAllObjetosByNombreAsync(filters);
            var objetoDto = _mapper.Map<IEnumerable<ObjetoDto>>(objetos);
            
            var response = new ApiResponse
                <IEnumerable<ObjetoDto>>(objetoDto);

            return Ok(response);
        }

        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetObjetoDtoMapper()
        {
            var objetos = await _objetoService.GetAllObjetosAsync();
            var objetoDto = _mapper.Map<IEnumerable<ObjetoDto>>(objetos);
           

            var response = new ApiResponse
                <IEnumerable<ObjetoDto>>(objetoDto);

            return Ok(response);
        }

        /// <summary>CU-05 Consultar detalle del objeto.</summary>
        [Authorize(Roles = nameof(RoleType.Administrador))]
        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetObjetoById(uint id)
        {
            if (id == 0)
                return BadRequest(new { message = "El ID debe ser mayor que cero." });

            try
            {
                var detalle = await _objetoService.GetDetalleAsync(id);

                if (detalle == null)
                    return NotFound(new { message = "Objeto no encontrado." });

                return Ok(new ApiResponse<ObjetoDto>(detalle));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al consultar el objeto", error = ex.Message });
            }
        }

        /// <summary>CU-02 Registrar objeto encontrado.</summary>
        [Authorize(Roles = nameof(RoleType.Administrador))]
        [HttpPost("dto/mapper/")]
        public async Task<IActionResult> InsertObjeto([FromBody] CrearObjetoDto objetoDto)
        {
            var validationResult = await _crearValidator.ValidateAsync(objetoDto);
            if (!validationResult.IsValid)
                return BadRequest(ValidationResponse.From(validationResult));

            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(objetoDto.IdUsuarioRegistro);
                if (usuario == null || !usuario.Estado)
                    return BadRequest(new { message = "El usuario que registra el objeto no existe o está inactivo." });

                var categoria = await _categoriaService.GetCategoriaByIdAsync(objetoDto.IdCategoria);
                if (categoria == null || !categoria.Estado)
                    return BadRequest(new { message = "La categoría no existe o está inactiva." });

                var lugar = await _lugarService.GetLugarByIdAsync(objetoDto.IdLugarAlmacenamiento);
                if (lugar == null || !lugar.Estado)
                    return BadRequest(new { message = "El lugar de almacenamiento no existe o está inactivo." });

                if (await _objetoService.ExistsCodigoAsync(objetoDto.Codigo))
                    return Conflict(new { message = $"Ya existe un objeto con el código {objetoDto.Codigo}." });

                var objeto = _mapper.Map<Objeto>(objetoDto);
                await _objetoService.InsertObjeto(objeto);

                // Se vuelve a consultar para traer el nombre de la categoría y del lugar.
                var guardado = await _objetoService.GetObjetoByIdAsync(objeto.Id);
                var resultDto = _mapper.Map<ObjetoDto>(guardado);

                return CreatedAtAction(
                    nameof(GetObjetoById),
                    new { id = objeto.Id },
                    new ApiResponse<ObjetoDto>(resultDto, "Objeto registrado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al registrar el objeto", error = ex.Message });
            }
        }

        [Authorize(Roles = nameof(RoleType.Administrador))]
        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateObjeto(
            uint id,
            [FromBody] ActualizarObjetoDto objetoDto)
        {
            if (id == 0)
                return BadRequest(new { message = "El ID debe ser mayor que cero." });

            if (id != objetoDto.Id)
                return BadRequest(new { message = "El ID de la ruta no coincide con el del cuerpo." });

            var validationResult = await _actualizarValidator.ValidateAsync(objetoDto);
            if (!validationResult.IsValid)
                return BadRequest(ValidationResponse.From(validationResult));

            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(objetoDto.IdUsuarioRegistro);
                if (usuario == null || !usuario.Estado)
                    return BadRequest(new { message = "El usuario que registra el objeto no existe o está inactivo." });

                var categoria = await _categoriaService.GetCategoriaByIdAsync(objetoDto.IdCategoria);
                if (categoria == null || !categoria.Estado)
                    return BadRequest(new { message = "La categoría no existe o está inactiva." });

                var lugar = await _lugarService.GetLugarByIdAsync(objetoDto.IdLugarAlmacenamiento);
                if (lugar == null || !lugar.Estado)
                    return BadRequest(new { message = "El lugar de almacenamiento no existe o está inactivo." });

                var objeto = _mapper.Map<Objeto>(objetoDto);
                await _objetoService.UpdateObjeto(objeto);

                // Se vuelve a consultar para traer el nombre de la categoría y del lugar.
                var actualizado = await _objetoService.GetObjetoByIdAsync(objeto.Id);
                var resultDto = _mapper.Map<ObjetoDto>(actualizado);

                return Ok(new ApiResponse<ObjetoDto>(resultDto, "Objeto actualizado correctamente."));
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)ex.StatusCode, new { message = ex.Message, errors = ex.Details });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el objeto", error = ex.Message });
            }
        }

        [Authorize(Roles = nameof(RoleType.Administrador))]
        [HttpDelete("dto/mapper/{id}")]
            public async Task<IActionResult> DeletePostDtoMapper(uint id)
            {
                // Validar que el ID sea válido
                if (id <= 0)
                    return BadRequest(new { message = "El ID debe ser mayor que cero." });

                var objeto = await _objetoService.GetObjetoByIdAsync(id);
                if (objeto == null)
                    return NotFound(new { message = "Post no encontrado." });

                try
                {
                    await _objetoService.DeleteObjeto(id);
                    return NoContent(); // 204 sin contenido
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "Error al eliminar el post", error = ex.Message });
                }
            }
        
    }
}
