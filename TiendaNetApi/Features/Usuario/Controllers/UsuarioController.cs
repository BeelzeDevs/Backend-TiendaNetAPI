using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Data;
using TiendaNetApi.Usuario.Services;
using TiendaNetApi.Usuario.DTOs;

namespace TiendaNetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuariosController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _service.GetAll();

            return usuarios is not null ? Ok(usuarios) : NotFound();
        }
        [HttpGet("actives")]
        public async Task<IActionResult> GetAllActives()
        {
            var usuariosActivos = await _service.GetAllActives();
            return Ok(usuariosActivos);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var encontrado = await _service.GetById(id);
            return encontrado is not null ? Ok(encontrado) : NotFound();
        }

        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> GetbyIdDetallado(int id)
        {
            var usuarioDetallado = await _service.GetByIdDetallado(id);
            return usuarioDetallado is not null ? Ok(usuarioDetallado) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCrearDTO dto)
        {
            var created = await _service.Create(dto);
            if (created is null) return NotFound();
            return CreatedAtAction(nameof(GetById), new { Id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioUpdateDTO dto)
        {
            var updated = await _service.Update(id, dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("logico/{id}")]
        public async Task<IActionResult> DeleteLogico(int id)
        {
            var deletedLogico = await _service.DeleteLogico(id);
            return deletedLogico ? Ok() : NotFound();
        }
        [HttpDelete("fisico/{id}")]
        public async Task<IActionResult> DeleteFisico(int id)
        {
            var deletedFisico = await _service.DeleteFisico(id);
            return deletedFisico ? Ok() : NotFound();
        }

    }
}