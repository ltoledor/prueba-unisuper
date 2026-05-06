using prueba_tecnica.Models;

namespace prueba_tecnica.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<int> CreateAsync(Employee employee);
    Task<bool> UpdateAsync(int id, Employee employee);
}
