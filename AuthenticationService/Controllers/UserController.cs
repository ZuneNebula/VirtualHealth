using AuthenticationService.Models;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService; 
        }

        [HttpPost]
        [Route("/users/auth")]
        public async Task<IActionResult> Post(User user)
        {
            var returnuser = await userService.CreateAsync(user);

            return Created("created", returnuser);
        }

        [HttpGet]
        [Route("/users/auth")]
        public async Task<IActionResult> Get()
        {
            var users = await userService.GetAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("/users/auth/{email}")]
        public async Task<IActionResult> Get(String email)
        {
            var user = await userService.GetAsync(email);

            return Ok(user);
        }

        [HttpPut]
        [Route("/users/auth/{email}")]
        public async Task<IActionResult> Put(String email, User user)
        {
            var updateuser = await userService.UpdateAsync(email, user);

            return Ok(updateuser);
        }

        [HttpDelete]
        [Route("/users/auth/{email}")]
        public async Task<IActionResult> Delete(String email)
        {
            var result = await userService.DeleteAsync(email);

            return Ok(result);
        }
    }
}
