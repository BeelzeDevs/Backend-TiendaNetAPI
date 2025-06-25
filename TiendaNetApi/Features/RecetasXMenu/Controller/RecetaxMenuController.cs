using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.RecetasXMenu.Services;

namespace TiendaNetApi.RecetasXMenu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetasXMenuController : ControllerBase
    {
        private readonly IRecetasXMenuService _service;

        public RecetasXMenuController(IRecetasXMenuService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var menusConRecetas = await _service.GetAll();
            return Ok(menusConRecetas);
        }

        [HttpGet("receta/{idReceta}/menu/{idMenu}")]
        public async Task<IActionResult> GetById(int idReceta, int idMenu)
        {
            var rxm = await _service.GetById(idReceta, idMenu);
            return rxm is not null ? Ok(rxm) : NotFound($"No se encontró la RecetaxMenu = MenuId :{idMenu}, RecetaId: {idReceta}");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpPost("receta/{idReceta}/menu/{idMenu}")]
        public async Task<IActionResult> Create(int idReceta, int idMenu)
        {
            var creado = await _service.Create(idReceta, idMenu);
            return creado ? Ok("RecetaxMenu creada correctamente.") : BadRequest($"No se pudo crear la RecetaxMenu. Verifique que ambos elementos existan o que la relación no exista ya.");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpDelete("receta/{idReceta}/menu/{idMenu}")]
        public async Task<IActionResult> DeleteFisico(int idReceta, int idMenu)
        {
            var eliminado = await _service.DeleteFisico(idReceta, idMenu);
            return eliminado ? Ok("RecetaxMenu eliminada correctamente.") : NotFound($"No se encontró la RecetaxMenu para eliminar.\n RecetaId = {idReceta}, MenuId = {idMenu}");
        }
    }
}
