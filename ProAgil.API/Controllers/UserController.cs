using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using ProAgil.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper _mapper;

        public UserController(IConfiguration config,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IMapper mapper)
        {
            this.config = config;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                return Ok(new UserDto());
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro API - " + ex.Message);
            }
        }

        [HttpPost("Login")]      
        [AllowAnonymous]  
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await this.userManager.FindByNameAsync(userLogin.UserName);
                var result = await this.signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await this.userManager.Users
                                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userLogin.UserName.ToUpper());

                    var userToReturn = this._mapper.Map<UserLoginDto>(appUser);

                    return Ok(new {
                        token = GenerateJWToken(appUser).Result,
                        user = userToReturn
                    });
                }

                return Unauthorized();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro API - " + ex.Message);
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto user)
        {
            try
            {
                var usu = _mapper.Map<User>(user);
                var result = await this.userManager.CreateAsync(usu, user.Password);
                var userToReturn = _mapper.Map<UserDto>(usu);

                if (result.Succeeded)
                {
                    return Created("GetUser", userToReturn);
                }
                return BadRequest(result.Errors);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro API - " + ex.Message);
            }
        }

        private async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await this.userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            var tokenDescripter = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripter);

            return tokenHandler.WriteToken(token);
        }   

    }
}