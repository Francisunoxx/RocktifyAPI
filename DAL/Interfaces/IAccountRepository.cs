using ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAccountRepository
    {
        Transaction ValidateUsername(Registration registration);
        Transaction ValidateEmail(Registration registration);
        Transaction CreateUser(UserRegistration userRegistration);
        Transaction ValidateAccount(User user);
    }
}
