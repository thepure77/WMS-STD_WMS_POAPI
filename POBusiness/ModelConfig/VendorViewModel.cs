using GRBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace PlanGRBusiness.ViewModels
{
    public class VendorViewModel : Pagination
    {
        [DataMember]
        public Guid VendorIndex { get; set; }
        [DataMember]
        public Guid VendorTypeIndex { get; set; }
        [DataMember]
        public string VendorTypeName { get; set; }
        [DataMember]
        public string VendorId { get; set; }
        [DataMember]
        public string VendorName { get; set; }     
        [DataMember]
        public string VendorAddress { get; set; }
        [DataMember]
        public string VendorDistrict { get; set; }

        [DataMember]
        public Guid PostCodeIndex { get; set; }

        [DataMember]
        public Guid CountryIndex { get; set; }

        [DataMember]
        public Guid SubDistrictIndex { get; set; }

        [DataMember]
        public Guid DistrictIndex { get; set; }

        [DataMember]
        public Guid ProvinceIndex { get; set; }
        
        [DataMember]
        public string SubDistrictName { get; set; }

        [DataMember]
        public string DistrictName { get; set; }

        [DataMember]
        public string ProvinceName { get; set; }

        [DataMember]
        public string PostCodeName { get; set; }
      
        [DataMember]
        public string CountryName { get; set; }

        public int count { get; set; }

        [DataMember]
        public int IsActive { get; set; }
        [DataMember]
        public int IsDelete { get; set; }
        [DataMember]
        public int IsSystem { get; set; }
        [DataMember]
        public int StatusId { get; set; }

        [StringLength(200)]
        public string CreateBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreateDate { get; set; }

        [StringLength(200)]
        public string UpdateBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDate { get; set; }

        [StringLength(200)]
        public string CancelBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CancelDate { get; set; }
    }
    public class actionResultVendorViewModel
    {
        public IList<VendorViewModel> itemsVendor { get; set; }
        public Pagination pagination { get; set; }
    }
}
