using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using VidRental.DataAccess.DbModels;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using VidRental.Services.Dtos.Auth;
using AutoMapper;
using VidRental.Services.Dtos.Response.User;
using VidRental.Services.Dtos.Response.Role;

namespace VidRental.Services.Services
{
    public class TokenService : ITokenService
    {
        public TokenService(
            IConfiguration configuration,
            UserManager<User> userManager,
            IMapper mapper
            )
        {
            Configuration = configuration;
            UserManager = userManager;
            Mapper = mapper;
        }

        public IConfiguration Configuration { get; }
        public UserManager<User> UserManager { get; }
        public IMapper Mapper { get; }

        /// <summary>
        /// Generates jwt token for given user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Jwt token as string</returns>
        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await UserManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(Configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                Configuration["JwtIssuer"],
                Configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            var tokenHanlder = new JwtSecurityTokenHandler();

            return tokenHanlder.WriteToken(token);
        }

        /// <summary>
        /// Refreshes jwt token
        /// </summary>
        /// <param name="token">Old token</param>
        /// <returns>New token</returns>
        public async Task<RefreshTokenResult> RefreshToken(string token)
        {
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
            };

            ClaimsPrincipal principal;
            try
            {
                principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParams, out var securityToken);
            }
            catch (Exception e)
            {
                return null;
            }

            if (principal == null)
                return null;

            if (!principal.Identity.IsAuthenticated)
                return null;

            var userEmail = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            if (userEmail == null)
                return null;

            var userDb = await UserManager.FindByEmailAsync(userEmail);

            if (userDb == null)
                return null;
            
            var userRoles = await UserManager.GetRolesAsync(userDb);
            var userBaseInfo = Mapper.Map<User, UserBaseInfo>(userDb);
            userBaseInfo.Roles = userRoles.Select(r => new RoleDto { Name = r });

            var result = new RefreshTokenResult
            {
                Token = await GenerateJwtToken(userDb),
                User = userBaseInfo
            };

            return result;
        }
    }
}
