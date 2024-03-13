using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using JwtStore.Core.SharedContext.Extensions;
using JwtStore.Core.SharedContext.ValueObjetcs;

namespace JwtStore.Core.AccountContext.ValueObjetcs
{
    public class Email : ValueObject
    {
        public string Address { get; }
        public string Hash => Address.ToBase64();
    }
}
