using Microsoft.AspNetCore.Mvc;
using UBC.Core.Domain.Interfaces.Notifications;
using UBC.Core.Domain.Interfaces.Services.Identity;
using UBC.Core.Domain.Notifications;
using UBC.Core.WebApi.Models.Responses;

namespace UBC.Core.WebApi.Controllers
{
    public abstract class MainAPIController : ControllerBase
    {
        #region Properties

        protected ICollection<string> Erros = new List<string>();

        private readonly INotificador _notificador;

        public readonly IUserAppService _appUser;

        protected Guid UsuarioId { get; set; }

        protected bool UsuarioAutenticado { get; set; }

        #endregion

        #region Constructor

        protected MainAPIController(INotificador notificador,
                                    IUserAppService appUser)
        {
            _notificador = notificador;
            _appUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        #endregion

        #region Methods Protecteds

        protected ActionResult CustomResponse(object result = null, bool? error = false, string message = null)
        {
            if (error != null && error == true)
            {
                var objErros = new Erros
                {
                    Key = "Error",
                    Value = message
                };

                return NotFound(new ResponseFailed
                {
                    Success = false,
                    Errors = Erros.Append(objErros.Value.ToString())
                });
            }

            if (ValidatedOperation())
            {
                return Ok(new ResponseSuccess<object>
                {
                    Success = true,
                    Data = result
                });
            }

            return Conflict(new ResponseFailed
            {
                Success = false,
                Errors = (IEnumerable<string>)_notificador.GetNotifications()
            });
        }

        protected bool ResponseHasErrors(ResponseResult resposta)
        {
            if (resposta == null || !resposta.Errors.Mensagens.Any()) return false;

            foreach (var mensagem in resposta.Errors.Mensagens)
            {
                AddErrorProcessing(mensagem);
            }

            return true;
        }

        protected bool ValidatedOperation()
        {
            return !_notificador.HasNotification();
        }

        protected void AddErrorProcessing(string erro) => Erros.Add(erro);

        protected void ClearErrorsProcessing() => Erros.Clear();

        #endregion
    }
}
