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
        public JsonResult GetCollectionInfo(string url)
        
        {
            //tfsCol = TeamCollectionRepository.Get(new Uri("http://tfs2.de.abb.com:8080/tfs/"));
            var tfsCol = TeamCollectionRepository.Get(new Uri(url));
            Session["tfsCol"] = tfsCol;
            return Json(tfsCol, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProjectInfo(string collectionName)
        {
            var tfsCol = Session["tfsCol"];
            IEnumerable<TeamCollection> collection = (IEnumerable<TeamCollection>)tfsCol;
            if (tfsCol == null)
            {
                throw new Exception();
            }
            TeamCollection selectedCollection = collection.Where(m => m.Name == collectionName).First();
            selectedCollection = TeamProjectRepository.Get(selectedCollection);
            var projectList = selectedCollection.Projects;
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }
    }
}
