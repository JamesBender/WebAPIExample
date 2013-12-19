using System;
using Ninject.Modules;
using WebDirectory.Web.Core.Models;

namespace WebDirectory.Web.Core
{
    public class WebModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPersonModel>().To<PersonModel>();
            Bind<WebDirectory.Core.IPersonRepository>().To<WebDirectory.Core.PersonRepository>();
        }
    }
}