using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjetoController : ControllerBase
    {
        private readonly IObjetoRepository _objetoRepository;
        private readonly IMapper _mapper;
        public ObjetoController(IMapper mapper, IObjetoRepository objetoRepository)
        {
            _objetoRepository = objetoRepository;
            _mapper = mapper;
        }
        #region con Dto
        [HttpGet("dto")]
        public async Task<IActionResult> GetObjetosDto()
        {
            var objetos = await _objetoRepository.GetAllObjetosAsync();
            var objetosDtos = objetos.Select(o => new ObjetoDto
            {
                Id = o.Id,
                Codigo = o.Codigo,
                Nombre = o.Nombre,
                Imagen = o.Imagen,
                LugarEncontrado = o.LugarEncontrado,
                FechaEncontrada = o.FechaEncontrada,
                Descripcion = o.Descripcion,
                Estado = o.Estado,
                IdUsuario = o.IdUsuario
            });
            return Ok(objetosDtos);
        }

        [HttpGet("dto/{nombre}")]

        public async Task<IActionResult> GetObjetoByNombre(string nombre)
        {
            var objeto = await _objetoRepository.GetObjetoByNombreAsync(nombre);
            var objetoDto = new ObjetoDto
            {
                Id = objeto.Id,
                Codigo = objeto.Codigo,
                Nombre = objeto.Nombre,
                Imagen = objeto.Imagen,
                LugarEncontrado = objeto.LugarEncontrado,
                FechaEncontrada = objeto.FechaEncontrada,
                Descripcion = objeto.Descripcion,
                Estado = objeto.Estado,
                IdUsuario = objeto.IdUsuario
            };
            return Ok(objeto);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InsertObjetoDto(ObjetoDto newObjeto)
        {
            var objeto = new Objeto
            {
                Id = newObjeto.Id,
                Codigo = newObjeto.Codigo,
                Nombre = newObjeto.Nombre,
                Imagen = newObjeto.Imagen,
                LugarEncontrado = newObjeto.LugarEncontrado,
                FechaEncontrada = newObjeto.FechaEncontrada,
                Descripcion = newObjeto.Descripcion,
                Estado = newObjeto.Estado,
                IdUsuario = newObjeto.IdUsuario
            };
            await _objetoRepository.InsertObjeto(objeto);
            return Created($"api/comment/{newObjeto.Id}", newObjeto);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdateObjetoDto(string nombre, [FromBody] ObjetoDto objetoDto)
        {
            if (nombre != objetoDto.Nombre)
            {
                return BadRequest("El nombre del Objeto no coincide");
            }
            var objeto = await _objetoRepository.GetObjetoByNombreAsync(nombre);

            if (objeto == null)
                return NotFound("objeto no encontrado");

            objeto.Id = objetoDto.Id;
            objeto.Codigo = objetoDto.Codigo;
            objeto.Nombre = objetoDto.Nombre;
            objeto.Imagen = objetoDto.Imagen;
            objeto.LugarEncontrado = objetoDto.LugarEncontrado;
            objeto.FechaEncontrada = objetoDto.FechaEncontrada;
            objeto.Descripcion = objetoDto.Descripcion;
            objeto.Estado = objetoDto.Estado;
            objeto.IdUsuario = objetoDto.IdUsuario;


            await _objetoRepository.UpdateObjeto(objeto);
            return NoContent();
        }

        [HttpDelete("dto/{id}")]
        public async Task<IActionResult> DeleteObjetoDto(string nombre)
        {
            var objeto = await _objetoRepository.GetObjetoByNombreAsync(nombre);
            if (objeto == null)
                return NotFound("Objeto no encontrado.");

            await _objetoRepository.DeleteObjeto(objeto);

            return NoContent(); 
        }
        #endregion

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
        #endregion
    }
}
