using FirstApiSQ011.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace FirstApiSQ011.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceReturnType<IdentityResult>> AddUserAsync(User user, string password);
        public Task<User> GetUserAsync(string id);
        public Task<bool> AlreadyExistsAsync(string email);

        public Task<IdentityResult> AddRoleAsync(User user, string roleName);
    }
}
