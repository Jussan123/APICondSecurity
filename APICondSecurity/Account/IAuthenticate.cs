﻿using APICondSecurity.Infra.Data.Models;

namespace APICondSecurity.Account
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string senha);
        Task<bool> UserExists(string email);
        public string GenerateToken(int id, string email);
        public Task<User> GetUserByEmail(string email);
    }
}
