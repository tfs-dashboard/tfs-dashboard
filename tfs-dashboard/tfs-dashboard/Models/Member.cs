using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace tfs_dashboard.Models
{
    public class Member
    {
        public string Name { get; private set; }
        public bool Show { get; private set; }
        public List<Bug> BugsAssigned;
        public List<ChangeRequest> ChangeRequestsAssigned;
        public List<Requirement> RequirementsAssigned;

        public int Backlog;
        public int InWork;
        public int WaitingForTest;
        public int InTest;
        public int WaitingForRelease;

        public Member(string name)
        {
            Name = name;
            Show = true;
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

        public void CountTicketsInColumn()
        {
            Backlog = BugsAssigned.Count(m => m.Status == "Backlog") + RequirementsAssigned.Count(m => m.Status == "Backlog") + ChangeRequestsAssigned.Count(m => m.Status == "Backlog");
            InWork = BugsAssigned.Count(m => m.Status == "In Work") + RequirementsAssigned.Count(m => m.Status == "In Work") + ChangeRequestsAssigned.Count(m => m.Status == "In Work");
            WaitingForTest = BugsAssigned.Count(m => m.Status == "Waiting For Test") + RequirementsAssigned.Count(m => m.Status == "Waiting For Test") + ChangeRequestsAssigned.Count(m => m.Status == "Waiting For Test");
            InTest = BugsAssigned.Count(m => m.Status == "In Test") + RequirementsAssigned.Count(m => m.Status == "In Test") + ChangeRequestsAssigned.Count(m => m.Status == "In Test");
            WaitingForRelease = BugsAssigned.Count(m => m.Status == "Waiting For Release") + RequirementsAssigned.Count(m => m.Status == "Waiting For Release") + ChangeRequestsAssigned.Count(m => m.Status == "Waiting For Release");

        }
    }
}