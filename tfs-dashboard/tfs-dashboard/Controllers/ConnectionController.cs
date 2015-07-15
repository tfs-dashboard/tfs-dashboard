using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                var tfsCollections = TeamCollectionRepository.Get(teamServer);
                Session["TeamCollection"] = tfsCollections;
                return Json(tfsCollections, JsonRequestBehavior.AllowGet);
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
            var tfsCollections = (IEnumerable<TeamCollection>) Session["TeamCollection"];

            if (tfsCollections == null)
                throw new Exception("Couldn't load the list of TFS collections from session object.");

            var selectedCollection = tfsCollections.First(m => m.Name == collectionName);
            var projectList = TeamProjectRepository.Get(selectedCollection).Projects;

            GetWorkItemStore();
            return Json(projectList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetSharedQueriesList(string projectName)
        {
            var workItemStore = (WorkItemStore) Session["WorkItemStore"];
            var project = workItemStore.Projects[projectName];

            var queryItem = project.QueryHierarchy["Shared Queries"];
            var queryFolder = queryItem as QueryFolder;

            var names = (IEnumerable<string>) GetQueriesNames(GetAllContainedQueriesList(queryFolder));

            return Json(names, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetWorkItems(string queryName, string projectName)
        {
            var workItemStore = (WorkItemStore) Session["WorkItemStore"];
            var sel = (QueryDefinition) GetQueryByName(queryName, projectName);
            var result = workItemStore.Query(sel.QueryText, new Dictionary<string,string>{{"project", projectName}});
            var store = new TeamItemStore(result, workItemStore);
            return Json(store);
        }

        private QueryItem GetQueryByName(string name, string projectName)
        {

            var workItemStore = (WorkItemStore) Session["WorkItemStore"];
            var project = workItemStore.Projects[projectName];

            var queryFolder = (QueryFolder) project.QueryHierarchy;
            var queryItem = queryFolder["Shared Queries"];
            queryFolder = queryItem as QueryFolder;

            var queries = GetAllContainedQueriesList(queryFolder);

            return queries.Cast<QueryItem>().FirstOrDefault(item => item.Name == name);
        }

        private void GetWorkItemStore()
        {
            var tfsUrl = (string) Session["TfsUrl"];
            var collectionName = (string) Session["SelectedCollection"];
            var teamCollectionUri = new Uri(tfsUrl.TrimEnd('/') + "/" + collectionName);
            Session["WorkItemStore"] = WorkItemRepository.Get(teamCollectionUri);
        }

        private IEnumerable GetAllContainedQueriesList(QueryFolder queryFolder)
        {

            var queryItems = new List<QueryItem>();
            foreach (var item in queryFolder)
            {
                if (item.GetType().Name == "QueryFolder")
                {
                    var subQueryItems = GetAllContainedQueriesList(item as QueryFolder);
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
            return (from QueryItem item in queryFolder select item.Name).ToList();
        }
    }
}
