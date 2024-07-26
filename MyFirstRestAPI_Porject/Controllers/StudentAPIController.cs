using Microsoft.AspNetCore.Mvc;
using StudentAPIBusinessLayer;
using StudentDataAccessLayer;
using System.Collections.Generic;


namespace StudentApi.Controllers
{
    [ApiController] 
    [Route("api/Students")]

    public class StudentsController : ControllerBase 
    {

        [HttpGet("All", Name = "GetAllStudents")] 

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents() 
        {

            var students = StudentAPIBusinessLayer.Student.GetAllStudent();
            if (students.Count == 0)
            {
                return NotFound("Not Found Students !");
            }

            return Ok(students);
        }

        //[HttpGet("Passed", Name = "GetPassedStudents")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<IEnumerable<Student>> GetPassedStudents()
        //{
        //    var Passed = StudentDataSimulation.StudentsList.Where(Student => Student.Grade > 50).ToList();
        //    if (Passed.Count == 0)
        //    {
        //        return NotFound("Student Not Passed");
        //    }
        //    return Ok(Passed);
        //}

        //[HttpGet("AverageGrade", Name = "GetAverageGrade")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<double> GetAverageGrade()
        //{

        //    //StudentDataSimulation.StudentsList.Clear();
        //    if (StudentDataSimulation.StudentsList.Count == 0)
        //    {
        //        return NotFound("No Found Student");
        //    }

        //    var AverageGrade = StudentDataSimulation.StudentsList.Average(Student => Student.Grade);
        //    return Ok(AverageGrade);
        //}

        //[HttpGet("{id}", Name = "GetSudentById")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<Student> GetStudent(int id)
        //{
        //    if (id > 1)
        //    {
        //        return BadRequest($"Not Accepted {id}");
        //    }
        //    var Student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);

        //    if (Student == null)
        //    {
        //        return NotFound($"Studen Id {id} Not Found");
        //    }
        //    return Ok(Student);
        //}


        //[HttpPost(Name = "AddStudent")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<Student> AddStudent(Student newStudent)
        //{
        //    if (newStudent == null || string.IsNullOrEmpty(newStudent.Name) || newStudent.Age < 0 || newStudent.Grade < 0)
        //    {
        //        return BadRequest("Not Bad Request !");
        //    }

        //    newStudent.Id = StudentDataSimulation.StudentsList.Count > 0 ? StudentDataSimulation.StudentsList.Max(s => s.Id) + 1 : 1;
        //    StudentDataSimulation.StudentsList.Add(newStudent);

        //    return CreatedAtRoute("GetSudentById", new { id = newStudent.Id }, newStudent);
        //}

        //[HttpDelete("{id}", Name = "DeleteStudent")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult DeleteStudent(int id)
        //{
        //    if (id < 1)
        //    {
        //        return BadRequest($"Not Accepted ID {id} !");
        //    }

        //    var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);

        //    if (student == null)
        //    {
        //        return NotFound($" Student with ID : {id} Not Found !");
        //    }
        //    StudentDataSimulation.StudentsList.Remove(student);

        //    return Ok($"Student with ID : {id} has been Deleted");

        //}

        //[HttpPut(Name = "UpdateStudent")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<Student> UpdateStudent(int id, Student UpdateStudent)
        //{
        //    if (id < 1 || string.IsNullOrEmpty(UpdateStudent.Name) || UpdateStudent.Age < 0 || UpdateStudent.Grade < 0)
        //    {
        //        return BadRequest("Invalid student data !");
        //    }

        //    var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);
        //    if (student == null)
        //    {
        //        return NotFound($"Student with ID {id} not found.");
        //    }

        //    student.Name = UpdateStudent.Name;
        //    student.Age = UpdateStudent.Age;
        //    student.Grade = UpdateStudent.Grade;

        //    return Ok();
        //}
    }
}
