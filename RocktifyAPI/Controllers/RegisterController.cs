using BLL.Interfaces;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RocktifyAPI.Controllers
{
    [RoutePrefix("api/v1/register")]
    public class RegisterController : ApiController
    {
        private readonly IAccountService ias;

        public RegisterController() { }

        public RegisterController(IAccountService ias)
        {
            this.ias = ias;
        }



        [Route("test")]
        [HttpGet]
        public void Test(int y)
        {
            var a = 0;
            var b = 0;
            var w = a / b;
        }

        [Route("check-email")]
        [HttpGet]
        public IHttpActionResult CheckEmail(Registration registration)
        {
            return Ok(this.ias.ServeCheckEmail(registration));
        }

        [Route("check-username")]
        [HttpGet]
        public IHttpActionResult CheckUserName(Registration registration)
        {
            return Ok(this.ias.ServeCheckUserName(registration));
        }


        [Route("create")]
        [HttpPost]
        public IHttpActionResult CreateUser(Registration registration)
        {
            return Ok(this.ias.ServeCreateUser(registration));
        }
    }
}
