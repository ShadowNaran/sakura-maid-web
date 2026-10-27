using System.ComponentModel.DataAnnotations;

namespace SakuraApi.DTOs;

public class EventoRequestDto
{
    [Required, StringLength(140, MinimumLength = 2)]
    public string Titulo { get; set; } = string.Empty;

    [Required, StringLength(4000)]
    public string Descripcion { get; set; } = string.Empty;

    public DateTimeOffset IniciaEn { get; set; }

    [StringLength(200)]
    public string? Ubicacion { get; set; }

    [StringLength(500)]
    public string? AficheUrl { get; set; }

    [Required]
    [RegularExpression("^(Published|Cancelled)$", ErrorMessage = "El estado debe ser Published o Cancelled")]
    public string Estado { get; set; } = "Published";
}
