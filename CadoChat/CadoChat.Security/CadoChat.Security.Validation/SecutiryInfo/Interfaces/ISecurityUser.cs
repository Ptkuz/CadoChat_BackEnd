using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Validation.SecutiryInfo.Interfaces
{
    public interface ISecurityUser
    {
        string Id { get; }

        string[] Scopes { get; }

        int AccessTokenLifetime { get; }

        ICollection<string> AllowedGrantTypes { get; }

        bool RequireClientSecret { get; }
    }
}
