using CadoChat.Security.Validation.ConfigLoad;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Validation.Services.Interfaces
{
    public interface ISecurityKeyService<TKey> 
        where TKey : SecurityKey
    {
        TKey Key { get; }

        SigningCredentials SigningCredentials { get; }
    }
}
