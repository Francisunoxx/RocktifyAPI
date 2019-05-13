using BLL.Interfaces;
using ViewModel;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace RocktifyAPI.Controllers
{
    [RoutePrefix("api/v1/authentication")]
    public class AuthenticationController : ApiController
    {
        private readonly IAccountService ias;

        public AuthenticationController() { }
        public AuthenticationController(IAccountService ias)
        {
            this.ias = ias;
        }

        [Route("signin")]
        [HttpPost]
        public IHttpActionResult Login([FromBody] JObject jObject)
        {
            Registration r = new Registration();
            UserAccount ua = new UserAccount();

            foreach (JProperty x in (JToken)jObject)
            {
                string name = x.Name;
                JToken value = x.Value;

                foreach (var i in r.GetType().GetProperties())
                {
                    if (name.Equals(i.Name))
                    {
                        r.GetType().GetProperty(name).SetValue(r, value.ToString());

                    }
                }

                foreach (var i in ua.GetType().GetProperties())
                {
                    if (name.Equals(i.Name))
                    {
                        ua.GetType().GetProperty(name).SetValue(ua, value.ToString());

                    }
                }
            }

            User user = new User();
            user.Registration = r;
            user.UserAccount = ua;

            return Ok(this.ias.ServeValidateAccount(user));
        }
    }
}
