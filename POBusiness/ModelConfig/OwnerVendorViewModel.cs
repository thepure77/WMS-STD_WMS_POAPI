using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class OwnerVendorViewModel
    {

        
        public string VendorName { get; set; }

        public string VendorId { get; set; }

        public string OwnerId { get; set; }

        
        public string OwnerName { get; set; }

        public Guid OwnerVendorIndex { get; set; }

        public Guid? OwnerIndex { get; set; }

        public Guid? VendorIndex { get; set; }

        public string OwnerVendorId { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? StatusId { get; set; }


    }
}
