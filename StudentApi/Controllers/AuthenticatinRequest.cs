namespace StudentApi.Controllers
{
    public class AuthenticatinRequest
    {
        public required string userName { get; set; }
        public required string password { get; set; }
    }
}