using Microsoft.AspNetCore.Mvc;
using SakuraApi.DTOs;
using SakuraApi.Services;

namespace SakuraApi.Controllers;

[ApiController]
[Route("api/v1/productos")]
public class ProductosController : ControllerBase
{
    private ProductoService _productoService;

    public ProductosController(ProductoService productoService)
    {
        this._productoService = productoService;
    }

    [HttpGet]
    public async Task<List<ProductoResponseDto>> ObtenerTodos()
    {
        return await _productoService.ObtenerTodosAsync();
    }

    [HttpDelete("{id:int}")]
    public async Task<bool> EliminarProducto(int id)
    {
        return await _productoService.EliminarAsync(id);
    }

    [HttpPost]
    public async Task<ProductoResponseDto> CrearProducto(ProductoRequestDto producto)
    {
        return await _productoService.CrearProductoAsync(producto);
    }

    [HttpPatch("{id:int}")] //Por URL
    public async Task<ProductoResponseDto> ActualizarProducto(int id, ProductoRequestDto producto)
    {
        return await _productoService.ActualizarProducto(id, producto);
    }

    [HttpGet("{id:int}")]
    public async Task<ProductoResponseDto> BuscarProductoPorId(int id)
    {
        return await _productoService.BuscarProductoPorIdAsync(id);
    }
}
