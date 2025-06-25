using TiendaNetApi.Rol.DTOs;
using TiendaNetApi.Rol.Services;
using TiendaNetApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "SoloAdmin")]
    public class RolController : ControllerBase
    {
        private readonly IRolService _service;
        public RolController(IRolService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _service.GetAll();
            if (roles is null) return NotFound("No se encontraron Roles");
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rolEncontrado = await _service.GetById(id);
            return rolEncontrado is not null ? Ok(rolEncontrado) : NotFound($"Rol no encontrado, datos incorrectos RolId = {id}");
        }
        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> GetByIdDetallado(int id)
        {
            var rolDetallado = await _service.GetByIdDetallado(id);
            return rolDetallado is not null ? Ok(rolDetallado) : NotFound($"Rol no encontrado, datos incorrectos RolId {id}");
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolCreateDTO dto)
        {
            var created = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolUpdateDTO dto)
        {
            var updated = await _service.Update(id, dto);
            return updated ? Ok("Rol actualizado con éxito.") : NotFound($"No se pudo actualizar Rol, datos incorrectos RolId = {id} \nRolUpdateDTO = {dto}");
        }
        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisico(int id)
        {
            var deleted = await _service.DeleteFisico(id);
            return deleted ? Ok("Rol eliminado con exito.") : NotFound($"No se pudo eliminar rol, rol en uso RolId = {id}");
        }

    }
}