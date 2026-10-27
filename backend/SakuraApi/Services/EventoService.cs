using Microsoft.EntityFrameworkCore;
using SakuraApi.Data;
using SakuraApi.DTOs;
using SakuraApi.Models;

namespace SakuraApi.Services;

public class EventoService : IEventoService
{
    private readonly AppDbContext _context;

    public EventoService(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<EventoResponseDto> ActualizarEvento(int id, EventoRequestDto evento)
    {
        var existe = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id);
        if (existe is null)
            return null;

        AplicarDatos(existe, evento);
        await _context.SaveChangesAsync();
        return CrearRespuesta(existe);
    }

    public async Task<EventoResponseDto> BuscarEventoPorIdAsync(int id)
    {
        var existe = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id);
        if (existe is null)
            return null;

        return CrearRespuesta(existe);
    }

    public async Task<EventoResponseDto> CrearEventoAsync(EventoRequestDto eventoRequest)
    {
        var evento = new Evento();
        AplicarDatos(evento, eventoRequest);
        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();
        return await this.BuscarEventoPorIdAsync(evento.Id);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var existe = await _context.Eventos.FindAsync(id);
        if (existe is null)
            return false;

        _context.Eventos.Remove(existe);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<EventoResponseDto>> ObtenerTodosAsync()
    {
        return await _context.Eventos
            .Select(e => new EventoResponseDto()
            {
                Id = e.Id,
                Titulo = e.Titulo,
                Descripcion = e.Descripcion,
                IniciaEn = e.IniciaEn,
                Ubicacion = e.Ubicacion,
                AficheUrl = e.AficheUrl,
                Estado = ObtenerEstado(e.Estado),
            })
            .ToListAsync();
    }

    private static void AplicarDatos(Evento evento, EventoRequestDto solicitud)
    {
        evento.Titulo = solicitud.Titulo;
        evento.Descripcion = solicitud.Descripcion;
        evento.IniciaEn = solicitud.IniciaEn;
        evento.Ubicacion = solicitud.Ubicacion;
        evento.AficheUrl = solicitud.AficheUrl;
        evento.Estado = ConvertirEstado(solicitud.Estado);
    }

    private static EstadoEvento ConvertirEstado(string estado)
    {
        return estado.ToLower() switch
        {
            "published" => EstadoEvento.Publicado,
            "publicado" => EstadoEvento.Publicado,
            "cancelled" => EstadoEvento.Cancelado,
            "cancelado" => EstadoEvento.Cancelado,
            _ => EstadoEvento.Publicado,
        };
    }

    private static string ObtenerEstado(EstadoEvento estado)
    {
        return estado switch
        {
            EstadoEvento.Publicado => "Published",
            EstadoEvento.Cancelado => "Cancelled",
            _ => "Published",
        };
    }

    private static EventoResponseDto CrearRespuesta(Evento evento)
    {
        return new EventoResponseDto()
        {
            Id = evento.Id,
            Titulo = evento.Titulo,
            Descripcion = evento.Descripcion,
            IniciaEn = evento.IniciaEn,
            Ubicacion = evento.Ubicacion,
            AficheUrl = evento.AficheUrl,
            Estado = ObtenerEstado(evento.Estado),
        };
    }
}
