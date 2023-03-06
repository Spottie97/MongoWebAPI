using Microsoft.AspNetCore.Mvc;
using MongoWebAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(User user)
        {
            await _userRepository.InsertUser(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            var isUpdated = await _userRepository.UpdateUser(id, user);
            if (isUpdated)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var isDeleted = await _userRepository.DeleteUser(id);
            if (isDeleted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("lastname/{lastName}")]
        public async Task<List<User>> GetUsersByLastName(string lastName)
        {
            return await _userRepository.GetUsersByLastName(lastName);
        }
    }
}

