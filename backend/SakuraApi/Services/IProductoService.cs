using SakuraApi.DTOs;

namespace SakuraApi.Services;

public interface IProductoService
{
    Task<List<ProductoResponseDto>> ObtenerTodosAsync();
    Task<ProductoResponseDto> CrearProductoAsync(ProductoRequestDto producto);
    Task<ProductoResponseDto> ActualizarProducto(int id, ProductoRequestDto producto);
    Task<bool> EliminarAsync(int id);
    Task<ProductoResponseDto> BuscarProductoPorIdAsync(int id);
}
