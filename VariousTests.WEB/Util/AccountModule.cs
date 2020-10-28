using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using VariousTests.BLL.Services;
using VariousTests.BLL.Interfaces;

namespace VariousTests.WEB.Util
{
    public class AccountModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountService>().To<AccountService>();
        }
    }
}