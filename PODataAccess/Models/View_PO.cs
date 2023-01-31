using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PODataAccess.Models
{
    public partial class View_PO
    {

        [Key]
        public Guid PurchaseOrder_Index { get; set; }

        public DateTime? PurchaseOrder_Date { get; set; }

        public DateTime? PurchaseOrder_Due_Date { get; set; }


        public string PurchaseOrder_No { get; set; }

        public Guid? Owner_Index { get; set; }


        public string Owner_Id { get; set; }


        public string Owner_Name { get; set; }

        public int? Document_Status { get; set; }

        public Guid? DocumentType_Index { get; set; }


        public string DocumentType_Id { get; set; }


        public string DocumentType_Name { get; set; }


        public string Create_By { get; set; }


        public string update_By { get; set; }


        public string Cancel_By { get; set; }

        public decimal QTY { get; set; }
    }
}
