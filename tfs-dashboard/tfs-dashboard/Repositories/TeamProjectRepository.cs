using System;
using Microsoft.TeamFoundation.Framework.Common;
using tfs_dashboard.Models;

namespace tfs_dashboard.Repositories
{
    public class TeamProjectRepository
    {
        public static TeamCollection Get(TeamCollection collection)
        {
            if (collection == null || collection.CollectionNode == null)
                throw new Exception("Collection needs to be initialized.");
            collection.Projects.Clear();
            foreach (var projectNode in collection.CollectionNode.QueryChildren(new[] { CatalogResourceTypes.TeamProject }, false, CatalogQueryOptions.None))
            {
                collection.Projects.Add(new TeamProject(projectNode));
            }
            return collection;
        }
    }
}