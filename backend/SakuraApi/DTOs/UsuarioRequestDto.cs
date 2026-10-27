using System.ComponentModel.DataAnnotations;

namespace SakuraApi.DTOs;

public class UsuarioRequestDto
{
    [Required, StringLength(120, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(200)]
    public string Correo { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Rol { get; set; } = string.Empty;

    public bool Estado { get; set; } = true;
}
