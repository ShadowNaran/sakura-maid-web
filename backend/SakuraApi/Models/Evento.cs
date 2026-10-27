namespace SakuraApi.Models;

public enum EstadoEvento
{
    Publicado,
    Cancelado
}

public sealed class Evento
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTimeOffset IniciaEn { get; set; }
    public string? Ubicacion { get; set; }
    public string? AficheUrl { get; set; }
    public EstadoEvento Estado { get; set; } = EstadoEvento.Publicado;
}
