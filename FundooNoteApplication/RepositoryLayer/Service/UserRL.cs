using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        FundooContext fundoo;
        private readonly string secret;
        //private readonly string expDate;
        public UserRL(FundooContext fundoo, IConfiguration config)
        {
            this.fundoo = fundoo;
            secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            //expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }

        public UEntity RegisterUser(UserRegistration userRegistration)
        {
            try
            {
                UEntity objUEntity = new UEntity();
                objUEntity.FirstName = userRegistration.FirstName;
                objUEntity.LastName = userRegistration.LastName;
                objUEntity.Email = userRegistration.Email;
                objUEntity.Password= userRegistration.Password;
                fundoo.UserTable.Add(objUEntity);
                int result = fundoo.SaveChanges();
                if(result > 0)
                {
                    return objUEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LoginUser(UserLogin userLogin)
        {
            try
            {
                var result = fundoo.UserTable.Where(x => x.Email == userLogin.Email && x.Password == userLogin.Password).FirstOrDefault();
                //if(result.Email == userLogin.Email && result.Password == userLogin.Password)
                if(result != null)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateSecurityToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userId",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public string ForgotPassword(string email)
        {
            try
            {
                var result = fundoo.UserTable.Where(x => x.Email == email).FirstOrDefault();
                if(result != null )
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    MSMQModel objMSMQ = new MSMQModel();
                    objMSMQ.sendData2Queue(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string email, ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if(resetPasswordModel.new_Password == resetPasswordModel.confirm_Password)
                {
                    var result = fundoo.UserTable.Where(x => x.Email == email).FirstOrDefault();
                    result.Password = resetPasswordModel.new_Password;
                    fundoo.SaveChanges();
                    return true;
                }
                else
                { 
                    return false; 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
