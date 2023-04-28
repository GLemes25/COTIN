using Common.Api.Logic.Business.Intefaces;
using Common.Api.Logic.Business.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.Api.Logic.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {

        private readonly INotification _notification;

        public BaseApiController(INotification notification)
        {
            _notification = notification;
        }

        protected bool isValid()
        {
            return !_notification.HasNotifications;
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (isValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notification.List.Select(n => n.ToString())
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificationModelisValid(modelState);
            return CustomResponse();
        }

        protected void NotificationModelisValid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificationError(errorMsg);
            }
        }

        protected void NotificationError(string mensagem)
        {
            _notification.Add(new Description(mensagem));
        }



    }

}

