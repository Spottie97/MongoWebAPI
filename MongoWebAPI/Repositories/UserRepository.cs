using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IOptions<RepoOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);
            _users = database.GetCollection<User>(options.Value.CollectionName);
        }
        //Get All Users
        public async Task<List<User>> GetAllUsers()
        {
            return await _users.Find(user => true).ToListAsync();
        }
        //Get ID
        public async Task<User> GetUserById(int id)
        {
            return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task InsertUser(User user)
        {
            // Generate a unique Id for the new user
            var highestId = await _users.CountDocumentsAsync(Builders<User>.Filter.Empty);
            user.Id = highestId != 0 ? (int)(highestId + 1) : 1;

            await _users.InsertOneAsync(user);
        }
        //Update
        public async Task<bool> UpdateUser(int id, User user)
        {
            var updateResult = await _users.ReplaceOneAsync(user => user.Id == id, replacement: user);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        //Delete
        public async Task<bool> DeleteUser(int id)
        {
            var deleteResult = await _users.DeleteOneAsync(user => user.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
        //LastName
        public async Task<List<User>> GetUsersByLastName(string lastName)
        {
            //New method like you showed me...this right? xD
            return await _users.Find(Builders<User>.Filter.Eq(user => user.LastName, lastName)).ToListAsync();
        }
    }
    //Used to get connection info
    public class RepoOptions
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}

