using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoWebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task InsertUser(User user);
        Task<bool> UpdateUser(int id, User user);
        Task<bool> DeleteUser(int id);
        Task<List<User>> GetUsersByLastName(string lastName);
    }
}