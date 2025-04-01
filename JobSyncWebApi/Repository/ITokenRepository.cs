using Microsoft.AspNetCore.Identity;

namespace JobSyncWebApi.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser identityUser, List<string> roles);
    }
}
