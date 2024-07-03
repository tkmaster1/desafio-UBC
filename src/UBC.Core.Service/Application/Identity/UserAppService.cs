using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Interfaces.Repositories.Identity;
using UBC.Core.Domain.Interfaces.Services.Identity;
using UBC.Core.Domain.Models;

namespace UBC.Core.Service.Application.Identity
{
    public class UserAppService : IUserAppService
    {
        #region Properties

        private readonly IUserRepository _userRepository;
        private UserManager<UserEntity> _userManager;
        private SignInManager<UserEntity> _signInManager;

        #endregion

        #region Constructor

        public UserAppService(IUserRepository userRepository,
                                      UserManager<UserEntity> userManager,
                                      SignInManager<UserEntity> signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        #region Methods Public

        public async Task<UserEntity> FindByEmail(string email)
            => await _userRepository.FindByEmailAsync(email);

        public async Task<UserEntity> GetUserIdentityById(string codigoUser)
            => await _userRepository.GetUserIdentityById(codigoUser);

        //public async Task<Pagination<UserIdentityEntity>> GetListByFilter(UserIdentityFilter filter)
        //{
        //    if (filter == null)
        //        throw new ValidationException("Filtro é nulo.");

        //    if (filter.PageSize > 100)
        //        throw new ValidationException("O tamanho máximo de página permitido é 100.");

        //    if (filter.CurrentPage <= 0) filter.PageSize = 1;

        //    var total = await _userIdentityRepository.CountByFilterAsync(filter);

        //    if (total == 0) return new Pagination<UserIdentityEntity>();

        //    var paginateResult = await _userIdentityRepository.GetListByFilterAsync(filter);

        //    var result = new Pagination<UserIdentityEntity>
        //    {
        //        Count = total,
        //        CurrentPage = filter.CurrentPage,
        //        PageSize = filter.PageSize,
        //        Result = paginateResult.ToList()
        //    };

        //    return result;
        //}

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion
    }
}
