using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Data;
using TiendaNetApi.Usuario.Services;
using TiendaNetApi.Usuario.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "SoloAdmin")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _service.GetAll();

            return usuarios is not null ? Ok(usuarios) : NotFound("No se encontraron Usuarios");
        }
        [HttpGet("actives")]
        public async Task<IActionResult> GetAllActives()
        {
            var usuariosActivos = await _service.GetAllActives();
            return usuariosActivos is not null ? Ok(usuariosActivos) : NotFound("No se encontraron Usuarios Activos");

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var encontrado = await _service.GetById(id);
            return encontrado is not null ? Ok(encontrado) : NotFound($"No se encontro el Usuario, datos incorrectos UsuarioId = {id}");
        }

        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> GetbyIdDetallado(int id)
        {
            var usuarioDetallado = await _service.GetByIdDetallado(id);
            return usuarioDetallado is not null ? Ok(usuarioDetallado) : NotFound($"No se encontro el Usuario, datos incorrectos UsuarioId = {id}");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCrearDTO dto)
        {
            var created = await _service.Create(dto);
            if (created is null) return NotFound();
            return CreatedAtAction(nameof(GetById), new { Id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioUpdateDTO dto)
        {
            var updated = await _service.Update(id, dto);
            return updated ? Ok("Usuario creado con éxito.") : NotFound($"Usuario no encontrado, datos incorrectos UsuarioId = {id} \n UsuarioUpdateDTO {dto}");
        }

        [HttpDelete("logico/{id}")]
        public async Task<IActionResult> DeleteLogico(int id)
        {
            var deletedLogico = await _service.DeleteLogico(id);
            return deletedLogico ? Ok("Usuario eliminado lógicamente con éxito.") : NotFound($"No se encontro el Usuario, datos incorrectos UserId = {id}");
        }
        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisico(int id)
        {
            var deletedFisico = await _service.DeleteFisico(id);
            return deletedFisico ? Ok("Usuario eliminado fisicamente con éxito.") : NotFound($"No se encontro el Usuario | Usuario actualmente en uso, UserId = {id}");
        }

    }
}