using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SafewalkApplication.Contracts;
using SafewalkApplication.Helpers;
using SafewalkApplication.Models;
using System;
using System.Threading.Tasks;

namespace SafewalkApplication.Repository
{
    public class SafewalkerRepository : ISafewalkerRepository
    {
        private readonly SafewalkDatabaseContext _context;
        private IMemoryCache _cache;

        public SafewalkerRepository(SafewalkDatabaseContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Safewalker> Get(string email)
        {
            var cachedWalker = _cache.Get<Safewalker>(email);

            if (cachedWalker != null)
            {
                return cachedWalker;
            }
            else
            {
                var walker = await _context.Safewalker.SingleOrDefaultAsync(m => m.Email == email);

                if (walker == null)
                {
                    return null;
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                _cache.Set(walker.Email, walker, cacheEntryOptions);

                return walker;
            }
        }

        public async Task<Safewalker> Update(Safewalker walker)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(60));
            _cache.Set(walker.Email, walker, cacheEntryOptions);

            _context.Safewalker.Update(walker);
            await _context.SaveChangesAsync();
            return walker;
        }

        public async Task<bool> Exists(string email)
        {
            var cachedWalker = _cache.Get<Safewalker>(email);

            if (cachedWalker != null)
            {
                return true;
            }
            else
            {
                var walker = await _context.Safewalker.SingleOrDefaultAsync(m => m.Email == email);

                if (walker == null)
                {
                    return false;
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                _cache.Set(walker.Email, walker, cacheEntryOptions);

                return walker != null;
            }
        }

        public async Task<bool> Authenticated(string token, string email)
        {
            //var cachedWalker = _cache.Get<Safewalker>(email);

            // Cached walker is commented out for testing purposes 
            Safewalker cachedWalker = null;

            if (cachedWalker != null)
            {
                return cachedWalker.Token == token && cachedWalker.Email == email;
            }
            else
            {
                var walker = await _context.Safewalker.SingleOrDefaultAsync(m => m.Token == token && m.Email == email);

                if (walker == null)
                {
                    return false;
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                _cache.Set(walker.Email, walker, cacheEntryOptions);

                return walker.Token == token && walker.Email == email;
            }
        }
    }
}
