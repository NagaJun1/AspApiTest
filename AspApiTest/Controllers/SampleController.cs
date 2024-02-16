using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace AspApiTest.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase {

        private const string ClaimA = "ClaimA";

        private const string SESSION_KEY_A = "SESSION_KEY_A";

        private readonly ILogger<SampleController> _logger;

        public SampleController(ILogger<SampleController> logger) {
            _logger = logger;
        }

        [HttpGet]
        [Route("/set_cookie")]
        public string SetCookie(string key, string value) {
            this.Response.Cookies.Append(key, value);
            return "success";
        }

        [HttpGet]
        [Route("/get_cookie")]
        public string? GetCookie(string key) {
            this.Request.Cookies.TryGetValue(key, out var value);
            return value;
        }

        [HttpGet]
        [Route("/set_session")]
        public string SetSession([FromQuery(Name = "value")] int value) {
            this.HttpContext.Session.Set(SESSION_KEY_A, BitConverter.GetBytes(value));

            // 設定が不足しているので動かない
            return "success";
        }

        [HttpGet]
        [Route("/get_session")]
        public int GetSessionValue() {
            // 設定が不足しているので動かない
            return BitConverter.ToInt32(this.HttpContext.Session.Get(SESSION_KEY_A));
        }

        [HttpGet]
        [Route("/set_claim")]
        public String SetClaim() {

            ClaimsIdentity identity = new ClaimsIdentity();

            identity.AddClaim(new Claim(ClaimA, "first"));
            this.HttpContext.User.AddIdentity(identity);

            // Claim 書換できないのでは？
            // というか、ログイン時以外は書き込みできない仕様では？
            return "set success";
        }

        [HttpGet]
        [Route("/get_claim")]
        public string GetClaim() {

            if (this.HttpContext.User.Identity is ClaimsIdentity identity
                && identity.FindFirst(ClaimA) is Claim claim) {
                return claim.Value;
            }
            return "miss";
        }

        /// <summary>
        /// https://localhost:7105/sample にアクセスで、「request success」を返却
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/sample")]
        public string Sample([FromQuery(Name = "param1")] string? param1) {

            if (string.IsNullOrWhiteSpace(param1)) {
                return "param1 is null or white space";
            }

            return "request success \nparam1 = " + param1;
        }
    }
}
