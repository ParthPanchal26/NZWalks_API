using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories.AuthRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
