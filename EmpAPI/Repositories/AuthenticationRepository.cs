using EmpAPI.Context;
using EmpAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmpAPI.Repositories
{
    public class AuthenticationRepository : IAuthentication
    {
        private readonly IConfiguration _config;
        public AuthenticationRepository(IConfiguration config)
        {
            _config = config;
        }

        UserModel IAuthentication.AuthenticateUser(UserModel model)
        {
            try
            {
                var _context = new EmpDbContext();

                var user = _context.Users.FirstOrDefault(s => s.UserName == model.UserName && s.Password == model.Password);

                if (user == null)
                {
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                //TODO
                //Logging.Logging.WriteErrorLog(ex);
            }

            return null;
        }

        public string GenerateJSONWebToken(UserModel model)
        {
            string key = _config["AppSettingKey:SecretKey"].ToString(); //Secret key which will be used later during validation    
            var issuer = _config["AppSettingKey:Issuer"].ToString();   //normally this will be your site URL 
            int ExpireInTime = Convert.ToInt32(_config["AppSettingKey:ExpireInTime"].ToString());

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                new Claim(JwtRegisteredClaimNames.Email, model.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserID", model.ID.ToString())
            };

            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            claims,
                            expires: DateTime.Now.AddMinutes(ExpireInTime),
                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string RegisterUser(UserModel model)
        {
            string result = "";

            try
            {
                using (var _context = new EmpDbContext())
                {
                    if (_context.Users.Any(s => s.UserName.Equals(model.UserName)))
                    {
                        result = "username is already exist in system. Please other username";
                    }

                    if (string.IsNullOrEmpty(result))
                    {
                        if (string.IsNullOrEmpty(model.EmailAddress))
                        {
                            model.EmailAddress = $"{model.UserName}@gmamil.com";
                        }

                        _context.Users.Add(model);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                //TODO
                result = ex.Message;
                //Logging.Logging.WriteErrorLog(ex);
            }

            return result;
        }
    }
}
