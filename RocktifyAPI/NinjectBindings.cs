using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocktifyAPI
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAccountRepository>().To<AccountRepository>();
            this.Bind<IAccountService>().To<AccountService>();
            this.Bind<IRocktifyRepository>().To<RocktifyRepository>();
            this.Bind<IRocktifyService>().To<RocktifyService>();
        }
    }
}