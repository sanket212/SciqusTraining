using Microsoft.AspNetCore.Identity;

namespace SciqusTraining.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
