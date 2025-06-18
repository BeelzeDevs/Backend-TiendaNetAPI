using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Ingredientes.Services;
using TiendaNetApi.Ingredientes.DTOs;
using TiendaNetApi.Model;

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

        [HttpGet()]
        public async Task<IActionResult> GetIngredientes()
        {
            var ingredientes = await _service.GetAllAsync(); 

            return ingredientes is not null ? Ok(ingredientes) : NotFound();
        }
        [HttpGet("actives")]
        public async Task<IActionResult> GetIngredientesActives()
        {
            var ingredientes = await _service.GetAllActivesAsync();

            return ingredientes is not null ? Ok(ingredientes) : NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredienteById(int id)
        {
            var ingredientes = await _service.GetByIdAsync(id);

            return ingredientes is not null ? Ok(ingredientes) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngrediente([FromBody] IngredienteCreateDTO dto)
        {
            var creado = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetIngredienteById), new { id = creado.Id },creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngrediente(int id, [FromBody] IngredienteUpdateDTO dto)
        {
            var modificado = await _service.UpdateAsync(id, dto);
            return modificado ? Ok() : NotFound();
        }

        [HttpDelete("logico/{id}")]
        public async Task<IActionResult> DeleteLogicoIngrediente(int id)
        {
            var deleted = await _service.DeleteLogicoAsync(id);
            return deleted ? Ok() : NotFound();
        }

        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisicoIngrediente(int id)
        {
            var deleted = await _service.DeleteFisicoAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }

}