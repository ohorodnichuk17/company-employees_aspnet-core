using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IEmployeeService
{
   Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
   Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);
   Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId,
       EmployeeForCreationDto employeeForCreation, bool trackChanges);
   Task DeleteEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);
   Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate,
       bool compTrackChanges, bool empTrackChanges);
   Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
       Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges);
   Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
}