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
        public async Task<List<User>> getAllUsersAsync()
        {
            return await _users.Find(user => true).ToListAsync();
        }
        //Get ID
        public async Task<User> getUserByIdAsync(int id)
        {
            return await _users.Find(Builders<User>.Filter.Eq(user => user.Id, id)).FirstOrDefaultAsync();
        }
        
        //Insert
        public async Task insertUserAsync(User user)
        {
            // Keep generating a new id until it's unique
            int id;
            do
            {
                var highestId = await _users.Find(Builders<User>.Filter.Empty)
                    .SortByDescending(user => user.Id)
                    .FirstOrDefaultAsync();
                id = highestId != null ? highestId.Id + 1 : 1;
            }
            while (await _users.Find(Builders<User>.Filter.Eq(user => user.Id, id)).AnyAsync());

            user.Id = id;
            await _users.InsertOneAsync(user);
        }

        //Update
        public async Task<bool> updateUserAsync(int id, User user)
        {
            var updateResult = await _users.ReplaceOneAsync(Builders<User>.Filter.Eq(user => user.Id, id), user);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        //Delete
        public async Task<bool> deleteUserAsync(int id)
        {
            var deleteResult = await _users.DeleteOneAsync(Builders<User>.Filter.Eq(user => user.Id, id));
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
        //LastName
        public async Task<List<User>> getUsersByLastNameAsync(string lastName)
        {
            //New method like you showed me...this right? xD
            return await _users.Find(Builders<User>.Filter.Eq(user => user.LastName, lastName)).ToListAsync();
        }
    }
}


