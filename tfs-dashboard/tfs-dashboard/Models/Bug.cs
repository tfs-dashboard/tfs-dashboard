using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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