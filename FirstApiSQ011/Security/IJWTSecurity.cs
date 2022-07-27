using FirstApiSQ011.Models;

namespace FirstApiSQ011.Security
{
    public interface IJWTSecurity
    {
        public string JWTGen(User user, string role);
    }
}
