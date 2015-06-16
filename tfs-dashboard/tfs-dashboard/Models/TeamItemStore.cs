using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfs_dashboard.Models
{
    public class TeamItemStore
    {
       public ICollection<Member> members;

       public void AddMember(string name)
       {
           members.Add(new Member(){Name = name});
       }
    }
}