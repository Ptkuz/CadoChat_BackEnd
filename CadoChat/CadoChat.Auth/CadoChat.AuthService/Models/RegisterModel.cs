namespace CadoChat.AuthService.Models
{
    public class RegisterModel
    {

        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public RegisterModel()
        {
        }

        public RegisterModel(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }

    }
}
