using System;
using System.Collections.Generic;
using DataAccess;
using GRBusiness;
using GRBusiness.PlanGoodsReceive;
using MasterDataBusiness.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlanGRBusiness.PlanGoodsReceive;
using PlanGRBusiness.ViewModels;
using POBusiness.PopupPurchaseOrderBusiness;
using PopupPurchaseOrderBusiness;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanGRAPI.Controllers
{
    [Route("api/Popup")]
    public class PopupController : Controller
    {
        private PODbContext context;

        public PopupController(PODbContext context)
        {
            this.context = context;
        }

        #region PopupPlanPOfilter
        [HttpPost("popupPlanPOfilter")]
        public IActionResult popupPlanPOfilter([FromBody]JObject body)
        {
            try
            {
                var service = new PopupPurchaseOrderService(context);
                var Models = new PopupPurchaseOrderDocViewModel();
                Models = JsonConvert.DeserializeObject<PopupPurchaseOrderDocViewModel>(body.ToString());
                var result = service.popupPlanPOfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        [HttpPost("GetPlanPOIfilter")]
        public IActionResult GetPlanPOIfilter([FromBody]JObject body)
        {
            try
            {
                var service = new PopupPurchaseOrderService(context);
                var Models = new View_GetPurchaseOrderItemViewModel();
                Models = JsonConvert.DeserializeObject<View_GetPurchaseOrderItemViewModel>(body.ToString());
                var result = service.GetPlanPOIfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("GetPlanPOIPendingfilter")]
        public IActionResult GetPlanPOIPendingfilter([FromBody]JObject body)
        {
            try
            {
                var service = new PopupPurchaseOrderService(context);
                var Models = new View_GetPurchaseOrderItemViewModel();
                Models = JsonConvert.DeserializeObject<View_GetPurchaseOrderItemViewModel>(body.ToString());
                var result = service.GetPlanPOIPendingfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
