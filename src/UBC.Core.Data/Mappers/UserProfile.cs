using AutoMapper;
using UBC.Core.Domain.Entities;
using UBC.Core.Service.DTO.Identity;

namespace UBC.Core.Data.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateUserProfile();
        }

        private void CreateUserProfile()
        {
            CreateMap<UserEntity, UserDTO>()
              .ForMember(o => o.CodeUser, s => s.MapFrom(z => z.Id))
              .ForMember(o => o.UserName, s => s.MapFrom(z => z.Email))
              .ForMember(o => o.Email, s => s.MapFrom(z => z.Email))
              .ForMember(o => o.Password, s => s.MapFrom(z => z.PasswordHash))
              .ReverseMap();
        }
    }
}
