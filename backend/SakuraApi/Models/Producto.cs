namespace SakuraApi.Models;

public sealed class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal? Precio { get; set; }
    public int? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public string? ImagenUrl { get; set; }
    public bool EsDestacado { get; set; }
    public bool EstaDisponible { get; set; } = true;
}
