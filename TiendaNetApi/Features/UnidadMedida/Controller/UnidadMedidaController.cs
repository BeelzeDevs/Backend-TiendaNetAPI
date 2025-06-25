using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.UnidadMedida.DTOs;
using TiendaNetApi.UnidadMedida.Services;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "SoloAdmin")]
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
            return unidad is not null ? Ok(unidad) : NotFound($"Unidad de medida no encontrada, datos incorrectos UnidadMedidaId = {id}");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UnidadMedidaCreateDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UnidadMedidaUpdateDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? Ok("Unidad de medida actualizada con éxito.") : NotFound($"Unidad de medida no encontrada, datos incorrectos UnidadMedidaId = {id} \nUnindadMedidaDTO = {dto}");
        }

        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteFisicoAsync(id);
            return deleted ? Ok("Unidad de medida eliminada físicamente con éxito.") : NotFound($"Unidad de medida en uso \nUnidad de medida no encontrada UnidadMedidaId = {id}");
        }
    }    
}