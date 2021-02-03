using mysite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedMembers()
        {
            var memberData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var members = JsonConvert.DeserializeObject<List<Member>>(memberData);
            foreach (var member in members)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                member.PasswordHash = passwordHash;
                member.PasswordSalt = passwordSalt;
                member.UserName = member.UserName.ToLower();

                _context.Members.Add(member);
            
            }
            _context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
