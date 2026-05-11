using Domain.Common.Enums;
using Domain.Entities;


namespace Domain.Interfaces;

public interface IBattleshipGameRepository : IGenericRepository<BattleshipGame>
{
    Task<bool> SetStatus(Guid gameId, GameStatus status);
    Task<bool> ChangeTurn(Guid gameId);
    Task UpdateLastMove(Guid gameId);
}