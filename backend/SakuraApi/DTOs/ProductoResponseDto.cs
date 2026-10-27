namespace SakuraApi.DTOs;

public class ProductoResponseDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal? Precio { get; set; }
    public int? CategoriaId { get; set; }
    public string? NombreCategoria { get; set; }
    public string? ImagenUrl { get; set; }
    public bool EsDestacado { get; set; }
    public bool EstaDisponible { get; set; }
}
