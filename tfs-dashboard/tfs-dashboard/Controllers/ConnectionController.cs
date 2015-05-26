using Microsoft.TeamFoundation.Client;
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
        public JsonResult GetCollectionInfo()
        {
            try
            {
                TfsConfigurationServer teamServer = (TfsConfigurationServer)Session["TeamConfigurationServer"];
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
            var tfsCol = Session["TeamCollection"];
            IEnumerable<TeamCollection> collection = (IEnumerable<TeamCollection>)tfsCol;
            if (tfsCol == null)
            {
                throw new Exception();
            }
            TeamCollection selectedCollection = collection.Where(m => m.Name == collectionName).First();
            Session["SelectedCollection"] = selectedCollection = TeamProjectRepository.Get(selectedCollection);
            var projectList = selectedCollection.Projects;
            return Json(projectList, JsonRequestBehavior.AllowGet); 
            
        }

        public void GetTeamServerConfig(string url)
        {
            try
            {
                var teamServer = TeamCollectionRepository.GetTeamConfigurationServer(new Uri(url));
                Session["TeamConfigurationServer"] = teamServer;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
