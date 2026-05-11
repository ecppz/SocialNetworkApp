namespace Domain.Interfaces;

public interface IGenericRepository<Entity>
       where Entity : class
{
    Task<Entity> AddAsync(Entity entity);
    Task<List<Entity>> AddRangeAsync(List<Entity> entities);
    Task<Entity?> UpdateAsync(Guid id, Entity entity);
    Task DeleteAsync(Guid entity);
    Task<Entity?> GetByIdAsync(Guid id);
    Task<List<Entity>> GetAllAsync();
    IQueryable<Entity> GetAllQueryable();
    IQueryable<Entity> GetAllQueryWithInclude(List<string> properties);
}