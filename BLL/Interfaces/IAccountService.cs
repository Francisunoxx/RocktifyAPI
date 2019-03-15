using DAL;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Transaction ServeValidateEmail(Registration registration);
        Transaction ServeValidateUsername(Registration registration);
        Transaction ServeCreateUser(UserRegistration userRegistration);
    }
}
