using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.UnidadMedida.DTOs;
using TiendaNetApi.UnidadMedida.Services;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly IUnidadMedidaService _service;
        public UnidadMedidaController(IUnidadMedidaService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var unidades = await _service.GetAllAsync();
            return Ok(unidades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var unidad = await _service.GetByIdAsync(id);
            return unidad is null ? NotFound() : Ok(unidad);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UnidadMedidaCreateDTO dto)
        {
            var creada = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UnidadMedidaUpdateDTO dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            return ok ? Ok() : NotFound();
        }

        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteFisicoAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }    
}