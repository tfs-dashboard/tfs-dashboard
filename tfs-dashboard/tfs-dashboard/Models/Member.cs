using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfs_dashboard.Models
{
    public class Member
    {
        List<Bug> BugsAssigned;
        List<ChangeRequest> ChangeRequestsAssigned;
        List<Requirement> RequirementsAssigned;
    }
}