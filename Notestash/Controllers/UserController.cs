using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Notestash.Models;

namespace Notestash.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Create(UserModel objUser)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Model State Invalid");

            UserModel ob = new UserModel();
            bool created = ob.Create(objUser);

            if (created)
                return Request.CreateResponse(HttpStatusCode.Created, "Successfully Registered");
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Registration Unsuccessful");
        }
    }
}
