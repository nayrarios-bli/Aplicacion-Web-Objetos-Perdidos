using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Api.Controllers.Response;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamoController : ControllerBase
    {
        private readonly IReclamoRepository _reclamoRepository;
        private readonly IMapper _mapper;
        private readonly CrearReclamoValidator _crearValidator;
        private readonly ActualizarReclamoDtoValidator _actualizarValidator;
        public ReclamoController(IReclamoRepository reclamoRepository, IMapper mapper, CrearReclamoValidator crearValidator, ActualizarReclamoDtoValidator actualizarValidator)
        {
            _reclamoRepository = reclamoRepository;
            _mapper = mapper;
            _crearValidator = crearValidator;
            _actualizarValidator = actualizarValidator;
        }

        #region Sin_DTOs
        /*        [HttpGet]
                public async Task<IActionResult> GetPosts()
                {
                    var posts = await _postRepository.GetAllPostsAsync();
                    return Ok(posts);
                }

                [HttpGet("{id}")]
                public async Task<IActionResult> GetPostById(int id)
                {
                    var post = await _postRepository.GetPostByIdAsync(id);
                    return Ok(post);
                }

                [HttpPost]
                public async Task<IActionResult> InserPost(Post newPost)
                {
                    await _postRepository.InsertPost(newPost);
                    return Created($"api/post/{newPost.Id}", newPost);
                }

                [HttpPut]
                public async Task<IActionResult> UpdatePost(Post post)
                {
                    await _postRepository.UpdatePost(post);
                    return NoContent();
                }

                [HttpDelete]
                public async Task<IActionResult> DeletePost(Post post)
                {
                    await _postRepository.DeletePost(post);
                    return NoContent();
                }
                #endregion
        */
/*
                #region Con_DTOs
                [HttpGet("dto")]
                public async Task<IActionResult> GetPostsDto()
                {
                    var posts = await _postRepository.GetAllPostsAsync();
                    var postDto = posts.Select(p => new PostDto
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        Date = p.Date.ToString(),
                        Description = p.Description,
                        Imagen = p.Imagen
                    });
                    return Ok(postDto);
                }

                [HttpGet("dto/{id}")]
                public async Task<IActionResult> GetPostByIdDto(int id)
                {
                    var post = await _postRepository.GetPostByIdAsync(id);
                    var postDto = new PostDto
                    {
                        Id = post.Id,
                        UserId = post.UserId,
                        Date = post.Date.ToString(),
                        Description = post.Description,
                        Imagen = post.Imagen
                    };
                    return Ok(post);
                }

                [HttpPost("dto")]
                public async Task<IActionResult> InserPostDto(PostDto newPost)
                {
                    var post = new Post
                    {
                        Id = newPost.Id,
                        UserId = newPost.UserId,
                        Date = Convert.ToDateTime(newPost.Date),
                        Description = newPost.Description,
                        Imagen = newPost.Imagen
                    };
                    await _postRepository.InsertPost(post);
                    return Created($"api/post/{newPost.Id}", newPost);
                }

                [HttpPut("dto")]
                public async Task<IActionResult> UpdatePostDto(int id, [FromBody] PostDto postDto)
                {
                    if(id != postDto.Id)
                    {
                        return BadRequest("el id del post no coincide");
                    }
                    var post = await _postRepository.GetPostByIdAsync(id);
                    if(post == null)
                    {
                        return NotFound();
                    }
                    //mapeo
                    post.UserId = post.UserId;
                    post.Date = post.Date;
                    post.Description = post.Description;
                    post.Imagen = post.Imagen;
                    await _postRepository.UpdatePost(post);
                    return Ok(post);
                }

                [HttpDelete("dto")]
                public async Task<IActionResult> DeletePostDto(int id)
                {
                    var post = await _postRepository.GetPostByIdAsync(id);
                    if(post == null)
                    {
                        return NotFound("post no encontrado");
                    }
                    await _postRepository.DeletePost(post);
                    return NoContent();
                }*/
        #endregion

        #region Dto-AutoMapper
        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetReclamosDtoMapper()
        {
            var reclamos = await _reclamoRepository.GetAllReclamosAsync();
            var reclamoDto = _mapper.Map<IEnumerable<ReclamoDto>>(reclamos);
            var response = new ApiResponse<IEnumerable<ReclamoDto>>(reclamoDto);
            return Ok(response);
        }

        [HttpGet("dto/mapper/[id]")]
        public async Task<IActionResult> GetReclamoByIdDtoMapper(int id)
        {
            var reclamo = await _reclamoRepository.GetReclamoByIdAsync(id);
            var reclamoDto = _mapper.Map<ReclamoDto>(reclamo);
            var response = new ApiResponse<ReclamoDto>(reclamoDto);
            return Ok(response);
        }

        [HttpPost("dto/mapper/")]
        public async Task<IActionResult> InsertReclamoDtoMapper(ReclamoDto reclamoDto)
        {
            // Validar el DTO
            var validationResult = await _crearValidator.ValidateAsync(reclamoDto);
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
                var reclamo = _mapper.Map<Reclamo>(reclamoDto);
                await _reclamoRepository.InsertReclamo(reclamo);

                var resultDto = _mapper.Map<ReclamoDto>(reclamo);
                var response = new ApiResponse<Reclamo>(reclamo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el reclamo", error = ex.Message });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateReclamoDtoMapper(int id, [FromBody] ReclamoDto reclamoDto)
        {
            if (id != reclamoDto.Id)
                return BadRequest("El ID del reclamo no coincide.");

            // Validar el DTO
            var validationResult = await _actualizarValidator.ValidateAsync(reclamoDto);
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

            var reclamo = await _reclamoRepository.GetReclamoByIdAsync(id);
            if (reclamo == null)
                return NotFound(new { message = "Reclamo no encontrado." });

            try
            {
                _mapper.Map(reclamoDto, reclamo);
                await _reclamoRepository.UpdateReclamo(reclamo);

                var resultDto = _mapper.Map<ReclamoDto>(reclamo);
                return Ok(resultDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el post", error = ex.Message });
            }
        }

        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeleteReclamoDtoMapper(int id)
        {
            // Validar que el ID sea válido
            if (id <= 0)
                return BadRequest(new { message = "El ID debe ser mayor que cero." });

            var reclamo = await _reclamoRepository.GetReclamoByIdAsync(id);
            if (reclamo == null)
                return NotFound(new { message = "Reclamo no encontrado." });

            try
            {
                await _reclamoRepository.DeleteReclamo(reclamo);
                return NoContent(); // 204 sin contenido
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el reclamo", error = ex.Message });
            }
        }

        #endregion
    }
}
