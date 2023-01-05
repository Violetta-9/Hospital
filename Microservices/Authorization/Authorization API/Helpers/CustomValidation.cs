using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Rest;
using Newtonsoft.Json;

namespace Authorization_API.Helpers
{
    public class CustomValidation<T> where T : Exception
    {
        public static ProblemDetails CustomerDetails(T ex)
        {
            if (ex.GetType() == typeof(HttpOperationException))
            {
                var httpEx =(HttpOperationException)(object)ex;
                try
                {
                    return JsonConvert.DeserializeObject<ProblemDetails>(httpEx.Response.Content);
                }
                catch (Exception e)
                {
                   return new ProblemDetails()
                    {
                        Type = ex.GetType().ToString(),
                        Detail = ErrorToString(e),
                        Status = (int)HttpStatusCode.BadRequest,

                    };

                }

            }
            return new ProblemDetails()
            {
                Type = ex.GetType().ToString(),
                Detail = ErrorToString((Exception)(object)ex),
                Status = (int)HttpStatusCode.BadRequest,

            };

        }


        private static string? ErrorToString(Exception ex)
        {
            
            if (ex.GetType() == typeof(FluentValidation.ValidationException))
            {
                FluentValidation.ValidationException validation=(FluentValidation.ValidationException)ex;

                return string.Join(";", validation.Errors.Select(x => x.ErrorMessage).ToArray());
            }
            return ex.Message;
        }
    }
}
