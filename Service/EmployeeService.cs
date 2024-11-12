using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public class EmployeeService : IEmployeeService
{
   private readonly IRepositoryManager _repository;
   private readonly ILoggerManager _logger;
   private readonly IMapper _mapper;

   public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
   {
      _repository = repository;
      _logger = logger;
      _mapper = mapper;
   }

   public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges)
   {
      await CheckIfCompanyExists(companyId, trackChanges);

      var employeesFromDb = await _repository.Employee.GetEmployeesAsync(companyId, trackChanges);
      var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

      return employeesDto;
   }

   public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
   {
      await CheckIfCompanyExists(companyId, trackChanges);

      var employeeFromDb = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, trackChanges);
      if (employeeFromDb is null)
         throw new EmployeeNotFoundException(employeeId);

      var employee = _mapper.Map<EmployeeDto>(employeeFromDb);

      return employee;
   }

   public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
   {
      await CheckIfCompanyExists(companyId, trackChanges);

      var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

      _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
      await _repository.SaveAsync();

      var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

      return employeeToReturn;
   }

   public async Task DeleteEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
   {
      await CheckIfCompanyExists(companyId, trackChanges);

      var employeeForCompany = await GetEmployeeForCompanyAndCheckIfItExists(companyId, employeeId, trackChanges);

      _repository.Employee.DeleteEmployee(employeeForCompany);
      await _repository.SaveAsync();
   }

   public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges,
      bool empTrackChanges)
   {
      await CheckIfCompanyExists(companyId, compTrackChanges);

      var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);
      
      _mapper.Map(employeeForUpdate, employeeEntity);
      await _repository.SaveAsync();
   }

   public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)>
      GetEmployeeForPatchAsync(Guid companyId, Guid id,
      bool compTrackChanges, bool empTrackChanges)
   {
      await CheckIfCompanyExists(companyId, compTrackChanges);

      var employeeEntity = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);

      var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

      return (employeeToPatch: employeeToPatch, employeeEntity: employeeEntity);
   }

   public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
   {
      _mapper.Map(employeeToPatch, employeeToPatch);
      await _repository.SaveAsync();
   }

   private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges)
   {
      var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
      if (company is null)
         throw new CompanyNotFoundException(companyId);
   }

   private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId, Guid id, bool trackChanges)
   {
      var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
      if(employee is null)
         throw new EmployeeNotFoundException(id);
      return employee;
   }
}