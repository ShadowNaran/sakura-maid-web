using SakuraApi.DTOs;

namespace SakuraApi.Services;

public interface INovedadService
{
    Task<List<NovedadResponseDto>> ObtenerTodosAsync();
    Task<NovedadResponseDto> CrearNovedadAsync(NovedadRequestDto novedad);
    Task<NovedadResponseDto> ActualizarNovedad(int id, NovedadRequestDto novedad);
    Task<bool> EliminarAsync(int id);
    Task<NovedadResponseDto> BuscarNovedadPorIdAsync(int id);
}
