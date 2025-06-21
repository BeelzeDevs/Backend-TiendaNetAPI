using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.Ingredientes.Services;
using TiendaNetApi.Ingredientes.DTOs;

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

            return ingredientes is not null ? Ok(ingredientes) : NotFound();
        }
        [HttpGet("actives")]
        public async Task<IActionResult> GetActives()
        {
            var ingredientes = await _service.GetAllActivesAsync();

            return ingredientes is not null ? Ok(ingredientes) : NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ingredientes = await _service.GetByIdAsync(id);

            return ingredientes is not null ? Ok(ingredientes) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IngredienteCreateDTO dto)
        {
            var creado = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = creado.Id },creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IngredienteUpdateDTO dto)
        {
            var modificado = await _service.UpdateAsync(id, dto);
            return modificado ? Ok() : NotFound();
        }

        [HttpDelete("logico/{id}")]
        public async Task<IActionResult> DeleteLogico(int id)
        {
            var deleted = await _service.DeleteLogicoAsync(id);
            return deleted ? Ok() : NotFound();
        }

        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisico(int id)
        {
            var deleted = await _service.DeleteFisicoAsync(id);
            return deleted ? Ok() : NotFound();
        }
    }

}