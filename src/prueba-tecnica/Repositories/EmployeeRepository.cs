using Dapper;
using prueba_tecnica.Data;
using prueba_tecnica.Models;

namespace prueba_tecnica.Repositories;

public class EmployeeRepository(IDbConnectionFactory connectionFactory) : IEmployeeRepository
{
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        const string sql = @"
            SELECT
                ID AS Id,
                FIRST_NAME AS FirstName,
                LAST_NAME AS LastName,
                EMAIL AS Email,
                SALARY AS Salary,
                DEPARTMENT AS Department,
                HIREDATE AS HireDate,
                IS_ACTIVE AS IsActive
            FROM TRA_EMPLOYEE
            ORDER BY ID";

        using var connection = connectionFactory.CreateConnection();
        return await connection.QueryAsync<Employee>(sql);
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        const string sql = @"
            SELECT
                ID AS Id,
                FIRST_NAME AS FirstName,
                LAST_NAME AS LastName,
                EMAIL AS Email,
                SALARY AS Salary,
                DEPARTMENT AS Department,
                HIREDATE AS HireDate,
                IS_ACTIVE AS IsActive
            FROM TRA_EMPLOYEE
            WHERE ID = @Id";

        using var connection = connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Employee>(sql, new { Id = id });
    }

    public async Task<int> CreateAsync(Employee employee)
    {
        const string sql = @"
            INSERT INTO TRA_EMPLOYEE (FIRST_NAME, LAST_NAME, EMAIL, SALARY, DEPARTMENT, HIREDATE, IS_ACTIVE)
            VALUES (@FirstName, @LastName, @Email, @Salary, @Department, @HireDate, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        using var connection = connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, employee);
    }

    public async Task<bool> UpdateAsync(int id, Employee employee)
    {
        const string sql = @"
            UPDATE TRA_EMPLOYEE
            SET FIRST_NAME = @FirstName,
                LAST_NAME = @LastName,
                EMAIL = @Email,
                SALARY = @Salary,
                DEPARTMENT = @Department,
                HIREDATE = @HireDate,
                IS_ACTIVE = @IsActive
            WHERE ID = @Id";

        using var connection = connectionFactory.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sql, new
        {
            Id = id,
            employee.FirstName,
            employee.LastName,
            employee.Email,
            employee.Salary,
            employee.Department,
            employee.HireDate,
            employee.IsActive
        });

        return affectedRows > 0;
    }
}
