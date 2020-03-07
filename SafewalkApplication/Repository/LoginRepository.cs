using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SafewalkApplication.Contracts;
using SafewalkApplication.Helpers;
using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SafewalkApplication.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SafewalkDatabaseContext _context;
        private readonly AppSettings _appSettings;

        public LoginRepository(SafewalkDatabaseContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }


        public async Task<User> GetUser(string email, string password)
        {
            var user = await _context.User.SingleAsync(m => m.Email == email && m.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            _context.User.Update(user);
            await _context.SaveChangesAsync();

            return user;

            // set up later
            //return user.WithoutPassword();

        }

        public async Task<Safewalker> GetWalker(string email, string password)
        {
            var walker = await _context.Safewalker.SingleOrDefaultAsync(m => m.Email == email && m.Password == password);

            if (walker == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, walker.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            walker.Token = tokenHandler.WriteToken(token);

            _context.Safewalker.Update(walker);
            await _context.SaveChangesAsync();

            return walker;

            // set up later
            //return walker.WithoutPassword();
        }
    }
}
