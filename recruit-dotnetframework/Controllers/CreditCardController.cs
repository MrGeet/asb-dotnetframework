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



namespace recruit_dotnetframework.Controllers
{
    public class CreditCardController : ApiController
    {
    

  
        [HttpPost]
        public async Task<IHttpActionResult> PostCardInformation(CreditCard creditCard)
        {


                return Ok();
        

        }
    }
}
