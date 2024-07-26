using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Globalization;




namespace StudentDataAccessLayer
{
    public class StudentDTO
    {
        public StudentDTO (int Id, string Name, int Age, int Grade)
        {
            this.Id = Id;
            this.Name = Name;   
            this.Age = Age;
            this.Grade = Grade; 
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }

    }

    public class StudentsData
    {
        static string ConnectionString = "Server=localhost;Database=StudentsDB;User Id=sa;Password=123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
        public static List<StudentDTO> GetAllStudent()
        {
            var studentList = new List<StudentDTO>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllStudents", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            studentList.Add(new StudentDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            ));
                        }
                    }
                }
            }

            return studentList;

        }
    }
}
