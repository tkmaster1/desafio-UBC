using System.Security.Claims;

namespace UBC.Core.Domain.Interfaces.Services.Identity
{
    public interface IUserLoginAppService
    {
        string Name { get; }

        Guid GetUserId();

        string GetUserEmail();

        bool IsAuthenticated();

        bool IsInRole(string role);

        string ObterUserToken();

        string ObterUserRefreshToken();

        IEnumerable<Claim> GetClaimsIdentity();
    }
}
