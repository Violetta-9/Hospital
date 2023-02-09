using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;
using System.Net;
using Newtonsoft.Json;
using ValidationException = FluentValidation.ValidationException;

namespace Appoitment.API.Helpers
{
    public class CustomValidation<T> where T : Exception
    {
        public static ProblemDetails CustomerDetails(T ex)
        {
            if (ex.GetType() == typeof(HttpOperationException))
            {
                var httpEx = (HttpOperationException)(object)ex;
                try
                {
                    return JsonConvert.DeserializeObject<ProblemDetails>(httpEx.Response.Content);
                }
                catch (Exception e)
                {
                    return new ProblemDetails
                    {
                        Type = ex.GetType().ToString(),
                        Detail = ErrorToString(e),
                        Status = (int)HttpStatusCode.BadRequest
                    };
                }
            }

            return new ProblemDetails
            {
                Type = ex.GetType().ToString(),
                Detail = ErrorToString(ex),
                Status = (int)HttpStatusCode.BadRequest
            };
        }


        private static string? ErrorToString(Exception ex)
        {
            if (ex.GetType() == typeof(ValidationException))
            {
                var validation = (ValidationException)ex;

                return string.Join(";", validation.Errors.Select(x => x.ErrorMessage).ToArray());
            }

            return ex.Message;
        }
    }
}
