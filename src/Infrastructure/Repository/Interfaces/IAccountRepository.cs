using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(int id);
        Task<IEnumerable<Account>> GetAllAsync();
        Task<int> AddAsync(Account account);
    }

}
