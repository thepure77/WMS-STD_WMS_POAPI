using System;
using System.Collections.Generic;
using DataAccess;
using GRBusiness;
using GRBusiness.PlanGoodsReceive;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanGRAPI.Controllers
{
    [Route("api/Autocomplete")]
    public class AutocompleteController : Controller
    {
        private PODbContext context;

        public AutocompleteController(PODbContext context)
        {
            this.context = context;
        }


        #region AutobasicSuggestion
        [HttpPost("autobasicSuggestion")]
        public IActionResult autobasicSuggestion([FromBody]JObject body)

        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autobasicSuggestion(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutobasicSuggestionPO
        [HttpPost("autobasicSuggestionPO")]
        public IActionResult autobasicSuggestionPO([FromBody]JObject body)

        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autobasicSuggestionPO(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region AutobasicSuggestionVender
        [HttpPost("autobasicSuggestionVender")]
        public IActionResult autobasicSuggestionVender([FromBody]JObject body)

        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autobasicSuggestionVender(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion


        #region AutoOwnerfilter
        [HttpPost("autoOwnerfilter")]
        public IActionResult autoOwnerfilter([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoOwnerfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoVenderfilter

        [HttpPost("autoVenderfilter")]
        public IActionResult autoVenderfilter([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoVenderfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoStatusfilter
        [HttpPost("autoStatusfilter")]
        public IActionResult autoStatusfilter([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoStatusfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoWarehousefilter
        [HttpPost("autoWarehousefilter")]
        public IActionResult autoWarehousefilter([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoWarehousefilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoDocumentTypefilter
        [HttpPost("autoDocumentTypefilter")]
        public IActionResult autoDocumentTypefilter([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoDocumentTypefilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoUser
        [HttpPost("autoUser")]
        public IActionResult autoUser([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoUser(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoSkufilter
        [HttpPost("autoSkufilter")]
        public IActionResult autoSkufilter([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoSkufilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region AutoProductfilter
        [HttpPost("autoProductfilter")]
        public IActionResult autoProdutfilter([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoProductfilter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoOwnerfilterName
        [HttpPost("autoOwnerfilterName")]
        public IActionResult autoOwnerfilterName([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoOwnerfilterName(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoPurchaseOrderNoAndOwner
        [HttpPost("autoPurchaseOrderNoAndOwner")]
        public IActionResult autoPurchaseOrderNoAndOwner([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoPurchaseOrderNoAndOwner(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
