using System;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace tfs_dashboard.Repositories
{
    public class WorkItemRepository
    {
        public static WorkItemStore Get(Uri teamServerUri)
        {
            TfsTeamProjectCollection teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(teamServerUri);
            return teamProjectCollection.GetService<WorkItemStore>();
        }
    }
}