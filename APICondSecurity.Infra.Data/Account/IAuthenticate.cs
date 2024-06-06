namespace APICondSecurity.Infra.Data.Account
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string senha);
        Task<bool> UserExists(string email);
        string GenerateToken(int id, string email);
    }
}
