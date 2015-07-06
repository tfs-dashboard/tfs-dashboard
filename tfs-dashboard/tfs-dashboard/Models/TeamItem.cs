using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                    this.VerificationStatus = (string)field.Value;
                }
                if (field.Name == "Blocked")
                {
                    this.Blocked = (string)field.Value == "Yes" ? true : false;
                }
                if (field.Name == "Assigned To")
                {
                    this.AssignedTo = (string)field.Value;
                }
            }
            switch (workItem.State)
            {
                case "Proposed":
                    if (this.Priority < 4)
                        this.Status = "Backlog";
                    break;
                case "Active":
                    this.Status = "In work";
                    break;
                case "Resolved":
                    if (this.VerificationStatus == "Not Executed")
                        this.Status = "Waiting for test";
                    else if (this.VerificationStatus == "Resolved")
                        this.Status = "In test";
                    else if (this.VerificationStatus == "Passed")
                        this.Status = "Waiting for release";
                    break;
            }

            TaskList = new List<Task>();
            this.OverallCompletedTime = 0;
            this.OverallEstimatedTime = 0;
            this.OverallRemainingTime = 0;

            foreach (WorkItemLink workItemLink in workItem.WorkItemLinks)
            {
                WorkItem item = workItemStore.GetWorkItem(workItemLink.TargetId);
                if (item.Type.Name == "Task")
                {
                    Task task = new Task(item);
                    this.TaskList.Add(task);
                    this.OverallCompletedTime += task.CompletedWork;
                    this.OverallEstimatedTime += task.OriginalEstimate;
                    this.OverallRemainingTime += task.RemainingWork;
                }
            }

            this.Title = workItem.Title;
            this.State = workItem.State;
            this.Id = workItem.Id;
        }
    }
}