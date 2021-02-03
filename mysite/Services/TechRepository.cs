using Microsoft.EntityFrameworkCore;
using mysite.Data;
using mysite.Interfaces;
using mysite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Services
{
    public class TechRepository : ITechRepository
    {
        private readonly DataContext _context;
        public TechRepository(DataContext context)
        {
            _context = context;
        }

        public void AddMember(Member member)
        {
            _context.Members.Add(member);
        }

        public List<Member> GetAllMembers()
        {
            return _context.Members.ToList();
        }

        public Member GetMember(int id)
        {
            return _context.Members.FirstOrDefault(m => m.Id == id);
        }

        public void RemoveMember(int id)
        {
            _context.Members.Remove(GetMember(id));
        }

        public void UpdateMember(Member member)
        {
            _context.Members.Update(member);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

    }
}
