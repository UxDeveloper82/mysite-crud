using mysite.Models;
using mysite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Data.Repository
{
    public interface IPortRepository
    {
        Portfolio GetPort(int id);
        List<Portfolio> GetAllPortfolios();
        IndexViewModel GetAllPortfolios(int pageNumber, string category, string search, string orderBy);
        void AddPortfolio(Portfolio portfolio);
        void UpdatePortfolio(Portfolio portfolio);
        void RemovePort(int id);
        Task<bool> SaveChangesAsync();
    }
}
