using Microsoft.EntityFrameworkCore;
using SakuraApi.Data;
using SakuraApi.DTOs;
using SakuraApi.Models;

namespace SakuraApi.Services;

public class CategoriaService : ICategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        this._context = context;
    }

    public async Task<CategoriaResponseDto> ActualizarCategoria(int id, CategoriaRequestDto categoria)
    {
        var existe = await _context.Categorias
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (existe is null)
            return null;

        existe.Nombre = categoria.Nombre;
        existe.Descripcion = categoria.Descripcion;
        await _context.SaveChangesAsync();

        return CrearRespuesta(existe);
    }

    public async Task<CategoriaResponseDto> BuscarCategoriaPorIdAsync(int id)
    {
        var existe = await _context.Categorias
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (existe is null)
            return null;

        return CrearRespuesta(existe);
    }

    public async Task<CategoriaResponseDto> CrearCategoriaAsync(CategoriaRequestDto categoriaRequest)
    {
        var categoria = new Categoria()
        {
            Nombre = categoriaRequest.Nombre,
            Descripcion = categoriaRequest.Descripcion,
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return await this.BuscarCategoriaPorIdAsync(categoria.Id);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var existe = await _context.Categorias.FindAsync(id);
        if (existe is null)
            return false;

        _context.Categorias.Remove(existe);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<CategoriaResponseDto>> ObtenerTodosAsync()
    {
        return await _context.Categorias
            .Include(c => c.Productos)
            .Select(c => new CategoriaResponseDto()
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                CantidadProductos = c.Productos.Count,
            })
            .ToListAsync();
    }

    private static CategoriaResponseDto CrearRespuesta(Categoria categoria)
    {
        return new CategoriaResponseDto()
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            CantidadProductos = categoria.Productos.Count,
        };
    }
}
