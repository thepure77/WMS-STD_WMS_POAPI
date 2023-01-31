using System;
using System.Collections.Generic;
using System.Text;

namespace PlanGRBusiness.Reports
{
    public class ReportDNViewModel
    {
        public Guid? planGoodsReceive_Index { get; set; }
        public Guid? planGoodsReceiveItem_Index { get; set; }
        public string owner_Id { get; set; }
        public string owner_Name { get; set; }
        public string planGoodsReceive_Due_Date { get; set; }
        public string planGoodsReceive_Date { get; set; }
        public string lineNum { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public decimal qty { get; set; }
        public string productConversion_Name { get; set; }
        public decimal? unitWidth { get; set; }
        public decimal? unitLength { get; set; }
        public decimal? unitHeight { get; set; }
        public string volume_Name { get; set; }
        public string planGoodsReceive_No_Barcode { get; set; }
        public string planGoodsReceive_No { get; set; }
        public string size { get; set; }
        public int count { get; set; }
        public string due_Date { get; set; }
        public string date { get; set; }
    }


}
