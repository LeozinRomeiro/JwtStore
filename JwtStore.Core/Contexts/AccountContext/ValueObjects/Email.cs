﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.SharedContext.Extensions;
using JwtStore.Core.Contexts.SharedContext.ValueObjetcs;

namespace JwtStore.Core.AccountContext.ValueObjects
{
    public partial class Email : ValueObject
    {
		private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
		public string Address { get; } = string.Empty;
        public string Hash => Address.ToBase64();
		public Verification Verification { get; private set; } = new();
        protected Email()
        {
            
        }
        public void ResendVerification()=>Verification = new();

        public static implicit operator string(Email email) => email.ToString();
		public static implicit operator Email(string address) => new Email(address);
        public override string ToString() => Address;
        public Email(string address) {
			
			if(string.IsNullOrEmpty(address)) throw new Exception("E-mail invalido");

			Address = address.Trim().ToLower();

			if (Address.Length < 5) throw new Exception("E-mail invalido");
			if (!EmailRegex().IsMatch(Address)) throw new Exception("E-mail invalido");
		}

		[GeneratedRegex(Pattern)]
		private static partial Regex EmailRegex();
	}
}
