using System;
using System.Collections.Generic;
using DataAccess;
using GRBusiness;
using GRBusiness.PlanGoodsReceive;
using MasterDataBusiness.CargoType;
using MasterDataBusiness.ContainerType;
using MasterDataBusiness.CostCenter;
using MasterDataBusiness.Currency;
using MasterDataBusiness.DockDoor;
using MasterDataBusiness.DocumentPriority;
using MasterDataBusiness.ShipmentType;
using MasterDataBusiness.VehicleType;
using MasterDataBusiness.ViewModels;
using MasterDataBusiness.Volume;
using MasterDataBusiness.Weight;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlanGRBusiness.PlanGoodsReceive;
using PlanGRBusiness.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlanGRAPI.Controllers
{
    [Route("api/Dropdown")]
    public class DropdownController : Controller
    {
        private PODbContext context;

        public DropdownController(PODbContext context)
        {
            this.context = context;
        }

        #region DropdownDocumentType
        [HttpPost("dropdownDocumentType")]
        public IActionResult dropdownDocumentType([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new DocumentTypeViewModel();
                Models = JsonConvert.DeserializeObject<DocumentTypeViewModel>(body.ToString());
                var result = service.DropdownDocumentType(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region DropdownStatus
        [HttpPost("dropdownStatus")]
        public IActionResult DropdownStatus([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ProcessStatusViewModel();
                Models = JsonConvert.DeserializeObject<ProcessStatusViewModel>(body.ToString());
                var result = service.dropdownStatus(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region DropdownWarehouse
        [HttpPost("dropdownWarehouse")]
        public IActionResult DropdownWarehouse([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new warehouseDocViewModel();
                Models = JsonConvert.DeserializeObject<warehouseDocViewModel>(body.ToString());
                var result = service.dropdownWarehouse(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region DropdownRound
        [HttpPost("dropdownRound")]
        public IActionResult dropdownRound([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new roundDocViewModel();
                Models = JsonConvert.DeserializeObject<roundDocViewModel>(body.ToString());
                var result = service.dropdownRound(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownProductconversion
        [HttpPost("dropdownProductconversion")]
        public IActionResult dropdownProductconversion([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ProductConversionViewModelDoc();
                Models = JsonConvert.DeserializeObject<ProductConversionViewModelDoc>(body.ToString());
                var result = service.dropdownProductconversion(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownItemStatus
        [HttpPost("dropdownItemStatus")]
        public IActionResult dropdownItemStatus([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new ItemStatusDocViewModel();
                Models = JsonConvert.DeserializeObject<ItemStatusDocViewModel>(body.ToString());
                var result = service.dropdownItemStatus(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownVehicle
        [HttpPost("dropdownVehicle")]
        public IActionResult dropdownTypeCar([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new VehicleViewModel();
                Models = JsonConvert.DeserializeObject<VehicleViewModel>(body.ToString());
                var result = service.dropdownVehicle(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownTransport
        [HttpPost("dropdownTransport")]
        public IActionResult dropdownTransport([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService(context);
                var Models = new TransportViewModel();
                Models = JsonConvert.DeserializeObject<TransportViewModel>(body.ToString());
                var result = service.dropdownTransport(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownCostCenter
        [HttpPost("dropdownCostCenter")]
        public IActionResult dropdownCostCenter([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new CostCenterViewModel();
                Models = JsonConvert.DeserializeObject<CostCenterViewModel>(body.ToString());
                var result = service.dropdownCostCenter(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownWeight
        [HttpPost("dropdownWeight")]
        public IActionResult dropdownWeight([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new WeightViewModel();
                Models = JsonConvert.DeserializeObject<WeightViewModel>(body.ToString());
                var result = service.dropdownWeight(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownCurrency
        [HttpPost("dropdownCurrency")]
        public IActionResult dropdownCurrency([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new CurrencyViewModel();
                Models = JsonConvert.DeserializeObject<CurrencyViewModel>(body.ToString());
                var result = service.dropdownCurrency(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion


        #region dropdownVolume
        [HttpPost("dropdownVolume")]
        public IActionResult dropdownVolume([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new VolumeViewModel();
                Models = JsonConvert.DeserializeObject<VolumeViewModel>(body.ToString());
                var result = service.dropdownVolume(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion


        #region dropdownShipmentType
        [HttpPost("dropdownShipmentType")]
        public IActionResult dropdownShipmentType([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new ShipmentTypeViewModel();
                Models = JsonConvert.DeserializeObject<ShipmentTypeViewModel>(body.ToString());
                var result = service.dropdownShipmentType(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownContainerType
        [HttpPost("dropdownContainerType")]
        public IActionResult dropdownContainerType([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new ContainerTypeViewModelV2();
                Models = JsonConvert.DeserializeObject<ContainerTypeViewModelV2>(body.ToString());
                var result = service.dropdownContainerType(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownDockDoor
        [HttpPost("dropdownDockDoor")]
        public IActionResult dropdownDockDoor([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new DockDoorViewModelV2();
                Models = JsonConvert.DeserializeObject<DockDoorViewModelV2>(body.ToString());
                var result = service.dropdownDockDoor(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownVehicleType
        [HttpPost("dropdownVehicleType")]
        public IActionResult dropdownVehicleType([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new VehicleTypeViewModel();
                Models = JsonConvert.DeserializeObject<VehicleTypeViewModel>(body.ToString());
                var result = service.dropdownVehicleType(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownUnloadingType
        [HttpPost("dropdownUnloadingType")]
        public IActionResult dropdownUnloadingType([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new UnloadingTypeViewModel();
                Models = JsonConvert.DeserializeObject<UnloadingTypeViewModel>(body.ToString());
                var result = service.dropdownUnloadingType(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownCargoType
        [HttpPost("dropdownCargoType")]
        public IActionResult dropdownCargoType([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new CargoTypeViewModel();
                Models = JsonConvert.DeserializeObject<CargoTypeViewModel>(body.ToString());
                var result = service.dropdownCargoType(Models);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
        #endregion

        #region dropdownDocumentPriority
        [HttpPost("dropdownDocumentPriority")]
        public IActionResult dropdownDocumentPriority([FromBody]JObject body)
        {
            try
            {
                var service = new PurchaseOrderService();
                var Models = new DocumentPriorityViewModel();
                Models = JsonConvert.DeserializeObject<DocumentPriorityViewModel>(body.ToString());
                var result = service.dropdownDocumentPriority(Models);
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
