using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace tfs_dashboard.Models
{
    public class Bug : TeamItem
    {
        public Bug(WorkItem workItem, WorkItemStore workItemStore) : base(workItem, workItemStore)
        {
            Type = "Bug";
        }
    }
}