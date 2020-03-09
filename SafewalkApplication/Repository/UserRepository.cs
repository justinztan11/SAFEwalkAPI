﻿using Microsoft.EntityFrameworkCore;
using SafewalkApplication.Contracts;
using SafewalkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafewalkApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SafewalkDatabaseContext _context;

        public UserRepository(SafewalkDatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Get(string email)
        {
            return await _context.User.SingleOrDefaultAsync(m => m.Email == email);
        }

        public async Task<User> Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> Delete(string email)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.Email == email);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> Exists(string email)
        {
            return await _context.User.AnyAsync(m => m.Email == email);
        }

        public Task<bool> Authenticated(string token, string email)
        {
            return _context.User.AnyAsync(m => m.Token == token && m.Email == email);
        }
    }
}
