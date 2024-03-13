﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.SharedContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.Entities
{
    public class User : Entity
    {
        public string Name { get; set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public string Image { get; private set; } = string.Empty;
        protected User() { }
        public User(string email, string? password = null)
        {
            Email = email;
            Password = new(password);
        }
        public void UpdatePassword(string plainTextPassword, string code)
        {
            if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("Código de restauração inválido");

            var password = new Password(plainTextPassword);
            Password = password;
        }

        public void UpdateEmail(Email email)
        {
            Email = email;
        }

        public void ChangePassword(string plainTextPassword)
        {
            var password = new Password(plainTextPassword);
            Password = password;
        }
    }
}
