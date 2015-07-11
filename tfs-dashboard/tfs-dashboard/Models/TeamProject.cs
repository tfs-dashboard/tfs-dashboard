using Microsoft.TeamFoundation.Framework.Client;

namespace tfs_dashboard.Models
{
    public class TeamProject
    {
        public string Name { get; set; }

        public TeamProject(CatalogNode node)
        {
            Name = node.Resource.DisplayName;
        }
    }
}