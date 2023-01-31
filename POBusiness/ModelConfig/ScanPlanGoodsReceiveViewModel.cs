using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PlanGRBusiness.ModelConfig
{
    public class ScanPlanGoodsReceiveViewModel
    {
        [Key]
        public Guid planGoodsReceive_Index { get; set; }

        public Guid owner_Index { get; set; }


        [StringLength(50)]
        public string owner_Id { get; set; }


        [StringLength(50)]
        public string owner_Name { get; set; }

        public Guid vendor_Index { get; set; }


        [StringLength(50)]
        public string vendor_Id { get; set; }


        [StringLength(200)]
        public string vendor_Name { get; set; }

        public Guid documentType_Index { get; set; }


        [StringLength(50)]
        public string documentType_Id { get; set; }


        [StringLength(200)]
        public string documentType_Name { get; set; }


        [StringLength(50)]
        public string planGoodsReceive_No { get; set; }


        public string planGoodsReceive_Date { get; set; }


        public string planGoodsReceive_Due_Date { get; set; }



        [StringLength(200)]
        public string documentRef_No1 { get; set; }

        [StringLength(200)]
        public string documentRef_No2 { get; set; }

        [StringLength(200)]
        public string documentRef_No3 { get; set; }

        [StringLength(200)]
        public string documentRef_No4 { get; set; }

        [StringLength(200)]
        public string documentRef_No5 { get; set; }

        public int? document_Status { get; set; }

        [StringLength(200)]
        public string udf_1 { get; set; }

        [StringLength(200)]
        public string udf_2 { get; set; }

        [StringLength(200)]
        public string udf_3 { get; set; }

        [StringLength(200)]
        public string udf_4 { get; set; }

        [StringLength(200)]
        public string udf_5 { get; set; }

        public int? documentPriority_Status { get; set; }

        [StringLength(200)]
        public string document_Remark { get; set; }


        [StringLength(200)]
        public string create_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public string create_Date { get; set; }

        [StringLength(200)]
        public string update_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public string update_Date { get; set; }

        [StringLength(200)]
        public string cancel_By { get; set; }

        [Column(TypeName = "smalldatetime")]
        public string cancel_Date { get; set; }

        public Guid? warehouse_Index { get; set; }

        [StringLength(50)]
        public string warehouse_Id { get; set; }

        [StringLength(200)]
        public string warehouse_Name { get; set; }

        public Guid? warehouse_Index_To { get; set; }

        [StringLength(50)]
        public string warehouse_Id_To { get; set; }

        [StringLength(200)]
        public string warehouse_Name_To { get; set; }
    }
}
