﻿using BLL.Domain;
using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Helpers;
using CIL.Models;
using DAL;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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
        private readonly EmailConfiguration _emailConfig;

        public UserAuthService(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole<Guid>> roleManager, ApplicationContext myDbContext,
            EmailConfiguration emailConfig)
        {
            this._userManager = userManager;
            this._configuration = configuration;
            this._roleManager = roleManager;
            this._myDbContext = myDbContext;
            this._emailConfig = emailConfig;
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
            var uniqueFileName = UploadedFile(registerModel);
            var newUser = new User
            {
                Name = registerModel.Name,
                Surname = registerModel.Surname,
                BirthDate = registerModel.BirthDate,
                UserName = registerModel.Username,
                Email = registerModel.Email,
                PasswordHash = registerModel.PasswordHash,
                Subscription = selectedSubscription,
                ProfileImage = uniqueFileName,
                RegistrationDate = DateTime.Now
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

            SendEmail(newUser);

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
            var uniqueFileName = UploadedFile(registerModel);
            var newUser = new User
            {
                Name = registerModel.Name,
                Surname = registerModel.Surname,
                BirthDate = registerModel.BirthDate,
                UserName = registerModel.Username,
                Email = registerModel.Email,
                PasswordHash = registerModel.PasswordHash,
                Subscription = selectedSubscription,
                ProfileImage = uniqueFileName,
                RegistrationDate = DateTime.Now
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

            SendEmail(newUser);

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

        private string UploadedFile(RegisterModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine("Resources", "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private async void SendEmail(User user)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("ChapterOne", _emailConfig.From));
            emailMessage.To.Add(new MailboxAddress(user.UserName, user.Email));
            emailMessage.Subject = "ChapterOne - Welcome";

            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources", "welcome-email.html");
            string EmailTemplateText = File.ReadAllText(FilePath);
            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.HtmlBody = EmailTemplateText;
            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
            //client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}
