using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EMSApi.Modals;
using Microsoft.AspNetCore.Authorization;
using EMSApi.Repository;
using EMSApi.Modals.dto;

namespace EMSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUsersRepository _usersRepo;

        //Dependency injection
        public UserController(IUsersRepository usersRepo)
        {
            _usersRepo = usersRepo;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Auth([FromBody] userLoginDTO model)
        {
            var user = _usersRepo.Authenticate(model.email, model.password);
            if (user == null)
            {
                return BadRequest(new
                {
                    message = "Invalid email id or password."
                });
            }
            return Ok(user);
        }

        //Get all users
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Users>>> getUsers()
        {
            var usersList=_usersRepo.getUserList();
            return Ok(usersList);
        }

        //Get single user by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> getUsers(int id)
        {
            var singleUser = _usersRepo.findUser(id); 
            if (singleUser == null)
            {
                return NotFound(new
                {
                    message="User not found."
                });
            }
            return Ok(singleUser);
        }

        //Create new user
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Users>>> postUsers([FromBody]Users user)
        {
           var addedUser= _usersRepo.Register(user); 
            if (addedUser == null)
            {
               return NotFound();
            }

            return Ok(addedUser);
            
        }

        //Delete user by id
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Users>>> deleteUser(int id)
        {
            var delUser=_usersRepo.DeleteUser(id); 
            if(delUser == null)
            {
                return NotFound(new
                {
                    message="User not found.",
                    userId=id

                });
            }
            return Ok(new
            {
                message="User deleted.",
                user=delUser
            });
        }
    }
}
