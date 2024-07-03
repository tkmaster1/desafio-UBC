using AutoMapper;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Filters;
using UBC.Core.Domain.Models;
using UBC.Core.Service.DTO;
using UBC.Core.Service.DTO.Filters;
using UBC.Core.Service.DTO.Students;

namespace UBC.Core.Data.Mappers
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateStudentProfile();
        }

        private void CreateStudentProfile()
        {
            CreateMap<StudentEntity, StudentDTO>().ReverseMap();
            CreateMap<StudentFilterDTO, StudentFilter>();
            CreateMap<StudentRequestDTO, StudentEntity>();

            CreateMap<Pagination<StudentEntity>, PaginationDTO<StudentDTO>>()
            .AfterMap((source, converted, context) =>
            {
                converted.Result = context.Mapper.Map<List<StudentDTO>>(source.Result);
            });
        }
    }
}
