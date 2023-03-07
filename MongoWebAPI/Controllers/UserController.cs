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
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));;
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.getAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.getUserByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUserAsync(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                if (string.IsNullOrEmpty(user.FirstName))
                {
                    throw new ArgumentException("The FirstName field is required.", nameof(user.FirstName));
                }

                if (string.IsNullOrEmpty(user.LastName))
                {
                    throw new ArgumentException("The LastName field is required.", nameof(user.LastName));
                }

                await _userRepository.insertUserAsync(user);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                if (string.IsNullOrEmpty(user.FirstName))
                {
                    throw new ArgumentException("The FirstName field is required.", nameof(user.FirstName));
                }

                if (string.IsNullOrEmpty(user.LastName))
                {
                    throw new ArgumentException("The LastName field is required.", nameof(user.LastName));
                }

                var isUpdated = await _userRepository.updateUserAsync(id, user);
                if (isUpdated)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
                return BadRequest(ModelState);
            }
        }
        
        [HttpGet("lastname/{lastName}")]
        public async Task<ActionResult<List<User>>> GetUsersByLastNameAsync(string lastName)
        {
            try
            {
                if (string.IsNullOrEmpty(lastName))
                {
                    throw new ArgumentNullException(nameof(lastName));
                }

                var users = await _userRepository.getUsersByLastNameAsync(lastName);
                return Ok(users);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}



