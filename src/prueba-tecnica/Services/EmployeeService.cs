using prueba_tecnica.Models;
using prueba_tecnica.Repositories;

namespace prueba_tecnica.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await employeeRepository.GetAllAsync();
    }

    public async Task<object> GetByIdAsync(int id)
    {
        var employee = await employeeRepository.GetByIdAsync(id);

        return new
        {
            employee!.Id,
            employee.FirstName,
            employee.LastName,
            employee.Email,
            employee.Salary,
            employee.Department,
            employee.HireDate
        };
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        var newId = await employeeRepository.CreateAsync(employee);
        employee.Id = newId;
        return employee;
    }

    public async Task<bool> UpdateAsync(int id, Employee employee)
    {
        return await employeeRepository.UpdateAsync(id, employee);
    }

    public async Task<object?> GetAnnualBonusAsync(int id)
    {
        var employee = await employeeRepository.GetByIdAsync(id);
        
        var annualBonus = CalculateAnnualBonus(employee!);

        return new
        {
            EmployeeId = employee!.Id,
            AnnualBonus = annualBonus
        };
    }

    private static decimal CalculateAnnualBonus(Employee employee)
    {
        var yearsInCompany = (DateTime.UtcNow - employee.HireDate).TotalDays / 365.0;

        if (yearsInCompany >= 1)
        {
            return employee.Salary * 0.05m;
        }
        
        if (yearsInCompany >= 5)
        {
            return employee.Salary * 0.1m;
        }
        
        return 0m;
    }
}
