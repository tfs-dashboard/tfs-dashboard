using Microsoft.TeamFoundation.WorkItemTracking.Client;

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
                    Blocked = (string)field.Value == "Yes";
                }
                if (field.Name == "Assigned To")
                {
                    AssignedTo = (string)field.Value;
                }
                if (field.Name == "Original Estimate")
                {

                    double val = field.Value == null ? 0 : (double)field.Value;
                    OriginalEstimate = (int)val;
                }
                if (field.Name == "Completed Work")
                {
                    double val = field.Value == null ? 0 : (double)field.Value;
                    CompletedWork = (int)val;
                }
                if (field.Name == "Remaining Work")
                {
                    double val = field.Value == null ? 0 : (double)field.Value;
                    RemainingWork = (int)val;
                }
            }
            Status = workItem.State;
            Title = workItem.Title;
            Id = workItem.Id;
            Type = "Task";
        }
    }
}