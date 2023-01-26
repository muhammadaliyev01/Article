using Articles.Domain.Entites;

namespace Articles.DataAccess.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(User item);
        Task<bool> UpdateAsync(int id, User item);

        Task<bool> DeleteAsync(int id);

        Task<User?> GetByIdAsync(int id);

        Task<List<User>> GetAllAsync();
    }
}
