using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        IUserRL userIUserRL;
        public UserBL(IUserRL userIUserRL)
        {
            this.userIUserRL = userIUserRL;
        }

        public UEntity RegisterUser(UserRegistration userRegistration)
        {
            try
            {
                return userIUserRL.RegisterUser(userRegistration);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string LoginUser(UserLogin userLogin)
        {
            try
            {
                return userIUserRL.LoginUser(userLogin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                return userIUserRL.ForgotPassword(email);
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
                return userIUserRL.ResetPassword(email, resetPasswordModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
