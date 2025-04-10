using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Common.Validation.Exceptions
{
    public class IncorrectDateValidation : Exception
    {
        public IncorrectDateValidation()
            : base("Некорректная дата")
        {
            
        }

        public IncorrectDateValidation(string message)
            : base(message)
        {

        }
    }
}
