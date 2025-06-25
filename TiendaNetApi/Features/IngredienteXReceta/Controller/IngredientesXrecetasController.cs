using TiendaNetApi.IngredienteXReceta.DTOs;
using TiendaNetApi.IngredienteXReceta.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TiendaNetApi.IngredienteXReceta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientesxRecetaController : ControllerBase
    {
        private readonly IIngredientesXRecetaService _service;
        public IngredientesxRecetaController(IIngredientesXRecetaService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ingredientesxrecetas = await _service.GetAll();
            return ingredientesxrecetas is not null ? Ok(ingredientesxrecetas) : NotFound("No se encontraron Recetas con Ingredientes");
        }
        [HttpGet("receta/{id}/ingredientes")]
        public async Task<IActionResult> GetIngredientesByIdReceta(int id)
        {
            var ingredientesEnReceta = await _service.GetIngredientesByIdReceta(id);
            return ingredientesEnReceta is not null ? Ok(ingredientesEnReceta) : NotFound($"No se encontró la Receta,datos incorrectos, IdReceta = {id}");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpPost("receta/addIngrediente")]
        public async Task<IActionResult> AddIngredienteAReceta(IngredientesXRecetaCreateDTO dto)
        {
            var ixrReadDto = await _service.AddIngredienteAReceta(dto);
            return ixrReadDto is not null ? Ok(ixrReadDto) : NotFound($"No se pudo agregar el Ingrediente, datos incorrectos, IngredientesXRecetaCreateDTO = {dto} ");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpPost("receta/addLoteIngredientes")]
        public async Task<IActionResult> AddIngredientesARecetaPorBatch(IngredientesXRecetaBatchCreateDTO dto)
        {
            var addedConExito = await _service.AddIngredientesARecetaPorBatch(dto);
            return addedConExito ? Ok("Se agrego con éxito el lote de ingredientes.") : BadRequest("Verifique IDs, datos incorrectos");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpDelete("receta/removeIngrediente")]
        public async Task<IActionResult> RemoveIngredienteAReceta(IngredienteXRecetaDeleteDTO dto)
        {
            var deleted = await _service.RemoveIngredienteDeReceta(dto);
            return deleted ? Ok("Eliminado con éxito.") : NotFound($"No se pudo eliminar el Ingrediente, datos incorrectos, \nIngredienteXRecetaDeleteDTO = {dto}");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpDelete("receta/removeLoteIngredientes")]
        public async Task<IActionResult> RemoveIngredientesARecetaPorBatch(IngredientesXRecetaBatchRemoveDTO dto)
        {
            var deleted = await _service.RemoveIngredientesARecetaPorBatch(dto);
            return deleted ? Ok("Lote de Ingredientes eliminado con éxito.") : BadRequest($"Verifique IDs o datos incorrectos \nIngredientesXRecetaBatchRemoveDTO = {dto}");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpPut("receta/updateCantidad")]
        public async Task<IActionResult> UpdateCantidadIngrediente(IngredienteXRecetaUpdateDTO dto)
        {
            var updated = await _service.UpdateCantidadIngrediente(dto);
            return updated ? Ok("Cantidad actualizada con éxito") : NotFound($"Datos incorrectos, IngredienteXRecetaUpdateDTO = {dto}");
        }

    }
}