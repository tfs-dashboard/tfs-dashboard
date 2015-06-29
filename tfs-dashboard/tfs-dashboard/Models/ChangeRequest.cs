using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace tfs_dashboard.Models
{
    public class ChangeRequest : TeamItem
    {
        public ChangeRequest(WorkItem workItem) : base(workItem)
        {

        }
    }
}