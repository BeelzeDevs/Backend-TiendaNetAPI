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
            var rolEncontrado = await _service.GetById(id);
            return rolEncontrado is not null ? Ok(rolEncontrado) : NotFound();
        }
        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> GetByIdDetallado(int id)
        {
            var rolDetallado = await _service.GetByIdDetallado(id);
            return rolDetallado is not null ? Ok(rolDetallado) : NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolCreateDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolUpdateDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? Ok() : NotFound();
        }
        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisico(int id)
        {
            var deleted = await _service.DeleteFisicoAsync(id);
            return deleted ? Ok() : NotFound();
        }
        
    }
}