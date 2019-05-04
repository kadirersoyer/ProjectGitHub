using OrderManagment.Core.Repositories;
using OrderManagment.Entities.Entities;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace OrderManagment.Core
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IRepository<Customer>, Repository<Customer>>();
            container.RegisterType<IRepository<Category>, Repository<Category>>();
            container.RegisterType<IRepository<Product>, Repository<Product>>();
            container.RegisterType<IRepository<Order>, Repository<Order>>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}