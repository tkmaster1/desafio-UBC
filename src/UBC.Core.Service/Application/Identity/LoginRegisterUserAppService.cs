using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Interfaces.Repositories.Identity;
using UBC.Core.Domain.Interfaces.Services.Identity;
using UBC.Core.Domain.Models;
using UBC.Core.Domain.Models.Identity;

namespace UBC.Core.Service.Application.Identity
{
    public class LoginRegisterUserAppService : ILoginRegisterUserAppService
    {
        #region Properties

        private readonly IUserRepository _userIdentityRepository;
        private UserManager<UserEntity> _userManager;
        private SignInManager<UserEntity> _signInManager;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public LoginRegisterUserAppService(IUserRepository userIdentityRepository,
                                      IMapper mapper,
                                      UserManager<UserEntity> userManager,
                                      SignInManager<UserEntity> signInManager)
        {
            _userIdentityRepository = userIdentityRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        #region Methods Public

        public async Task<IdentityResult> RegisterUserIdentity(UserEntity entity)
        {
            var verifyEmail = await _userIdentityRepository.FindByEmailAsync(entity.Email);

            if (verifyEmail != null)
                throw new ValidationException("Conta já existente!");

            IdentityResult resultado = await _userManager.CreateAsync(entity, entity.PasswordHash);

            await _signInManager.SignInAsync(entity, false);

            return resultado;
        }

        public async Task<LoginUserResponse> GerarJwt(string email, AuthorizationSettings authorizationSettings)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims, authorizationSettings);

            return ObterRespostaToken(encodedToken, user, claims, authorizationSettings);
        }

        public async Task<SignInResult> PasswordSignIn(string userName, LoginUser loginUser)
            => await _signInManager.PasswordSignInAsync(userName, loginUser.Password, true, lockoutOnFailure: false);

        public async Task<SignInResult> PasswordSignIn(UserEntity userEntity, LoginUser loginUser)
            => await _signInManager.PasswordSignInAsync(userEntity, loginUser.Password, true, lockoutOnFailure: false);

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion

        #region Methods Private

        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, UserEntity user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CodificarToken(ClaimsIdentity identityClaims, AuthorizationSettings authorizationSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authorizationSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = authorizationSettings.Issuer,
                Audience = authorizationSettings.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddDays(authorizationSettings.ExpirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //HmacSha512Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private LoginUserResponse ObterRespostaToken(string encodedToken, UserEntity user, IEnumerable<Claim> claims, AuthorizationSettings authorizationSettings)
        {
            return new LoginUserResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromDays(authorizationSettings.ExpirationDays).Days,
                UserToken = new UserTokenResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Claims = claims.Select(c => new UserClaimResponse { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
           => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        #endregion
    }
}
