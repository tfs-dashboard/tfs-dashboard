using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace tfs_dashboard.Models
{
    public class ChangeRequest : TeamItem
    {
        public ChangeRequest(WorkItem workItem, WorkItemStore workItemStore)
            : base(workItem, workItemStore)
        {
            Type = "ChangeRequest";
        }
    }
}