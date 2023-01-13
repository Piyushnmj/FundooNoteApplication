using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UEntity RegisterUser(UserRegistration userRegistration);
        public string LoginUser(UserLogin userLogin);
        public string ForgotPassword(string email);
        public bool ResetPassword(string email, ResetPasswordModel resetPasswordModel);
    }
}
