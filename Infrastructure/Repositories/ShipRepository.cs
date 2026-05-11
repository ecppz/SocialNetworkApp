using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class ShipRepository : GenericRepository<Ship>, IShipRepository
{
    public ShipRepository(SocialMediaContextDB context) : base(context)
    {
    }
}