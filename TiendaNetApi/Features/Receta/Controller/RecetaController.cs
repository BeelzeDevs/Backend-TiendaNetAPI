using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.Receta.DTOs;
using TiendaNetApi.Receta.Services;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetasController : ControllerBase
    {
        private readonly IRecetaService _service;
        public RecetasController(IRecetaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recetas = await _service.GetAll();
            return recetas is not null ? Ok(recetas) : NotFound("No se encontraron Recetas.");
        }
        [HttpGet("actives")]
        public async Task<IActionResult> GetAllActives()
        {
            var recetasActivas = await _service.GetAllActives();
            return recetasActivas is not null ? Ok(recetasActivas) : NotFound("No se encontraron Recetas activas.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var encontrado = await _service.GetById(id);
            return encontrado is not null ? Ok(encontrado) : NotFound($"No se encontró la receta, datos incorrectos IdReceta = {id}");
        }
        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> GetByIdDetallado(int id)
        {
            var recetaDetallado = await _service.GetByIdConDetalles(id);
            return recetaDetallado is not null ? Ok(recetaDetallado) : NotFound($"No se encontró la receta, datos incorrectos IdReceta = {id}");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RecetaCreateDTO dto)
        {
            var created = await _service.Create(dto);
            if (created is null) return NotFound();

            return CreatedAtAction(nameof(GetById), new { Id = created.Id }, created);
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RecetaUpdateDTO dto)
        {
            var updated = await _service.Update(id, dto);
            return updated ? Ok("Receta actualizada con éxito.") : NotFound($"No se encontro la receta, datos incorrectos idReceta = {id} \nRecetaUpdateDTO = {dto}");
        }

        [Authorize(Policy = "SoloAdmin")]
        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisico(int id)
        {
            var deletedFisico = await _service.DeleteFisico(id);
            return deletedFisico ? Ok("Receta eliminada Físicamente con éxito.") : NotFound($"Receta en uso | datos incorrectos IdReceta = {id}");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpDelete("logico/{id}")]
        public async Task<IActionResult> DeleteLogico(int id)
        {
            var deletedLogico = await _service.DeleteLogico(id);
            return deletedLogico ? Ok("Receta eliminada lógicamente con éxito.") : NotFound($"No se encontro la receta, datos incorrectos IdReceta = {id}");
        }
    }
}