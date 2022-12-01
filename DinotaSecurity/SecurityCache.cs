using System;
using System.Collections.Concurrent;

namespace Dinota.Security
{
    public class SecurityCache : ConcurrentDictionary<string, WeakReference>, ISecurityCache
    {
    }
}
