using prueba_tecnica.Models;

namespace prueba_tecnica.Services;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<object> GetByIdAsync(int id);
    Task<Employee> CreateAsync(Employee employee);
    Task<bool> UpdateAsync(int id, Employee employee);
    Task<object?> GetAnnualBonusAsync(int id);
}
