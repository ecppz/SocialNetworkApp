using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class ShipPositionRepository : GenericRepository<ShipPosition>, IShipPositionRepository
{
    public ShipPositionRepository(SocialMediaContextDB context) : base(context)
    {
    }
}