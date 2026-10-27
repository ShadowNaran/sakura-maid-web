using Microsoft.EntityFrameworkCore;
using SakuraApi.Data;
using SakuraApi.DTOs;
using SakuraApi.Models;

namespace SakuraApi.Services;

public class ProductoService : IProductoService
{
    private readonly AppDbContext _context;

    public ProductoService(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<ProductoResponseDto> ActualizarProducto(int id, ProductoRequestDto producto)
    {
        var existe = await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existe is null)
            return null;

        existe.Nombre = producto.Nombre;
        existe.Descripcion = producto.Descripcion;
        existe.Precio = producto.Precio;
        existe.CategoriaId = producto.CategoriaId;
        existe.ImagenUrl = producto.ImagenUrl;
        existe.EsDestacado = producto.EsDestacado;
        existe.EstaDisponible = producto.EstaDisponible;

        await _context.SaveChangesAsync();
        return await this.BuscarProductoPorIdAsync(existe.Id);
    }

    public async Task<ProductoResponseDto> BuscarProductoPorIdAsync(int id)
    {
        var existe = await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existe is null)
            return null;

        return CrearRespuesta(existe);
    }

    public async Task<ProductoResponseDto> CrearProductoAsync(ProductoRequestDto productoRequest)
    {
        var producto = new Producto()
        {
            Nombre = productoRequest.Nombre,
            Descripcion = productoRequest.Descripcion,
            Precio = productoRequest.Precio,
            CategoriaId = productoRequest.CategoriaId,
            ImagenUrl = productoRequest.ImagenUrl,
            EsDestacado = productoRequest.EsDestacado,
            EstaDisponible = productoRequest.EstaDisponible,
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
        return await this.BuscarProductoPorIdAsync(producto.Id);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var existe = await _context.Productos.FindAsync(id);
        if (existe is null)
            return false;

        _context.Productos.Remove(existe);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<ProductoResponseDto>> ObtenerTodosAsync()
    {
        return await _context.Productos
            .Include(p => p.Categoria)
            .Select(p => new ProductoResponseDto()
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                CategoriaId = p.CategoriaId,
                NombreCategoria = p.Categoria != null ? p.Categoria.Nombre : null,
                ImagenUrl = p.ImagenUrl,
                EsDestacado = p.EsDestacado,
                EstaDisponible = p.EstaDisponible,
            })
            .ToListAsync();
    }

    private static ProductoResponseDto CrearRespuesta(Producto producto)
    {
        return new ProductoResponseDto()
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            CategoriaId = producto.CategoriaId,
            NombreCategoria = producto.Categoria?.Nombre,
            ImagenUrl = producto.ImagenUrl,
            EsDestacado = producto.EsDestacado,
            EstaDisponible = producto.EstaDisponible,
        };
    }
}
