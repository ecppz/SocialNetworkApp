using Application;
using Infrastructure.Identity;
using Infrastructure.Identity.Contexts; // Asegura que EF encuentre tu IdentityContext
using Infrastructure.Persistence;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace ItlaSocialMedia
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(60);
                opt.Cookie.HttpOnly = true;
            });

            builder.Services.PersistenceLayerIoc(builder.Configuration);
            builder.Services.ApplicationLayerIoc();
            builder.Services.IdentityLayerIoc(builder.Configuration);
            builder.Services.SharedLayerIoc(builder.Configuration);
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            // =========================================================================
            // 🛠️ BLOQUE MÁGICO: UPDATE-DATABASE AUTOMÁTICO Y SEEDS EN LA NUBE
            // =========================================================================
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    Console.WriteLine("⏳ Aplicando 'Update-Database' en Railway...");

                    // 1. Aplica las tablas de Identity (Usuarios, Roles, etc.)
                    var identityContext = services.GetRequiredService<IdentityContext>();
                    await identityContext.Database.MigrateAsync();

                    // 2. Aplica las tablas de tu Red Social (Posts, Comentarios, etc.)
                    // NOTA: Si el contexto tiene otro nombre exacto en tu persistencia, cámbialo aquí
                    var persistenceContext = services.GetRequiredService<SocialMediaContextDB>();
                    await persistenceContext.Database.MigrateAsync();

                    Console.WriteLine("✅ ¡Tablas creadas/actualizadas con éxito en Railway!");

                    // 3. Ahora que las tablas SÍ existen, corremos el Seed de forma segura
                    Console.WriteLine("🌱 Sembrando datos iniciales (Seeds)...");
                    await app.Services.RunIdentitySeedAsync();
                    Console.WriteLine("✨ ¡Seeds aplicados con éxito!");
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "❌ Ocurrió un error aplicando las migraciones o los seeds en Railway.");
                }
            }
            // =========================================================================

            app.Run();
        }
    }
}