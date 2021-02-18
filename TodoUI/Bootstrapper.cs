using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using TodoLibrary;
using TodoLibrary.Data.Categories;
using TodoLibrary.Data.Tasks;
using TodoLibrary.Data.Users;
using TodoLibrary.Data.UserTasks;
using TodoUI.ViewModels;

namespace TodoUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
        {
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<ShellViewModel>();

            _container.PerRequest<IUsersData, UsersData>();
            _container.PerRequest<ITasksData, TasksData>();
            _container.PerRequest<ICategoriesData, CategoriesData>();
            _container.PerRequest<IUserTasksData, UserTasksData>();

            var dbContext = new ApplicationDbContextFactory().CreateDbContext(new string[]{});
            _container.RegisterInstance(typeof(ApplicationDbContext), "ApplicationDbContext", dbContext);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
