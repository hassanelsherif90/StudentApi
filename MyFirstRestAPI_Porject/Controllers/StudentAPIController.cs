using Microsoft.AspNetCore.Mvc; 
using StudentApi.Models;
using StudentApi.DataSimulation;
using System.Collections.Generic; 

namespace StudentApi.Controllers 
{
    [ApiController] // Marks the class as a Web API controller with enhanced features.
   // [Route("[controller]")] // Sets the route for this controller to "students", based on the controller name.
    [Route("api/Students")]

    public class StudentsController : ControllerBase // Declare the controller class inheriting from ControllerBase.
    {
        
        [HttpGet("All", Name = "GetAllStudents")] // Marks this method to respond to HTTP GET requests.

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]   
        public ActionResult<IEnumerable<Student>> GetAllStudents() // Define a method to get all students.
        {
            if(StudentDataSimulation.StudentsList.Count == 0)
            {
                return NotFound("Not Found Students !");
            }
            return Ok(StudentDataSimulation.StudentsList); // Returns the list of students.
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var Passed = StudentDataSimulation.StudentsList.Where(Student => Student.Grade > 50).ToList();
            if(Passed.Count == 0)
            {
                return NotFound("Student Not Passed");
            }
            return Ok(Passed);
        }

        [HttpGet ("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAverageGrade()
        {

            //StudentDataSimulation.StudentsList.Clear();
            if(StudentDataSimulation.StudentsList.Count == 0)
            {
                return NotFound("No Found Student");
            }

            var AverageGrade = StudentDataSimulation.StudentsList.Average(Student => Student.Grade);    
            return Ok(AverageGrade);    
        }

        [HttpGet("{id}", Name = "GetSudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <Student> GetStudent(int id) 
        {
            if (id > 1)
            {
                return BadRequest($"Not Accepted {id}");
            }
            var Student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);

            if(Student == null)
            {
                return NotFound($"Studen Id {id} Not Found");  
            }
            return Ok(Student);
        }

    }
}
