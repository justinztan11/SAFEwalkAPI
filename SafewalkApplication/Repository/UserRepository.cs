using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SafewalkApplication.Contracts;
using SafewalkApplication.Helpers;
using SafewalkApplication.Models;
using System;
using System.Threading.Tasks;

namespace SafewalkApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SafewalkDatabaseContext _context;
        private IMemoryCache _cache;

        public UserRepository(SafewalkDatabaseContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<User> Add(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Get(string email)
        {
            var cachedUser = _cache.Get<User>(email);
            
            if (cachedUser != null)
            {
                return cachedUser;
            } 
            else
            {
                var user = await _context.User.SingleOrDefaultAsync(m => m.Email == email);

                if (user == null)
                {
                    return null;
                }
                
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                _cache.Set(user.Email, user, cacheEntryOptions);

                return user;
            }
        }

        public async Task<User> Update(User user)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(60));
            _cache.Set(user.Email, user, cacheEntryOptions);

            _context.User.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Delete(string email)
        {
            _cache.Remove(email);

            var user = await _context.User.SingleOrDefaultAsync(m => m.Email == email);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> Exists(string email)
        {
            var cachedUser = _cache.Get<User>(email);

            if (cachedUser != null)
            {
                return true;
            }
            else
            {
                var user = await _context.User.SingleOrDefaultAsync(m => m.Email == email);

                if (user == null)
                {
                    return false;
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                _cache.Set(user.Email, user, cacheEntryOptions);

                return user != null;
            }
        }

        public async Task<bool> Authenticated(string token, string email)
        {
            var cachedUser = _cache.Get<User>(email);

            if (cachedUser != null)
            {
                return cachedUser.Token == token && cachedUser.Email == email;
            }
            else
            {
                var user = await _context.User.SingleOrDefaultAsync(m => m.Token == token && m.Email == email);

                if (user == null)
                {
                    return false;
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                _cache.Set(user.Email, user, cacheEntryOptions);

                return user.Token == token && user.Email == email;
            }
        }
    }
}
