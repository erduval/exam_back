using Examen.DTO.Auth;
using Examen.Exceptions;
using Examen.Models;
using Examen.Service.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Examen.Services.Auth
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterTechAsync (RegisterDTO dto)
        {
            var userExist = await _userManager.FindByEmailAsync(dto.Email);
            if (userExist != null) return IdentityResult.Failed(new IdentityError { Description = "User already exists." });
            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            IdentityResult result;

            try
            {
                result = await _userManager.CreateAsync(user, dto.Password);
            }
            catch (Exception e)
            {
                throw new CustomErrorMessageException("error.creating.technician");
            }

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Technicien");
            }

            return result;
        }

        public async Task<IdentityResult> RegisterClientAsync (RegisterDTO dto)
        {
            var userExist = await _userManager.FindByEmailAsync(dto.Email);
            if (userExist != null) return IdentityResult.Failed(new IdentityError { Description = "User already exists." });
            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            IdentityResult result;

            try
            {
                result = await _userManager.CreateAsync(user, dto.Password);
            }
            catch (Exception e)
            {
                throw new CustomErrorMessageException("error.creating.client");
            }

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Client");
            }

            return result;
        }

        public async Task<string?> LoginAsync (LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded) return null;

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
