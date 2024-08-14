using System;
using System.Data;
using Microsoft.Data.SqlClient;


namespace StudentDataAccessLayer
{

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

        public static List<StudentDTO> GetPassedStudents()
        {
            var StudentPassed = new List<StudentDTO>();    

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPassedStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            StudentPassed.Add(new StudentDTO
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

            return StudentPassed;   
        }
        
        public static double GetAverageGrade()
        {
            double AverageGrade = 0; 
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAverageGrade", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open ();

                    Object result = command.ExecuteScalar();

                    if(result != DBNull.Value)
                    {
                        AverageGrade = Convert.ToDouble(result);
                    }
                    else
                    {
                        AverageGrade = 0;
                    }
                  
                }
            }

            return AverageGrade;    
        }
    
        public static StudentDTO GetStudentByID(int studentId)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetStudentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                      if (reader.Read())
                      {
                        return new StudentDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            );
                      }
                      else
                      
                            return null;
                    }
                }
            }
        }

        public static int AddNewStudent(StudentDTO SDTO)
        {
            int Id = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("SP_AddStudent", Connection))
                    {
                        /*@Name, @Age, @Grade*/
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.AddWithValue("@Name", SDTO.Name);
                        Command.Parameters.AddWithValue("@Age", SDTO.Age);
                        Command.Parameters.AddWithValue("@Grade", SDTO.Grade);
                        var outputIdParam = new SqlParameter("@NewStudentId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        Command.Parameters.Add(outputIdParam);

                        Connection.Open();
                        Command.ExecuteNonQuery();

                        Id = (int)outputIdParam.Value;


                    }
                }

                
            }
            catch (Exception ex) 
            {
                return Id;  
            }
            return Id;
        }

        public static bool UpdateStudent(StudentDTO SDTO)
        { 
            try
            {
                using SqlConnection Connection = new SqlConnection(ConnectionString);
                using (SqlCommand Command = new SqlCommand("SP_UpdateStudent", Connection))
                {
                    /*@Name, @Age, @Grade*/
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@StudentId", SDTO.Id);
                    Command.Parameters.AddWithValue("@Name", SDTO.Name);
                    Command.Parameters.AddWithValue("@Age", SDTO.Age);
                    Command.Parameters.AddWithValue("@Grade", SDTO.Grade);

                    Connection.Open();
                    Command.ExecuteNonQuery();
                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            

        }

        
    }
}
