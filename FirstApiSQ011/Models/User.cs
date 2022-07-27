using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FirstApiSQ011.Models

{
    public class User : IdentityUser
    {
        //public string Id { get; set; } = Guid.NewGuid().ToString();
        public string LastName { get; set; }
        public string FirstName { get; set; }
        //public string Email { get; set; }
        

        public IEnumerable<UserTask> Tasks { get; set; }

        public User()
        {
            Tasks = new List<UserTask>();
        }
    }
}
