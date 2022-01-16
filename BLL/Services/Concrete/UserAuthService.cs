using BLL.Domain;
using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _myDbContext;

        public UserAuthService(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole<Guid>> roleManager, ApplicationContext myDbContext)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            this._roleManager = roleManager;
            this._myDbContext = myDbContext;
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterModel registerModel)
        {
            var existingUser = await _userManager.FindByNameAsync(registerModel.Username);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with such username already exists" }
                };
            }
            var selectedSubscription = await _myDbContext.Subscription.Where(c => c.Id == registerModel.Subscription).FirstOrDefaultAsync();
            var newUser = new User
            {
                Name = registerModel.Name,
                Surname = registerModel.Surname,
                BirthDate = registerModel.BirthDate,
                UserName = registerModel.Username,
                Email = registerModel.Email,
                PasswordHash = registerModel.PasswordHash,
                Subscription = selectedSubscription,
                ProfileImage = registerModel.ProfileImage
            };

            var createdUser = await _userManager.CreateAsync(newUser, registerModel.PasswordHash);


            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.User));
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }

            return GenerateAuthenticationResultForUser(newUser);
        }


        public async Task<AuthenticationResult> RegisterAdminAsync(RegisterModel registerModel)
        {
            var existingUser = await _userManager.FindByNameAsync(registerModel.Username);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with such username already exists" }
                };
            } else if (registerModel.Username.Equals("admin"))
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "You can't register a user using username starts with 'admin'" }
                };
            }
            var selectedSubscription = await _myDbContext.Subscription.Where(c => c.Id == registerModel.Subscription).FirstOrDefaultAsync();
            var newUser = new User
            {
                Name = registerModel.Name,
                Surname = registerModel.Surname,
                BirthDate = registerModel.BirthDate,
                UserName = registerModel.Username,
                Email = registerModel.Email,
                PasswordHash = registerModel.PasswordHash,
                Subscription = selectedSubscription,
                ProfileImage = registerModel.ProfileImage
            };

            var createdUser = await _userManager.CreateAsync(newUser, registerModel.PasswordHash);


            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Admin));
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
            }

            return GenerateAuthenticationResultForUser(newUser);
        }

        public async Task<AuthenticationResult> LoginAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User credentials are wrong" }
                };
            }
            var userRoles = await _userManager.GetRolesAsync(user);

            return GenerateAuthenticationResultForUser(user);
        }

        private AuthenticationResult GenerateAuthenticationResultForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("id", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"]
            };
            foreach (var userRole in userRoles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, userRole));
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                AccessToken = tokenHandler.WriteToken(token),
                Username = user.UserName
            };
        }
    }
}
