using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Api.Controllers.Response;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Interfaces;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        /// <summary>Lista las categorías (para llenar el filtro del catálogo y el formulario de CU-02).</summary>
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetCategorias([FromQuery] bool soloActivas = true)
        {
            var categorias = await _categoriaRepository.GetAllCategoriasAsync(soloActivas);
            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
            return Ok(new ApiResponse<IEnumerable<CategoriaDto>>(categoriasDto));
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetCategoriaById(uint id)
        {
            var categoria = await _categoriaRepository.GetCategoriaByIdAsync(id);
            if (categoria == null)
                return NotFound(new { message = "Categoría no encontrada." });

            return Ok(new ApiResponse<CategoriaDto>(_mapper.Map<CategoriaDto>(categoria)));
        }
    }
}
