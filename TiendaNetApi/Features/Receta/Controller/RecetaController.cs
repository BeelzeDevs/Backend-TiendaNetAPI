using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.Receta.DTOs;
using TiendaNetApi.Receta.Services;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetasController : ControllerBase
    {
        private readonly RecetaService _service;
        public RecetasController(RecetaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recetas = await _service.GetAll();
            return recetas is not null ? Ok(recetas) : NotFound();
        }
        [HttpGet("actives")]
        public async Task<IActionResult> GetAllActives()
        {
            var recetasActivas = await _service.GetAllActives();
            return recetasActivas is not null ? Ok(recetasActivas) : NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var encontrado = await _service.GetById(id);
            return encontrado is not null ? Ok(encontrado) : NotFound();
        }
        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> GetByIdDetallado(int id)
        {
            var recetaDetallado = await _service.GetByIdConDetalles(id);
            return recetaDetallado is not null ? Ok(recetaDetallado) : NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecetaCreateDTO dto)
        {
            var created = await _service.Create(dto);
            if (created is null) return NotFound();

            return CreatedAtAction(nameof(GetById), new { Id = created.Id }, created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RecetaUpdateDTO dto)
        {
            var updated = await _service.Update(id, dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisico(int id)
        {
            var deletedFisico = await _service.DeleteFisico(id);
            return deletedFisico ? Ok() : NotFound();
        }
        [HttpDelete("logico/{id}")]
        public async Task<IActionResult> DeleteLogico(int id)
        {
            var deletedLogico = await _service.DeleteLogico(id);
            return deletedLogico ? Ok() : NotFound();
        }
    }
}