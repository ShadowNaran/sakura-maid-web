using Microsoft.AspNetCore.Mvc;
using SakuraApi.DTOs;
using SakuraApi.Services;

namespace SakuraApi.Controllers;

[ApiController]
[Route("api/v1/categorias")]
public class CategoriasController : ControllerBase
{
    private CategoriaService _categoriaService;

    public CategoriasController(CategoriaService categoriaService)
    {
        this._categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<List<CategoriaResponseDto>> ObtenerTodos()
    {
        return await _categoriaService.ObtenerTodosAsync();
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> EliminarCategoria(int id)
    {
        return await _categoriaService.EliminarAsync(id);
    }

    [HttpPost]
    public async Task<CategoriaResponseDto> CrearCategoria(CategoriaRequestDto categoria)
    {
        return await _categoriaService.CrearCategoriaAsync(categoria);
    }

    [HttpPatch("{id:int}")]
    public async Task<CategoriaResponseDto> ActualizarCategoria(int id, CategoriaRequestDto categoria)
    {
        return await _categoriaService.ActualizarCategoria(id, categoria);
    }

    [HttpGet("{id:int}")]
    public async Task<CategoriaResponseDto> BuscarCategoriaPorId(int id)
    {
        return await _categoriaService.BuscarCategoriaPorIdAsync(id);
    }
}
