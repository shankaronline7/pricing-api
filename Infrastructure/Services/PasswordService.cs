using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _hasher =
            new PasswordHasher<object>();

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string storedHash, string enteredPassword)
        {
            var result = _hasher.VerifyHashedPassword(
                null,
                storedHash,
                enteredPassword);

            return result == PasswordVerificationResult.Success;
        }
    }
}

