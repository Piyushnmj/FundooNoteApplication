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
        public static string key = "piyushnmj@dotnet";

        public UserRL(FundooContext fundoo, IConfiguration config)
        {
            this.fundoo = fundoo;
            secret = config.GetSection("JwtConfig").GetSection("secret").Value;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns></returns>
        public UEntity RegisterUser(UserRegistration userRegistration)
        {
            try
            {
                UEntity objUEntity = new UEntity();
                objUEntity.FirstName = userRegistration.FirstName;
                objUEntity.LastName = userRegistration.LastName;
                objUEntity.Email = userRegistration.Email;
                objUEntity.Password = EncryptPassword(userRegistration.Password);
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

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns></returns>
        public string LoginUser(UserLogin userLogin)
        {
            try
            {
                var result = fundoo.UserTable.Where(x => x.Email == userLogin.Email).FirstOrDefault();
                var decryptPassword = DecryptPassword(result.Password);
                if(result != null && decryptPassword == userLogin.Password)
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

        /// <summary>
        /// Generates the security token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Sends a token to reset password
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="resetPasswordModel">The reset password model.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Encrypts the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            password += key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }

        /// <summary>
        /// Decrypts the password.
        /// </summary>
        /// <param name="encodedPassword">The encoded password.</param>
        /// <returns></returns>
        public static string DecryptPassword(string encodedPassword)
        {
            if (string.IsNullOrEmpty(encodedPassword))
            {
                return "";
            }
            var encodedPasswordBytes = Convert.FromBase64String(encodedPassword);
            var result = Encoding.UTF8.GetString(encodedPasswordBytes);
            result = result.Substring(0, result.Length - key.Length);
            return result;
        }
    }
}
