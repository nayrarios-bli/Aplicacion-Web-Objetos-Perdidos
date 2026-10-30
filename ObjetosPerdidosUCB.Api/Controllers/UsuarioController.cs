using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ObjetosPerdidosUCB.Api.Controllers.Response;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Entities;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Services.Validators.Usuarios;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly CrearUsuarioDtoValidator _crearValidator;
        private readonly ActualizarUsuarioDtoValidator _actualizarValidator;

        public UsuarioController(
            IUsuarioService usuarioService,
            IMapper mapper,
            CrearUsuarioDtoValidator crearValidator,
            ActualizarUsuarioDtoValidator actualizarValidator)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _crearValidator = crearValidator;
            _actualizarValidator = actualizarValidator;
        }

        [HttpGet("dto/mapper")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            return Ok(new ApiResponse<IEnumerable<UsuarioDto>>(usuariosDto));
        }

        [HttpGet("dto/mapper/{id}")]
        public async Task<IActionResult> GetUsuarioById(uint id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado." });

            return Ok(new ApiResponse<UsuarioDto>(_mapper.Map<UsuarioDto>(usuario)));
        }

        [HttpPost("dto/mapper/")]
        public async Task<IActionResult> InsertUsuario([FromBody] CrearUsuarioDto usuarioDto)
        {
            var validationResult = await _crearValidator.ValidateAsync(usuarioDto);
            if (!validationResult.IsValid)
                return BadRequest(ValidationResponse.From(validationResult));

            if (await _usuarioService.GetUsuarioByCorreoAsync(usuarioDto.CorreoElectronico) != null)
                return Conflict(new { message = "Ya existe un usuario con ese correo electrónico." });

            try
            {
                var usuario = _mapper.Map<Usuario>(usuarioDto);
                // Requiere el paquete NuGet BCrypt.Net-Next en el proyecto Api.
                usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Contrasena);

                await _usuarioService.InsertUsuario(usuario);

                var resultDto = _mapper.Map<UsuarioDto>(usuario);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id },
                    new ApiResponse<UsuarioDto>(resultDto, "Usuario registrado correctamente."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el usuario", error = ex.Message });
            }
        }

        [HttpPut("dto/mapper/{id}")]
        public async Task<IActionResult> UpdateUsuario(uint id, [FromBody] UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
                return BadRequest(new { message = "El ID del usuario no coincide." });

            var validationResult = await _actualizarValidator.ValidateAsync(usuarioDto);
            if (!validationResult.IsValid)
                return BadRequest(ValidationResponse.From(validationResult));

            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado." });

            var otro = await _usuarioService.GetUsuarioByCorreoAsync(usuarioDto.CorreoElectronico);
            if (otro != null && otro.Id != id)
                return Conflict(new { message = "Ya existe otro usuario con ese correo electrónico." });

            try
            {
                _mapper.Map(usuarioDto, usuario); // la contraseña no se toca
                await _usuarioService.UpdateUsuario(usuario);
                return Ok(new ApiResponse<UsuarioDto>(_mapper.Map<UsuarioDto>(usuario)));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el usuario", error = ex.Message });
            }
        }

        /// <summary>
        /// Baja lógica: el usuario tiene objetos y reclamos asociados (FK), por lo que
        /// se desactiva (Estado = false) en vez de borrarse.
        /// </summary>
        [HttpDelete("dto/mapper/{id}")]
        public async Task<IActionResult> DeactivateUsuario(uint id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound(new { message = "Usuario no encontrado." });

            try
            {
                usuario.Estado = false;
                await _usuarioService.UpdateUsuario(usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al desactivar el usuario", error = ex.Message });
            }
        }
    }
}
