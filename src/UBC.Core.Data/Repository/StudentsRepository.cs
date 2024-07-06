using Microsoft.EntityFrameworkCore;
using UBC.Core.Data.Context;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Filters;
using UBC.Core.Domain.Interfaces.Repositories;

namespace UBC.Core.Data.Repository
{
    public class StudentsRepository : RepositoryBase<StudentEntity>, IStudentsRepository
    {
        #region Constructor

        public StudentsRepository(MeuContexto contexto) : base(contexto) { }

        #endregion

        #region Methods

        public async Task<int> CountByFilterAsync(StudentFilter filter)
        {
            var query = DbContext.Students.AsQueryable();

            query = ApplyFilter(filter, query);

            return await query.CountAsync();
        }

        public async Task<List<StudentEntity>> GetListByFilterAsync(StudentFilter filter)
        {
            var query = DbContext.Students.AsQueryable();

            query = ApplyFilter(filter, query);

            query = ApplySorting(filter, query);

            if (filter.CurrentPage > 0)
                query = query.Skip((filter.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize);

            return await query.ToListAsync();
        }

        #endregion

        #region Methods Private

        private static IQueryable<StudentEntity> ApplySorting(StudentFilter filter,
            IQueryable<StudentEntity> query)
        {
            query = filter?.OrderBy.ToLower()
                switch
            {
                "firstname" => filter.SortBy.ToLower() == "asc"
                    ? query.OrderBy(x => x.Name)
                    : query.OrderByDescending(x => x.Name),
            };

            return query;
        }

        private static IQueryable<StudentEntity> ApplyFilter(StudentFilter filter,
        IQueryable<StudentEntity> query)
        {
            if (filter.Code > 0)
                query = query.Where(x => x.Code == filter.Code);

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(x => EF.Functions.Like(x.Name, $"%{filter.Name}%"));

            return query;
        }

        #endregion
    }
}
