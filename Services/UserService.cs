using estacionamento.Data;
using estacionamento.Models;
using estacionamento.Utils;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace estacionamento.Services
{
    public class UserService
    {
        private readonly DataContext _ctx;
        public UserService(DataContext context)
        {
            _ctx = context;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var hashedPassword = EncryptionHelper.Encrypt(password);

            var result = await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == username && u.PasswordHash == hashedPassword);
            if (result == null)
            {
                throw new Exception("Usuário não encontrado com essas credenciais");
            }
            result.PasswordHash = EncryptionHelper.Decrypt(hashedPassword);
            return result;
        }

       
        private void decrypt(User user)
        {
            user.PasswordHash = EncryptionHelper.Decrypt(user.PasswordHash);
        }
        private void encrypt(User user)
        {
            user.PasswordHash = EncryptionHelper.Encrypt(user.PasswordHash);
        }

        public async Task<User> CreateAsync(User user)
        {
            
            if (user == null)
            {
                throw new Exception("UserException - Usuário está nulo");
            }
            var actualUser = await GetByUserNameAsync(user.UserName);
            if (actualUser != null)
            {
            throw new Exception(@"UserException - Já existe usuário com esse Login");
            }
            user.PasswordHash = EncryptionHelper.Encrypt(user.PasswordHash);
            var result = await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();
            result.Entity.PasswordHash = EncryptionHelper.Decrypt(user.PasswordHash);
            return result.Entity;

        }
        public async Task<User> GetByUserNameAsync(string username)
        {
            var result = await _ctx.Users.FirstOrDefaultAsync(x => x.UserName == username);
            return result;
        }
        public async Task<User> GetByIdAsync(int id)
        {
            var User = await _ctx.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (User == null)
                throw new Exception("UserException - Id não encontrado");
            decrypt(User);
            return User;
        }
    }
}
