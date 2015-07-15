using System;
using System.Net;
using System.Web.Mvc;
using Elmah;
using Error = tfs_dashboard.Models.Errors.Error;

namespace tfs_dashboard.Controllers
{
    public class BaseController : Controller
    {
        protected JsonResult ReturnError(Exception ex)
        {
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Json(new Error(ex.Message));
        }

        protected void LogError(Exception ex)
        {
            ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}