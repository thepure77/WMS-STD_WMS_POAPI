using GRBusiness.PlanGoodsReceive;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PlanGRBusiness.Reports
{
    public class ReportPlanGoodsReceiveViewModel
    {
        public Guid? planGoodsReceive_Index { get; set; }

        public Guid? planGoodsReceiveItem_Index { get; set; }

        public string planGoodsReceive_No { get; set; }

        public string planGoodsReceive_Date { get; set; }

        public string planGoodsReceive_Time { get; set; }

        public Guid? warehouse_Index { get; set; }

        public string warehouse_Id { get; set; }
        
        public string warehouse_Name { get; set; }

        public Guid documentType_Index { get; set; }

        public string documentType_Id { get; set; }

        public string documentType_Name { get; set; }

        public string planGoodsReceive_Due_Date { get; set; }

        public string license_Name { get; set; }

        public Guid? dock_Index { get; set; }

        public string dock_Id { get; set; }

        public string dock_Name { get; set; }
                
        public Guid owner_Index { get; set; }

        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public Guid vendor_Index { get; set; }

        public string vendor_Id { get; set; }

        public string vendor_Name { get; set; }

        public Guid? Vehicle_Index { get; set; }

        public string Vehicle_Id { get; set; }

        public string Vehicle_Name { get; set; }

        public Guid? driver_Index { get; set; }

        public string driver_Id { get; set; }

        public string driver_Name { get; set; }

        public string document_Remark { get; set; }

        public Guid? transport_Index { get; set; }

        public string transport_Id { get; set; }

        public string transport_Name { get; set; }

        public Guid? round_Index { get; set; }

        public string round_Id { get; set; }

        public string round_Name { get; set; }
                
        public string documentRef_No1 { get; set; }

        public Guid? product_Index { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }

        public Guid? itemStatus_Index { get; set; }

        public string itemStatus_Id { get; set; }

        public string itemStatus_Name { get; set; }

        public string product_Lot { get; set; }

        public Guid? productConversion_Index { get; set; }

        public string productConversion_Id { get; set; }

        public string productConversion_Name { get; set; }

        public decimal? qty { get; set; }

        public decimal? weight { get; set; }

        public string documentItem_Remark { get; set; }

        public string processStatus_Name { get; set; }

        public int table_No { get; set; }

        public string planGoodsReceiveNo_Barcode { get; set; }

    }

}
