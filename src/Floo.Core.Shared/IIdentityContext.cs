using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floo.Core.Shared
{
    public interface IIdentityContext
    {
        long? UserId { get; }

        string UserName { get; }
    }
}