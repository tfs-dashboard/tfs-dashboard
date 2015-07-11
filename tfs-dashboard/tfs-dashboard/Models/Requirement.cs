using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace tfs_dashboard.Models
{
    public class Requirement : TeamItem
    {
        public Requirement(WorkItem workItem, WorkItemStore workItemStore)
            : base(workItem, workItemStore)
        {
            Type = "Requirement";
        }
    }
}