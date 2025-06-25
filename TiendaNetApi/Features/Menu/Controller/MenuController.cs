using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.Menu.DTOs;
using TiendaNetApi.Menu.Services;
namespace TiendaNetApi.Menu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;
        public MenuController(IMenuService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var menus = await _service.GetAll();
            return menus is not null ? Ok(menus) : NotFound("No se encontraron menus.");
        }
        [HttpGet("actives")]
        public async Task<IActionResult> GetAllActives()
        {
            var menusActivos = await _service.GetAllActives();
            return menusActivos is not null ? Ok(menusActivos) : NotFound("No se encontraron menus activos.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var menu = await _service.GetById(id);
            return menu is not null ? Ok(menu) : NotFound($"No se encontró el Menu,datos incorrectos, IdMenu = {id}");
        }
        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> GetByIdDetallado(int id)
        {
            var menu = await _service.GetByIdDetallado(id);
            return menu is not null ? Ok(menu) : NotFound($"No se encontró el Menú,datos incorrectos, IdMenu = {id}");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuCreateDTO dto)
        {
            var created = await _service.Create(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MenuUpdateDTO dto)
        {
            var updated = await _service.Update(id, dto);
            return updated ? Ok("Menú actualizado con éxito.") : NotFound($"datos incorrectos, IdMenu = {id} \nMenuUpdateDTO = {dto}");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisico(int id)
        {
            var deleted = await _service.DeleteFisico(id);
            return deleted ? Ok("Menú eliminado físicamente con éxito.") : NotFound($"Menú en uso | datos incorrectos, IdMenu = {id}");
        }
        [Authorize(Policy = "SoloAdmin")]
        [HttpDelete("logico/{id}")]
        public async Task<IActionResult> DeleteLogico(int id)
        {
            var ok = await _service.DeleteLogico(id);
            return ok ? Ok("Menú eliminado lógicamente con éxito.") : NotFound($"No se encontro el Menú, datos incorrectos, IdMenu = {id}");
        }

    }
}