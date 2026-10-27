using Microsoft.AspNetCore.Mvc;
using SakuraApi.DTOs;
using SakuraApi.Services;

namespace SakuraApi.Controllers;

[ApiController]
[Route("api/v1/usuarios")]
public class UsuariosController : ControllerBase
{
    private UsuarioService _usuarioService;

    public UsuariosController(UsuarioService usuarioService)
    {
        this._usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<List<UsuarioResponseDto>> ObtenerTodos()
    {
        return await _usuarioService.ObtenerTodosAsync();
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> EliminarUsuario(int id)
    {
        return await _usuarioService.EliminarAsync(id);
    }

    [HttpPost]
    public async Task<UsuarioResponseDto> CrearUsuario(UsuarioRequestDto usuario)
    {
        return await _usuarioService.CrearUsuarioAsync(usuario);
    }

    [HttpPatch("{id:int}")]
    public async Task<UsuarioResponseDto> ActualizarUsuario(int id, UsuarioRequestDto usuario)
    {
        return await _usuarioService.ActualizarUsuario(id, usuario);
    }

    [HttpGet("{id:int}")]
    public async Task<UsuarioResponseDto> BuscarUsuarioPorId(int id)
    {
        return await _usuarioService.BuscarUsuarioPorIdAsync(id);
    }
}
