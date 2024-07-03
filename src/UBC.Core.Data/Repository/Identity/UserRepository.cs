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

        //public async Task<int> CountByFilterAsync(UserIdentityFilter filter)
        //{
        //    var query = DbIdentityContext.TbUsers.AsQueryable();

        //    query = ApplyFilter(filter, query);

        //    return await query.CountAsync();
        //}

        //public async Task<List<UserIdentityEntity>> GetListByFilterAsync(UserIdentityFilter filter)
        //{
        //    var query = DbIdentityContext.TbUsers.AsQueryable();

        //    query = ApplyFilter(filter, query);

        //    query = ApplySorting(filter, query);

        //    if (filter.CurrentPage > 0)
        //        query = query.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize);

        //    return await query.ToListAsync();
        //}

        #endregion

        #region Methods Private

        //private static IQueryable<UserEntity> ApplySorting(UserIdentityFilter filter,
        //    IQueryable<UserEntity> query)
        //{
        //    query = filter?.OrderBy.ToLower()
        //        switch
        //    {
        //        "firstname" => filter.SortBy.ToLower() == "asc"
        //            ? query.OrderBy(x => x.UserName)
        //            : query.OrderByDescending(x => x.UserName),
        //    };

        //    return query;
        //}

        //private static IQueryable<UserEntity> ApplyFilter(UserIdentityFilter filter,
        //IQueryable<UserEntity> query)
        //{
        //    if (!string.IsNullOrEmpty(filter.CodeUser))
        //        query = query.Where(x => x.Id == filter.CodeUser);

        //    if (!string.IsNullOrWhiteSpace(filter.Name))
        //        query = query.Where(x => x.UserName.Trim().ToUpper().Contains(filter.Name.Trim().ToUpper()));

        //    return query;
        //}

        #endregion
    }
}
