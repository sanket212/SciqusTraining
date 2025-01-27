using Microsoft.AspNetCore.Mvc;
using Demo.ADO.Domain;
using Microsoft.Extensions.Configuration;

namespace Demo.ADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repository;

        // Use Dependency Injection to inject EmployeeRepository
        public EmployeeController(EmployeeRepository repository)
        {
            _repository = repository;
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest("Invalid input.");

            await _repository.AddEmployeeAsync(employee);
            return Ok("Employee added successfully.");
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _repository.GetEmployeesAsync(); 
            return Ok(employees); 
        }       
    }
}
