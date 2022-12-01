using System.Reflection;
using Autofac;
using Dinota.Security;
using Dinota.Security.Crypto;

namespace Dinota.Security
{
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).Where(t => t.Name.EndsWith("Service"));

            builder.RegisterType<SecurityCache>().As<ISecurityCache>().SingleInstance();

            builder.RegisterType<SHA256CryptoProvider>().As<ICryptoProvider>();
        }

    }
}
