using Application.Interfaces.IRepository;
using Domain.Entities;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly MainDbContext context;

        public UserRepository(MainDbContext context)
        {
            this.context = context;
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            return await context.users.
                 Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
