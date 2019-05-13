using BLL.Interfaces;
using DAL.Interfaces;
using ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RocktifyService : IRocktifyService
    {
        private readonly IRocktifyRepository irr;
        
        public RocktifyService(IRocktifyRepository irr)
        {
            this.irr = irr;
        }

        public Task<object> ServeAccessToken()
        {
            return this.irr.AccessToken();
        }
    }
}
