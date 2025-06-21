using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.Usuario.DTOs;
namespace TiendaNetApi.Usuario.Services
{
    public interface IUsuarioService
    {
        Task<List<UsuarioReadDTO>> GetAll();
        Task<List<UsuarioReadDTO>> GetAllActives();
        Task<UsuarioReadDTO?> GetById(int id);
        Task<UsuarioReadConDetallesDTO?> GetByIdDetallado(int id);
        Task<UsuarioReadConDetallesDTO> Create([FromBody] UsuarioCrearDTO dto);
        Task<bool> Update(int id,[FromBody] UsuarioUpdateDTO dto);
        Task<bool> DeleteLogico(int id);
        Task<bool> DeleteFisico(int id);
    }
}