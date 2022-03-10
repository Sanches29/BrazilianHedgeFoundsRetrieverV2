using BrazilianHedgeFoundsRetriever.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BrazilianHedgeFoundsRetriever.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult ResponseDefault(object data = null)
        {
            if (ModelState.IsValid)
                return Ok(new ApiResponse(true, data));
            else
                return BadRequest(new ApiResponse(false, GetErrorsModelState()));
        }

        private string[] GetErrorsModelState()
        {
            if (ModelState.IsValid)
                return null;
            else
                return ModelState.SelectMany(x => x.Value.Errors.Select(y => y.ErrorMessage)).ToArray();
        }
    }

    
}
