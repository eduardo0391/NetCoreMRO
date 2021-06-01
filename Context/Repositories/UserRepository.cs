using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCoreReact.Context;

namespace NetCoreReact.Context.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        
        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public User Add(User user)
        {
            var userAux= this.appDbContext.Add(user);
            this.appDbContext.SaveChanges();
            return userAux.Entity;
        }

        public User userByName(string userName)
        {
            var user = this.appDbContext.User.FirstOrDefault(x => (userName == x.UserName || userName == x.Email));
            return user;
        }
        /// Encripta una cadena
        public string Encrypt(string stringToEncrypt)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(stringToEncrypt);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public string Decrypt(string stringToDecrypt)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(stringToDecrypt);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
