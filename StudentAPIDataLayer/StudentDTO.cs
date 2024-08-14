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
}
