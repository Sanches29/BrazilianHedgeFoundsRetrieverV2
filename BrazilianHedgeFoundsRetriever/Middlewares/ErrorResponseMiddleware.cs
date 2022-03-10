using BrazilianHedgeFoundsRetriever.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace BrazilianHedgeFoundsRetriever.Middlewares
{
    public class ErrorResponseMiddleware
    {
        public ErrorResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public RequestDelegate _next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                ApiResponse apiResponse = new ApiResponse(false, new string[] {e.Message} );
                await context.Response.WriteAsJsonAsync(apiResponse);
            }
        }
    }
}
