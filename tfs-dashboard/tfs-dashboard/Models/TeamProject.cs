﻿using Microsoft.TeamFoundation.Framework.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfs_dashboard.Models
{
    public class TeamProject
    {
        public string Name { get; set; }

        public TeamProject(CatalogNode node)
        {
            Name = node.Resource.DisplayName;
        }
    }
}