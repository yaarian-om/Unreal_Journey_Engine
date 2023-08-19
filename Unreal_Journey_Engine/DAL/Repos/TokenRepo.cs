using DAL.Database.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class TokenRepo : Repo, IRepo<Token, int, Token>
    {

        public Token Create(Token obj)
        {
            db.Tokens.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Token> Get()
        {
            return db.Tokens.ToList();
        }

        public Token Get(int id)
        {
            return db.Tokens.Find(id);

        }

        public Token Update(Token obj)
        {
            var oldTtoken = db.Tokens.Find(obj.Id);
            oldTtoken.ExpiredAt = DateTime.Now;
            var decision = db.SaveChanges();
            return (decision > 0) ? oldTtoken : null;
        }
    }
}
