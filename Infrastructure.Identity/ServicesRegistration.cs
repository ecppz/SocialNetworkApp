using Application.Interfaces;
using Infrastructure.Identity.Contexts;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Seeds;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity
{
    public static class ServicesRegistration
    {
        public static void IdentityLayerIoc(this IServiceCollection services, IConfiguration config)
        {
            GeneralConfiguration(services, config);

           //Identity 
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;

                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            });

            services.AddIdentityCore<AccountUser>()
                .AddRoles<IdentityRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddTokenProvider<DataProtectorTokenProvider<AccountUser>>(TokenOptions.DefaultProvider);

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(12);
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                opt.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddCookie(IdentityConstants.ApplicationScheme, opt =>
            {
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(180);
                opt.LoginPath = "/Login";
            });

           // Services
               services.AddScoped<IAccountService, AccountUserService>();
        }


        public static async Task RunIdentitySeedAsync(this IServiceProvider service)
        {
            using var scope = service.CreateScope();
            var servicesProvider = scope.ServiceProvider;

            var userManager = servicesProvider.GetRequiredService<UserManager<AccountUser>>();
            var roleManager = servicesProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await DefaultRoles.SeedAsync(roleManager);
        }

        //private methods
        private static void GeneralConfiguration(IServiceCollection services, IConfiguration config)
        {
            //Contexts
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(opt =>
                                              opt.UseInMemoryDatabase("AppDb"));
            }
            else
            {
                var provider = config.GetValue<string>("DatabaseProvider");
                var sqlConnectionstring = config.GetConnectionString("SqlConnection");
                var postgreConnectionstring = config.GetConnectionString("PostgresConnection");

                services.AddDbContext<IdentityContext>(
                (serviceProvider, opt) =>
                {
                    opt.EnableSensitiveDataLogging();

                    switch (provider)
                    {
                        case "Postgres":
                            opt.UseNpgsql(postgreConnectionstring,
                                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                            break;

                        case "SqlServer":
                        default:
                            opt.UseSqlServer(sqlConnectionstring,
                                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                            break;
                    }
                },
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped
                );
            }
        }
    }
}

