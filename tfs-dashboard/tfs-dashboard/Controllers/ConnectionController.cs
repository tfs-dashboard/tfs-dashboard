using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using tfs_dashboard.Models;
using tfs_dashboard.Models.Errors;
using tfs_dashboard.Repositories;

namespace tfs_dashboard.Controllers
{
    public class ConnectionController : BaseController
    {
        public JsonResult GetCollectionInfo(string url)
        {
            try
            {
                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    throw new InvalidInputException(string.Format("Provided URL \"{0}\" is invalid", url));
                Session["TfsUrl"] = url;
                var teamServer = TeamCollectionRepository.GetTeamConfigurationServer(new Uri(url));
                var tfsCol = TeamCollectionRepository.Get(teamServer);
                Session["TeamCollection"] = tfsCol;
                return Json(tfsCol, JsonRequestBehavior.AllowGet);
            }
            catch (InvalidInputException ex)
            {
                return ReturnError(ex);
            }
            catch (Exception ex)
            {
                LogError(ex);
                return ReturnError(ex);
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


            TeamCollection selectedCollection = collection.First(m => m.Name == collectionName);
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
            var queryItem = queryFolder["Shared Queries"];
            queryFolder = queryItem as QueryFolder;

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
            TeamItemStore store = new TeamItemStore(result, workItemStore);
            return Json(store);
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

            return queries.Cast<QueryItem>().FirstOrDefault(item => item.Name == name);
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

            var queryItems = new List<QueryItem>();
            foreach (var item in queryFolder)
            {
                var type = item.GetType();
                if (type.Name == "QueryFolder")
                {
                    IEnumerable subQueryItems = GetAllContainedQueriesList(item as QueryFolder);
                    queryItems.AddRange(subQueryItems.Cast<QueryItem>());
                }
                else
                {
                    queryItems.Add(item);
                }
            }
            return queryItems;
        }


        private static IEnumerable GetQueriesNames(IEnumerable queryFolder)
        {
            return (from QueryItem item in queryFolder select String.Format(item.Name)).ToList();
        }
    }
}
