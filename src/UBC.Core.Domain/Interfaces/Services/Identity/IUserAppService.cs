using Microsoft.AspNetCore.Identity;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Models;

namespace UBC.Core.Domain.Interfaces.Services.Identity
{
    public interface IUserAppService : IDisposable
    {
        Task<UserEntity> FindByEmail(string email);

        Task<UserEntity> GetUserIdentityById(string codeUser);

        //   Task<Pagination<UserEntity>> GetListByFilter(UserIdentityFilter filter);
    }
}
