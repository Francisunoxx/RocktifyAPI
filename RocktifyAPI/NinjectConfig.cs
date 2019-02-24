using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocktifyAPI
{
    public class NinjectConfig
    {
        public static StandardKernel CreateKernel()
        {
            return new StandardKernel(new NinjectBindings());
        }
    }
}