using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfs_dashboard.Models
{
    public class Member
    {
        public string Name { get; private set; }
        public List<Bug> BugsAssigned;
        public List<ChangeRequest> ChangeRequestsAssigned;
        public List<Requirement> RequirementsAssigned;


        public Member(string name)
        {
            Name = name;
            BugsAssigned = new List<Bug>();
            ChangeRequestsAssigned = new List<ChangeRequest>();
            RequirementsAssigned = new List<Requirement>();
        }

        public void AddWorkItem(WorkItem workItem, WorkItemStore workItemStore)
        {
            switch (workItem.Type.Name)
            {
                case "Bug":
                    BugsAssigned.Add(new Bug(workItem, workItemStore));
                    break;
                case "Requirement":
                    RequirementsAssigned.Add(new Requirement(workItem, workItemStore));
                    break;
                case "Change Request":
                    ChangeRequestsAssigned.Add(new ChangeRequest(workItem, workItemStore));
                    break;
            }
        }
    }
}