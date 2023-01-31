using System;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace PlanGRAPI.Controllers
{
    [Route("api/FilterTable")]
    [ApiController]
    public class FilterTableController : ControllerBase
    {
        private PODbContext context;

        public FilterTableController(PODbContext context)
        {
            this.context = context;
        }






    }
}