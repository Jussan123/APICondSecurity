using APICondSecurity.Infra.Data.Context;
using APICondSecurity.Infra.Data.Interfaces;
using APICondSecurity.Infra.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APICondSecurity.Infra.Data.Repositories
{
    public class UserRepository : IUser
    {
        private readonly condSecurityContext _context;
        public readonly IUser _repository;
        private readonly IConfiguration _configuration;

        public UserRepository(IUser repository)
        {
            _repository = repository;
        }

        public UserRepository(condSecurityContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<User> Alterar(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }

        }

        public async Task<bool> Excluir(int idUser)
        {
            try
            {
                var userExcluido = await _repository.Excluir(idUser);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }

        public async Task<User> Incluir(User user)
        {
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                int linhasAfetadas = await _context.SaveChangesAsync();
                return linhasAfetadas > 0;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Erro ao salvar as alterações no banco de dados: {ex.Message}");
                return false;
            }
        }

        public async Task<User> Get(int IdUser)
        {
            try
            {
                return await _context.User.FirstOrDefaultAsync(c => c.Id_user == IdUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Erro ao buscar user: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _context.User.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> ExcluirUser(User user)
        {
            try
            {
                var userExcluido = await _repository.ExcluirUser(user);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), ex.Message);
                throw;
            }
        }



        public async Task<User> Login(string email, string senha)
        {
            try
            {
                var user = await _repository.Login(email, senha);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> AuthenticateAsync(string email, string senha)
        {
            var user = await _context.User.Where(x => x.Email.ToUpper().Equals(email.ToUpper())).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }

            using var hmac = new HMACSHA512(user.SenhaSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            for (int x = 0; x < computedHash.Length; x++)
            {
                if (computedHash[x] != user.SenhaHash[x]) return false;
            }
            return true;
        }

        public string GenerateToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_configuration["jwt:secretKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await _context.User.Where(x => x.Email.ToUpper().Equals(email.ToUpper())).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            return true;
        }

    }
}
