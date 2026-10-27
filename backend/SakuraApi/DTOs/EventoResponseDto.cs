namespace SakuraApi.DTOs;

public class EventoResponseDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTimeOffset IniciaEn { get; set; }
    public string? Ubicacion { get; set; }
    public string? AficheUrl { get; set; }
    public string Estado { get; set; } = string.Empty;
}
