namespace Shared
{
    using Autofac;
    using Tasky.Interfaces;
    using Tasky.Managers;
    using Tasky.Models;
    using Tasky.Repository;

    public static class BootStrapper
    {
        static BootStrapper()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.Register(n => new TaskItem()).As<ITaskItem>();
            containerBuilder.RegisterType<TaskRepository>().As<ITaskRepository>().SingleInstance();
            containerBuilder.RegisterType<TaskItemManager>().As<ITaskItemManager>().SingleInstance();

            Container = containerBuilder.Build();
        }
        
        private static IContainer Container { get; set; }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}