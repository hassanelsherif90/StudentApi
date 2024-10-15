using StudentApi.Model.Student;

namespace StudentApi.Repositery
{
    public interface IStudentData
    {
        List<Student> GetAllStudent();

        List<Student> GetPassedStudents();

        double GetAverageGrade();

        Student GetStudentByID(int studentId);

        int AddNewStudent(Student student);

        void UpdateStudent(Student student);

        int Save();
    }
}
