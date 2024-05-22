using Microsoft.EntityFrameworkCore;
using RegistrodeTecnicos.Components;
using RegistrodeTecnicos.Pages.DAL;
using RegistrodeTecnicos.Services;

namespace RegistrodeTecnicos;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // Setup connection string
        var ConStr = builder.Configuration.GetConnectionString("ConStr");
        builder.Services.AddDbContext<Contexto>(options => options.UseSqlite(ConStr));

        // Register services
        builder.Services.AddScoped<TecnicoService>();
        builder.Services.AddScoped<TiposTecnicoService>();
        builder.Services.AddScoped<IncentivosTecnicoService>();
        builder.Services.AddBlazorBootstrap();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}