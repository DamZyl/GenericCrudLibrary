using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GenericCrud.Options;
using IdentityModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GenericCrud.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly IOptions<JwtOption> _jwtOption;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;
        
        public JwtHandler(IOptions<JwtOption> jwtOption)
        {
            _jwtOption = jwtOption;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Value.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = _jwtOption.Value.Issuer,
                ValidateLifetime = _jwtOption.Value.ValidateLifetime
            };
        }
        
        public string CreateToken(Guid userId, string fullName, string role)
        {
            var now = DateTime.Now;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, fullName),
                new Claim(JwtClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expires = now.AddMinutes(_jwtOption.Value.ExpiryMinutes);
            
            var jwt = new JwtSecurityToken(
                issuer: _jwtOption.Value.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}