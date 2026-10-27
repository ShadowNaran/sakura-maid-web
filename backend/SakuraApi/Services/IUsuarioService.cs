using SakuraApi.DTOs;

namespace SakuraApi.Services;

public interface IUsuarioService
{
    Task<List<UsuarioResponseDto>> ObtenerTodosAsync();
    Task<UsuarioResponseDto> CrearUsuarioAsync(UsuarioRequestDto usuario);
    Task<UsuarioResponseDto> ActualizarUsuario(int id, UsuarioRequestDto usuario);
    Task<bool> EliminarAsync(int id);
    Task<UsuarioResponseDto> BuscarUsuarioPorIdAsync(int id);
}
