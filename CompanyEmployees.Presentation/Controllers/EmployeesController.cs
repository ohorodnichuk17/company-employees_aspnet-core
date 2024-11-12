using ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
   private readonly IServiceManager _service;

   public EmployeesController(IServiceManager service) => _service = service;

   [HttpGet]
   public async Task<IActionResult> GetEmployeesAsync(Guid companyId)
   {
      var employees = await _service.EmployeeService.GetEmployeesAsync(companyId, trackChanges: false);

      return Ok(employees);
   }

   [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
   public async Task<IActionResult> GetEmployeeAsync(Guid companyId, Guid id)
   {
      var employee = await 
         _service.EmployeeService.GetEmployeesAsync(companyId, trackChanges: false);

      return Ok(employee);
   }

   [HttpPost]
   [ServiceFilter(typeof(ValidationFilterAttribute))]
   public async Task<IActionResult> CreateEmployeeForCompanyAsync(Guid companyId,
       [FromBody] EmployeeForCreationDto employee)
   {
      var employeeToReturn = await
          _service.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee,
              trackChanges: false);

      return CreatedAtRoute("GetEmployeeForCompany", new
      {
         companyId,
         id = employeeToReturn.Id
      }, employeeToReturn);
   }

   [HttpDelete("{employeeId:guid}")]
   public async Task<IActionResult> DeleteEmployeeAsync(Guid companyId, Guid employeeId)
   {
      await _service.EmployeeService.DeleteEmployeeAsync(companyId, employeeId, trackChanges: false);

      return NoContent();
   }

   [HttpPut("{id:guid}")]
   [ServiceFilter(typeof(ValidationFilterAttribute))]
   public async Task<IActionResult> UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)
   {
      await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, 
         employee, compTrackChanges: false, empTrackChanges: true);

      return NoContent();
   }

   [HttpPatch("{id:guid}")]
   public async Task<IActionResult> PartiallyUpdateEmployeeForCompanyAsync(Guid companyId, Guid id,
      [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
   {
      if (patchDoc is null)
         return BadRequest("patchDoc object sent from client is null.");
      
      var result = await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, id,
         compTrackChanges: false,
         empTrackChanges: true);
      
      patchDoc.ApplyTo(result.employeeToPatch, ModelState);

      TryValidateModel(result.employeeToPatch);
      
      if (!ModelState.IsValid)
         return UnprocessableEntity(ModelState);
      
      await _service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch,
         result.employeeEntity);
      return NoContent();
   }
}