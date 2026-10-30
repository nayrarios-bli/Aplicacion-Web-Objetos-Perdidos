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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaService categoriaService, IMapper mapper)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        /// <summary>Lista las categorías (para llenar el filtro del catálogo y el formulario de CU-02).</summary>
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetCategorias([FromQuery] bool soloActivas = true)
        {
            var categorias = await _categoriaService.GetAllCategoriasAsync(soloActivas);
            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
            return Ok(new ApiResponse<IEnumerable<CategoriaDto>>(categoriasDto));
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetCategoriaById(uint id)
        {
            var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
            if (categoria == null)
                return NotFound(new { message = "Categoría no encontrada." });

            return Ok(new ApiResponse<CategoriaDto>(_mapper.Map<CategoriaDto>(categoria)));
        }
    }
}
