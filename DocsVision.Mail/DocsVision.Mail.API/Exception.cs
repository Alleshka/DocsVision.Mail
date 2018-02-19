using System;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http;

namespace DocsVision.Mail.API
{
    public class CustomExceptionAtribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext != null)
            {
                HttpResponseMessage response = null;
                String message = "";

                // Если необработанная ошибка
                if (response == null)
                {
                    // Дефолтные значения
                    response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                    message = actionExecutedContext.Exception.Message;
                    // Logger.Log.Instance.Error(actionExecutedContext.Exception.Message);
                }

                response.Content = new StringContent(message);
                throw new HttpResponseException(response);
            }
        }
    }
}