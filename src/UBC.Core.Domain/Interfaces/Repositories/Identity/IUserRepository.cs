using UBC.Core.Domain.Entities;

namespace UBC.Core.Domain.Interfaces.Repositories.Identity
{
    public interface IUserRepository : IRepositoryBase<UserEntity>
    {
        Task<UserEntity> FindByEmailAsync(string email);

        Task<UserEntity> GetUserIdentityById(string codeUser);
    }
}
