using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoWebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> getAllUsersAsync();
        Task<User> getUserByIdAsync(int id);
        Task insertUserAsync(User user);
        Task<bool> updateUserAsync(int id, User user);
        Task<bool> deleteUserAsync(int id);
        Task<List<User>> getUsersByLastNameAsync(string lastName);
    }
}