using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Api.Controllers.Response;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Services.Validators.Reclamos;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamoController : ControllerBase
    {
        private readonly IReclamoRepository _reclamoRepository;
        private readonly IObjetoRepository _objetoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly CrearReclamoDtoValidator _crearValidator;

        public ReclamoController(
            IReclamoRepository reclamoRepository,
            IObjetoRepository objetoRepository,
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            CrearReclamoDtoValidator crearValidator)
        {
            _reclamoRepository = reclamoRepository;
            _objetoRepository = objetoRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _crearValidator = crearValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetReclamos()
        {
            var reclamos = await _reclamoRepository.GetAllReclamosAsync();
            var reclamosDto = _mapper.Map<IEnumerable<ReclamoDto>>(reclamos);
            return Ok(new ApiResponse<IEnumerable<ReclamoDto>>(reclamosDto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReclamoById(uint id)
        {
            var reclamo = await _reclamoRepository.GetReclamoByIdAsync(id);
            if (reclamo == null)
                return NotFound(new { message = "Reclamo no encontrado." });

            return Ok(new ApiResponse<ReclamoDto>(_mapper.Map<ReclamoDto>(reclamo)));
        }

        /// <summary>CU-07 Registrar reclamo (queda en estado "pendiente").</summary>
        [HttpPost]
        public async Task<IActionResult> InsertReclamo([FromBody] CrearReclamoDto reclamoDto)
        {
            var validationResult = await _crearValidator.ValidateAsync(reclamoDto);
            if (!validationResult.IsValid)
                return BadRequest(ValidationResponse.From(validationResult));

            try
            {
                var usuario = await _usuarioRepository.GetUsuarioByIdAsync(reclamoDto.IdUsuario);
                if (usuario == null || !usuario.Estado)
                    return BadRequest(new { message = "El usuario no existe o está inactivo." });

                var objeto = await _objetoRepository.GetObjetoByIdAsync(reclamoDto.IdObjeto);
                if (objeto == null)
                    return NotFound(new { message = "Objeto no encontrado." });

                if (!objeto.Estado)
                    return BadRequest(new { message = "El objeto ya fue entregado y no puede reclamarse." });

                if (await _reclamoRepository.ExistsPendienteAsync(reclamoDto.IdObjeto, reclamoDto.IdUsuario))
                    return Conflict(new { message = "Ya tienes un reclamo pendiente sobre este objeto." });

                var reclamo = _mapper.Map<Reclamo>(reclamoDto);
                await _reclamoRepository.InsertReclamo(reclamo);

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
