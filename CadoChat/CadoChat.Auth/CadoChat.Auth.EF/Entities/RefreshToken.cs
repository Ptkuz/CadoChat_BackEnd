using CadoChat.DAL.Entity.BaseEntity;

namespace CadoChat.Auth.EF.Entities
{
    public class RefreshToken : Entity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }

        public User? User { get; set; }

        public RefreshToken()
        {

        }
    }
}
