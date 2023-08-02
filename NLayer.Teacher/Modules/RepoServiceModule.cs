using Autofac;
using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.GenericService;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.GenericManager;
using NLayer.Service.Mapping;
using System.Reflection;
using Module = Autofac.Module;

namespace NLayer.Academy.Modules
{
    public class RepoServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepositoy<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssmbly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssmbly).Where(x => x.Name.EndsWith
            ("Repository")).AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssmbly).Where(x => x.Name.EndsWith
            ("Service")).AsImplementedInterfaces().InstancePerDependency();

            base.Load(builder);
        }
    }
}
