using System.ComponentModel.DataAnnotations;

namespace FirstApiSQ011.DTOs
{
    public class UserDetailToReturnDto
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}
