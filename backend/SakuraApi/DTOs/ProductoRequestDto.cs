using System.ComponentModel.DataAnnotations;

namespace SakuraApi.DTOs;

public class ProductoRequestDto
{
    [Required, StringLength(120, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, StringLength(2000)]
    public string Descripcion { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "1000000")]
    public decimal? Precio { get; set; }

    [Range(1, int.MaxValue)]
    public int? CategoriaId { get; set; }

    [StringLength(500)]
    public string? ImagenUrl { get; set; }

    public bool EsDestacado { get; set; }

    public bool EstaDisponible { get; set; } = true;
}
