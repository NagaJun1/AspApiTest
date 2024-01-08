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
        [HttpGet(Name ="sample")]
        public String Sample() {
            return "request success";
        }
    }
}
