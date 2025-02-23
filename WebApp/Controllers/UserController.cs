using Business.Interfaces;
using Business.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserRegistrationForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var createdUser = await _userService.CreateAsync(form);
            var result = createdUser != null ? Ok(createdUser) : Problem("User could not be created.");
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var allUsers = await _userService.GetAllAsync();
            if (allUsers != null)
                return Ok(allUsers);
            return NotFound("There are no users to view.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user != null)
                return Ok(user);
            return NotFound($"User with id {id} not found.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, UserUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                    return NotFound($"User with id {id} not found.");
                var updatedUser = await _userService.UpdateAsync(form);
                return updatedUser != null ? Ok(updatedUser) : Problem("User could not be updated.");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            return deleted ? Ok("User deleted") : NotFound($"User with id {id} not found.");
        }

        
        //public async Task<IActionResult> AuthenticateAsync([FromBody] UserLoginForm form)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Invalid login request.");

        //    var authenticatedUser = await _userService.AuthenticateAsync(form.Email, form.Password);
        //    if (authenticatedUser == null)
        //        return Unauthorized("Invalid email or password.");

        //    return Ok(authenticatedUser);
        //}
        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserLoginForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid login request.");

            var authenticatedUser = await _userService.AuthenticateAsync(form.Email, form.Password);
            if (authenticatedUser == null)
                return Unauthorized("Invalid email or password.");

            var token = JwtTokenGenerator.GenerateToken(authenticatedUser.Email, "User");
            return Ok(new { Token = token });
        }

        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePasswordAsync(int id, [FromBody] ChangePasswordForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid password change request.");

            var changed = await _userService.ChangePasswordAsync(id, form.CurrentPassword, form.NewPassword);
            if (!changed)
                return BadRequest("Password could not be changed.");

            return Ok("Password changed successfully.");
        }
    }
}
