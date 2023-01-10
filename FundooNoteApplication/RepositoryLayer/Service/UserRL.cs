using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        FundooContext fundoo;
        public UserRL(FundooContext fundoo)
        {
            this.fundoo = fundoo;
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
                    return "Login Successfull";
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
    }
}
