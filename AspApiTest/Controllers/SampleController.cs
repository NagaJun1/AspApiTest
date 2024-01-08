using Microsoft.AspNetCore.Mvc;

namespace AspApiTest.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase {

        private readonly ILogger<SampleController> _logger;

        public SampleController(ILogger<SampleController> logger) {
            _logger = logger;
        }

        /// <summary>
        /// https://localhost:7105/sample にアクセスで、「request success」を返却
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "sample")]
        public String Sample([FromQuery(Name = "param1")] string? param1) {

            if (String.IsNullOrWhiteSpace(param1)) {
                return "param1 is null or white space";
            }

            return "request success \nparam1 = " + param1;
        }
    }
}
