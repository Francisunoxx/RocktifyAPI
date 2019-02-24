using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iar;

        public AccountService(IAccountRepository iar)
        {
            this.iar = iar;
        }

        public Transaction ServeCheckEmail(Registration registration)
        {
            return this.iar.CheckEmail(registration);
        }

        public Transaction ServeCheckUserName(Registration registration)
        {
            return this.iar.CheckUserName(registration);
        }

        public Transaction ServeCreateUser(Registration registration)
        {
            return this.iar.CreateUser(registration);
        }
    }
}
