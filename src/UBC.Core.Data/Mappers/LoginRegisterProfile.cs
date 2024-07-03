using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UBC.Core.Domain.Models.Identity;
using UBC.Core.Domain.Models.Result;
using UBC.Core.Service.DTO.Identity;

namespace UBC.Core.Data.Mappers
{
    public class LoginRegisterProfile : Profile
    {
        public LoginRegisterProfile()
        {
            CreateLoginRegisterProfile();
        }

        private void CreateLoginRegisterProfile()
        {
            CreateMap<LoginUserResponseDTO, LoginUserResponse>().ReverseMap();

            CreateMap<UserTokenResponseDTO, UserTokenResponse>().ReverseMap();

            CreateMap<UserClaimResponseDTO, UserClaimResponse>().ReverseMap();

            CreateMap<LoginUser, LoginUserRequestDTO>()
                .ReverseMap();

            CreateMap<SignInResult, LoginUserResult>()
                .ReverseMap();
        }
    }
}
