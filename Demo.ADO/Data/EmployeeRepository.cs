using MySqlConnector;
using Demo.ADO.Domain;
using System.Data;
using System.Text.Json;


public class EmployeeRepository
{
    private readonly string _connectionString;

    public EmployeeRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MySQLDBString");
    }

    // Asynchronous method to add an employee
    public async Task AddEmployeeAsync(Employee employee)
    {
        // Serialize the employee object to JSON
        string jsonData = JsonSerializer.Serialize(employee);

        using (var connection = new MySqlConnection(_connectionString))
        {
            using (var command = new MySqlCommand("AddEmployee", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Pass the JSON data to the stored procedure
                command.Parameters.AddWithValue("@jsonInput", jsonData);

                // Open the connection and execute the stored procedure
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }


    // Asynchronous method to get all employees
    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var employees = new List<Employee>();

        using (var connection = new MySqlConnection(_connectionString))
        {
            using (var command = new MySqlCommand("GetEmployees", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var employee = new Employee
                        {
                            Id = reader.GetInt32("Id"),
                            Name = reader.GetString("Name"),
                            Age = reader.GetInt32("Age"),
                            Position = reader.GetString("Position")
                        };
                        employees.Add(employee);
                    }
                }
            }
        }

        return employees;
    }
}
