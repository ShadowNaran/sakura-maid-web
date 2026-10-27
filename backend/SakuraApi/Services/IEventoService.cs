using SakuraApi.DTOs;

namespace SakuraApi.Services;

public interface IEventoService
{
    Task<List<EventoResponseDto>> ObtenerTodosAsync();
    Task<EventoResponseDto> CrearEventoAsync(EventoRequestDto evento);
    Task<EventoResponseDto> ActualizarEvento(int id, EventoRequestDto evento);
    Task<bool> EliminarAsync(int id);
    Task<EventoResponseDto> BuscarEventoPorIdAsync(int id);
}
