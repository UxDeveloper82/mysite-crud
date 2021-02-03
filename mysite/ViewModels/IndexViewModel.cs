using mysite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.ViewModels
{
    public class IndexViewModel
    {
        public int PageNumber { get; set; }

        public bool NextPage { get; set; }

        public int PageCount { get; set; }

        public string Category { get; set; }
        public string Search { get; set; }

        public string OrderBy { get; set; }

        public IEnumerable<Portfolio> Portfolios { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public IEnumerable<Member> Users { get; set; }
    }
}
