using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfs_dashboard.Models
{
    public class ChangeRequest
    {
        
        public string Title { get; set; }
        public string State { get; set; }

        public ChangeRequest(WorkItem workItem)
        {
            Title = workItem.Title;
            State = workItem.State;
        }
    }
}