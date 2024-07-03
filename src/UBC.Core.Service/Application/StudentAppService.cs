using System.ComponentModel.DataAnnotations;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Extensions;
using UBC.Core.Domain.Filters;
using UBC.Core.Domain.Interfaces.Repositories;
using UBC.Core.Domain.Interfaces.Services;
using UBC.Core.Domain.Models;

namespace UBC.Core.Service.Application
{
    public class StudentAppService : IStudentAppService
    {
        #region Properties

        private readonly IStudentRepository _studentRepository;

        #endregion

        #region Constructor

        public StudentAppService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        #endregion

        #region Methods

        public async Task<StudentEntity> GetByCode(int code)
         => await _studentRepository.GetByCodeAsync(code);

        public async Task<IEnumerable<StudentEntity>> ListAll()
              => await _studentRepository.ListAll();

        public async Task<Pagination<StudentEntity>> ListByFilters(StudentFilter filter)
        {
            if (filter == null)
                throw new ValidationException("Filtro é nulo.");

            if (filter.PageSize > 100)
                throw new ValidationException("O tamanho máximo de página permitido é 100.");

            if (filter.CurrentPage <= 0) filter.PageSize = 1;

            var total = await _studentRepository.CountByFilterAsync(filter);

            if (total == 0) return new Pagination<StudentEntity>();

            var paginateResult = await _studentRepository.GetListByFilterAsync(filter);

            var result = new Pagination<StudentEntity>
            {
                Count = total,
                CurrentPage = filter.CurrentPage,
                PageSize = filter.PageSize,
                Result = paginateResult.ToList()
            };

            return result;
        }

        public async Task<int> CreateStudent(StudentEntity studentEntity)
        {
            if (studentEntity == null)
                throw new ValidationException("O objeto StudentEntity está nulo.");

            Validate(studentEntity);

            _studentRepository.ToAdd(studentEntity);

            await _studentRepository.ToSaveChangesAsync();

            return studentEntity.Code;
        }

        public async Task<bool> UpdateStudent(StudentEntity studentEntity)
        {
            Validate(studentEntity, true);

            var model = await _studentRepository.GetByCodeAsync(studentEntity.Code);

            if (model != null)
            {
                model.Name = studentEntity.Name;
                model.Age = studentEntity.Age;
                model.Series = studentEntity.Series;
                model.AverageGrade = studentEntity.AverageGrade;
                model.Address = studentEntity.Address;
                model.FatherName = studentEntity.FatherName;
                model.MotherName = studentEntity.MotherName;
                model.DateBirth = studentEntity.DateBirth;

                _studentRepository.ToUpdate(model);
            }

            return await _studentRepository.ToSaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveStudent(int codigo)
        {
            var studentDomain = await _studentRepository.GetByCodeAsync(codigo);

            if (studentDomain == null)
                throw new ValidationException("O objeto StudentEntity está nulo.");

            _studentRepository.ToRemove(studentDomain);

            return await _studentRepository.ToSaveChangesAsync() > 0;
        }

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion

        #region Methods Private

        private void Validate(StudentEntity studentEntity, bool update = false)
        {
            studentEntity.ValidateName();

            studentEntity.ValidateAge();

            studentEntity.ValidateDateBirth();

            if (update)
                studentEntity.ValidateStudentId();
        }

        #endregion
    }
}
