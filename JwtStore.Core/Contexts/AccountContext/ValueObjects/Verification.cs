using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects
{
    public class Verification
    {
        internal Verification()
        {

        }
        public string Code { get; } = Guid.NewGuid().ToString("N")[0..6].ToUpper();
        public DateTime? ExpireAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
        public DateTime? VerifiedAt { get; private set; } = null;
        public bool IsActive => VerifiedAt != null && ExpireAt == null;

        public void Verify(string code)
        {
            if (IsActive)
                throw new Exception("Este item já foi ativado");
            if (ExpireAt < DateTime.UtcNow)
                throw new Exception("Este codigo já expirou");
            if (!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Codigo invalido");

            ExpireAt = null;
            VerifiedAt = DateTime.UtcNow;
        }
    }
}
