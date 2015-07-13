using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace tfs_dashboard.Models
{
    public class TeamItem
    {
        public string Title { get; private set; }
        public string State { get; private set; }
        public int Priority { get; private set; }
        public string Status { get; protected set; }
        public string VerificationStatus { get; private set; }
        public int Id { get; private set; }
        public bool Blocked { get; private set; }
        public List<Task> TaskList { get; private set; }
        public string AssignedTo { get; private set; }
        public int OverallEstimatedTime { get; private set; }
        public int OverallCompletedTime { get; private set; }
        public int OverallRemainingTime { get; private set; }
        public string Type { get; protected set; }
        public int TaskAmount { get; private set; }
        public int ClosedTaskAmount { get; private set; }

        public TeamItem(WorkItem workItem, WorkItemStore workItemStore)
        {
            foreach (Field field in workItem.Fields)
            {
                if (field.Name == "Priority")
                {
                    Priority = (int)field.Value;
                }
                if (field.Name == "Verification Status")
                {
                    VerificationStatus = (string)field.Value;
                }
                if (field.Name == "Blocked")
                {
                    Blocked = (string)field.Value == "Yes";
                }
                if (field.Name == "Assigned To")
                {
                    AssignedTo = (string)field.Value;
                }
            }
            switch (workItem.State)
            {
                case "Proposed":
                    Status = "Backlog";
                    break;
                case "Active":
                    Status = "In Work";
                    break;
                case "Resolved":
                    if (VerificationStatus == "Not Executed")
                        Status = "Waiting For Test";
                    else if (VerificationStatus == "Resolved")
                        Status = "In Test";
                    else if (VerificationStatus == "Passed")
                        Status = "Waiting For Release";
                    break;
            }

            TaskList = new List<Task>();
            OverallCompletedTime = 0;
            OverallEstimatedTime = 0;
            OverallRemainingTime = 0;


            foreach (WorkItemLink workItemLink in workItem.WorkItemLinks)
            {
                WorkItem item = workItemStore.GetWorkItem(workItemLink.TargetId);
                if (item.Type.Name == "Task")
                {
                    Task task = new Task(item);
                    TaskList.Add(task);
                    OverallCompletedTime += task.CompletedWork;
                    OverallEstimatedTime += task.OriginalEstimate;
                    OverallRemainingTime += task.RemainingWork;
                }
            }
            ClosedTaskAmount = TaskList.Count(m => m.Status == "Closed");
            TaskAmount = TaskList.Count();

            Title = workItem.Title;
            State = workItem.State;
            Id = workItem.Id;
        }
    }
}