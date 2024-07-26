using System;
using System.Data;
using StudentDataAccessLayer;


namespace StudentAPIBusinessLayer
{
    public class Student
    {
        public StudentDTO SDTO
        {
            get { return (new StudentDTO(this.Id, this.Name, this.Age, this.Grade)); }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }

        public Student(StudentDTO SDTO)
        {
            this.Id = SDTO.Id;  
            this.Name = SDTO.Name;  
            this.Age = SDTO.Age;    
            this.Grade = SDTO.Grade;
        }

        public static List<StudentDTO> GetAllStudent()
        {
            return StudentsData.GetAllStudent();
        }
    }


    
}
