using StudentApi.DTO;
using StudentApi.Model.Student;
using StudentApi.Repositery;


namespace StudentApi.Services
{
    public class StudentService(IStudentData _studentData)
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public IStudentData studentData = _studentData;

        public List<StudentDTO> GetAllStudent()
        {
            var students = studentData.GetAllStudent();

            var studentDTO = new List<StudentDTO>();

            foreach (var student in students)
            {
                var STDO = new StudentDTO(student.Id, student.Name, student.Age, student.Grade);

                studentDTO.Add(STDO);
            }
            return studentDTO;
        }

        public List<StudentDTO> GetPassedStudent()
        {
            var students = studentData.GetPassedStudents();
            var studentDTO = new List<StudentDTO>();

            foreach (var student in students)
            {
                var STDO = new StudentDTO(student.Id, student.Name, student.Age, student.Grade);
                studentDTO.Add(STDO);
            }
            return studentDTO;
        }

        public double GetAverageGrade()
        {
            double studentsAverage = studentData.GetAverageGrade();
            return studentsAverage;
        }

        public int Save()
        {
            return studentData.Save();
        }

        public void UpdateStudent(StudentDTO studentDTO)
        {
            var student = new Student();

            student.Id = studentDTO.Id;
            student.Name = studentDTO.Name;
            student.Age = studentDTO.Age;
            student.Grade = studentDTO.Grade;

            studentData.AddNewStudent(student);
        }

        public void AddNewStudent(StudentDTO studentDTO)
        {
            var student = new Student();
            student.Id = studentDTO.Id;
            student.Name = studentDTO.Name;
            student.Age = studentDTO.Age;
            student.Grade = studentDTO.Grade;


            studentData.AddNewStudent(student);
        }

        public StudentDTO Find(int Id)
        {
            var student = studentData.GetStudentByID(Id);

            var studentDTO = new StudentDTO(student.Id, student.Name, student.Age, student.Grade);
            //studentDTO.Id = student.Id;
            //studentDTO.Name = student.Name;
            //studentDTO.Age = student.Age;
            //studentDTO.Grade = student.Grade;


            return studentDTO;
        }
    }



}
