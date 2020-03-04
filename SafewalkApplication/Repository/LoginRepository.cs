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


        public async Task<User> Get(string email, string password, bool isUser)
        {
            dynamic user = null;
            if (isUser)
            {
                user = await _context.User.SingleAsync(m => m.Email == email && m.Password == password);
            } else
            {
                user = await _context.Safewalker.SingleAsync(m => m.Email == email && m.Password == password);
            }

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
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
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
    }
}
