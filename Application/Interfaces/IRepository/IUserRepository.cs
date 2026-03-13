using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IRepository
{
    public  interface IUserRepository
    {
        Task<Users> GetUserByEmail(string email);
    }
}
