using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Filters;
using UBC.Core.Domain.Models;
using UBC.Core.Service.DTO;
using UBC.Core.Service.DTO.Filters;

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

            //CreateMap<StudentResultDTO, StudentResult>()
            //    .ForMember(o => o.ListSubMenuSystems, s => s.MapFrom(z => z.ListSubMenuSystems))
            //    .ReverseMap();

            CreateMap<Pagination<StudentEntity>, PaginationDTO<StudentDTO>>()
            .AfterMap((source, converted, context) =>
            {
                converted.Result = context.Mapper.Map<List<StudentDTO>>(source.Result);
            });
        }
    }
}
