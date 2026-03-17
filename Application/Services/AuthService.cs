using Application.Dtos;
using Application.Interfaces.IRepository;
using Application.Interfaces.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class AuthService :IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContext;

        public AuthService(IUserRepository userRepository,IHttpContextAccessor httpContext )
        {
            _userRepository = userRepository;
            _httpContext = httpContext;
        }
        public async Task<UserDto?>Login(LoginDto loginDto)
        {
           var user = await _userRepository.GetUserByEmail(loginDto.Email);
            if (user == null)
                return null;

            //using var sha256 = SHA256.Create();
            //var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            //var hashedPassword = Convert.ToBase64String(bytes);

            //if (hashedPassword != user.PasswordHash)
            //    return null;

            //if (loginDto.Password != user.PasswordHash)
            //    return null;

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                return null;

            return new UserDto
            {
                Id=user.Id,
                Email = user.Email,
                Role = user.Roles?.Name
            };
        }
    }
}
