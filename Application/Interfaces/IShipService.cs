using Application.Dtos.Ship;
using Domain.Common.Enums;

namespace Application.Interfaces;

public interface IShipService : IGenericService<ShipDto>
{
    Task<Result<List<ShipType>>> GetMissingShips(Guid userId, Guid gameId);
    Task<Result> CheckIfShipSunk(Guid shipId);
}