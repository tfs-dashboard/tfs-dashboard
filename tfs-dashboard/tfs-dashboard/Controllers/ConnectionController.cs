using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tfs_dashboard.Repositories;

namespace tfs_dashboard.Controllers
{
    public class ConnectionController : Controller
    {
        public JsonResult GetCollectionInfo()
        {
        //    NetworkCredential netCred = new NetworkCredential("dawid.swietoslawski@pl.abb.com", "");
        //    BasicAuthCredential basicCred = new BasicAuthCredential(netCred);
        //    TfsClientCredentials tfsCred = new TfsClientCredentials(basicCred);
        //    tfsCred.AllowInteractive = false;
            var tfsCol = TeamCollectionRepository.Get(new Uri("https://dawidswietoslawski.visualstudio.com"));

            var projectsList = tfsCol.ToList();
            //tfsCon.Authenticate();
            ////var workItems = new WorkItemStore(tfsCon);
            //var projectsList = (from Project p in workItems.Projects select p.Name).ToList();
            //var projectsList = new List<string>();
            //projectsList.Add("aa");
            //projectsList.Add("bb");
            //projectsList.Add("cc");

            return Json(tfsCol, JsonRequestBehavior.AllowGet);
        }
    }
}
