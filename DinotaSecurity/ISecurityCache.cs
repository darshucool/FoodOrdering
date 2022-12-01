using System;
using System.Collections.Generic;

namespace Dinota.Security
{
    public interface ISecurityCache : IDictionary<string, WeakReference>
    {

    }
}
