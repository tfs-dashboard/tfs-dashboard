using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfs_dashboard.Models
{
    public class Member
    {
        public string Name;
        public List<Bug> BugsAssigned;
        public List<ChangeRequest> ChangeRequestsAssigned;
        public List<Requirement> RequirementsAssigned;
    }
}