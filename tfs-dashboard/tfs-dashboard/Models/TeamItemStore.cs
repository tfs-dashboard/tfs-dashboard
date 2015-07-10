using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace tfs_dashboard.Models
{
    public class TeamItemStore
    {
        public ICollection<Member> Members;

        [ScriptIgnore]
        public WorkItemStore WorkItemStore;

        public TeamItemStore(WorkItemCollection collection, WorkItemStore workItemStore)
        {
            WorkItemStore = workItemStore;
            PopulateMemberNames(collection);
            foreach (WorkItem workItem in collection)
            {
                var assignedTo = (string)workItem["Assigned To"];
                AddWorkItemToMember(workItem, assignedTo);
            }
        }

        private void AddWorkItemToMember(WorkItem workItem, string memberName)
        {
            var member = Members.First(m => m.Name == memberName);
            member.AddWorkItem(workItem, WorkItemStore);
        }

        private void PopulateMemberNames(WorkItemCollection collection)
        {
            Members = new List<Member>();
            var names = (from WorkItem workitem in collection select (string) workitem["Assigned To"]).ToList();

            names = names.Distinct().ToList();

            foreach (string name in names)
            {
                Members.Add(new Member(name));
            }
        }
    }
}