using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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