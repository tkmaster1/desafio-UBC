using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Interfaces.Notifications;
using UBC.Core.Domain.Interfaces.Services.Identity;
using UBC.Core.Domain.Models;
using UBC.Core.Domain.Models.Identity;
using UBC.Core.Domain.Models.Result;
using UBC.Core.Service.DTO.Identity;
using UBC.Core.Service.Facades.Interfaces.Identity;
using UBC.Core.Service.Services;

namespace UBC.Core.Service.Facades.Identity
{
    public class LoginRegisterUserFacade : BaseService, ILoginRegisterUserFacade
    {
        #region Properties

        private readonly IMapper _mapper;
        private readonly IUserAppService _userAppService;
        private readonly ILoginRegisterUserAppService _loginRegisterUserAppService;

        #endregion

        #region Constructor

        public LoginRegisterUserFacade(IMapper mapper,
            IUserAppService userAppService,
            ILoginRegisterUserAppService loginRegisterUserAppService,
            INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _userAppService = userAppService;
            _loginRegisterUserAppService = loginRegisterUserAppService;
        }

        #endregion

        #region Methods Publics

        public async Task<LoginUserResult> Login(LoginUserRequestDTO userRequestDto)
        {
            var user = await _userAppService.FindByEmail(userRequestDto.Email);

            if (user == null) throw new ValidationException("Usuário ou Senha incorretos");

            var login = _mapper.Map<LoginUser>(userRequestDto);

            var result = await _loginRegisterUserAppService.PasswordSignIn(user.UserName, login);

            return _mapper.Map<LoginUserResult>(result);
        }

        public async Task<LoginUserResponseDTO> GerarJwt(string email, AuthorizationSettings authorizationSettings)
        {
            var gerarJwtDomain = await _loginRegisterUserAppService.GerarJwt(email, authorizationSettings);

            var login = _mapper.Map<LoginUserResponseDTO>(gerarJwtDomain);

            return login;
        }

        public async Task<IdentityResult> RegisterUserIdentity(LoginUserRequestDTO loginUserRequest)
        {
            var user = GetUserFromRegisterUser(loginUserRequest);

            var usuario = _mapper.Map<UserEntity>(user);

            return await _loginRegisterUserAppService.RegisterUserIdentity(usuario);
        }

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion

        #region Methods Private

        private UserDTO GetUserFromRegisterUser(LoginUserRequestDTO registerUser)
        {
            return new UserDTO
            {
                CodeUser = Guid.NewGuid().ToString(),
                UserName = registerUser.Email,
                Password = registerUser.Password,
                Email = registerUser.Email,
            };
        }

        #endregion
    }
}