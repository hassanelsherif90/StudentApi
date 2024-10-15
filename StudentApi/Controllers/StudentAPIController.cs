using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Authorization;
using StudentApi.DTO;
using StudentApi.Model.User;
using StudentApi.Services;


namespace StudentApi.Controllers
{
    [ApiController]
    [Route("Students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        public StudentService studentService;

        public StudentsController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        [CheckPermission(Permission.ReadStudent)]
        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {

            List<StudentDTO>? students = studentService.GetAllStudent();
            return students.Count == 0 ? (ActionResult<IEnumerable<StudentDTO>>)NotFound("Not Found Students !") : Ok(students);
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [CheckPermission(Permission.ReadStudent)]
        public ActionResult<IEnumerable<StudentDTO>> GetPassedStudents()
        {

            var PassedStudent = studentService.GetPassedStudent();

            return PassedStudent.Count == 0 ? (ActionResult<IEnumerable<StudentDTO>>)NotFound("Student Not Passed") : Ok(PassedStudent);
        }

        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<double> GetAverageGrade()
        {
            double averageGrade = studentService.GetAverageGrade();

            return averageGrade == 0 ? (ActionResult<double>)NotFound("No Found Student") : (ActionResult<double>)Ok(averageGrade);
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
            var student = studentService.Find(id);

            return student == null ? (ActionResult<StudentDTO>)NotFound($"Studen Id {id} Not Found") : Ok(student);
        }


        [HttpPost("AddStudent", Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [CheckPermission(Permission.AddStudent)]
        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudentDTO)
        {
            if (newStudentDTO == null || string.IsNullOrEmpty(newStudentDTO.Name) || newStudentDTO.Age < 0 || newStudentDTO.Grade < 0)
            {
                return BadRequest("Not Bad Request !");
            }

            studentService.AddNewStudent(newStudentDTO);

            studentService.Save();

            //newStudentDTO.Id = student.Id;

            return CreatedAtRoute("GetSudentById", new { id = newStudentDTO.Id }, newStudentDTO);
        }

        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [CheckPermission(Permission.EditStudent)]
        public ActionResult<StudentDTO> UpdateStudent(int id, StudentDTO UpdateStudentDTO)
        {
            if (id < 1 || UpdateStudentDTO == null || string.IsNullOrEmpty(UpdateStudentDTO.Name) || UpdateStudentDTO.Age < 0 || UpdateStudentDTO.Grade < 0)
            {
                return BadRequest("Invalid student data !");
            }

            var student = studentService.Find(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            student.Name = UpdateStudentDTO.Name;
            student.Age = UpdateStudentDTO.Age;
            student.Grade = UpdateStudentDTO.Grade;

            studentService.Save();

            return Ok(student);
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
