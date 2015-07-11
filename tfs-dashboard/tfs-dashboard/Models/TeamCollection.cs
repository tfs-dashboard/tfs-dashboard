using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;

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