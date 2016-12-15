using System.Collections.Generic;
using Octopus.Server.Extensibility.Extensions.Infrastructure.Web.Content;

namespace OctopusStyles
{
    internal class OctopusStylesCss : IContributesCSS
    {
        public IEnumerable<string> GetCSSUris()
        {
            yield return "~/styles/OctopusStyles.css";
        }
    }
}