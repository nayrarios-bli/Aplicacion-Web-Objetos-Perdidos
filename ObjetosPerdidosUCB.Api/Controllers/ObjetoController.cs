using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Api.Response;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Repositories;
using ObjetosPerdidosUCB.Services.Validators;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetoController : ControllerBase
    {
        private readonly IObjetoRepository _objetoRepository;
        private readonly IMapper _mapper;
        private readonly IDetalleobjetoRepository _detalleobjetoRepository;
        private readonly CrearObjetoDtoValidator _crearValidator;

        private readonly ActualizarObjetoDtoValidator _actualizaValidator;
        public ObjetoController(IMapper mapper, IObjetoRepository objetoRepository,
            CrearObjetoDtoValidator crearValidator, ActualizarObjetoDtoValidator actualizaValidator,
            IDetalleobjetoRepository detalleobjetoRepository)
        {
            _objetoRepository = objetoRepository;
            _mapper = mapper;
            _detalleobjetoRepository = detalleobjetoRepository;
            _crearValidator = crearValidator;
            _actualizaValidator = actualizaValidator;
        }
        
        #region Mapper
        [HttpGet("dto/mapper")]

        public async Task<IActionResult> GetObjetoDtoMapper()
        {
            var objetos = await _objetoRepository.GetAllObjetosAsync();
            var objetosDto = _mapper.Map<IEnumerable<ObjetoDto>>(objetos);
            return Ok(objetosDto);
        }
        [HttpGet("dto/mapper/{nombre}")]

        public async Task<IActionResult> GetObjetoDtoByNombreMapper(string nombre)
        {
            var objeto = await _objetoRepository.GetObjetoByNombreAsync(nombre);
            var objetosDto = _mapper.Map<ObjetoDto>(objeto);
            return Ok(objeto);
        }

        [HttpGet("dto/mapper/buscar")]
        public async Task<IActionResult> BuscarObjetosDtoMapper([FromQuery] string? nombre)
        {
            try
            {
                var objetos = await _objetoRepository.GetAllObjetosAsync();

                if (!string.IsNullOrWhiteSpace(nombre))
                    objetos = objetos.Where(o => o.Nombre.Contains(nombre.Trim(),
                                                 StringComparison.OrdinalIgnoreCase));

                var objetosDto = _mapper.Map<IEnumerable<ObjetoDto>>(objetos);
                var response = new ApiResponse
                    <IEnumerable<ObjetoDto>>(objetosDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al buscar los objetos", error = ex.Message });
            }
        }

        [HttpPost("dto/mapper/")]
        public async Task<IActionResult> InsertObjetoDtoMapper(ObjetoDto objetoDto)
        {
            var validationResult = await _crearValidator.ValidateAsync(objetoDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            // Verificar que el detalle exista, si no la FK daría un error 500
            var detalle = await _detalleobjetoRepository.GetDetalleobjetoByIdAsync(objetoDto.IddetalleObjeto);
            if (detalle == null)
                return NotFound(new { message = "El detalle del objeto no existe." });

            try
            {
                var objeto = _mapper.Map<Objeto>(objetoDto);
                await _objetoRepository.InsertObjeto(objeto);

                var resultDto = _mapper.Map<ObjetoDto>(objeto);
                var response = new ApiResponse
                    <ObjetoDto>(resultDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el objeto", error = ex.Message });
            }
        }
        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateObjetoDtoMapper(int id, [FromBody] ObjetoDto objetoDto)
        {
            if (id != objetoDto.Id)
                return BadRequest("El ID del objeto no coincide.");

            var validationResult = await _actualizaValidator.ValidateAsync(objetoDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    message = "Error de validación",
                    errors = validationResult.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        error = e.ErrorMessage
                    })
                });
            }

            var objeto = await _objetoRepository.GetObjetoByIdAsync(id);
            if (objeto == null)
                return NotFound(new { message = "Objeto no encontrado." });

            // Si cambian el detalle, que el nuevo exista
            var detalle = await _detalleobjetoRepository.GetDetalleobjetoByIdAsync(objetoDto.IddetalleObjeto);
            if (detalle == null)
                return NotFound(new { message = "El detalle del objeto no existe." });

            try
            {
                _mapper.Map(objetoDto, objeto);
                await _objetoRepository.UpdateObjeto(objeto);

                var resultDto = _mapper.Map<ObjetoDto>(objeto);
                var resopnse = new ApiResponse
                    <ObjetoDto>(resultDto);
                return Ok(resopnse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el objeto", error = ex.Message });
            }
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteObjetoDtoMapper(int id)
        {
            if (id == 0)
                return BadRequest(new { message = "El ID debe ser mayor que cero." });

            var objeto = await _objetoRepository.GetObjetoByIdAsync(id);
            if (objeto == null)
                return NotFound(new { message = "Objeto no encontrado." });

            try
            {
                await _objetoRepository.DeleteObjeto(objeto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el objeto", error = ex.Message });
            }
        }
        #endregion
    }
}
