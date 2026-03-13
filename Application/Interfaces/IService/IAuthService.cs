using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IService
{
    public interface IAuthService
    {

        Task<UserDto?>Login(LoginDto loginDto);
    }
}
