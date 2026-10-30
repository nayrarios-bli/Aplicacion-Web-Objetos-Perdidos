using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Api.Controllers.Response;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Services.Interfaces;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LugarAlmacenamientoController : ControllerBase
    {
        private readonly ILugarAlmacenamientoService _lugarService;
        private readonly IMapper _mapper;

        public LugarAlmacenamientoController(ILugarAlmacenamientoService lugarService, IMapper mapper)
        {
            _lugarService = lugarService;
            _mapper = mapper;
        }

        /// <summary>Lista los lugares de almacenamiento (CU-02 y CU-06).</summary>
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetLugares([FromQuery] bool soloActivos = true)
        {
            var lugares = await _lugarService.GetAllLugaresAsync(soloActivos);
            var lugaresDto = _mapper.Map<IEnumerable<LugarAlmacenamientoDto>>(lugares);
            return Ok(new ApiResponse<IEnumerable<LugarAlmacenamientoDto>>(lugaresDto));
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetLugarById(uint id)
        {
            var lugar = await _lugarService.GetLugarByIdAsync(id);
            if (lugar == null)
                return NotFound(new { message = "Lugar de almacenamiento no encontrado." });

            return Ok(new ApiResponse<LugarAlmacenamientoDto>(_mapper.Map<LugarAlmacenamientoDto>(lugar)));
        }
    }
}
