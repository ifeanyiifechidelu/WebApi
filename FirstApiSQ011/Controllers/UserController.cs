using AutoMapper;
using FirstApiSQ011.DTOs;
using FirstApiSQ011.Models;
using FirstApiSQ011.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FirstApiSQ011.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //GET: /<controller>/
        [HttpPost("add")]
        public async Task<IActionResult> AddUser(UserToAddDto model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid model");
                return BadRequest(model);
            }

            //Check if the user email already exist
            if (await _userService.AlreadyExistsAsync(model.Email))
            {
                ModelState.AddModelError("", "Email already exists");
                return BadRequest(model);
            }

            //Manual mapping of dto to user model
            /*var user = new User
            {
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
            };*/
                
            //Auto mapping of dto to user model
            var user = _mapper.Map<User>(model);
            var UserName = model.Email;

            //create user
            var result = await _userService.AddUserAsync(user, model.Password);

            if (result.Status == false)
            {
                foreach (var err in result.Error.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }

                return BadRequest(result);
            }

            //Add role to user
            var roleResult = await _userService.AddRoleAsync(user, model.Role);
            if(!roleResult.Succeeded)
            {
                foreach (var err in roleResult.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(model);
            }
            return Ok($"Added! => Id: " + user.Id);
        }

        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Null entry for Id");

            var user = await _userService.GetUserAsync(id);

            if (user == null)
                return NotFound($"User with Id: {id} was not found");

            // Map user to user to return Dto
            var userToReturn = _mapper.Map<UserDetailToReturnDto>(user);

            return Ok(userToReturn);
        }

    }
}
