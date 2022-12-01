using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Dinota.DataAccces
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Repo")).AsImplementedInterfaces();
        }
    }
}
