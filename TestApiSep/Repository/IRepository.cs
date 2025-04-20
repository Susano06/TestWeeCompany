using TestApiSep.DTOs;

namespace TestApiSep.Repository
{
    public interface IRepository<TEntity>
    {
        Task Add(RegistroInsertDto registro);
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetById(int id);
    }
}
