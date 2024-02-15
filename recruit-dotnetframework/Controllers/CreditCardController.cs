using recruit_dotnetframework.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

using recruit_dotnetframework.ApiClients;

namespace recruit_dotnetframework.Controllers
{
    public class CreditCardController : ApiController
    {
        private readonly IExternalApiClient _externalApiClient;

        public CreditCardController(IExternalApiClient externalApiService)
        {
            _externalApiClient = externalApiService;
        }
        [HttpPost]
        public async Task<IHttpActionResult> PostCardInformation(CreditCard creditCard)
        {
            try
            {
                var result = await _externalApiClient.ValidateCreditCardInformation(creditCard);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
