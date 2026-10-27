using Microsoft.EntityFrameworkCore;
using SakuraApi.Data;
using SakuraApi.DTOs;
using SakuraApi.Models;

namespace SakuraApi.Services;

public class NovedadService : INovedadService
{
    private readonly AppDbContext _context;

    public NovedadService(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<NovedadResponseDto> ActualizarNovedad(int id, NovedadRequestDto novedad)
    {
        var existe = await _context.Novedades.FirstOrDefaultAsync(n => n.Id == id);
        if (existe is null)
            return null;

        AplicarDatos(existe, novedad);
        await _context.SaveChangesAsync();
        return CrearRespuesta(existe);
    }

    public async Task<NovedadResponseDto> BuscarNovedadPorIdAsync(int id)
    {
        var existe = await _context.Novedades.FirstOrDefaultAsync(n => n.Id == id);
        if (existe is null)
            return null;

        return CrearRespuesta(existe);
    }

    public async Task<NovedadResponseDto> CrearNovedadAsync(NovedadRequestDto novedadRequest)
    {
        var novedad = new Novedad();
        AplicarDatos(novedad, novedadRequest);
        _context.Novedades.Add(novedad);
        await _context.SaveChangesAsync();
        return await this.BuscarNovedadPorIdAsync(novedad.Id);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var existe = await _context.Novedades.FindAsync(id);
        if (existe is null)
            return false;

        _context.Novedades.Remove(existe);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<NovedadResponseDto>> ObtenerTodosAsync()
    {
        return await _context.Novedades
            .Select(n => new NovedadResponseDto()
            {
                Id = n.Id,
                Titulo = n.Titulo,
                Resumen = n.Resumen,
                Contenido = n.Contenido,
                ImagenUrl = n.ImagenUrl,
                EstaPublicada = n.EstaPublicada,
                CreadaEn = n.CreadaEn,
            })
            .ToListAsync();
    }

    private static void AplicarDatos(Novedad novedad, NovedadRequestDto solicitud)
    {
        novedad.Titulo = solicitud.Titulo;
        novedad.Resumen = solicitud.Resumen;
        novedad.Contenido = solicitud.Contenido;
        novedad.ImagenUrl = solicitud.ImagenUrl;
        novedad.EstaPublicada = solicitud.EstaPublicada;
    }

    private static NovedadResponseDto CrearRespuesta(Novedad novedad)
    {
        return new NovedadResponseDto()
        {
            Id = novedad.Id,
            Titulo = novedad.Titulo,
            Resumen = novedad.Resumen,
            Contenido = novedad.Contenido,
            ImagenUrl = novedad.ImagenUrl,
            EstaPublicada = novedad.EstaPublicada,
            CreadaEn = novedad.CreadaEn,
        };
    }
}
