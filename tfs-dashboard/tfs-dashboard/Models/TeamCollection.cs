using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace tfs_dashboard.Models
{
    public class TeamCollection
    {
        public Guid InstanceId { get; set; }
        [ScriptIgnore]
        public CatalogNode CollectionNode { get; set; }
        [ScriptIgnore]
        public TfsTeamProjectCollection Collection { get; set; }
        public ICollection<TeamProject> Projects { get; set; }
        public string Name { get; set; }
    }
}