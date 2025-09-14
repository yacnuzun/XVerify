using Application.Interfaces;
using Domain.Entity;
using Infrastructure.Repository.Interfaces;
using Infrastructure.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implamentations
{
    public class AccountService:IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            _unitOfWork.Begin();
            var account = await _accountRepository.GetByIdAsync(id);
            _unitOfWork.Commit();
            return account;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            _unitOfWork.Begin();
            var accounts = await _accountRepository.GetAllAsync();
            _unitOfWork.Commit();
            return accounts;
        }

        public async Task<int> AddAsync(Account account)
        {
            _unitOfWork.Begin();
            var id = await _accountRepository.AddAsync(account);
            _unitOfWork.Commit();
            return id;
        }
    }

}
