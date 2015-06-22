﻿using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tfs_dashboard.Models;
using tfs_dashboard.Repositories;

namespace tfs_dashboard.Controllers
{
    public class ConnectionController : Controller
    {
        public JsonResult GetCollectionInfo(string url)
        {
            try
            {
                Session["TfsUrl"] = url;
                var teamServer = TeamCollectionRepository.GetTeamConfigurationServer(new Uri(url));
                var tfsCol = TeamCollectionRepository.Get(teamServer);
                Session["TeamCollection"] = tfsCol;
                return Json(tfsCol, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw new Exception();
            }
        }

        public JsonResult GetProjectInfo(string collectionName)
        {

            Session["SelectedCollection"] = collectionName;
            var tfsCol = Session["TeamCollection"];

            IEnumerable<TeamCollection> collection = (IEnumerable<TeamCollection>)tfsCol;
            if (tfsCol == null)
            {
                throw new Exception();
            }


            TeamCollection selectedCollection = collection.Where(m => m.Name == collectionName).First();
            selectedCollection = TeamProjectRepository.Get(selectedCollection);
            var projectList = selectedCollection.Projects;
            GetWorkItemStore();
            return Json(projectList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetSharedQueriesList(string projectName)
        {
            WorkItemStore workItemStore = (WorkItemStore)Session["WorkItemStore"];
            var project = workItemStore.Projects[projectName];

            QueryHierarchy query = project.QueryHierarchy;
            var queryFolder = query as QueryFolder;
            QueryItem queryItem = queryFolder["Shared Queries"];
            queryFolder = queryItem as QueryFolder;

            JsonResult x = Json(GetQueriesNames(GetAllContainedQueriesList(queryFolder)));
            IEnumerable<string> names = (IEnumerable<string>)GetQueriesNames(GetAllContainedQueriesList(queryFolder));

            return Json(names, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetWorkItems(string queryName, string projectName)
        {
            WorkItemStore workItemStore = (WorkItemStore)Session["WorkItemStore"];
            QueryItem sel = GetQueryByName(queryName, projectName);
            QueryDefinition def = (QueryDefinition)sel;
            IDictionary project = new Dictionary<string, string>();
            project.Add("project", projectName);
            WorkItemCollection result = workItemStore.Query(def.QueryText, project);

            TeamItemStore store = new TeamItemStore();
            store = PopulateMembers(store, result);
            return Json(store);
        }

        private TeamItemStore PopulateMembers(TeamItemStore store, WorkItemCollection collection)
        {
            store.members = new List<Member>();
            List<string> names = new List<string>();
            foreach (WorkItem workitem in collection)
            {
                names.Add((string)workitem["Assigned To"]);
            }
            names = names.Distinct().ToList();

            foreach (string name in names)
            {
                store.AddMember(name);
            }

            foreach (WorkItem workItem in collection)
            {
                string assignedTo = (string)workItem["Assigned To"];
                switch (workItem.Type.Name)
                {
                    case "Bug":
                        store.members.Add(new Bug() { Title});
                        break;
                    case "Requirement":
                        break;
                    case "Change Request":
                        break;
                }
            }

            return store;
        }

        private QueryItem GetQueryByName(string name, string projectName)
        {

            WorkItemStore workItemStore = (WorkItemStore)Session["WorkItemStore"];
            var project = workItemStore.Projects[projectName];

            QueryHierarchy query = project.QueryHierarchy;
            var queryFolder = query as QueryFolder;
            QueryItem queryItem = queryFolder["Shared Queries"];
            queryFolder = queryItem as QueryFolder;

            IEnumerable queries = GetAllContainedQueriesList(queryFolder);

            foreach (QueryItem item in queries)
            {
                if (item.Name == name) return item;
            }
            return null;
        }

        private void GetWorkItemStore()
        {
            try
            {
                string tfsUrl = (string)Session["TfsUrl"];
                string collectionName = (string)Session["SelectedCollection"];
                Uri teamCollectionUri = new Uri(tfsUrl + "/" + collectionName);
                WorkItemStore workItemStore = WorkItemRepository.Get(teamCollectionUri);
                Session["WorkItemStore"] = workItemStore;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private IEnumerable GetAllContainedQueriesList(QueryFolder queryFolder)
        {

            List<QueryItem> queryItems = new List<QueryItem>();
            foreach (QueryItem item in queryFolder)
            {
                var type = item.GetType();
                if (type.Name == "QueryFolder")
                {
                    IEnumerable subQueryItems = GetAllContainedQueriesList(item as QueryFolder);
                    foreach (QueryItem subItem in subQueryItems)
                    {
                        queryItems.Add(subItem);
                    }
                }
                else
                {
                    queryItems.Add(item);
                }
            }
            return queryItems;
        }


        private IEnumerable GetQueriesNames(IEnumerable queryFolder)
        {
            List<string> names = new List<string>();
            foreach (QueryItem item in queryFolder)
            {
                names.Add(String.Format(item.Name));
            }
            return names;
        }
    }
}
