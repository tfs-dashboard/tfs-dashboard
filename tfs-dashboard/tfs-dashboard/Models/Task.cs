using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfs_dashboard.Models
{
    public class Task
    {
        public int OriginalEstimate { get; private set; }
        public int RemainingWork { get; private set; }
        public int CompletedWork { get; private set; }

        public string Title { get; private set; }
        public string Status { get; protected set; }
        public int Id { get; private set; }
        public bool Blocked { get; private set; }
        public string AssignedTo { get; private set; }

        public string Type { get; private set; }

        public Task(WorkItem workItem)
        {
            foreach (Field field in workItem.Fields)
            {
                if (field.Name == "Blocked")
                {
                    this.Blocked = (string)field.Value == "Yes" ? true : false;
                }
                if (field.Name == "Assigned To")
                {
                    this.AssignedTo = (string)field.Value;
                }
                if (field.Name == "Original Estimate")
                {

                    double val = field.Value == null ? 0 : (double)field.Value;
                    this.OriginalEstimate = (int)val;
                }
                if (field.Name == "Completed Work")
                {
                    double val = field.Value == null ? 0 : (double)field.Value;
                    this.CompletedWork = (int)val;
                }
                if (field.Name == "Remaining Work")
                {
                    double val = field.Value == null ? 0 : (double)field.Value;
                    this.RemainingWork = (int)val;
                }
            }
            this.Status = workItem.State;
            this.Title = workItem.Title;
            this.Id = workItem.Id;
            this.Type = "Task";
        }
    }
}