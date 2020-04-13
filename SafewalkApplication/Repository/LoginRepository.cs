using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SafewalkApplication.Contracts;
using SafewalkApplication.Helpers;
using SafewalkApplication.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace SafewalkApplication.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SafewalkDatabaseContext _context;
        private readonly AppSettings _appSettings;
        private IMemoryCache _cache;

        public LoginRepository(SafewalkDatabaseContext context, IOptions<AppSettings> appSettings, IMemoryCache cache)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _cache = cache;
        }


        public async Task<string?> GetUser(string email, string password)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.Email == email && m.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            // Use secret verbatim when testing 
            //var key = Encoding.ASCII.GetBytes("owbpiwnbowhviervhpoweinvrbpi7459neoriug98908t345ijf803304");
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

            // update db with token
            _context.User.Update(user);
            await _context.SaveChangesAsync();

            // add user to cache
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(60));
            _cache.Set(email, user, cacheEntryOptions);

            return user.Token;
        }

        public async Task<string?> GetWalker(string email, string password)
        {
            var walker = await _context.Safewalker.SingleOrDefaultAsync(m => m.Email == email && m.Password == password);

            if (walker == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            // Use secret verbatim when testing 
            //var key = Encoding.ASCII.GetBytes("owbpiwnbowhviervhpoweinvrbpi7459neoriug98908t345ijf803304");
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

            // update db with token
            _context.Safewalker.Update(walker);
            await _context.SaveChangesAsync();

            // add walker to cache
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(60));
            _cache.Set(email, walker, cacheEntryOptions);

            return walker.Token;
        }
    }
}
