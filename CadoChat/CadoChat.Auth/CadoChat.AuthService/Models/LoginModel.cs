namespace CadoChat.AuthService.Models
{
    public class LoginModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }


        public LoginModel()
        {

        }

        public LoginModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
