using Octopus.Server.Extensibility.Extensions;
using Autofac;
using Octopus.Server.Extensibility.Extensions.Infrastructure.Web.Content;

namespace OctopusStyles
{
    [OctopusPlugin("Octopus Styles", "Ryan Hird")]
    public class OctopusStylesExtension : IOctopusExtension
    {
        public void Load(ContainerBuilder builder)
        {            
            builder.RegisterType<OctopusStylesCss>().As<IContributesCSS>().InstancePerDependency();
            builder.RegisterType<OctopusStylesStaticContent>().As<IContributesStaticContentFolders>().InstancePerDependency();
        }
    }
}
