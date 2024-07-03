using FluentValidation;
using FluentValidation.Results;
using UBC.Core.Domain.Entities;
using UBC.Core.Domain.Interfaces.Notifications;
using UBC.Core.Domain.Notifications;

namespace UBC.Core.Service.Services
{
    public abstract class BaseService
    {
        #region Properties

        private readonly INotificador _notificador;

        #endregion

        #region Constructor

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        #endregion

        #region Methods

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notification(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }

        #endregion
    }
}
