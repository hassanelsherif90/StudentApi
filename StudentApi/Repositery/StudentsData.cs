using StudentApi.Data;
using StudentApi.DTO;
using StudentApi.Model.Student;

namespace StudentApi.Repositery
{

    public class StudentsData : IStudentData
    {
        private readonly ApplicationDbcontext dbcontext;

        public StudentsData(ApplicationDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public List<Student> GetAllStudent()
        {
            var students = dbcontext.Students.ToList();
            return students;

        }

        public List<Student> GetPassedStudents()
        {
            return dbcontext.Students.Where(x => x.Grade >= 50).ToList();
        }

        public double GetAverageGrade()
        {
            return dbcontext.Students.Average(x => x.Grade);
        }

        public Student GetStudentByID(int studentId)
        {
            return dbcontext.Students.FirstOrDefault(x => x.Id == studentId);
        }

        public int AddNewStudent(Student student)
        {
            int Id = -1;

            return Id;
        }

        public void UpdateStudent(Student student)
        {


        }

        public int Save()
        {
            return dbcontext.SaveChanges();
        }
    }
}
