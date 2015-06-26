using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfs_dashboard.Models
{
    public class TeamItemStore
    {
        public ICollection<Member> members;

        public TeamItemStore(WorkItemCollection collection)
        {
            PopulateMemberNames(collection);
            foreach (WorkItem workItem in collection)
            {
                string assignedTo = (string)workItem["Assigned To"];
                AddWorkItemToMember(workItem, assignedTo);
            }
        }

        private void AddWorkItemToMember(WorkItem workItem, string memberName)
        {
            Member member = members.Where(m => m.Name == memberName).First();
            member.AddWorkItem(workItem);
        }

        private void PopulateMemberNames(WorkItemCollection collection)
        {
            members = new List<Member>();
            List<string> names = new List<string>();

            foreach (WorkItem workitem in collection)
            {
                names.Add((string)workitem["Assigned To"]);
            }
            names = names.Distinct().ToList();

            foreach (string name in names)
            {
                members.Add(new Member(name));
            }
        }
    }
}