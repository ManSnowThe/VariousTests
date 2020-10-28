using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Ninject;
using VariousTests.BLL.Infrastructure;
using VariousTests.BLL.Interfaces;

namespace VariousTests.WEB.Util
{
    public static class KernelHolder
    {
        private static StandardKernel kernel;

        public static StandardKernel Kernel
        {
            get
            {
                if (kernel == null)
                {
                    NinjectModule serviceModule = new ServiceModule("AppConnection");
                    NinjectModule accountModule = new AccountModule();
                    kernel = new StandardKernel(serviceModule, accountModule);
                    kernel.Unbind<ModelValidatorProvider>();
                }
                return kernel;
            }
        }

        public static IAccountService CreateAccountService()
        {
            return KernelHolder.Kernel.Get<IAccountService>();
        }
    }
}