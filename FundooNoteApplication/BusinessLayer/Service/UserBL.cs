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
    }
}
