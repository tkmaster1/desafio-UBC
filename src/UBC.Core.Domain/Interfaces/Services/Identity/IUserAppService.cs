using UBC.Core.Domain.Entities;

namespace UBC.Core.Domain.Interfaces.Services.Identity
{
    public interface IUserAppService : IDisposable
    {
        Task<UserEntity> FindByName(string userName);

        Task<UserEntity> GetUserIdentityById(string codeUser);
    }
}