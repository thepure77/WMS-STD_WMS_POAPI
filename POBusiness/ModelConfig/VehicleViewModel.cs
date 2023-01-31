using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataBusiness.ViewModels
{


    public  class VehicleViewModel
    {

        public Guid? Vehicle_Index { get; set; }
        public string Vehicle_Id { get; set; }
        public string Vehicle_Name { get; set; }
    }
}
