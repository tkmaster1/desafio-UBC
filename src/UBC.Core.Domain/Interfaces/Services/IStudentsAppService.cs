using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Filters;
using UBC.Core.Domain.Models;

namespace UBC.Core.Domain.Interfaces.Services
{
    public interface IStudentsAppService : IDisposable
    {
        Task<StudentEntity> GetByCode(int code);

        Task<IEnumerable<StudentEntity>> ListAll();

        Task<Pagination<StudentEntity>> ListByFilters(StudentFilter filter);

        Task<int> CreateStudent(StudentEntity studentEntity);

        Task<bool> UpdateStudent(StudentEntity studentEntity);

        Task<bool> RemoveStudent(int code);
    }
}