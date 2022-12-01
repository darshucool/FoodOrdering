using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Security.Crypto
{
    public interface ICryptoProvider
    {
        string HashPassword(string plainTextPassword);
    }
}
