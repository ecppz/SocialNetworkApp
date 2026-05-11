using Application.Dtos.Attack;
using Application.Dtos.BattleshipGame;
using Application.Dtos.User;
using Domain.Common.Enums;

namespace Application.Interfaces;

public interface IBattleshipGameService : IGenericService<BattleshipGameDto>
{
    Task<Result<List<BattleshipGameDto>>> GetAllBattleshipGamesOfUser(Guid userId);
    Task<Result<List<UserDto>>> GetAllTheUsersAvailableForAGame(Guid userId, string? userNameFilter = null);
    Task<Result> SetStatus(Guid gameId, GameStatus status);
    Task<Result> ChangeTurn(Guid gameId);
    Task<Result<bool>> CheckIfGameEnded(Guid gameId);
    Task<Result> UpdateLastMove(Guid gameId);
    Task<Result> ThisUserGiveUp(Guid gameId, Guid userId);
    Task<Result<BattleshipGameSummaryDto>> GetSummaryOfThisUser(Guid userId);
}