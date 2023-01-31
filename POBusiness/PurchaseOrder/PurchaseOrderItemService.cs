using Business.Library;
using Comone.Utils;
using DataAccess;
using GRBusiness;
using GRBusiness.PlanGoodsReceive;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.PlanGoodsReceive;
using POBusiness.PlanGoodsReceive;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PlanGRBusiness.PlanGoodsReceiveItem
{
    public class PurchaseOrderItemService
    {
        private PODbContext db ;


        public PurchaseOrderItemService(PODbContext db)
        {
            this.db = db;
        }

        public PurchaseOrderItemService()
        {
        }

        public List<PurchaseOrderItemDocViewModel> GetByPurchaseOrderItemId(Guid id)
        {
            try
            {
                var result = new List<PurchaseOrderItemDocViewModel>();

                var queryResult = db.im_PurchaseOrderItem.Where(c => c.PurchaseOrder_Index == id && c.Document_Status != -1).ToList();

                foreach (var data in queryResult)
                {
                    var item = new PurchaseOrderItemDocViewModel();

                    item.purchaseOrder_Index = data.PurchaseOrder_Index;
                    item.purchaseOrderItem_Index = data.PurchaseOrderItem_Index;
                    item.product_Index = data.Product_Index;
                    item.lineNum = data.LineNum;
                    item.product_Id = data.Product_Id;
                    item.product_Name = data.Product_Name;
                    item.product_SecondName = data.Product_SecondName;
                    item.product_ThirdName = data.Product_ThirdName;
                    item.product_Lot = data.Product_Lot;
                    item.itemStatus_Index = data.ItemStatus_Index;
                    item.itemStatus_Id = data.ItemStatus_Id;
                    item.itemStatus_Name = data.ItemStatus_Name;
                    item.qty = data.Qty;
                    item.ratio = data.Ratio;
                    item.totalQty = data.TotalQty;
                    item.productConversion_Index = data.ProductConversion_Index;
                    item.productConversion_Id = data.ProductConversion_Id;
                    item.productConversion_Name = data.ProductConversion_Name;
                    item.mFG_Date = data.MFG_Date.toString();
                    item.eXP_Date = data.EXP_Date.toString();

                    item.unitWeight = data.UnitWeight;
                    item.weight = data.Weight;
                    item.weight_Index = data.Weight_Index;
                    item.weight_Id = data.Weight_Id;
                    item.weight_Name = data.Weight_Name;
                    item.netWeight = data.NetWeight;

                    item.unitGrsWeight = data.UnitGrsWeight;
                    item.grsWeight = data.GrsWeight;
                    item.grsWeight_Index = data.GrsWeight_Index;
                    item.grsWeight_Id = data.GrsWeight_Id;
                    item.grsWeight_Name = data.GrsWeight_Name;

                    item.unitWidth = data.UnitWidth;
                    item.width = data.Width;
                    item.width_Index = data.Width_Index;
                    item.width_Id = data.Width_Id;
                    item.width_Name = data.Width_Name;

                    item.unitLength = data.UnitLength;
                    item.length = data.Length;
                    item.length_Index = data.Length_Index;
                    item.length_Id = data.Length_Id;
                    item.length_Name = data.Length_Name;

                    item.unitHeight = data.UnitHeight;
                    item.height = data.Height;
                    item.height_Index = data.Height_Index;
                    item.height_Id = data.Height_Id;
                    item.height_Name = data.Height_Name;

                    item.unitVolume = data.UnitVolume;
                    item.volume = data.Volume;


                    item.unitPrice = data.UnitPrice;
                    item.price = data.Price;
                    item.totalPrice = data.TotalQty;

                    item.currency_Index = data.Currency_Index;
                    item.currency_Id = data.Currency_Id;
                    item.currency_Name = data.Currency_Name;

                    item.ref_Code1 = data.Ref_Code1;
                    item.ref_Code2 = data.Ref_Code2;
                    item.ref_Code3 = data.Ref_Code3;
                    item.ref_Code4 = data.Ref_Code4;
                    item.ref_Code5 = data.Ref_Code5;

                    item.documentRef_No1 = data.DocumentRef_No1;
                    item.documentRef_No2 = data.DocumentRef_No2;
                    item.documentRef_No3 = data.DocumentRef_No3;
                    item.documentRef_No4 = data.DocumentRef_No4;
                    item.documentRef_No5 = data.DocumentRef_No5;
                    item.document_Status = data.Document_Status;
                    item.documentItem_Remark = data.DocumentItem_Remark;
                    item.uDF_1 = data.UDF_1;
                    item.uDF_2 = data.UDF_2;
                    item.uDF_3 = data.UDF_3;
                    item.uDF_4 = data.UDF_4;
                    item.uDF_5 = data.UDF_5;


                    //var sku = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoSkufilter"), new { key = data.Product_Id}.sJson()).FirstOrDefault();

                    result.Add(item);
                }
                return result;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
