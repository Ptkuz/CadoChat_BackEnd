using CadoChat.Common.Validation.Exceptions;

namespace CadoChat.DAL.Entity.BaseEntity
{
    public class Entity : IEntity
    {
        public Guid Id { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime ModifiedAt { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();

            var currentDateTime = DateTime.UtcNow;

            if (CreatedAt == default)
            {
                CreatedAt = currentDateTime;
            }

            ModifiedAt = currentDateTime;
        }

        public virtual void Validate()
        {
            ValidateModifiedAt();
        }

        private void ValidateModifiedAt()
        {
            if (ModifiedAt < CreatedAt)
            {
                throw new IncorrectDateValidation("Дата изменения меньше даты добавления записи");
            }
        }

    }
}
