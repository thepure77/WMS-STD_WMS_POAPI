using GRBusiness.PlanGoodsReceive;
using PTTPL.TMS.Business.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace POBusiness.PlanGoodsReceive
{
    public class PurchaseOrderDocViewModel
    {
        [Key]
        public Guid? purchaseOrder_Index { get; set; }

        public Guid? owner_Index { get; set; }

        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public Guid? vendor_Index { get; set; }

        public string vendor_Id { get; set; }

        public string vendor_Name { get; set; }

        public Guid documentType_Index { get; set; }

        public string documentType_Id { get; set; }

        public string documentType_Name { get; set; }

        public string purchaseOrder_No { get; set; }

        public string purchaseOrder_Date { get; set; }
        public string purchaseOrder_Time { get; set; }


        public string purchaseOrder_Due_Date { get; set; }
        public string purchaseOrder_Due_DateTime { get; set; }

        public string documentRef_No1 { get; set; }

        public string documentRef_No2 { get; set; }

        public string documentRef_No3 { get; set; }

        public string documentRef_No4 { get; set; }

        public string documentRef_No5 { get; set; }

        public int? document_Status { get; set; }


        public string uDF_1 { get; set; }

        public string uDF_2 { get; set; }

        public string uDF_3 { get; set; }

        public string uDF_4 { get; set; }

        public string uDF_5 { get; set; }

        public int? documentpriority_Status { get; set; }


        public string document_Remark { get; set; }


        public string create_By { get; set; }


        public string create_date { get; set; }


        public string update_By { get; set; }


        public string update_date { get; set; }


        public string cancel_By { get; set; }


        public string cancel_date { get; set; }

        public Guid? warehouse_Index { get; set; }


        public string warehouse_Id { get; set; }


        public string warehouse_Name { get; set; }

        public Guid? warehouse_Index_To { get; set; }


        public string warehouse_Id_To { get; set; }


        public string warehouse_Name_To { get; set; }
        public string userAssign { get; set; }
        public string userAssignKey { get; set; }
        public string processStatus_Name { get; set; }
        public Guid? dock_Index { get; set; }
        public string dock_Id { get; set; }
        public string dock_Name { get; set; }
        public Guid? vehicleType_Index { get; set; }
        public string vehicleType_Id { get; set; }
        public string vehicleType_Name { get; set; }
        public string driver_Name { get; set; }
        public Guid? transport_Index { get; set; }
        public string transport_Id { get; set; }
        public string transport_Name { get; set; }
        public Guid? round_Index { get; set; }
        public string round_Id { get; set; }
        public string round_Name { get; set; }
        public string license_Name { get; set; }
        public Guid? forwarder_Index { get; set; }

        public string forwarder_Id { get; set; }

        public string forwarder_Name { get; set; }
        public Guid? shipmentType_Index { get; set; }

        public string shipmentType_Id { get; set; }

        public string shipmentType_Name { get; set; }
        public Guid? cargoType_Index { get; set; }

        public string cargoType_Id { get; set; }

        public string cargoType_Name { get; set; }
        public Guid? unloadingType_Index { get; set; }

        public string unloadingType_Id { get; set; }

        public string unloadingType_Name { get; set; }
        public Guid? containerType_Index { get; set; }

        public string containerType_Id { get; set; }

        public string containerType_Name { get; set; }

        public string container_No1 { get; set; }

        public string container_No2 { get; set; }
        public string labur { get; set; }
        public string ownerDocumentRef_No1 { get; set; }
        public string ownerDocumentRef_No2 { get; set; }
        public string ownerDocumentRef_No3 { get; set; }

        public Guid? import_Index { get; set; }

        public List<PurchaseOrderItemDocViewModel> listPurchaseOrderItemViewModel { get; set; }
        public List<document> documents { get; set; }

        public class actionResult
        {
            public string document_No { get; set; }
            public Boolean Message { get; set; }
        }

        public class document
        {
            public Guid? index { get; set; }
            public string filename { get; set; }
            public string path { get; set; }
            public string urlAttachFile { get; set; }
            public Boolean isDelete { get; set; }
            public string type { get; set; }
        }
    }
}
