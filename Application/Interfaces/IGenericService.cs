namespace Application.Interfaces
{
    public interface IGenericService<DtoModel>        
        where DtoModel : class
    {
        Task<Result<List<DtoModel>>> GetAllAsync();
        Task<Result<DtoModel>> GetByIdAsync(Guid id);
        Task<Result<DtoModel>> AddAsync(DtoModel dtoModel);
        Task<Result<List<DtoModel>>> AddRangeAsync(List<DtoModel> models);
        Task<Result<DtoModel>> UpdateAsync(Guid id, DtoModel model);
        Task<Result> DeleteAsync(Guid id);
    }
}