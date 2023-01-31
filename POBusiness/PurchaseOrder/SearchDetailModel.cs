using GRBusiness;
using System;
using System.Collections.Generic;
using System.Text;

namespace POBusiness.PlanGoodsReceive
{
    public partial class SearchDetailModel : Pagination
    {
        public SearchDetailModel()
        {
            sort = new List<sortViewModel>();

            status = new List<statusViewModel>();

        }

        public Guid? purchaseOrder_Index { get; set; }

        public Guid? owner_Index { get; set; }

        public string owner_Id { get; set; }

        public string owner_Name { get; set; }

        public Guid? vendor_Index { get; set; }

        public string vendor_Id { get; set; }

        public string vendor_Name { get; set; }

        public Guid? documentType_Index { get; set; }

        public string documentType_Id { get; set; }

        public string documentType_Name { get; set; }

        public string purchaseOrder_No { get; set; }

        public string purchaseOrder_date { get; set; }

        public string purchaseOrder_date_To { get; set; }

        public string purchaseOrder_due_date { get; set; }
        public string purchaseOrder_due_date_To { get; set; }

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
        public Guid? processStatus_Index { get; set; }
        public int? processStatus_Id { get; set; }
        public string processStatus_Name { get; set; }

        public string item_Document_Remark { get; set; }
        public string key { get; set; }

        public bool advanceSearch { get; set; }

        public string name { get; set; }
        public Guid? index { get; set; }
        public decimal qty { get; set; }
        public decimal weight { get; set; }
        public string our_Reference { get; set; }


        public List<sortViewModel> sort { get; set; }
        public List<statusViewModel> status { get; set; }


        public class actionResultPOViewModel
        {
            public IList<SearchDetailModel> items { get; set; }
            public Pagination pagination { get; set; }
        }

        public class sortViewModel
        {
            public string value { get; set; }
            public string display { get; set; }
            public int seq { get; set; }
        }

        public class statusViewModel
        {
            public int? value { get; set; }
            public string display { get; set; }
            public int seq { get; set; }
        }

        public class SortModel
        {
            public string ColId { get; set; }
            public string Sort { get; set; }

            public string PairAsSqlExpression
            {
                get
                {
                    return $"{ColId} {Sort}";
                }
            }
        }

        public class StatusModel
        {
            public string Name { get; set; }
        }
    }

}
