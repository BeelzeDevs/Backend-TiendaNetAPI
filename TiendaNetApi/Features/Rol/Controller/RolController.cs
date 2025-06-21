using TiendaNetApi.Rol.DTOs;
using TiendaNetApi.Rol.Services;
using TiendaNetApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var roles = await _service.GetAllAsync();
            if (roles is null) return NotFound();
            return Ok(roles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rol = await _service.GetById(id);
            return rol is not null ? Ok(rol) : NotFound();
        }
        [HttpGet("detalle/{id}")]
        public async Task<IActionResult> GetByIdDetallado(int id)
        {
            var rol = await _service.GetByIdDetallado(id);
            return rol is not null ? Ok(rol) : NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolCreateDTO dto)
        {
            var creado = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, creado);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolUpdateDTO dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            return ok ? Ok() : NotFound();
        }
        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisico(int id)
        {
            var deleted = await _service.DeleteFisicoAsync(id);
            return deleted ? Ok() : NotFound();
        }
        
    }
}