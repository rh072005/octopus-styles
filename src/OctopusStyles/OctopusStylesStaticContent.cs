using Octopus.Server.Extensibility.Extensions.Infrastructure.Web.Content;
using System.Collections.Generic;

namespace OctopusStyles
{
    class OctopusStylesStaticContent : IContributesStaticContentFolders
    {
        public IEnumerable<StaticContentEmbeddedResourcesFolder> GetStaticContentFolders()
        {
            var type = typeof(OctopusStylesStaticContent);
            var assembly = type.Assembly;
            return new[] { new StaticContentEmbeddedResourcesFolder("", assembly, type.Namespace + ".Static") };
        }
    }
}
