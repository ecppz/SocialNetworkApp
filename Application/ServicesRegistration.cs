
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServicesRegistration
    {
        //Extension method - Decorator pattern
        public static void ApplicationLayerIoc(this IServiceCollection services)
        {
            //configurations
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //services IOC
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IReactionService, ReactionService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IFriendRequestService, FriendRequestService>();
            services.AddScoped<IFriendService, FriendService>();
            //battleship
            services.AddScoped<IBattleshipGameService, BattleshipGameService>();
            services.AddScoped<IShipService, ShipService>();
            services.AddScoped<IShipPositionService, ShipPositionService>();
            services.AddScoped<IAttackService, AttackService>();

        }

    }
}
