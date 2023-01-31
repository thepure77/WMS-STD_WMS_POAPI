
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using POBusiness.PopupPurchaseOrderBusiness;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PopupPurchaseOrderBusiness
{
    public class PopupPurchaseOrderService
    {
        private DbContextOptions<PODbContext> options;

        private PODbContext db;

        public PopupPurchaseOrderService(PODbContext db)
        {
            this.db = db;
        }

        public PopupPurchaseOrderService()
        {

        }

        #region PopupPlanGRfilter
        public List<PopupPurchaseOrderDocViewModel> popupPlanPOfilter(PopupPurchaseOrderDocViewModel data)
        {
            try
            {

                var items = new List<PopupPurchaseOrderDocViewModel>();

                //PlanGR popup GRCreate Page

                var query = db.View_GetPurchaseOrder_Popup.AsQueryable();

                if (!string.IsNullOrEmpty(data.purchaseOrder_No))
                {
                    query = query.Where(c => c.PurchaseOrder_No.Contains(data.purchaseOrder_No));
                }
                else if (!string.IsNullOrEmpty(data.vendor_Name))
                {
                    query = query.Where(c => c.Vendor_Name.Contains(data.vendor_Name));
                }
                else if (!string.IsNullOrEmpty(data.purchaseOrder_Date))
                {
                    query = query.Where(c => c.PurchaseOrder_Date.toString().Contains(data.purchaseOrder_Date));
                }
                else if (!string.IsNullOrEmpty(data.purchaseOrder_Due_Date))
                {
                    query = query.Where(c => c.PurchaseOrder_Due_Date.toString().Contains(data.purchaseOrder_Due_Date));
                }
                else if (!string.IsNullOrEmpty(data.owner_Name))
                {
                    query = query.Where(c => c.Owner_Name.Contains(data.owner_Name));
                }
                else if (!string.IsNullOrEmpty(data.owner_Index.ToString()))
                {
                    query = query.Where(c => c.Owner_Index == data.owner_Index);
                }
                query = query.Where(c => c.Document_Status == 1);


                var result = query.ToList();

                foreach (var item in result)
                {
                    var resultItem = new PopupPurchaseOrderDocViewModel
                    {
                        purchaseOrder_Index = item.PurchaseOrder_Index,
                        purchaseOrder_No = item.PurchaseOrder_No,
                        purchaseOrder_Date = item.PurchaseOrder_Date.toString(),
                        purchaseOrder_Due_Date = item.PurchaseOrder_Due_Date.toString(),
                        vendor_Index = item.Vendor_Index,
                        vendor_Id = item.Vendor_Id,
                        vendor_Name = item.Vendor_Name,
                        owner_Index = item.Owner_Index,
                        owner_Id = item.Owner_Id,
                        owner_Name = item.Owner_Name,
                        documentRef_No1 = item.DocumentRef_No1,
                        document_Status = item.Document_Status,
                        warehouse_Index = item.Warehouse_Index,
                        warehouse_Index_To = item.Warehouse_Index_To,
                        warehouse_Id = item.Warehouse_Id,
                        warehouse_Id_To = item.Warehouse_Id_To,
                        warehouse_Name = item.Warehouse_Name,
                        warehouse_Name_To = item.Warehouse_Name_To,
                        documentType_Index = item.DocumentType_Index,
                        documentType_Id = item.DocumentType_Id,
                        documentType_Name = item.DocumentType_Name,
                        document_Remark = item.Document_Remark,
                        //grDocumentType_Index = new Guid(DataDocumentTypeRef.dataincolumn1),
                        //grDocumentType_Id = DataDocumentTypeRef.dataincolumn2,
                        //grDocumentType_Name = DataDocumentTypeRef.dataincolumn3
                    };
                    items.Add(resultItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetPlanGRIfilter
        public List<View_GetPurchaseOrderItemViewModel> GetPlanPOIfilter(View_GetPurchaseOrderItemViewModel data)
        {
            try
            {

                var items = new List<View_GetPurchaseOrderItemViewModel>();



                var query = db.View_GetPurchaseOrderItem.AsQueryable();


                if (!string.IsNullOrEmpty(data.owner_Index.ToString()))
                {
                    query = query.Where(c => c.Owner_Index == data.owner_Index);
                }

                if (data.id?.Count > 1)
                {
                    var G = new List<Guid>();

                    data.id.ForEach(c => G.Add(new Guid(c.Replace("'", ""))));



                    query = query.Where(c => G.Contains(c.PurchaseOrder_Index));
                }
                else if (!string.IsNullOrEmpty(data.purchaseOrder_Index.ToString()))
                {
                    query = query.Where(c => c.PurchaseOrder_Index == data.purchaseOrder_Index);
                }




                var result = query.ToList();

                foreach (var item in result)
                {

                    var resultItem = new View_GetPurchaseOrderItemViewModel();

                    resultItem.purchaseOrder_Index = item.PurchaseOrder_Index;
                    resultItem.purchaseOrderItem_Index = item.PurchaseOrderItem_Index;
                    resultItem.purchaseOrder_No = item.PurchaseOrder_No;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.lineNum = item.LineNum;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.product_SecondName = item.Product_SecondName;
                    resultItem.product_ThirdName = item.Product_ThirdName;
                    resultItem.product_Lot = item.Product_Lot;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.defult_qty = item.Qty.GetValueOrDefault();
                    resultItem.qty = item.Qty.GetValueOrDefault();
                    resultItem.ratio = item.Ratio.GetValueOrDefault();
                    resultItem.totalQty = item.TotalQty;
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.mfg_Date = item.MFG_Date.toString();
                    resultItem.exp_Date = item.EXP_Date.toString();

                    resultItem.unitWeight = item.UnitWeight;
                    resultItem.weight = item.Weight;
                    resultItem.weight_Index = item.Weight_Index;
                    resultItem.weight_Id = item.Weight_Id;
                    resultItem.weight_Name = item.Weight_Name;
                    resultItem.weightRatio = item.WeightRatio;
                    resultItem.netWeight = item.NetWeight;
                    resultItem.unitGrsWeight = item.UnitGrsWeight;
                    resultItem.grsWeightRatio = item.GrsWeightRatio;
                    resultItem.grsWeight = item.GrsWeight;
                    resultItem.grsWeight_Index = item.GrsWeight_Index;
                    resultItem.grsWeight_Id = item.GrsWeight_Id;
                    resultItem.grsWeight_Name = item.GrsWeight_Name;
                    resultItem.unitWidth = item.UnitWidth;
                    resultItem.widthRatio = item.WidthRatio;
                    resultItem.width = item.Width;
                    resultItem.width_Index = item.Width_Index;
                    resultItem.width_Id = item.Width_Id;
                    resultItem.width_Name = item.Width_Name;
                    resultItem.unitLength = item.UnitLength;
                    resultItem.lengthRatio = item.LengthRatio;
                    resultItem.length = item.Length;
                    resultItem.length_Index = item.Length_Index;
                    resultItem.length_Id = item.Length_Id;
                    resultItem.length_Name = item.Length_Name;
                    resultItem.unitHeight = item.UnitHeight;
                    resultItem.heightRatio = item.HeightRatio;
                    resultItem.height = item.Height;
                    resultItem.height_Index = item.Height_Index;
                    resultItem.height_Id = item.Height_Id;
                    resultItem.height_Name = item.Height_Name;
                    if (item.UnitVolume == 0 || item.UnitVolume == null)
                    {
                        resultItem.unitVolume = 0;
                    }
                    else
                    {
                        resultItem.unitVolume = item.UnitVolume;

                    }

                    if (item.Volume == 0 || item.Volume == null)
                    {
                        resultItem.volume = 0;
                    }
                    else
                    {
                        resultItem.volume = item.Volume;
                    }

                    resultItem.unitPrice = item.UnitPrice;
                    resultItem.price = item.Price;
                    resultItem.totalPrice = item.TotalQty;

                    resultItem.currency_Index = item.Currency_Index;
                    resultItem.currency_Id = item.Currency_Id;
                    resultItem.currency_Name = item.Currency_Name;

                    resultItem.ref_Code1 = item.Ref_Code1;
                    resultItem.ref_Code2 = item.Ref_Code2;
                    resultItem.ref_Code3 = item.Ref_Code3;
                    resultItem.ref_Code4 = item.Ref_Code4;
                    resultItem.ref_Code5 = item.Ref_Code5;


                    resultItem.documentRef_No1 = item.DocumentRef_No1;
                    resultItem.documentRef_No2 = item.DocumentRef_No2;
                    resultItem.documentRef_No3 = item.DocumentRef_No3;
                    resultItem.documentRef_No4 = item.DocumentRef_No4;
                    resultItem.documentRef_No5 = item.DocumentRef_No5;
                    resultItem.document_Status = item.Document_Status;
                    resultItem.documentItem_Remark = item.DocumentItem_Remark;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    items.Add(resultItem);

                }


                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GetPlanGRIPendingfilter
        public List<View_GetPurchaseOrderItemViewModel> GetPlanPOIPendingfilter(View_GetPurchaseOrderItemViewModel data)
        {
            try
            {

                var items = new List<View_GetPurchaseOrderItemViewModel>();



                var query = db.View_PurchaseOrderItem.AsQueryable();


                if (!string.IsNullOrEmpty(data.owner_Index.ToString()))
                {
                    query = query.Where(c => c.Owner_Index == data.owner_Index);
                }
                if (data.id.Count > 1)
                {
                    var G = new List<Guid>();

                    data.id.ForEach(c => G.Add(new Guid(c.Replace("'", ""))));



                    query = query.Where(c => G.Contains(c.PurchaseOrder_Index));
                }
                else if (!string.IsNullOrEmpty(data.purchaseOrder_Index.ToString()))
                {
                    query = query.Where(c => c.PurchaseOrder_Index == data.purchaseOrder_Index);
                }



                var result = query.ToList();

                foreach (var item in result)
                {
                    var resultProductconversion = new List<ProductConversionViewModelDoc>();

                    var filterModel = new ProductConversionViewModelDoc();

                    if (!string.IsNullOrEmpty(item.Product_Index.ToString()))
                    {
                        filterModel.product_Index = item.Product_Index;
                    }

                    if (!string.IsNullOrEmpty(item.ProductConversion_Index.ToString()))
                    {
                        filterModel.productConversion_Index = item.ProductConversion_Index;
                    }
                    resultProductconversion = utils.SendDataApi<List<ProductConversionViewModelDoc>>(new AppSettingConfig().GetUrl("dropdownProductconversion"), filterModel.sJson());

                    var resultItem = new View_GetPurchaseOrderItemViewModel();

                    var remainingPO = db.View_CheckRemainingPO_Qty.Where(c => c.PurchaseOrderItem_Index == item.PurchaseOrderItem_Index && c.PurchaseOrder_Index == item.PurchaseOrder_Index && c.Product_Index == item.Product_Index).FirstOrDefault();

                    resultItem.purchaseOrder_Index = item.PurchaseOrder_Index;
                    resultItem.purchaseOrderItem_Index = item.PurchaseOrderItem_Index;
                    resultItem.purchaseOrder_No = item.PurchaseOrder_No;
                    resultItem.product_Index = item.Product_Index;
                    resultItem.lineNum = item.LineNum;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.product_SecondName = item.Product_SecondName;
                    resultItem.product_ThirdName = item.Product_ThirdName;
                    resultItem.product_Lot = item.Product_Lot;
                    resultItem.owner_Index = item.Owner_Index;
                    resultItem.defult_qty = item.Qty; // add new
                    resultItem.qty = item.Total;
                    resultItem.ratio = item.Ratio;
                    resultItem.totalQty = (item.Total * item.Ratio);
                    resultItem.remainingPO_Qty = (remainingPO == null) ? 0 : remainingPO.Remain_Qty; // add new
                    resultItem.productConversion_Index = item.ProductConversion_Index;
                    resultItem.productConversion_Id = item.ProductConversion_Id;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.mfg_Date = item.MFG_Date.toString();
                    resultItem.exp_Date = item.EXP_Date.toString();
                    resultItem.weight = item.Weight;
                    resultItem.unitWeight = resultProductconversion.FirstOrDefault().productConversion_Weight;
                    resultItem.unitWidth = item.UnitWidth;
                    resultItem.unitLength = item.UnitLength;
                    resultItem.unitHeight = item.UnitHeight;
                    //resultItem.unitVolume = resultProductconversion.FirstOrDefault().productConversion_Volume;
                    //resultItem.volume = item.Volume;

                    if (resultProductconversion.FirstOrDefault().productConversion_Volume == 0 || resultProductconversion.FirstOrDefault().productConversion_Volume == null)
                    {
                        resultItem.unitVolume = 0;
                    }
                    else
                    {
                        resultItem.unitVolume = resultProductconversion.FirstOrDefault().productConversion_Volume;
                    }

                    if (item.Volume == 0 || item.Volume == null)
                    {
                        resultItem.volume = 0;
                    }
                    else
                    {
                        resultItem.volume = item.Volume;
                    }
                    resultItem.unitPrice = item.UnitPrice;
                    resultItem.price = item.Price;
                    resultItem.documentRef_No1 = item.DocumentRef_No1;
                    resultItem.documentRef_No2 = item.DocumentRef_No2;
                    resultItem.documentRef_No3 = item.DocumentRef_No3;
                    resultItem.documentRef_No4 = item.DocumentRef_No4;
                    resultItem.documentRef_No5 = item.DocumentRef_No5;
                    resultItem.document_Status = item.Document_Status;
                    resultItem.documentItem_Remark = item.DocumentItem_Remark;
                    resultItem.udf_1 = item.UDF_1;
                    resultItem.udf_2 = item.UDF_2;
                    resultItem.udf_3 = item.UDF_3;
                    resultItem.udf_4 = item.UDF_4;
                    resultItem.udf_5 = item.UDF_5;
                    items.Add(resultItem);
                }


                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
