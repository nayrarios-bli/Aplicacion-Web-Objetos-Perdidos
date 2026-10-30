using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Api.Controllers.Response;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Services.Interfaces;
using ObjetosPerdidosUCB.Services.Validators.Reclamos;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamoController : ControllerBase
    {
        private readonly IReclamoService _reclamoService;
        private readonly IObjetoService _objetoService;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly CrearReclamoDtoValidator _crearValidator;

        public ReclamoController(
           IReclamoService reclamoService,
           IObjetoService objetoService,
           IUsuarioService usuarioService,
            IMapper mapper,
            CrearReclamoDtoValidator crearValidator)
        {
            _reclamoService = reclamoService;
            _objetoService = objetoService;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _crearValidator = crearValidator;
        }

        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetReclamos()
        {
            var reclamos = await _reclamoService.GetAllReclamosAsync();
            var reclamosDto = _mapper.Map<IEnumerable<ReclamoDto>>(reclamos);
            return Ok(new ApiResponse<IEnumerable<ReclamoDto>>(reclamosDto));
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetReclamoById(uint id)
        {
            var reclamo = await _reclamoService.GetReclamoByIdAsync(id);
            if (reclamo == null)
                return NotFound(new { message = "Reclamo no encontrado." });

            return Ok(new ApiResponse<ReclamoDto>(_mapper.Map<ReclamoDto>(reclamo)));
        }

        /// <summary>CU-07 Registrar reclamo (queda en estado "pendiente").</summary>
        [HttpPost("dto/mapper/")]
        public async Task<IActionResult> InsertReclamo([FromBody] CrearReclamoDto reclamoDto)
        {
            var validationResult = await _crearValidator.ValidateAsync(reclamoDto);
            if (!validationResult.IsValid)
                return BadRequest(ValidationResponse.From(validationResult));

            try
            {
                var usuario = await _usuarioService.GetUsuarioByIdAsync(reclamoDto.IdUsuario);
                if (usuario == null || !usuario.Estado)
                    return BadRequest(new { message = "El usuario no existe o está inactivo." });

                var objeto = await _objetoService.GetObjetoByIdAsync(reclamoDto.IdObjeto);
                if (objeto == null)
                    return NotFound(new { message = "Objeto no encontrado." });

                if (!objeto.Estado)
                    return BadRequest(new { message = "El objeto ya fue entregado y no puede reclamarse." });

                if (await _reclamoService.ExistsPendienteAsync(reclamoDto.IdObjeto, reclamoDto.IdUsuario))
                    return Conflict(new { message = "Ya tienes un reclamo pendiente sobre este objeto." });

                var reclamo = _mapper.Map<Reclamo>(reclamoDto);
                await _reclamoService.InsertReclamo(reclamo);

                var resultDto = _mapper.Map<ReclamoDto>(reclamo);
                return CreatedAtAction(nameof(GetReclamoById), new { id = reclamo.Id },
                    new ApiResponse<ReclamoDto>(resultDto, "Reclamo registrado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el reclamo", error = ex.Message });
            }
        }
    }
}
