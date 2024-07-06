using UBC.Core.Domain.Entities;

namespace UBC.Core.Domain.Interfaces.Services.Identity
{
    public interface IUserAppService : IDisposable
    {
        Task<UserEntity> FindByEmail(string email);

        Task<UserEntity> GetUserIdentityById(string codeUser);
    }
}