using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanGRBusiness.PlanGoodsReceive
{


    public  class OwnerViewModel
    {
        public Guid owner_Index { get; set; }

        public string owner_Id { get; set; }


        public string owner_Name { get; set; }


        public string owner_Address { get; set; }

        public Guid ownerType_Index { get; set; }

        public Guid? subDistrict_Index { get; set; }

        public Guid? district_Index { get; set; }

        public Guid? province_Index { get; set; }

        public Guid? country_Index { get; set; }

        public Guid? postcode_Index { get; set; }

        public int? isActive { get; set; }

        public int? isDelete { get; set; }

        public int? isSystem { get; set; }

        public int? status_Id { get; set; }


        public string create_By { get; set; }


        public DateTime? create_Date { get; set; }


        public string update_By { get; set; }


        public DateTime? update_Date { get; set; }


        public string cancel_By { get; set; }


        public DateTime? cancel_Date { get; set; }
        public string owner_TaxID { get; set; }


        public string key { get; set; }
        public string name { get; set; }




    }
}
