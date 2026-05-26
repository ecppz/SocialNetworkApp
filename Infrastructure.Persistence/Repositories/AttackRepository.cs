using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Persistence.Repositories;

namespace Infrastructure.Persistence.Repositories;

public class AttackRepository : GenericRepository<Attack>, IAttackRepository
{
    public AttackRepository(SocialMediaContextDB context) : base(context)
    {
    }
}