using BLL.Domain;
using CIL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstract
{
    public interface IUserAuthService
    {
        Task<AuthenticationResult> RegisterAsync(RegisterModel user);
        Task<AuthenticationResult> RegisterAdminAsync(RegisterModel user);
        Task<AuthenticationResult> LoginAsync(LoginModel user);
    }
}
