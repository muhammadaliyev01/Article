using Articles.Domain.Entites;

namespace Articles.DataAccess.Interfaces.Repository
{
    public interface IArticleRepository
    {
        Task<bool> CreateAsync(Article item);
        Task<bool> UpdateAsync(int id, Article item);

        Task<bool> DeleteAsync(int id);

        Task<Article?> GetByIdAsync(int id);

        Task<List<Article>> GetAllAsync();
    }
}
