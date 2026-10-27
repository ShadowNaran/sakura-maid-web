using Microsoft.EntityFrameworkCore;
using SakuraApi.Data;
using SakuraApi.DTOs;
using SakuraApi.Models;

namespace SakuraApi.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<UsuarioResponseDto> ActualizarUsuario(int id, UsuarioRequestDto usuario)
    {
        var existe = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        if (existe is null)
            return null;

        AplicarDatos(existe, usuario);
        await _context.SaveChangesAsync();
        return CrearRespuesta(existe);
    }

    public async Task<UsuarioResponseDto> BuscarUsuarioPorIdAsync(int id)
    {
        var existe = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        if (existe is null)
            return null;

        return CrearRespuesta(existe);
    }

    public async Task<UsuarioResponseDto> CrearUsuarioAsync(UsuarioRequestDto usuarioRequest)
    {
        var usuario = new Usuario();
        AplicarDatos(usuario, usuarioRequest);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return await this.BuscarUsuarioPorIdAsync(usuario.Id);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var existe = await _context.Usuarios.FindAsync(id);
        if (existe is null)
            return false;

        _context.Usuarios.Remove(existe);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<UsuarioResponseDto>> ObtenerTodosAsync()
    {
        return await _context.Usuarios
            .Select(u => new UsuarioResponseDto()
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Correo = u.Correo,
                Rol = u.Rol,
                Estado = u.Estado,
                FechaRegistro = u.FechaRegistro,
            })
            .ToListAsync();
    }

    private static void AplicarDatos(Usuario usuario, UsuarioRequestDto solicitud)
    {
        usuario.Nombre = solicitud.Nombre;
        usuario.Correo = solicitud.Correo;
        usuario.Rol = solicitud.Rol;
        usuario.Estado = solicitud.Estado;
    }

    private static UsuarioResponseDto CrearRespuesta(Usuario usuario)
    {
        return new UsuarioResponseDto()
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Correo = usuario.Correo,
            Rol = usuario.Rol,
            Estado = usuario.Estado,
            FechaRegistro = usuario.FechaRegistro,
        };
    }
}
