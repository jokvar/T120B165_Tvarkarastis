using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using T120B165.Data;
using T120B165.Models;

namespace T120B165.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly T120B165Context _context;
        private readonly IRefreshTokenManager _refreshTokenManager;

        public LoginController(ILogger<LoginController> logger,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            T120B165Context context,
            IRefreshTokenManager refreshTokenManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _refreshTokenManager = refreshTokenManager;
        }


        /// <summary>
        /// Return authorized user Vidko
        /// </summary>
        /// <returns>Vidko</returns>
        /// <response code="200">Returns Vidko</response>
        /// <response code="401">Unauthorized.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Secret")]
        public async Task<IActionResult> Secret()
        {
            var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
            var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;
            var usernameClaim = claims.Where(c => c.Type == "Username").FirstOrDefault();
            var user = await _userManager.FindByNameAsync(usernameClaim.Value);
            if (user == null)
            {
                return Unauthorized("This should never happen.");
            }
            var vidko = _context.Students.Where(s => s.Username == user.UserName).FirstOrDefault()?.Vidko;
            return Ok( new { user.UserName, vidko });
        }

        /// <summary>
        /// Obtain access_token and refresh_token. 
        /// grant_type must be either:
        /// * "password  - for username:password login;
        /// * "refresh_token" - for refresh token.
        /// </summary>
        /// <returns>Vidko</returns>
        /// <response code="200">Returns access_token, refresh_token and their lifetimes, and timestamp</response>
        /// <response code="401">Unauthorized.</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("Token")]
        public async Task<IActionResult> Token(LoginForm loginForm)
        {
            if (loginForm.Grant_Type == "password")
            {
                if (!string.IsNullOrWhiteSpace(loginForm.Username))
                {
                    var username = _context.Students.Where(s => s.Username == loginForm.Username).FirstOrDefault()?.Username;
                    if (username == null)
                    {
                        return Unauthorized(new { error = "Incorrect username." });
                    }
                    var user = await _userManager.FindByNameAsync(username);
                    Microsoft.AspNetCore.Identity.SignInResult password_check =
                        await _signInManager.CheckPasswordSignInAsync(user, loginForm.Password, false);
                    if (!password_check.Succeeded)
                    {
                        return Unauthorized(new { error = "Incorrect password." });
                    }
                    _refreshTokenManager.RemoveByUsername(username);
                    var claims = new[]
                    {
                        new Claim("Username", username)
                    };
                    var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);
                    var algorithm = SecurityAlgorithms.HmacSha256;

                    var signingCredentials = new SigningCredentials(key, algorithm);
                    var now = DateTime.Now;
                    var token = new JwtSecurityToken(
                        Constants.Issuer,
                        Constants.Audiance,
                        claims,
                        notBefore: now,
                        expires: now.AddMinutes(5),
                        signingCredentials);

                    var access_token = new JwtSecurityTokenHandler().WriteToken(token);
                    var refresh_token = GenerateRefreshTokenString();
                    _refreshTokenManager.Add(new RefreshToken(username, refresh_token, now.AddHours(1)));
                    return Ok( new { current_time = now, access_token, access_token_expires = now.AddMinutes(5), refresh_token, refresh_token_expires = now.AddHours(1) });
                }
                return Unauthorized(new { error = "Missing username." });
            }
            if (loginForm.Grant_Type == "refresh_token")
            {
                if (string.IsNullOrWhiteSpace(loginForm.Refresh_Token))
                {
                    return Unauthorized(new { error = "Missing refresh token." });
                }
                try
                {
                    var _refresh_token = _refreshTokenManager.Get(loginForm.Refresh_Token);
                    if (_refresh_token == null)
                    {
                        return Unauthorized(new { error = "Invalid refresh token." });
                    }
                    var now = DateTime.Now;
                    //if refresh token expired
                    if (_refresh_token.ExpiresAt < now)
                    {
                        return Unauthorized(new { error = "Expired refresh token." });
                    }
                    //generate access token, remove old and generate new refresh token 
                     _refreshTokenManager.Remove(_refresh_token);
                    var claims = new[]
                    {
                        new Claim("Username", _refresh_token.Username)
                    };
                    var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
                    var key = new SymmetricSecurityKey(secretBytes);
                    var algorithm = SecurityAlgorithms.HmacSha256;

                    var signingCredentials = new SigningCredentials(key, algorithm);
                    var token = new JwtSecurityToken(
                        Constants.Issuer,
                        Constants.Audiance,
                        claims,
                        notBefore: now,
                        expires: now.AddMinutes(5),
                        signingCredentials);

                    var access_token = new JwtSecurityTokenHandler().WriteToken(token);
                    var refresh_token = GenerateRefreshTokenString();
                    _refreshTokenManager.Add(new RefreshToken(_refresh_token.Username, refresh_token, now.AddHours(1)));
                    return Ok(new { current_time = now, access_token, access_token_expires = now.AddMinutes(5), refresh_token, refresh_token_expires = now.AddHours(1) });
                }
                catch (SecurityTokenException e)
                {
                    return Unauthorized(new { error = e.Message });
                }
            }
            return Unauthorized($"Invalid 'grant_type':'{loginForm.Grant_Type}'");
        }

        private (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException("Missing access token.");
            }
            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token,
                new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = Constants.Issuer,
                    ValidAudience = Constants.Audiance,
                    IssuerSigningKey = key,
                    ValidateLifetime = false
                },
                out var validatedToken);
            return (principal, validatedToken as JwtSecurityToken);
        }

        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenrator = RandomNumberGenerator.Create();
            randomNumberGenrator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
