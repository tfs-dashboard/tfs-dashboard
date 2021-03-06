﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Common;
using tfs_dashboard.Models;

namespace tfs_dashboard.Repositories
{
    public static class TeamCollectionRepository
    {
        private static readonly IEnumerable<Guid> TeamObjectTypes = new[] {CatalogResourceTypes.ProjectCollection};

        public static IEnumerable<TeamCollection> Get(TfsConfigurationServer teamServer)
        {
            var teamCollections = new Collection<TeamCollection>();
            foreach (var collectionNode in teamServer.CatalogNode.QueryChildren(TeamObjectTypes, false, CatalogQueryOptions.None))
            {
                var instanceId = new Guid(collectionNode.Resource.Properties["InstanceID"]);
                teamCollections.Add(new TeamCollection
                {
                    InstanceId = instanceId,
                    CollectionNode = collectionNode,
                    Name = collectionNode.Resource.DisplayName,
                    Collection = teamServer.GetTeamProjectCollection(instanceId),
                    Projects = new Collection<TeamProject>()
                });
            }
            return teamCollections;
        }

        public static TfsConfigurationServer GetTeamConfigurationServer(Uri tfsUrl)
        {
            return TfsConfigurationServerFactory.GetConfigurationServer(tfsUrl);
        }
    }
}