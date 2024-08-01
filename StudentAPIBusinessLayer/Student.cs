using System;
using System.Data;
using StudentDataAccessLayer;


namespace StudentAPIBusinessLayer
{
    public class Student
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public StudentDTO SDTO
        {
            get { return (new StudentDTO(this.Id, this.Name, this.Age, this.Grade)); }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }

        public Student(StudentDTO SDTO, enMode cMode = enMode.AddNew)
        {
            this.Id = SDTO.Id;  
            this.Name = SDTO.Name;  
            this.Age = SDTO.Age;    
            this.Grade = SDTO.Grade;

            Mode = cMode;
        }

        public static List<StudentDTO> GetAllStudent()
        {
            return StudentsData.GetAllStudent();
        }

        public static List<StudentDTO> GetPassedStudent()
        {
            return StudentsData.GetPassedStudents();
        }

        public static double GetAverageGrade()
        {
            return StudentsData.GetAverageGrade();  
        }

        public static Student Find(int ID)
        {

            StudentDTO SDTO = StudentsData.GetStudentByID(ID);

            if (SDTO != null)
            //we return new object of that student with the right data
            {

                return new Student(SDTO, enMode.Update);
            }
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewStudent())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateStudent();
            }
            return false;
        }

        private bool _UpdateStudent()
        {
            return StudentsData.UpdateStudent(SDTO);
        }

        private bool _AddNewStudent()
        {
            this.Id = StudentsData.AddNewStudent(SDTO);
                return (this.Id != -1);
        }
    }


    
}
