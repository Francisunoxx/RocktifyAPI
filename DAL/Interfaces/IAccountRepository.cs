using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAccountRepository
    {
        Transaction CheckUserName(Registration registration);
        Transaction CheckEmail(Registration registration);
        Transaction CreateUser(Registration registration);
    }
}
