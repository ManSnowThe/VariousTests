using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using VariousTests.BLL.Services;
using VariousTests.BLL.Interfaces;

namespace VariousTests.WEB.Util
{
    public class TestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITestService>().To<TestService>();
        }
    }
}