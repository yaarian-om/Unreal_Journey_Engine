using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService
    {
        public static TokenDTO Login(string Email, string Password)
        {
            var data = RepoAccessFactory.AuthDataAccess().Authenticate(Email, Password);
            if (data != null)
            {
                var token = new Token();
                token.User_ID = data.User_ID;
                token.TokenKey = Guid.NewGuid().ToString();
                token.CreatedAt = DateTime.Now;
                token.ExpiredAt = null;
                var tk = RepoAccessFactory.TokenDataAccess().Create(token);

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Token, TokenDTO>();
                });
                var mapper = new Mapper(config);
                var ret = mapper.Map<TokenDTO>(tk);
                return ret;
            }
            return null;
        }
        public static int IsTokenValid(string token)
        {
            // If it has an expire time, Then the session is gone
            // If expire time is null then session is alive
            var tk = (from t in RepoAccessFactory.TokenDataAccess().Get()
                      where t.TokenKey.Equals(token)
                      && t.ExpiredAt == null
                      select t).SingleOrDefault();
            
            if (tk != null)
            {
                return tk.User_ID;
            }
            return -1;
        }


        public static bool Logout(string token)
        {
            var tk = (from t in RepoAccessFactory.TokenDataAccess().Get()
                      where t.TokenKey.Equals(token)
                      && t.ExpiredAt == null
                      select t).SingleOrDefault();

            var decision = RepoAccessFactory.TokenDataAccess().Update(tk);
            return (decision != null)? true : false;
        }


        public static bool IsAdmin(string token)
        {
            var tk = (from t in RepoAccessFactory.TokenDataAccess().Get()
                      where t.TokenKey.Equals(token)
                      && t.ExpiredAt == null
                      && t.User.Email.Equals("YOUR MAIL HERE")
                      select t).SingleOrDefault();
            throw new NotImplementedException();
            //return tk != null;
        }
    }
}
