using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Api.Data;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Interfaces;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamoController: ControllerBase
    {
        private readonly IReclamoRepository _reclamoRepository;
        public ReclamoController(IReclamoRepository reclamoRepository)
        {
            _reclamoRepository = reclamoRepository;
        }

        #region Sin_DTOs
        [HttpGet]
        public async Task<IActionResult> GetReclamos()
        {
            var reclamos = await _reclamoRepository.GetAllReclamosAsync();
            return Ok(reclamos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReclamoById(int id)
        {
            var reclamo = await _reclamoRepository.GetReclamoByIdAsync(id);
            return Ok(reclamo);
        }

        [HttpPost]
        public async Task<IActionResult> InserReclamo(Reclamo newReclamo)
        {
            await _reclamoRepository.InsertReclamo(newReclamo);
            return Created($"api/post/{newReclamo.Id}", newReclamo);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReclamo(Reclamo reclamo)
        {
            await _reclamoRepository.UpdateReclamo(reclamo);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReclamo(Reclamo reclamo)
        {
            await _reclamoRepository.DeleteReclamo(reclamo);
            return NoContent();
        }
        #endregion

        #region Con_DTOs
        [HttpGet("dto")]
        public async Task<IActionResult> GetReclamosDto()
        {
            var reclamos = await _reclamoRepository.GetAllReclamosAsync();
            var reclamoDto = reclamos.Select(p => new ReclamoDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Date = p.Date.ToString(),
                Description = p.Description,
                Imagen = p.Imagen
            });
            return Ok(reclamoDto);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetReclamoByIdDto(int id)
        {
            var reclamo = await _reclamoRepository.GetReclamoByIdAsync(id);
            var reclamoDto = new ReclamoDto
            {
                Id = reclamo.Id,
                UserId = reclamo.UserId,
                Date = reclamo.Date.ToString(),
                Description = reclamo.Description,
                Imagen = reclamo.Imagen
            };
            return Ok(reclamo);
        }

        [HttpPost("dto")]
        public async Task<IActionResult> InserPostDto(ReclamoDto newReclamo)
        {
            var reclamo = new Reclamo
            {
                Id = newReclamo.Id,
                UserId = newReclamo.UserId,
                Date = Convert.ToDateTime(newReclamo.Date),
                Description = newReclamo.Description,
                Imagen = newReclamo.Imagen
            };
            await _reclamoRepository.InsertReclamo(reclamo);
            return Created($"api/post/{newReclamo.Id}", newReclamo);
        }

        [HttpPut("dto")]
        public async Task<IActionResult> UpdateReclamoDto(int id, [FromBody] ReclamoDto reclamoDto)
        {
            if (id != reclamoDto.Id)
            {
                return BadRequest("el id del reclamo no coincide");
            }
            var reclamo = await _reclamoRepository.GetReclamoByIdAsync(id);
            if (reclamo == null)
            {
                return NotFound();
            }
            //mapeo
            reclamo.UserId = reclamo.UserId;
            reclamo.Date = reclamo.Date;
            reclamo.Description = reclamo.Description;
            reclamo.Imagen = reclamo.Imagen;
            await _reclamoRepository.UpdateReclamo(reclamo);
            return Ok(reclamo);
        }

        [HttpDelete("dto")]
        public async Task<IActionResult> DeleteReclamoDto(int id)
        {
            var reclamo = await _reclamoRepository.GetReclamoByIdAsync(id);
            if (reclamo == null)
            {
                return NotFound("reclamo no encontrado");
            }
            await _reclamoRepository.DeleteReclamo(reclamo);
            return NoContent();
        }
        #endregion

    }
}
