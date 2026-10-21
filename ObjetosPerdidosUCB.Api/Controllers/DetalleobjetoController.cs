using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Api.Response;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Services.Validators;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleobjetoController : ControllerBase
    {
        private readonly IDetalleobjetoRepository _detalleobjetoRepository;
        private readonly IMapper _mapper;
        private readonly CrearDetalleobjetoDtoValidator _crearValidator;
        private readonly ActualizarDetalleobjetoDtoValidator _actualizarValidator;

        public DetalleobjetoController(IMapper mapper, IDetalleobjetoRepository detalleobjetoRepository,
            CrearDetalleobjetoDtoValidator crearValidator, ActualizarDetalleobjetoDtoValidator actualizarValidator)
        {
            _detalleobjetoRepository = detalleobjetoRepository;
            _mapper = mapper;
            _crearValidator = crearValidator;
            _actualizarValidator = actualizarValidator;
        }

        #region Dto-AutoMapper

        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetDetalleobjetosDtoMapper()
        {
            var detalles = await _detalleobjetoRepository.GetAllDetalleobjetosAsync();
            var detallesDto = _mapper.Map<IEnumerable<DetalleobjetoDto>>(detalles);

            var response = new ApiResponse
                <IEnumerable<DetalleobjetoDto>>(detallesDto);
            return Ok(response);
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetDetalleobjetoDtoByIdMapper(int id)
        {
            var detalle = await _detalleobjetoRepository.GetDetalleobjetoByIdAsync(id);
            var detaleDto = _mapper.Map<DetalleobjetoDto>(detalle);

            var response = new ApiResponse
                <DetalleobjetoDto>(detaleDto);
            return Ok(response);
        }

        [HttpPost("dto/mapper/")]
        public async Task<IActionResult> InsertDetalleobjetoDtoMapper(DetalleobjetoDto detalleDto)
        {
            var validationResult = await _crearValidator.ValidateAsync(detalleDto);
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

            try
            {
                var detalle = _mapper.Map<Detalleobjeto>(detalleDto);
                await _detalleobjetoRepository.InsertDetalleobjeto(detalle);

                var resultDto = _mapper.Map<DetalleobjetoDto>(detalle);
                var response = new ApiResponse
                    <Detalleobjeto> (detalle);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el detalle", error = ex.Message });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateDetalleobjetoDtoMapper(int id, [FromBody] DetalleobjetoDto detalleDto)
        {
            if (id != detalleDto.Id)
                return BadRequest("El ID del detalle no coincide.");

            var validationResult = await _actualizarValidator.ValidateAsync(detalleDto);
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

            var detalle = await _detalleobjetoRepository.GetDetalleobjetoByIdAsync(id);
            if (detalle == null)
                return NotFound(new { message = "Detalle no encontrado." });

            try
            {
                _mapper.Map(detalleDto, detalle);
                await _detalleobjetoRepository.UpdateDetalleobjeto(detalle);

                var resultDto = _mapper.Map<DetalleobjetoDto>(detalle);
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el detalle", error = ex.Message });
            }
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteDetalleobjetoDtoMapper(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "El ID debe ser mayor que cero." });

            var detalle = await _detalleobjetoRepository.GetDetalleobjetoByIdAsync(id);
            if (detalle == null)
                return NotFound(new { message = "Detalle no encontrado." });

            try
            {
                await _detalleobjetoRepository.DeleteDetalleobjeto(detalle);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el detalle", error = ex.Message });
            }
        }

        #endregion
    }
}