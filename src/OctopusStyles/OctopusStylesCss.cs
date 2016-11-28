using System;
using System.Collections.Generic;
using Octopus.Server.Extensibility.Extensions.Infrastructure.Web.Content;

namespace OctopusStyles
{
    internal class OctopusStylesCss : IContributesCSS
    {
        public IEnumerable<string> GetCSSUris(string requestDirectoryPath)
        {
            yield return $"{requestDirectoryPath}/styles/OctopusStyles.css";
        }
    }
}