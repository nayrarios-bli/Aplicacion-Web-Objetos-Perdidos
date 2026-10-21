using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Api.Controllers.Response;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Services.Validators.Objetos;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetoController : ControllerBase
    {
        private readonly IObjetoRepository _objetoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILugarAlmacenamientoRepository _lugarRepository;
        private readonly IMapper _mapper;
        private readonly CrearObjetoDtoValidator _crearValidator;

        public ObjetoController(
            IObjetoRepository objetoRepository,
            IUsuarioRepository usuarioRepository,
            ICategoriaRepository categoriaRepository,
            ILugarAlmacenamientoRepository lugarRepository,
            IMapper mapper,
            CrearObjetoDtoValidator crearValidator)
        {
            _objetoRepository = objetoRepository;
            _usuarioRepository = usuarioRepository;
            _categoriaRepository = categoriaRepository;
            _lugarRepository = lugarRepository;
            _mapper = mapper;
            _crearValidator = crearValidator;
        }

        /// <summary>
        /// CU-03 Buscar objetos (y CU-06 filtrar por lugar de almacenamiento).
        /// Ejemplo: GET api/Objeto?texto=laptop&amp;idCategoria=1&amp;soloDisponibles=true
        /// </summary>
        [HttpGet("dto/mapper/buscar")]
        public async Task<IActionResult> BuscarObjetos(
            [FromQuery] string? texto,
            [FromQuery] uint? idCategoria,
            [FromQuery] uint? idLugarAlmacenamiento,
            [FromQuery] string? lugarEncontrado,
            [FromQuery] bool? soloDisponibles)
        {
            try
            {
                var objetos = await _objetoRepository.SearchObjetosAsync(
                    texto, idCategoria, idLugarAlmacenamiento, lugarEncontrado, soloDisponibles);

                var objetosDto = _mapper.Map<IEnumerable<ObjetoDto>>(objetos).ToList();

                var mensaje = objetosDto.Count == 0
                    ? "No se encontraron objetos que coincidan con la búsqueda."
                    : null;

                return Ok(new ApiResponse<IEnumerable<ObjetoDto>>(objetosDto, mensaje));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al consultar los objetos", error = ex.Message });
            }
        }
        [HttpGet("dto/mapper")]

        public async Task<IActionResult> GetObjetoDtoMapper()
        {
            var objetos = await _objetoRepository.GetAllObjetosAsync();
            var objetoDto = _mapper.Map<IEnumerable<ObjetoDto>>(objetos);
           

            var response = new ApiResponse
                <IEnumerable<ObjetoDto>>(objetoDto);

            return Ok(response);
        }

        /// <summary>CU-05 Consultar detalle del objeto.</summary>
        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetObjetoById(uint id)
        {
            if (id == 0)
                return BadRequest(new { message = "El ID debe ser mayor que cero." });

            try
            {
                var objeto = await _objetoRepository.GetObjetoByIdAsync(id);
                if (objeto == null)
                    return NotFound(new { message = "Objeto no encontrado." });

                return Ok(new ApiResponse<ObjetoDto>(_mapper.Map<ObjetoDto>(objeto)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al consultar el objeto", error = ex.Message });
            }
        }

        /// <summary>CU-02 Registrar objeto encontrado.</summary>
        [HttpPost("dto/mapper/")]
        public async Task<IActionResult> InsertObjeto([FromBody] CrearObjetoDto objetoDto)
        {
            var validationResult = await _crearValidator.ValidateAsync(objetoDto);
            if (!validationResult.IsValid)
                return BadRequest(ValidationResponse.From(validationResult));

            try
            {
                var usuario = await _usuarioRepository.GetUsuarioByIdAsync(objetoDto.IdUsuarioRegistro);
                if (usuario == null || !usuario.Estado)
                    return BadRequest(new { message = "El usuario que registra el objeto no existe o está inactivo." });

                var categoria = await _categoriaRepository.GetCategoriaByIdAsync(objetoDto.IdCategoria);
                if (categoria == null || !categoria.Estado)
                    return BadRequest(new { message = "La categoría no existe o está inactiva." });

                var lugar = await _lugarRepository.GetLugarByIdAsync(objetoDto.IdLugarAlmacenamiento);
                if (lugar == null || !lugar.Estado)
                    return BadRequest(new { message = "El lugar de almacenamiento no existe o está inactivo." });

                if (await _objetoRepository.ExistsCodigoAsync(objetoDto.Codigo))
                    return Conflict(new { message = $"Ya existe un objeto con el código {objetoDto.Codigo}." });

                var objeto = _mapper.Map<Objeto>(objetoDto);
                await _objetoRepository.InsertObjeto(objeto);

                // Se vuelve a consultar para traer el nombre de la categoría y del lugar.
                var guardado = await _objetoRepository.GetObjetoByIdAsync(objeto.Id);
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
    }
}
