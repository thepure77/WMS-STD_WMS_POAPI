using System;
using System.IO;
using System.Net;
using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using GRBusiness;
using GRBusiness.PlanGoodsReceive;
using POBusiness.PlanGoodsReceive;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace POAPI.Controllers
{
    [Route("api/PurchaseOrder")]
    public class PurchaseOrderController : Controller
    {
        private PODbContext context;

        private readonly IHostingEnvironment _hostingEnvironment;
        public PurchaseOrderController(PODbContext context, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            _hostingEnvironment = hostingEnvironment;
        }


        #region filter
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new SearchDetailModel();
                Models = JsonConvert.DeserializeObject<SearchDetailModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        #endregion

        #region CreateOrUpdate
        [HttpPost("createOrUpdate")]
        public IActionResult CreateOrUpdate([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new PurchaseOrderDocViewModel();
                Models = JsonConvert.DeserializeObject<PurchaseOrderDocViewModel>(body.ToString());
                var result = service.CreateOrUpdate(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region find
        [HttpGet("find/{id}")]
        public IActionResult find(Guid id)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var result = service.find(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region find
        [HttpGet("findbyYard/{id}")]
        public IActionResult findbyYard(string id)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var result = service.findbyYard(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion


        #region Delete
        [HttpPost("delete")]
        public IActionResult Delete([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new PurchaseOrderDocViewModel();
                Models = JsonConvert.DeserializeObject<PurchaseOrderDocViewModel>(body.ToString());
                var result = service.Delete(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region ConfirmStatus
        [HttpPost("confirmStatus")]
        public IActionResult ConfirmStatus([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new PurchaseOrderDocViewModel();
                Models = JsonConvert.DeserializeObject<PurchaseOrderDocViewModel>(body.ToString());
                var result = service.confirmStatus(Models);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region updateUserAssign
        [HttpPost("updateUserAssign")]
        public IActionResult updateUserAssign([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new PurchaseOrderDocViewModel();
                Models = JsonConvert.DeserializeObject<PurchaseOrderDocViewModel>(body.ToString());
                var result = service.updateUserAssign(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region deleteUserAssign
        [HttpPost("deleteUserAssign")]
        public IActionResult deleteUserAssign([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new PurchaseOrderDocViewModel();
                Models = JsonConvert.DeserializeObject<PurchaseOrderDocViewModel>(body.ToString());
                var result = service.deleteUserAssign(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion



    }
}
