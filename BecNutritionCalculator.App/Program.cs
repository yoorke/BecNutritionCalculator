using BecNutritionCalculator.BusinessLogic;
using BecNutritionCalculator.BusinessLogic.Interfaces;
using BecNutritionCalculator.Models;
using GenericRepositories;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace BecNutritionCalculator.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = new UnityContainer();
            container.RegisterType<Main, Main>();
            container.RegisterType<IGenericRepository<Sirovina>, GenericRepository<Sirovina>>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISirovinaBL, SirovinaBL>(new ContainerControlledLifetimeManager());
            container.RegisterType<INutritivniElementBL, NutritivniElementBL>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISmesaBL, SmesaBL>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGenericRepository<SirovinaNeVrednost>, GenericRepository<SirovinaNeVrednost>>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGenericRepository<NutritivniElementVrednost>, GenericRepository<NutritivniElementVrednost>>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGenericRepository<Smesa>, GenericRepository<Smesa>>(new ContainerControlledLifetimeManager());
            container.RegisterType<INutritivniElementVrednostBL, NutritivniElementVrednostBL>(new ContainerControlledLifetimeManager());
            container.RegisterType<ITipSirovineBL, TipSirovineBL>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGenericRepository<TipSirovine>, GenericRepository<TipSirovine>>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGenericRepository<Jm>, GenericRepository<Jm>>(new ContainerControlledLifetimeManager());
            container.RegisterType<IJmBL, JmBL>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISirovinaNutritivniElementVrednostBL, SirovinaNutritivniElementVrednostBL>(new ContainerControlledLifetimeManager());
            container.RegisterType<IGenericRepository<SirovinaNutritivniElementVrednost>, GenericRepository<SirovinaNutritivniElementVrednost>>(new ContainerControlledLifetimeManager());
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var program = container.Resolve<Main>();
            Application.Run(program);
        }
    }
}
