using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Authorization;
using StudentApi.Data;
using StudentAPIBusinessLayer;
using StudentDataAccessLayer;
using System.Collections.Generic;


namespace StudentApi.Controllers
{
    [ApiController]
    [Route("Students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {

        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [CheckPermission(Permission.ReadStudent)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {

            var students = StudentAPIBusinessLayer.Student.GetAllStudent();
            if (students.Count == 0)
            {
                return NotFound("Not Found Students !");
            }

            return Ok(students);
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [CheckPermission(Permission.ReadStudent)]
        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()
        {

            var PassedStudent = StudentAPIBusinessLayer.Student.GetPassedStudent();

            if (PassedStudent.Count == 0)
            {
                return NotFound("Student Not Passed");
            }

            return Ok(PassedStudent);
        }

        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAverageGrade()
        { 
            double averageGrade = StudentAPIBusinessLayer.Student.GetAverageGrade();

            if (averageGrade == 0)
            {
                return NotFound("No Found Student");

            }
            return Ok(averageGrade);
        }

        [HttpGet("{id}", Name = "GetSudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [CheckPermission(Permission.ReadStudent)]
        public ActionResult<StudentDTO> GetStudent(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not Accepted {id}");
            }
            StudentAPIBusinessLayer.Student student = StudentAPIBusinessLayer.Student.Find(id);

            if (student == null)
            {
                return NotFound($"Studen Id {id} Not Found");
            }
            return Ok(student);
        }


        [HttpPost("AddStudent",Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [CheckPermission(Permission.AddStudent)]
        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudentDTO)
        {
            if (newStudentDTO == null || string.IsNullOrEmpty(newStudentDTO.Name) || newStudentDTO.Age < 0 || newStudentDTO.Grade < 0)
            {
                return BadRequest("Not Bad Request !");
            }

            StudentAPIBusinessLayer.Student student = new StudentAPIBusinessLayer.Student(new StudentDTO(newStudentDTO.Id, newStudentDTO.Name, newStudentDTO.Age, newStudentDTO.Grade));

            student.Save();
            newStudentDTO.Id = student.Id;

            return CreatedAtRoute("GetSudentById", new { id = newStudentDTO.Id }, newStudentDTO);
        }

        [HttpPut("{id}",Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [CheckPermission(Permission.EditStudent)]
        public ActionResult<StudentDTO> UpdateStudent(int id, StudentDTO UpdateStudentDTO)
        {
            if (id < 1 || UpdateStudentDTO == null || string.IsNullOrEmpty(UpdateStudentDTO.Name) || UpdateStudentDTO.Age < 0 || UpdateStudentDTO.Grade < 0)
            {
                return BadRequest("Invalid student data !");
            }

            StudentAPIBusinessLayer.Student student = StudentAPIBusinessLayer.Student.Find(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            student.Name  = UpdateStudentDTO.Name;
            student.Age   = UpdateStudentDTO.Age;
            student.Grade = UpdateStudentDTO.Grade;

            student.Save();

            return Ok(student.SDTO);
        }

        //[HttpDelete("{id}", Name = "DeleteStudent")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[CheckPermission(Permission.DeleteStudent)]

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


    }
}
