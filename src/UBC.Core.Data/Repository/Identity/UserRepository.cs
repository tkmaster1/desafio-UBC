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

        public async Task<UserEntity> FindByEmailAsync(string email)
        {
            return await DbIdentityContext.TbUsers
                                          .AsNoTracking()
                                          .Where(x => x.Email == email)
                                          .FirstOrDefaultAsync();
        }

        public async Task<UserEntity> GetUserIdentityById(string codigoUser)
        {
            return await DbIdentityContext.TbUsers
                                          .AsNoTracking()
                                          .Where(x => x.Id == codigoUser)
                                          .FirstOrDefaultAsync();
        }

        #endregion        
    }
}
