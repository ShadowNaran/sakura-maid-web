namespace SakuraApi.DTOs;

public class NovedadResponseDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Resumen { get; set; } = string.Empty;
    public string Contenido { get; set; } = string.Empty;
    public string? ImagenUrl { get; set; }
    public bool EstaPublicada { get; set; }
    public DateTimeOffset CreadaEn { get; set; }
}
