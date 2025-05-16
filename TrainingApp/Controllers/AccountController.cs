using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TrainingApp.Data.DTOs.UserDto;
using TrainingApp.Data.Migrations;
using TrainingApp.Data.Models.Users;
using TrainingApp.Data.Repository;
using static TrainingApp.Data.DTOs.UserDto.LoginDto;

namespace TrainingApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly IDbRepository _repository;

        public AccountController(IDbRepository repository)
        {
            _repository = repository;

        }
        [HttpPost("register")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestErrorMessages();
            }

            var isEmailAlreadyRegistered = await _userManager.FindByEmailAsync(registerRequest.Email) != null;
            var isUserNameAlreadyRegistered = await _userManager.FindByNameAsync(registerRequest.Username) != null;

            if (isEmailAlreadyRegistered)
            {
                return Conflict($"Email Id {registerRequest.Email} is already registered.");
            }

            if (isUserNameAlreadyRegistered)
            {
                return Conflict($"Username {registerRequest.Username} is already registered");
            }

            var newUser = new User
            {
                Email = registerRequest.Email,
                UserName = registerRequest.Username,
                DisplayName = registerRequest.DisplayName,
            };

            var result = await _userManager.CreateAsync(newUser, registerRequest.Password);

            if (result.Succeeded)
            {
                return Ok("User created successfully");
            }
            else
            {
                return StatusCode(500, result.Errors.Select(e => new { Msg = e.Code, Desc = e.Description }).ToList());
            }
        }



        private async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var tempUserDb = await _repository.Set<ApplicationUser>().FirstOrDefaultAsync(u => u.Email == email);
            return  tempUserDb;
        }

        private async Task AddUser(ApplicationUser newUser)
        {
            newUser.Id =Guid.NewGuid();
            await _repository.Set<ApplicationUser>().AddAsync(newUser);
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestErrorMessages();
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            var isAuthorized = user != null && await _userManager.CheckPasswordAsync(user, request.Password);

            if (isAuthorized)
            {
                var authResponse = await GetTokens(user);
                user.RefreshToken = authResponse.RefreshToken;
                await _userManager.UpdateAsync(user);
                return Ok(authResponse);
            }
            else
            {
                return Unauthorized("Invalid credentials");
            }

        }



        private string GetRefreshToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            //ensure token is unique by checking against db
            var tokenIsUnique = !tempUserDb.Any(u => u.RefreshToken == token);

            if (!tokenIsUnique)
                return GetRefreshToken();  //recursive call

            return token;
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestErrorMessages();
            }

            //check if any user with this refresh token exists
            var user = await GetUserByRefreshToken(request.RefreshToken);
            if (user == null)
            {
                return BadRequest("Invalid refresh token");
            }

            //provide new access and refresh tokens
            var response = await GetTokens(user);
            return Ok(response);
        }

        private async Task<AuthResponse> GetTokens(User user)
        {
            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["token:subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId),
                        new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email)
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["token:key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["token:issuer"],
                _configuration["token:audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["token:accessTokenExpiryMinutes"])),
                signingCredentials: signIn);
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshTokenStr = GetRefreshToken();
            user.RefreshToken = refreshTokenStr;
            var authResponse = new AuthResponse { AccessToken = tokenStr, RefreshToken = refreshTokenStr };
            return await Task.FromResult(authResponse);
        }


    }
}
