using Entities.ErrorModel;
using Entities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Presentation.Controllers;

public class ApiControllerBase : ControllerBase
{
    public IActionResult ProcessError(ApiBaseResponse baseResponse)
    {
        if (baseResponse is ApiNotFoundResponse notFoundResponse)
        {
            return NotFound(new ErrorDetails
            {
                Message = notFoundResponse.Message,
                StatusCode = StatusCodes.Status404NotFound
            });
        }
        else if (baseResponse is ApiBadRequestResponse badRequestResponse)
        {
            return BadRequest(new ErrorDetails
            {
                Message = badRequestResponse.Message,
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        throw new NotImplementedException("Unknown response type.");
    }
}