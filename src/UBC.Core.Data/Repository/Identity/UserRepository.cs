using Microsoft.EntityFrameworkCore;
using UBC.Core.Data.Context;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Interfaces.Repositories.Identity;

namespace UBC.Core.Data.Repository.Identity
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        #region Constructor

        public UserRepository(IdentityContext context) : base(context)
        {
        }

        #endregion

        #region Methods Public

        public async Task<UserEntity> FindByNameAsync(string userName)
        {
            return await DbIdentityContext.TbUsers
                                          .AsNoTracking()
                                          .Where(x => x.UserName == userName)
                                          .FirstOrDefaultAsync();
        }

        public async Task<UserEntity> GetUserIdentityById(string codeUser)
        {
            return await DbIdentityContext.TbUsers
                                          .AsNoTracking()
                                          .Where(x => x.Id == codeUser)
                                          .FirstOrDefaultAsync();
        }

        #endregion        
    }
}