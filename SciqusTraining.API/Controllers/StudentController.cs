using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SciqusTraining.API.Controllers
{
    //https://localhost:portnumber/api/student
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: https://localhost:portnumber/api/student
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentnames = new string[] { "Rahul", "Atharv", "Sudarshan", "Aniket", "Om", "Abhishek" };
            return Ok(studentnames); 
        }
    }
}
