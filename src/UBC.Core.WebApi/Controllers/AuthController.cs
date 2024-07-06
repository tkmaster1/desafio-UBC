using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UBC.Core.Domain.Interfaces.Notifications;
using UBC.Core.Domain.Interfaces.Services.Identity;
using UBC.Core.Domain.Models;
using UBC.Core.Service.DTO.Identity;
using UBC.Core.Service.Facades.Interfaces.Identity;
using UBC.Core.WebApi.Models.Responses;

namespace UBC.Core.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : MainAPIController
    {
        #region Properties

        private readonly AuthorizationSettings _authorizationSettings;
        private readonly ILoginRegisterUserFacade _loginRegisterUserFacade;
        private readonly IUserFacade _userFacade;
        private readonly IUserLoginAppService _user;
        public IEnumerable<string> Errors { get; set; }

        #endregion

        #region Constructor

        public AuthController(IOptions<AuthorizationSettings> authorizationSettings,
                              ILoginRegisterUserFacade loginRegisterUserFacade,
                              IUserFacade userFacade,
                              INotificador notificador,
                              IUserLoginAppService user) : base(notificador, user)
        {
            _authorizationSettings = authorizationSettings.Value;
            _loginRegisterUserFacade = loginRegisterUserFacade;
            _userFacade = userFacade;
            _user = user;
        }

        #endregion

        #region Methods

        [HttpPost("login")]
        [Consumes("application/Json")]
        [Produces("application/Json")]
        [ProducesResponseType(typeof(ResponseBaseEntity), 200)]
        [ProducesResponseType(typeof(ResponseFailed), 400)]
        [ProducesResponseType(typeof(ResponseFailed), 401)]
        [ProducesResponseType(typeof(ResponseFailed), 403)]
        [ProducesResponseType(typeof(ResponseFailed), 409)]
        [ProducesResponseType(typeof(ResponseFailed), 500)]
        [ProducesResponseType(typeof(ResponseFailed), 502)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDTO loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var existe = await _userFacade.ExistsUserName(loginUser.UserName);

            if (!existe)
            {
                var resultRegister = await _loginRegisterUserFacade.RegisterUserIdentity(loginUser);

                if (resultRegister.Succeeded)
                {
                    var result = await _loginRegisterUserFacade.Login(loginUser);

                    if (result.Succeeded)
                        return CustomResponse(await _loginRegisterUserFacade.GerarJwt(loginUser.UserName, _authorizationSettings));

                    if (result.IsLockedOut) return CustomResponse(loginUser);
                }
            }
            else
            {
                var result = await _loginRegisterUserFacade.Login(loginUser);

                if (result.Succeeded)
                    return CustomResponse(await _loginRegisterUserFacade.GerarJwt(loginUser.UserName, _authorizationSettings));

                if (result.IsLockedOut) return CustomResponse(loginUser);
            }

            return CustomResponse(null, true, "Usuário ou Senha incorretos");
        }

        #endregion
    }
}