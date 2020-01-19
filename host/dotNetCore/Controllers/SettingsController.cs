using Host.Models;
using Host.Statics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private EWei _ewei = null;

        private readonly IHttpClientFactory _clientFactory;

        public SettingsController(IOptions<EWei> optionsAccessor,
IHttpClientFactory clientFactory)
        {
            _ewei = optionsAccessor.Value;
            _clientFactory = clientFactory;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<EWei> Get()
        {
            return _ewei;
        }

        // GET api/values/5
        [HttpGet("js")]
        public async Task<ActionResult<string>> GetJS()
        {
            var client = _clientFactory.CreateClient();
            var values = new List<KeyValuePair<string, string>>() {
                        new KeyValuePair<string, string>("resource", _ewei.PaResourceUri),
                        new KeyValuePair<string, string>("client_id", _ewei.PaClientId),
                        new KeyValuePair<string, string>("client_secret", _ewei.PaKey),
                        new KeyValuePair<string, string>("grant_type", "client_credentials"),
                         // expires_in
                        // new KeyValuePair<string, string>("expires_in", (10 * 60 * 60).ToString()),
                        };

            var request = new HttpRequestMessage(HttpMethod.Post, _ewei.PaAuthority)
            {
                Headers = {
                    { HttpRequestHeader.Accept.ToString(), "application/x-www-form-urlencoded" },
                },
                Content = new FormUrlEncodedContent(values)

            };

            var response = await client.SendAsync(request);


            var streamResult = await response.Content.ReadAsStringAsync();
            var result = JSON.AsObj<RefreshAuthenticateResult>(streamResult);




            var json = JSON.ToJson(new EWeiFront
            {
                AccessToken = result.AccessToken,
                PlatformUrl = _ewei.PlatformUrl
            });
            var js = $@"
var _platForm = {json};
";

            return new JavaScriptResult(js);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
