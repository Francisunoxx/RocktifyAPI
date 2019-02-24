using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountService
    {
        Transaction ServeCheckEmail(Registration registration);
        Transaction ServeCheckUserName(Registration registration);
        Transaction ServeCreateUser(Registration registration);
    }
}
