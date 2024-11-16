using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CompanyEmployees.Presentation.ActionFilters;

public class ValidateMediaTypeAttribute : IActionFilter
{
    private readonly IList<string> _supportedMediaTypes = new List<string>
    {
        "application/json",
        "application/vnd.codemaze.hateoas+json"
    };

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey("Accept"))
        {
            context.Result = new BadRequestObjectResult("Accept header is missing.");
            return;
        }

        var mediaType = context.HttpContext.Request.Headers["Accept"].FirstOrDefault();

        if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? outMediaType) ||
            !_supportedMediaTypes.Contains(outMediaType.MediaType.ToLower()))
        {
            context.Result = new BadRequestObjectResult("Unsupported media type. " +
                                                        "Please use one of the following: " + string.Join(", ", _supportedMediaTypes));
            return;
        }

        context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
