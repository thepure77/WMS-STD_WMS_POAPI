using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PODataAccess.Models
{
    public partial class View_CheckRemainingPO_Qty
    {

        [Key]
        public Guid PurchaseOrderItem_Index { get; set; }
        public Guid PurchaseOrder_Index { get; set; }
        public string PurchaseOrder_No { get; set; }
        public string LineNum { get; set; }
        public Guid Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal Qty { get; set; }
        public decimal Ratio { get; set; }
        public decimal? TotalQty { get; set; }
        public decimal? GR_TotalQty { get; set; }
        public decimal? Remain_Qty { get; set; }

    }
}
