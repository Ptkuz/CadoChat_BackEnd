using CadoChat.DAL.Entity.BaseEntity;

namespace CadoChat.Auth.EF.Entities
{
    public class LoginAttempt : Entity
    {

        public Guid UserId { get; set; }
        public DateTime AttemptTime { get; set; }
        public bool IsSuccessful { get; set; }

        public User? User { get; set; }

        public LoginAttempt()
        {

        }
    }
}
