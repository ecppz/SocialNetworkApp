using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Infrastructure.Persistence
{
    public static class ServicesRegistration
    {
        public static void PersistenceLayerIoc(this IServiceCollection services, IConfiguration config)
        {
            //Contexts
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<SocialMediaContextDB>(opt => opt.UseInMemoryDatabase("AppDb"));
            }
            else
            {
                var provider = config.GetValue<string>("DatabaseProvider");
                var sqlConnectionstring = config.GetConnectionString("SqlConnection");
                var postgreConnectionstring = config.GetConnectionString("PostgresConnection");

                services.AddDbContext<SocialMediaContextDB>((serviceProvider, opt) =>
                {
                    opt.EnableSensitiveDataLogging();

                    switch (provider)
                    {
                        case "Postgres":
                            opt.UseNpgsql(postgreConnectionstring,
                                m => m.MigrationsAssembly(typeof(SocialMediaContextDB).Assembly.FullName));
                            break;

                        case "SqlServer":
                        default:
                            opt.UseSqlServer(sqlConnectionstring,
                                m => m.MigrationsAssembly(typeof(SocialMediaContextDB).Assembly.FullName));
                            break;
                    }
                },
                contextLifetime: ServiceLifetime.Scoped,
                optionsLifetime: ServiceLifetime.Scoped
                );
            }

                //Repositories IOC
                services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                services.AddScoped<IPostRepository, PostRepository>();
                services.AddScoped<IReactionRepository, ReactionRepository>();
                services.AddScoped<ICommentRepository, CommentRepository>();
                services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
                services.AddScoped<IFriendRepository, FriendRepository>();
                //battleship

                services.AddScoped<IBattleshipGameRepository, BattleshipGameRepository>();
                services.AddScoped<IShipRepository, ShipRepository>();
                services.AddScoped<IShipPositionRepository, ShipPositionRepository>();
                services.AddScoped<IAttackRepository, AttackRepository>();
        }
    }
}
