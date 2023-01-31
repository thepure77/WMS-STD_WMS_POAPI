using System;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlanGRBusiness.PlanGoodsReceiveItem;

namespace PlanGRAPI.Controllers
{
    [Route("api/PurchaseOrderItem")]
    [ApiController]
    public class PurchaseOrderItemController : ControllerBase
    {
        private PODbContext context;

        public PurchaseOrderItemController(PODbContext context)
        {
            this.context = context;
        }

        #region getByPlanGoodReceiveId
        [HttpGet("GetByPurchaseOrderItemId/{id}")]
        public IActionResult GetByPlanGoodReceiveId(Guid id)
        {
            try
            {
                PurchaseOrderItemService service = new PurchaseOrderItemService(context);
                var result = service.GetByPurchaseOrderItemId(id);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion


    }
}