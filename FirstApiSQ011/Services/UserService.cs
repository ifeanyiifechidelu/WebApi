using FirstApiSQ011.DTOs;
using FirstApiSQ011.Models;
using FirstApiSQ011.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using NPOI.SS.Formula.Functions;
using System;
using System.Threading.Tasks;

namespace FirstApiSQ011.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserAsync (string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<ServiceReturnType<IdentityResult>> AddUserAsync(User user, string password)
        {
            //Validate the entity is not a null object
            if(user == null)
                return new ServiceReturnType<IdentityResult> { 
                    Status = false, Message = "Object must not be null", Data = null, Error = null };

            var res = await _userManager.CreateAsync(user, password);

            if (!res.Succeeded)
            {
                return new ServiceReturnType<IdentityResult>
                {
                    Status = false,
                    Message = "Failed to create user",
                    Data = null,
                    Error = res
                };
            }

            return new ServiceReturnType<IdentityResult>
            {
                Status = true,
                Message = "Added successfully",
                Data = res,
                Error = null
            };
        }

        public async Task<bool> AlreadyExistsAsync(string email)
        {
            var res = await _userManager.FindByEmailAsync(email);
            if (res == null) 
                return false;

            return true;
        }

        public async Task<IdentityResult> AddRoleAsync(User user, string roleName)
        {
            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }

    public class ServiceReturnType<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public T Error { get; set; }
    }
}
