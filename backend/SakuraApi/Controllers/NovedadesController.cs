using Microsoft.AspNetCore.Mvc;
using SakuraApi.DTOs;
using SakuraApi.Services;

namespace SakuraApi.Controllers;

[ApiController]
[Route("api/v1/novedades")]
public class NovedadesController : ControllerBase
{
    private NovedadService _novedadService;

    public NovedadesController(NovedadService novedadService)
    {
        this._novedadService = novedadService;
    }

    [HttpGet]
    public async Task<List<NovedadResponseDto>> ObtenerTodos()
    {
        return await _novedadService.ObtenerTodosAsync();
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> EliminarNovedad(int id)
    {
        return await _novedadService.EliminarAsync(id);
    }

    [HttpPost]
    public async Task<NovedadResponseDto> CrearNovedad(NovedadRequestDto novedad)
    {
        return await _novedadService.CrearNovedadAsync(novedad);
    }

    [HttpPatch("{id:int}")]
    public async Task<NovedadResponseDto> ActualizarNovedad(int id, NovedadRequestDto novedad)
    {
        return await _novedadService.ActualizarNovedad(id, novedad);
    }

    [HttpGet("{id:int}")]
    public async Task<NovedadResponseDto> BuscarNovedadPorId(int id)
    {
        return await _novedadService.BuscarNovedadPorIdAsync(id);
    }
}
