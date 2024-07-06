using Microsoft.AspNetCore.Identity;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Models;
using UBC.Core.Domain.Models.Identity;

namespace UBC.Core.Domain.Interfaces.Services.Identity
{
    public interface ILoginRegisterUserAppService : IDisposable
    {
        Task<LoginUserResponse> GerarJwt(string email, AuthorizationSettings authorizationSettings);

        Task<SignInResult> PasswordSignIn(string userName, LoginUser loginUser);

        Task<SignInResult> PasswordSignIn(UserEntity userEntity, LoginUser loginUser);

        Task<IdentityResult> RegisterUserIdentity(UserEntity userEntity);
    }
}