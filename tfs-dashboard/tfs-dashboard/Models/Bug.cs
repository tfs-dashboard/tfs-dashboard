using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfs_dashboard.Models
{
    public class Bug
    {
        public string Title { get; set; }
        public string State { get; set; }

        public Bug(WorkItem workItem)
        {
            Title = workItem.Title;
            State = workItem.State;
        }
    }
}