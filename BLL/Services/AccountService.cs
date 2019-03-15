using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Model;
using Newtonsoft.Json.Linq;
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

        public Transaction ServeValidateEmail(Registration registration)
        {
            return this.iar.ValidateEmail(registration);
        }

        public Transaction ServeValidateUsername(Registration registration)
        {
            return this.iar.ValidateUsername(registration);
        }

        public Transaction ServeCreateUser(UserRegistration userRegistration)
        {
            return this.iar.CreateUser(userRegistration);
        }
    }
}
