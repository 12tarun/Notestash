using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Notestash.Models;
using NotestashDataAccess;

namespace Notestash.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Check(LoginModel objUser)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Model State Invalid");

            LoginModel ob = new LoginModel();
            bool check = ob.Check(objUser);

            if (check)
                return Request.CreateResponse(HttpStatusCode.OK, "Welcome!");
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Login Failed!");
        }
    }
}
