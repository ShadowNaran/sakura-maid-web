using Microsoft.AspNetCore.Mvc;
using SakuraApi.DTOs;
using SakuraApi.Services;

namespace SakuraApi.Controllers;

[ApiController]
[Route("api/v1/eventos")]
public class EventosController : ControllerBase
{
    private EventoService _eventoService;

    public EventosController(EventoService eventoService)
    {
        this._eventoService = eventoService;
    }

    [HttpGet]
    public async Task<List<EventoResponseDto>> ObtenerTodos()
    {
        return await _eventoService.ObtenerTodosAsync();
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> EliminarEvento(int id)
    {
        return await _eventoService.EliminarAsync(id);
    }

    [HttpPost]
    public async Task<EventoResponseDto> CrearEvento(EventoRequestDto evento)
    {
        return await _eventoService.CrearEventoAsync(evento);
    }

    [HttpPatch("{id:int}")]
    public async Task<EventoResponseDto> ActualizarEvento(int id, EventoRequestDto evento)
    {
        return await _eventoService.ActualizarEvento(id, evento);
    }

    [HttpGet("{id:int}")]
    public async Task<EventoResponseDto> BuscarEventoPorId(int id)
    {
        return await _eventoService.BuscarEventoPorIdAsync(id);
    }
}
