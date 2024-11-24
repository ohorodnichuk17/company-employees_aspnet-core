using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Entities.Responses;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

// [ApiVersion("2.0", Deprecated = true)]
[Route("api/companies")]
[ApiController]
[ApiExplorerSettings(GroupName = "v2")]
public class CompaniesV2Controller : ControllerBase
{
    private readonly IServiceManager _service;

    public CompaniesV2Controller(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        var baseResult = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);

        var companies = ((ApiOkResponse<IEnumerable<CompanyDto>>)baseResult).Result;

        var companiesV2 = companies.Select(x => $"{x.Name} V2");

        return Ok(companiesV2);
    }
}