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
            CreateMap<StudentEntity, StudentsDTO>().ReverseMap();
            CreateMap<StudentsFilterDTO, StudentFilter>();
            CreateMap<StudentsRequestDTO, StudentEntity>();

            CreateMap<Pagination<StudentEntity>, PaginationDTO<StudentsDTO>>()
            .AfterMap((source, converted, context) =>
            {
                converted.Result = context.Mapper.Map<List<StudentsDTO>>(source.Result);
            });
        }
    }
}
