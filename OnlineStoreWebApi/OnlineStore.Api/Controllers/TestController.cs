using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineStore.Api.Controllers
{
    public class TestController : ApiController
    {
       
        //[Authorize]
        public TestController()
        {

        }

        [HttpGet]
        [Authorize(Roles ="Demo")]
        public List<string> Get()
        {
            var firmaList = new List<string>()
            {
                "Firma 1",
                "Firma 2",
                "Firma 3",
            };

            return firmaList;
        }
    }
}
