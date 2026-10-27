using System.ComponentModel.DataAnnotations;

namespace SakuraApi.DTOs;

public class NovedadRequestDto
{
    [Required, StringLength(160, MinimumLength = 2)]
    public string Titulo { get; set; } = string.Empty;

    [Required, StringLength(500)]
    public string Resumen { get; set; } = string.Empty;

    [Required, StringLength(12000)]
    public string Contenido { get; set; } = string.Empty;

    [StringLength(500)]
    public string? ImagenUrl { get; set; }

    public bool EstaPublicada { get; set; }
}
