using Avaliativa.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = false; // Evita erro de BD inexistente
});

// Adiciona os serviços ao container
builder.Services.AddControllersWithViews();

// Configuração do Entity Framework Core
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"), // <-- Forma correta
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }
    )
);

var app = builder.Build();

// Configuração do pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Configuração das rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Evento}/{action=Index}/{id?}"
);

// Inicializa o banco de dados com os dados padrão
SeedData.EnsurePopulated(app);

app.Run();
