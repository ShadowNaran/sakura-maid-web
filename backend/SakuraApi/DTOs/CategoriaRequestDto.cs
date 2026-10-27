using System.ComponentModel.DataAnnotations;

namespace SakuraApi.DTOs;

public class CategoriaRequestDto
{
    [Required]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "El nombre no tiene la longitud correcta")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "La descripción no puede superar los 300 caracteres")]
    public string? Descripcion { get; set; }
}
