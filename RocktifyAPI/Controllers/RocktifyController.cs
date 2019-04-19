using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace RocktifyAPI.Controllers
{
    [RoutePrefix("api/v1/rocktify")]
    public class RocktifyController : ApiController
    {
        private readonly IRocktifyService irs;

        public RocktifyController() { }
        public RocktifyController(IRocktifyService irs)
        {
            this.irs = irs;
        }

        [Route("get-spotify-token")]
        [HttpGet]
        public IHttpActionResult GetSpotifyToken()
        {
            return Ok(this.irs.ServeAccessToken());
        }
    }
}
