using AutoMapper;
using UBC.Core.Domain.Interfaces.Notifications;
using UBC.Core.Domain.Interfaces.Services.Identity;
using UBC.Core.Service.DTO.Identity;
using UBC.Core.Service.Facades.Interfaces.Identity;
using UBC.Core.Service.Services;

namespace UBC.Core.Service.Facades.Identity
{
    public class UserFacade : BaseService, IUserFacade
    {
        #region Properties

        private readonly IMapper _mapper;
        private readonly IUserAppService _userAppService;

        #endregion

        #region Constructor

        public UserFacade(IMapper mapper,
            IUserAppService userAppService,
            INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _userAppService = userAppService;
        }

        #endregion

        #region Methods Publics

        public async Task<UserDTO> GetUserByCode(string codeUser)
        {
            var result = await _userAppService.GetUserIdentityById(codeUser);

            return _mapper.Map<UserDTO>(result);
        }

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion
    }
}
