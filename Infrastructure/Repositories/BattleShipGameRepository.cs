using Domain.Common.Enums;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class BattleshipGameRepository : GenericRepository<BattleshipGame>, IBattleshipGameRepository
{
    public BattleshipGameRepository(SocialMediaContextDB context) : base(context)
    {
    }

    public async Task<bool> SetStatus(Guid gameId, GameStatus status)
    {
        var game = await context.Set<BattleshipGame>().FindAsync(gameId);
        if (game != null)
        {
            game.Status = status;
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> ChangeTurn(Guid gameId)
    {
        var game = await context.Set<BattleshipGame>().FindAsync(gameId);
        if (game != null)
        {
            if (game.CurrentTurnPlayerId == game.Player1Id)
            {
                game.CurrentTurnPlayerId = game.Player2Id;
            }
            else
            {
                game.CurrentTurnPlayerId = game.Player1Id;
            }
            await context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task UpdateLastMove(Guid gameId)
    {
        var game = await context.Set<BattleshipGame>().FindAsync(gameId);
        if (game != null)
        {
            game.LastMoveDate = DateTime.Now;
        }
    }
}