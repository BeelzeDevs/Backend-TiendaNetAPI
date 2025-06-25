using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.Ingredientes.Services;
using TiendaNetApi.Ingredientes.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientesController : ControllerBase
    {
        private readonly IIngredienteService _service;

        public IngredientesController(IIngredienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ingredientes = await _service.GetAllAsync(); 

            return ingredientes is not null ? Ok(ingredientes) : NotFound("No se encontró ingredientes");
        }
        [HttpGet("actives")]
        public async Task<IActionResult> GetActives()
        {
            var ingredientesActivos = await _service.GetAllActivesAsync();

            return ingredientesActivos is not null ? Ok(ingredientesActivos) : NotFound("No se encontró Ingredientes activos");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var encontrado = await _service.GetByIdAsync(id);

            return encontrado is not null ? Ok(encontrado) : NotFound($"No se encontró el Ingrediente, datos incorrectos IdIngrediente = {id}");
        }
        
        [Authorize(Policy = "SoloAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IngredienteCreateDTO dto)
        {
            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id },created);
        }

        [Authorize(Policy = "SoloAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IngredienteUpdateDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? Ok("Ingrediente actualizado correctamente") : NotFound($"No se encontró el Ingrediente, datos incorrectos IdIngrediente ={id} \nIngredienteUpdateDTO = {dto}");
        }

        [Authorize(Policy = "SoloAdmin")]
        [HttpDelete("logico/{id}")]
        public async Task<IActionResult> DeleteLogico(int id)
        {
            var deleteLogico = await _service.DeleteLogicoAsync(id);
            return deleteLogico ? Ok("Ingrediente borrado lógicamente correctamente") : NotFound($"No se encontró el Ingrediente, datos incorrectos, IdIngrediente = {id}");
        }

        [Authorize(Policy = "SoloAdmin")]
        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisico(int id)
        {
            var deleteFisico = await _service.DeleteFisicoAsync(id);
            return deleteFisico ? Ok("Ingrediente eliminado físicamente correctamente") : NotFound($"Ingrediente en uso | IngredienteId = {id} no encontrado");
        }
    }

}