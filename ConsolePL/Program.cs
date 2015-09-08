using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using BLL.Interface.Services;
using DependencyResolver;
using Ninject;

namespace ConsolePL
{
    class Program
    {
        private static readonly IKernel resolver;

        static Program()
        {
            resolver = new StandardKernel();
            resolver.ConfigurateResolverConsole();
        }

        static void Main(string[] args)
        {
            var service = resolver.Get<IUserService>();
            var list = service.GetAllUserEntities().ToList();
            foreach (var user in list)
            {
                Console.WriteLine(user.Name);
            }
        }
    }
}
