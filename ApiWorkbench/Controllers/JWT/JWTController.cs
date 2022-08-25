using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ApiWorkbench.Controllers.JWT
{

    [ApiController]
    [Route("jwt")]
    public class BloodPressureController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public BloodPressureController(
            IMediator mediator
            ,IConfiguration configuration
            )
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> get([FromBody] object request)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(credentials);
            var payload = new JwtPayload {
                { "exp", ((DateTimeOffset)DateTime.UtcNow.AddDays(1)).ToUnixTimeSeconds()},
                { "iss", _configuration["JWT:ValidIssuer"]},
                { "aud", _configuration["JWT:ValidAudience"]},
                { "role", 1},
            };
            var secToken = new JwtSecurityToken(header, payload);

            // var secToken = new JwtSecurityToken(
            //     signingCredentials: credentials,
            //     issuer: _configuration["JWT:ValidIssuer"],
            //     audience: _configuration["JWT:ValidAudience"],
            //     expires: DateTime.UtcNow.AddDays(1)
            // );

            var handler = new JwtSecurityTokenHandler();
            System.Console.WriteLine(secToken);
            var JWT = handler.WriteToken(secToken);
            System.Console.WriteLine(JWT);
            return Ok(JWT);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> check([FromBody] object request)
        {
            dynamic data = JsonConvert.DeserializeObject<dynamic>(request.ToString());
            var token = Request.Headers["Authorization"];
            Console.WriteLine(token);
            return Ok("JWT OKE");
        }
    }
}

