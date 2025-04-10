using CadoChat.Common.Validation.Exceptions;

namespace CadoChat.DAL.Entity.BaseEntity
{
    public class Entity : IEntity
    {
        

        public Entity() { }

        private Guid id;

        public Guid Id
        {
            get
            {
                return id;
            }
            set
            {
                SetEntityId();
            }
        }

        private DateTime createdAt;

        public DateTime CreatedAt
        {
            get
            {
                return createdAt;
            }
            set
            {
                SetCreatedAt();
            }
        }

        private DateTime modifiedAt;

        public DateTime ModifiedAt
        {
            get
            {
                return modifiedAt;
            }
            set
            {
                SetModifiedAt();
            }
        }

        private void SetEntityId()
        {
            id = Guid.NewGuid();
        }

        private void SetCreatedAt()
        {
            createdAt = DateTime.UtcNow;
        }

        private void SetModifiedAt()
        {

            modifiedAt = DateTime.UtcNow;
            ValidateModifiedAt();
        }

        private void ValidateModifiedAt()
        {
            if (modifiedAt < createdAt)
            {
                throw new IncorrectDateValidation("Дата изменения меньше даты добавления записи");
            }
        }

        public virtual void Validate()
        {
            ValidateModifiedAt();
        }

    }
}
