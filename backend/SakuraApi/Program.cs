using Microsoft.EntityFrameworkCore;
using SakuraApi.Data;
using SakuraApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendLocal", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var conexionLocal = builder.Configuration.GetConnectionString("SakuraConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conexionLocal));

builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<EventoService>();
builder.Services.AddScoped<NovedadService>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("FrontendLocal");

app.UseAuthorization();

app.MapControllers();

app.Run();
