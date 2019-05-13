using BLL.Interfaces;
using ViewModel;
using Newtonsoft.Json.Linq;
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

        [Route("validate-email")]
        [HttpGet]
        public IHttpActionResult ValidateEmail([FromBody] Registration registration)
        {
            return Ok(this.ias.ServeValidateEmail(registration));
        }

        [Route("validate-username")]
        [HttpGet]
        public IHttpActionResult ValidateUserName([FromBody] Registration registration)
        {
            return Ok(this.ias.ServeValidateUsername(registration));
        }


        [Route("create")]
        [HttpPost]
        public IHttpActionResult CreateUser([FromBody] JObject userRegistration)
        {
            Registration r = new Registration();
            UserAccount ua = new UserAccount();

            foreach (JProperty x in (JToken)userRegistration)
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

            UserRegistration ur = new UserRegistration();
            ur.Registration = r;
            ur.UserAccount = ua;

            return Ok(this.ias.ServeCreateUser(ur));
        }
    }
}
