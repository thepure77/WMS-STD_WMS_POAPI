using AspNetCore.Reporting;
using Business.Library;
using Common.Utils;
//using Comone.Utils;
using DataAccess;
using DataBusiness.AutoNumber;
using GRBusiness.GoodsReceive;
using GRBusiness.PlanGoodsReceive;
using GRDataAccess.Models;
using MasterDataBusiness.CargoType;
using MasterDataBusiness.ContainerType;
using MasterDataBusiness.CostCenter;
using MasterDataBusiness.Currency;
using MasterDataBusiness.DockDoor;
using MasterDataBusiness.DocumentPriority;
using MasterDataBusiness.ShipmentType;
using MasterDataBusiness.VehicleType;
using MasterDataBusiness.ViewModels;
using MasterDataBusiness.Volume;
using MasterDataBusiness.Weight;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness;
using PlanGRBusiness.ModelConfig;
using PlanGRBusiness.PlanGoodsReceive;
using PlanGRBusiness.Reports;
using POBusiness;
using POBusiness.PlanGoodsReceive;
using PODataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using static POBusiness.PlanGoodsReceive.PurchaseOrderDocViewModel;
using static POBusiness.PlanGoodsReceive.SearchDetailModel;
using Utils = PlanGRBusiness.Libs.Utils;

namespace GRBusiness
{
    public class PurchaseOrderService
    {
        private DbContextOptions<PODbContext> options;

        private PODbContext db;

        public PurchaseOrderService(PODbContext db)
        {
            this.db = db;
        }

        public PurchaseOrderService()
        {

        }

        #region CreateDataTable
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));

            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #endregion

        #region filter
        public actionResultPOViewModel filter(SearchDetailModel model)
        {
            try
            {


                var query = db.im_PurchaseOrder.AsQueryable();

                #region advanceSearch
                if (model.advanceSearch == true)
                {
                    if (!string.IsNullOrEmpty(model.purchaseOrder_No))
                    {
                        query = query.Where(c => c.PurchaseOrder_No.Contains(model.purchaseOrder_No));
                    }

                    if (!string.IsNullOrEmpty(model.owner_Name))
                    {
                        query = query.Where(c => c.Owner_Name.Contains(model.owner_Name));
                    }

                    if (!string.IsNullOrEmpty(model.vendor_Name))
                    {
                        query = query.Where(c => c.Vendor_Name.Contains(model.vendor_Name));
                    }
                    if (!string.IsNullOrEmpty(model.document_Status.ToString()))
                    {
                        query = query.Where(c => c.Document_Status == (model.document_Status));
                    }

                    if ((model.processStatus_Id ?? -99) != -99)
                    {
                        query = query.Where(c => c.Document_Status == model.processStatus_Id);
                    }

                    if (!string.IsNullOrEmpty(model.documentType_Index.ToString()) && model.documentType_Index.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        query = query.Where(c => c.DocumentType_Index == (model.documentType_Index));
                    }

                    if (!string.IsNullOrEmpty(model.purchaseOrder_date) && !string.IsNullOrEmpty(model.purchaseOrder_date_To))
                    {
                        var dateStart = model.purchaseOrder_date.toBetweenDate();
                        var dateEnd = model.purchaseOrder_date_To.toBetweenDate();
                        query = query.Where(c => c.PurchaseOrder_Date >= dateStart.start && c.PurchaseOrder_Date <= dateEnd.end);
                    }
      
                    if (!string.IsNullOrEmpty(model.purchaseOrder_due_date) && !string.IsNullOrEmpty(model.purchaseOrder_due_date_To))
                    {
                        var dateStart = model.purchaseOrder_due_date.toBetweenDate();
                        var dateEnd = model.purchaseOrder_due_date_To.toBetweenDate();
                        query = query.Where(c => c.PurchaseOrder_Due_Date >= dateStart.start && c.PurchaseOrder_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(model.create_By))
                    {
                        query = query.Where(c => c.Create_By == (model.create_By));
                    }
                    if (!string.IsNullOrEmpty(model.documentRef_No1))
                    {
                        query = query.Where(c => c.DocumentRef_No1 == model.documentRef_No1);
                    }
                }

                #endregion

                #region Basic
                else
                {
                    if (!string.IsNullOrEmpty(model.key))
                    {
                        query = query.Where(c => c.PurchaseOrder_No.Contains(model.key));
                    }

                    if (!string.IsNullOrEmpty(model.owner_Name))
                    {
                        query = query.Where(c => c.Owner_Name.Contains(model.owner_Name));
                    }

                    if (!string.IsNullOrEmpty(model.purchaseOrder_date) && !string.IsNullOrEmpty(model.purchaseOrder_date_To))
                    {
                        //var dateStart = model.purchaseOrder_date.toBetweenDate();
                        //var dateEnd = model.purchaseOrder_due_date.toBetweenDate();
                        query = query.Where(c => c.PurchaseOrder_Date >= model.purchaseOrder_date.toBetweenDate().start);
                        query = query.Where(c => c.PurchaseOrder_Date <= model.purchaseOrder_date_To.toBetweenDate().end);

                    }

                    var statusModels = new List<int?>();
                    var sortModels = new List<SortModel>();

                    if (model.status.Count > 0)
                    {
                        foreach (var item in model.status)
                        {

                            if (item.value == 0)
                            {
                                statusModels.Add(0);
                            }
                            if (item.value == 1)
                            {
                                statusModels.Add(1);
                            }
                            if (item.value == 2)
                            {
                                statusModels.Add(2);
                            }
                            if (item.value == 3)
                            {
                                statusModels.Add(3);
                            }
                            if (item.value == 4)
                            {
                                statusModels.Add(4);
                            }
                            if (item.value == -1)
                            {
                                statusModels.Add(-1);
                            }
                            if (item.value == -2)
                            {
                                statusModels.Add(-2);
                            }
                        }

                        query = query.Where(c => statusModels.Contains(c.Document_Status));
                    }

                    if (model.sort.Count > 0)
                    {
                        foreach (var item in model.sort)
                        {

                            if (item.value == "PurchaseOrder_No")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "PurchaseOrder_No",
                                    Sort = "desc"
                                });
                            }
                            if (item.value == "PurchaseOrder_Date")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "PurchaseOrder_Date",
                                    Sort = "desc"
                                });
                            }
                            if (item.value == "DocumentType_Name")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "DocumentType_Name",
                                    Sort = "desc"
                                });
                            }
                            if (item.value == "Qty")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "Qty",
                                    Sort = "desc"
                                });
                            }
                            if (item.value == "Weight")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "Weight",
                                    Sort = "desc"
                                });
                            }
                            if (item.value == "ProcessStatus_Name")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "Document_Status",
                                    Sort = "desc"
                                });
                            }
                            if (item.value == "Vendor_name")
                            {
                                sortModels.Add(new SortModel
                                {
                                    ColId = "Vendor_name",
                                    Sort = "desc"
                                });

                            }
                        }
                        query = query.KWOrderBy(sortModels);

                    }

                }

                #endregion

                var Item = new List<im_PurchaseOrder>();
                var TotalRow = new List<im_PurchaseOrder>();


               TotalRow = query.ToList();


                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    query = query.Skip(((model.CurrentPage - 1) * model.PerPage));
                }

                if (model.PerPage != 0)
                {
                    query = query.Take(model.PerPage);

                }

                if (model.sort.Count > 0)
                {
                    Item = query.ToList();
                }
                else
                {
                    Item = query.OrderByDescending(c => c.Create_Date).ToList();
                }

                //Item = query.ToList();

                var ProcessStatus = new List<ProcessStatusViewModel>();

                var filterModel = new ProcessStatusViewModel();

                filterModel.process_Index = new Guid("E6386717-A2F3-4DC0-837B-B94A2B44D274");

                //GetConfig
                ProcessStatus = utils.SendDataApi<List<ProcessStatusViewModel>>(new AppSettingConfig().GetUrl("processStatus"), filterModel.sJson());


                String Statue = "";
                var result = new List<SearchDetailModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchDetailModel();
                    resultItem.purchaseOrder_Index = item.PurchaseOrder_Index;
                    resultItem.purchaseOrder_No = item.PurchaseOrder_No;
                    resultItem.purchaseOrder_date = item.PurchaseOrder_Date.toString();
                    resultItem.purchaseOrder_due_date = item.PurchaseOrder_Due_Date.toString();
                    resultItem.documentType_Index = item.DocumentType_Index.GetValueOrDefault();
                    resultItem.documentType_Id = item.DocumentType_Id;
                    resultItem.documentType_Name = item.DocumentType_Name;
                    resultItem.document_Status = item.Document_Status;

                    resultItem.vendor_Index = item.Vendor_Index;
                    resultItem.vendor_Id = item.Vendor_Id;
                    resultItem.vendor_Name = item.Vendor_Name;

                    resultItem.owner_Index = item.Owner_Index.GetValueOrDefault();
                    resultItem.owner_Id = item.Owner_Id;
                    resultItem.owner_Name = item.Owner_Name;


                    Statue = item.Document_Status.ToString();
                    var ProcessStatusName = ProcessStatus.Where(c => c.processStatus_Id == Statue).FirstOrDefault();
                    resultItem.processStatus_Name = ProcessStatusName.processStatus_Name;

                    resultItem.create_By = item.Create_By;
                    resultItem.update_By = item.Update_By;
                    resultItem.cancel_By = item.Cancel_By;
                    result.Add(resultItem);
                }
                var count = TotalRow.Count;

                var actionResult = new actionResultPOViewModel();
                actionResult.items = result.OrderByDescending(o => o.create_date).ToList();
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage, };

                return actionResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region CreateOrUpdate
        public actionResult CreateOrUpdate(PurchaseOrderDocViewModel data)
        {
            Guid PurchaseOrderIndex = new Guid();
            String PurchaseOrderNo = "";

            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            Boolean IsNew = false;

            String userName = "";

            var actionResult = new actionResult();
            try
            {
                var itemDetail = new List<im_PurchaseOrderItem>();

                var PurchaseOrderOld = db.im_PurchaseOrder.Find(data.purchaseOrder_Index);

                if (data.create_By != null)
                {
                    userName = data.create_By;
                }
                if (data.update_By != null)
                {
                    userName = data.update_By;
                }

                if (PurchaseOrderOld == null)
                {
                    IsNew = true;
                    PurchaseOrderIndex = Guid.NewGuid();

                    var result = new List<GenDocumentTypeViewModel>();

                    var filterModel = new GenDocumentTypeViewModel();


                    filterModel.process_Index = new Guid("E6386717-A2F3-4DC0-837B-B94A2B44D274");
                    filterModel.documentType_Index = data.documentType_Index;
                    //GetConfig
                    result = utils.SendDataApi<List<GenDocumentTypeViewModel>>(new AppSettingConfig().GetUrl("dropDownDocumentType"), filterModel.sJson());

                    var genDoc = new AutoNumberService(db);
                    string DocNo = "";

                    DateTime DocumentDate = (DateTime)data.purchaseOrder_Date.toDate();
                    DocNo = genDoc.genAutoDocmentNumber(result, DocumentDate);

                    im_PurchaseOrder itemHeader = new im_PurchaseOrder();
                    var document_status = 0;

                    PurchaseOrderNo = DocNo;
                    itemHeader.PurchaseOrder_Index = PurchaseOrderIndex;
                    itemHeader.PurchaseOrder_No = DocNo;
                    itemHeader.Owner_Index = data.owner_Index;
                    itemHeader.Owner_Id = data.owner_Id;
                    itemHeader.Owner_Name = data.owner_Name;
                    itemHeader.Vendor_Index = data.vendor_Index;
                    itemHeader.Vendor_Id = data.vendor_Id;
                    itemHeader.Vendor_Name = data.vendor_Name;
                    itemHeader.DocumentType_Index = data.documentType_Index;
                    itemHeader.DocumentType_Id = data.documentType_Id;
                    itemHeader.DocumentType_Name = data.documentType_Name;
                    itemHeader.PurchaseOrder_Date = data.purchaseOrder_Date.toDate();
                    itemHeader.PurchaseOrder_Time = data.purchaseOrder_Time;
                    itemHeader.PurchaseOrder_Due_Date = data.purchaseOrder_Due_Date.toDate();
                    itemHeader.DocumentRef_No1 = data.documentRef_No1;
                    itemHeader.DocumentRef_No2 = data.documentRef_No2;
                    itemHeader.DocumentRef_No3 = data.documentRef_No3;
                    itemHeader.DocumentRef_No4 = data.documentRef_No4;
                    itemHeader.DocumentRef_No5 = data.documentRef_No5;
                    itemHeader.Document_Status = document_status;
                    itemHeader.UDF_1 = data.uDF_1;
                    itemHeader.UDF_2 = data.uDF_2;
                    itemHeader.UDF_3 = data.uDF_3;
                    itemHeader.UDF_4 = data.uDF_4;
                    itemHeader.UDF_5 = data.uDF_5;
                    itemHeader.Document_Remark = data.document_Remark;
                    itemHeader.Warehouse_Index = data.warehouse_Index;
                    itemHeader.Warehouse_Id = data.warehouse_Id;
                    itemHeader.Warehouse_Name = data.warehouse_Name;
                    itemHeader.Create_By = userName;
                    itemHeader.Create_Date = DateTime.Now;
                    itemHeader.Import_Index = data.import_Index;

                    db.im_PurchaseOrder.Add(itemHeader);

                    if (data.documents != null)
                    {
                        foreach (var d in data.documents.Where(c => !c.isDelete))
                        {
                            im_DocumentFile documents = new im_DocumentFile();

                            documents.DocumentFile_Index = Guid.NewGuid(); ;
                            documents.DocumentFile_Name = d.filename;
                            documents.DocumentFile_Path = d.path;
                            documents.DocumentFile_Url = d.urlAttachFile;
                            documents.DocumentFile_Type = d.type;
                            documents.DocumentFile_Status = 0;
                            documents.Create_By = userName;
                            documents.Create_Date = DateTime.Now;
                            documents.Ref_Index = itemHeader.PurchaseOrder_Index;
                            documents.Ref_No = itemHeader.PurchaseOrder_No;
                            db.im_DocumentFile.Add(documents);
                        }
                    }


                    int addNumber = 0;
                    foreach (var item in data.listPurchaseOrderItemViewModel)
                    {

                        var Productresult = new List<ProductViewModel>();

                        var ProductfilterModel = new ProductViewModel();
                        ProductfilterModel.product_Index = item.product_Index;

                        //GetConfig
                        Productresult = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("product"), ProductfilterModel.sJson());

                        im_PurchaseOrderItem resultItem = new im_PurchaseOrderItem();

                        addNumber++;
                        // Gen Index for line item

                        item.purchaseOrderItem_Index = Guid.NewGuid();
                        resultItem.PurchaseOrder_Index = PurchaseOrderIndex;
                        resultItem.LineNum = addNumber.ToString();
                        resultItem.ItemStatus_Index = item.itemStatus_Index;

                        resultItem.ItemStatus_Id = item.itemStatus_Id;

                        resultItem.ItemStatus_Name = item.itemStatus_Name;
                        resultItem.Product_Index = item.product_Index;
                        resultItem.Product_Id = item.product_Id;
                        resultItem.Product_Name = item.product_Name;
                        if (Productresult.Count > 0)
                        {
                            resultItem.Product_SecondName = Productresult.FirstOrDefault().product_SecondName;
                            resultItem.Product_ThirdName = Productresult.FirstOrDefault().product_ThirdName;
                        }

                        if (item.product_Lot != null)
                        {
                            resultItem.Product_Lot = item.product_Lot;
                        }
                        else
                        {
                            resultItem.Product_Lot = "";
                        }

                        resultItem.Qty = item.qty;
                        resultItem.Ratio = item.ratio;
                        if (item.ratio != 0)
                        {
                            var totalqty = item.qty * item.ratio;
                            item.totalQty = totalqty;
                        }
                        resultItem.TotalQty = item.totalQty;
                        resultItem.ProductConversion_Index = item.productConversion_Index;
                        resultItem.ProductConversion_Id = item.productConversion_Id;
                        resultItem.ProductConversion_Name = item.productConversion_Name;
                        resultItem.MFG_Date = item.mFG_Date.toDate();
                        resultItem.EXP_Date = item.eXP_Date.toDate();

                        resultItem.WeightRatio = item.weightRatio;
                        resultItem.UnitWeight = item.unitWeight;
                        resultItem.Weight = item.qty * (item.unitWeight * item.weightRatio);
                        resultItem.Weight_Index = item.weight_Index;
                        resultItem.Weight_Id = item.weight_Id;
                        resultItem.Weight_Name = item.weight_Name;
                        resultItem.NetWeight = item.netWeight;

                        resultItem.GrsWeightRatio = item.grsWeightRatio;
                        resultItem.GrsWeight = item.grsWeight;
                        resultItem.UnitGrsWeight = item.grsWeight / item.grsWeightRatio;
                        resultItem.GrsWeight_Index = item.grsWeight_Index;
                        resultItem.GrsWeight_Id = item.grsWeight_Id;
                        resultItem.GrsWeight_Name = item.grsWeight_Name;

                        resultItem.WidthRatio = item.widthRatio;
                        resultItem.UnitWidth = item.unitWidth;
                        resultItem.Width = item.unitWidth * item.qty;
                        resultItem.Width_Index = item.width_Index;
                        resultItem.Width_Id = item.width_Id;
                        resultItem.Width_Name = item.width_Name;

                        resultItem.LengthRatio = item.lengthRatio;
                        resultItem.UnitLength = item.unitLength;
                        resultItem.Length = item.unitLength * item.qty;
                        resultItem.Length_Index = item.length_Index;
                        resultItem.Length_Id = item.length_Id;
                        resultItem.Length_Name = item.length_Name;

                        resultItem.HeightRatio = item.heightRatio;
                        resultItem.UnitHeight = item.unitHeight;
                        resultItem.Height = item.unitHeight * item.qty;
                        resultItem.Height_Index = item.height_Index;
                        resultItem.Height_Id = item.height_Id;
                        resultItem.Height_Name = item.height_Name;

                        resultItem.UnitVolume = (resultItem.UnitWidth * resultItem.UnitLength * resultItem.UnitHeight) / item.volume_Ratio;
                        resultItem.Volume = resultItem.Qty * resultItem.UnitVolume;


                        resultItem.UnitPrice = item.unitPrice;
                        resultItem.Price = item.unitPrice * item.qty;
                        resultItem.TotalPrice = resultItem.Price * resultItem.Qty;

                        resultItem.Currency_Index = item.currency_Index;
                        resultItem.Currency_Id = item.currency_Id;
                        resultItem.Currency_Name = item.currency_Name;

                        resultItem.Ref_Code1 = item.ref_Code1;
                        resultItem.Ref_Code2 = item.ref_Code2;
                        resultItem.Ref_Code3 = item.ref_Code3;
                        resultItem.Ref_Code4 = item.ref_Code4;
                        resultItem.Ref_Code5 = item.ref_Code5;


                        resultItem.DocumentRef_No1 = item.documentRef_No1;
                        resultItem.DocumentRef_No2 = item.documentRef_No2;
                        resultItem.DocumentRef_No3 = item.documentRef_No3;
                        resultItem.DocumentRef_No4 = item.documentRef_No4;
                        resultItem.DocumentRef_No5 = item.documentRef_No5;
                        resultItem.Document_Status = 0;
                        resultItem.DocumentItem_Remark = item.documentItem_Remark;
                        resultItem.UDF_1 = item.uDF_1;
                        resultItem.UDF_2 = item.uDF_2;
                        resultItem.UDF_3 = item.uDF_3;
                        resultItem.UDF_4 = item.uDF_4;
                        resultItem.UDF_5 = item.uDF_5;
                        resultItem.Create_By = userName;
                        resultItem.Create_Date = DateTime.Now;
                        db.im_PurchaseOrderItem.Add(resultItem);

                    }
                }
                else
                {
                    PurchaseOrderNo = PurchaseOrderOld.PurchaseOrder_No;
                    PurchaseOrderOld.PurchaseOrder_Index = data.purchaseOrder_Index;
                    PurchaseOrderOld.PurchaseOrder_No = data.purchaseOrder_No;
                    PurchaseOrderOld.Owner_Index = data.owner_Index;
                    PurchaseOrderOld.Owner_Id = data.owner_Id;
                    PurchaseOrderOld.Owner_Name = data.owner_Name;
                    PurchaseOrderOld.Vendor_Index = data.vendor_Index;
                    PurchaseOrderOld.Vendor_Id = data.vendor_Id;
                    PurchaseOrderOld.Vendor_Name = data.vendor_Name;
                    PurchaseOrderOld.DocumentType_Index = data.documentType_Index;
                    PurchaseOrderOld.DocumentType_Id = data.documentType_Id;
                    PurchaseOrderOld.DocumentType_Name = data.documentType_Name;
                    PurchaseOrderOld.PurchaseOrder_Date = data.purchaseOrder_Date.toDate();
                    PurchaseOrderOld.PurchaseOrder_Due_Date = data.purchaseOrder_Due_Date.toDate();
                    PurchaseOrderOld.PurchaseOrder_Time = data.purchaseOrder_Time;

                    PurchaseOrderOld.DocumentRef_No1 = data.documentRef_No1;
                    PurchaseOrderOld.DocumentRef_No2 = data.documentRef_No2;
                    PurchaseOrderOld.DocumentRef_No3 = data.documentRef_No3;
                    PurchaseOrderOld.DocumentRef_No4 = data.documentRef_No4;
                    PurchaseOrderOld.DocumentRef_No5 = data.documentRef_No5;
                    PurchaseOrderOld.Document_Status = data.document_Status;
                    PurchaseOrderOld.UDF_1 = data.uDF_1;
                    PurchaseOrderOld.UDF_2 = data.uDF_2;
                    PurchaseOrderOld.UDF_3 = data.uDF_3;
                    PurchaseOrderOld.UDF_4 = data.uDF_4;
                    PurchaseOrderOld.UDF_5 = data.uDF_5;
                    PurchaseOrderOld.Document_Remark = data.document_Remark;
                    PurchaseOrderOld.Warehouse_Index = data.warehouse_Index;
                    PurchaseOrderOld.Warehouse_Id = data.warehouse_Id;
                    PurchaseOrderOld.Warehouse_Name = data.warehouse_Name;
                    if (IsNew != true)
                    {
                        PurchaseOrderOld.Update_By = data.update_By;
                        PurchaseOrderOld.Update_Date = DateTime.Now;
                    }

                    if (data.documents != null)
                    {
                        foreach (var d in data.documents)
                        {
                            if (d.index == null || d.index == Guid.Empty)
                            {
                                im_DocumentFile documents = new im_DocumentFile();

                                documents.DocumentFile_Index = Guid.NewGuid(); ;
                                documents.DocumentFile_Name = d.filename;
                                documents.DocumentFile_Path = d.path;
                                documents.DocumentFile_Url = d.urlAttachFile;
                                documents.DocumentFile_Type = d.type;
                                documents.DocumentFile_Status = 0;
                                documents.Create_By = userName;
                                documents.Create_Date = DateTime.Now;
                                documents.Ref_Index = PurchaseOrderOld.PurchaseOrder_Index;
                                documents.Ref_No = PurchaseOrderOld.PurchaseOrder_No;
                                db.im_DocumentFile.Add(documents);
                            }
                            else if ((d.index != null || d.index != Guid.Empty) && d.isDelete)
                            {
                                var Documents = db.im_DocumentFile.FirstOrDefault(c => c.DocumentFile_Index == d.index && c.Ref_Index == PurchaseOrderOld.PurchaseOrder_Index && c.DocumentFile_Status == 0);
                                Documents.DocumentFile_Status = -1;
                                Documents.Update_By = data.update_By;
                                Documents.Update_Date = DateTime.Now;
                            }
                        }
                    }

                    foreach (var item in data.listPurchaseOrderItemViewModel)
                    {

                        var PurchaseOrderItemOld = db.im_PurchaseOrderItem.Find(item.purchaseOrderItem_Index);

                        if (PurchaseOrderItemOld != null)
                        {

                            var Productresult = new List<ProductViewModel>();

                            var ProductfilterModel = new ProductViewModel();
                            ProductfilterModel.product_Index = item.product_Index;

                            //GetConfig
                            Productresult = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("product"), ProductfilterModel.sJson());


                            int addNumber = 0;

                            im_PurchaseOrderItem resultItem = new im_PurchaseOrderItem();

                            //Get ItemStatus

                            addNumber++;

                            PurchaseOrderItemOld.PurchaseOrderItem_Index = item.purchaseOrderItem_Index;
                            PurchaseOrderItemOld.PurchaseOrder_Index = item.purchaseOrder_Index;

                            if (item.lineNum == null)
                            {
                                PurchaseOrderItemOld.LineNum = addNumber.ToString();
                            }
                            else
                            {
                                PurchaseOrderItemOld.LineNum = item.lineNum;
                            }

                            PurchaseOrderItemOld.Product_Index = item.product_Index;
                            PurchaseOrderItemOld.Product_Id = item.product_Id;
                            PurchaseOrderItemOld.Product_Name = item.product_Name;
                            if (Productresult.Count > 0)
                            {
                                PurchaseOrderItemOld.Product_SecondName = Productresult.FirstOrDefault().product_SecondName;
                                PurchaseOrderItemOld.Product_ThirdName = Productresult.FirstOrDefault().product_ThirdName;
                            }

                            if (item.product_Lot != null)
                            {
                                PurchaseOrderItemOld.Product_Lot = item.product_Lot;
                            }
                            else
                            {
                                PurchaseOrderItemOld.Product_Lot = "";
                            }
                            PurchaseOrderItemOld.ItemStatus_Index = item.itemStatus_Index;
                            PurchaseOrderItemOld.ItemStatus_Id = item.itemStatus_Id;
                            PurchaseOrderItemOld.ItemStatus_Name = item.itemStatus_Name;

                            PurchaseOrderItemOld.Qty = item.qty;
                            PurchaseOrderItemOld.Ratio = item.ratio;
                            if (item.ratio != 0)
                            {
                                var totalqty = item.qty * item.ratio;
                                item.totalQty = totalqty;
                            }
                            PurchaseOrderItemOld.TotalQty = item.totalQty;
                            PurchaseOrderItemOld.ProductConversion_Index = item.productConversion_Index;
                            PurchaseOrderItemOld.ProductConversion_Id = item.productConversion_Id;
                            PurchaseOrderItemOld.ProductConversion_Name = item.productConversion_Name;
                            PurchaseOrderItemOld.MFG_Date = item.mFG_Date.toDate();
                            PurchaseOrderItemOld.EXP_Date = item.eXP_Date.toDate();

                            PurchaseOrderItemOld.WeightRatio = item.weightRatio;
                            PurchaseOrderItemOld.UnitWeight = item.unitWeight;
                            PurchaseOrderItemOld.Weight = item.qty * (item.unitWeight * item.weightRatio);
                            PurchaseOrderItemOld.Weight_Index = item.weight_Index;
                            PurchaseOrderItemOld.Weight_Id = item.weight_Id;
                            PurchaseOrderItemOld.Weight_Name = item.weight_Name;
                            PurchaseOrderItemOld.NetWeight = PurchaseOrderItemOld.Weight * item.qty;

                            PurchaseOrderItemOld.GrsWeightRatio = item.grsWeightRatio;
                            PurchaseOrderItemOld.UnitGrsWeight = item.unitGrsWeight;
                            PurchaseOrderItemOld.GrsWeight = item.qty * (item.unitGrsWeight * item.grsWeightRatio);
                            PurchaseOrderItemOld.GrsWeight_Index = item.grsWeight_Index;
                            PurchaseOrderItemOld.GrsWeight_Id = item.grsWeight_Id;
                            PurchaseOrderItemOld.GrsWeight_Name = item.grsWeight_Name;

                            PurchaseOrderItemOld.WidthRatio = item.widthRatio;
                            PurchaseOrderItemOld.UnitWidth = item.unitWidth;
                            PurchaseOrderItemOld.Width = item.unitWidth * item.qty;
                            PurchaseOrderItemOld.Width_Index = item.width_Index;
                            PurchaseOrderItemOld.Width_Id = item.width_Id;
                            PurchaseOrderItemOld.Width_Name = item.width_Name;

                            PurchaseOrderItemOld.LengthRatio = item.lengthRatio;
                            PurchaseOrderItemOld.UnitLength = item.unitLength;
                            PurchaseOrderItemOld.Length = item.unitLength * item.qty;
                            PurchaseOrderItemOld.Length_Index = item.length_Index;
                            PurchaseOrderItemOld.Length_Id = item.length_Id;
                            PurchaseOrderItemOld.Length_Name = item.length_Name;

                            PurchaseOrderItemOld.HeightRatio = item.heightRatio;
                            PurchaseOrderItemOld.UnitHeight = item.unitHeight;
                            PurchaseOrderItemOld.Height = item.unitHeight * item.qty;
                            PurchaseOrderItemOld.Height_Index = item.height_Index;
                            PurchaseOrderItemOld.Height_Id = item.height_Id;
                            PurchaseOrderItemOld.Height_Name = item.height_Name;


                            PurchaseOrderItemOld.UnitVolume = (PurchaseOrderItemOld.UnitWidth * PurchaseOrderItemOld.UnitLength * PurchaseOrderItemOld.UnitHeight) / item.volume_Ratio;
                            PurchaseOrderItemOld.Volume = resultItem.Qty * PurchaseOrderItemOld.UnitVolume;


                            PurchaseOrderItemOld.UnitPrice = item.unitPrice;
                            PurchaseOrderItemOld.Price = item.unitPrice * item.qty;
                            PurchaseOrderItemOld.TotalPrice = PurchaseOrderItemOld.Price * PurchaseOrderItemOld.Qty;

                            PurchaseOrderItemOld.Currency_Index = item.currency_Index;
                            PurchaseOrderItemOld.Currency_Id = item.currency_Id;
                            PurchaseOrderItemOld.Currency_Name = item.currency_Name;

                            PurchaseOrderItemOld.Ref_Code1 = item.ref_Code1;
                            PurchaseOrderItemOld.Ref_Code2 = item.ref_Code2;
                            PurchaseOrderItemOld.Ref_Code3 = item.ref_Code3;
                            PurchaseOrderItemOld.Ref_Code4 = item.ref_Code4;
                            PurchaseOrderItemOld.Ref_Code5 = item.ref_Code5;

                            PurchaseOrderItemOld.DocumentRef_No1 = item.documentRef_No1;
                            PurchaseOrderItemOld.DocumentRef_No2 = item.documentRef_No2;
                            PurchaseOrderItemOld.DocumentRef_No3 = item.documentRef_No3;
                            PurchaseOrderItemOld.DocumentRef_No4 = item.documentRef_No4;
                            PurchaseOrderItemOld.DocumentRef_No5 = item.documentRef_No5;
                            PurchaseOrderItemOld.Document_Status = 0;
                            PurchaseOrderItemOld.DocumentItem_Remark = item.documentItem_Remark;
                            PurchaseOrderItemOld.UDF_1 = item.uDF_1;
                            PurchaseOrderItemOld.UDF_2 = item.uDF_2;
                            PurchaseOrderItemOld.UDF_3 = item.uDF_3;
                            PurchaseOrderItemOld.UDF_4 = item.uDF_4;
                            PurchaseOrderItemOld.UDF_5 = item.uDF_5;
                            PurchaseOrderItemOld.Update_By = userName;
                            PurchaseOrderItemOld.Update_Date = DateTime.Now;

                        }

                        else
                        {
                            int addNumber = 0;

                            im_PurchaseOrderItem resultItem = new im_PurchaseOrderItem();

                            var Productresult = new List<ProductViewModel>();

                            var ProductfilterModel = new ProductViewModel();
                            ProductfilterModel.product_Index = item.product_Index;

                            //GetConfig
                            Productresult = utils.SendDataApi<List<ProductViewModel>>(new AppSettingConfig().GetUrl("product"), ProductfilterModel.sJson());


                            addNumber++;
                            // Gen Index for line item

                            item.purchaseOrderItem_Index = Guid.NewGuid();
                            //resultItem.PlanGoodsReceive_Index = item.planGoodsReceive_Index;
                            resultItem.PurchaseOrder_Index = data.purchaseOrder_Index;

                            // Index From Header

                            if (item.lineNum == null)
                            {
                                resultItem.LineNum = addNumber.ToString();
                            }
                            else
                            {
                                resultItem.LineNum = item.lineNum;
                            }
                            resultItem.ItemStatus_Index = item.itemStatus_Index;

                            resultItem.ItemStatus_Id = item.itemStatus_Id;

                            resultItem.ItemStatus_Name = item.itemStatus_Name;
                            resultItem.Product_Index = item.product_Index;
                            resultItem.Product_Id = item.product_Id;
                            resultItem.Product_Name = item.product_Name;
                            if (Productresult.Count > 0)
                            {
                                resultItem.Product_SecondName = Productresult.FirstOrDefault().product_SecondName;
                                resultItem.Product_ThirdName = Productresult.FirstOrDefault().product_ThirdName;
                            }
                            if (item.product_Lot != null)
                            {
                                resultItem.Product_Lot = item.product_Lot;
                            }
                            else
                            {
                                resultItem.Product_Lot = "";
                            }
                            resultItem.Qty = item.qty;
                            resultItem.Ratio = item.ratio;
                            if (item.ratio != 0)
                            {
                                var totalqty = item.qty * item.ratio;
                                item.totalQty = totalqty;
                            }
                            resultItem.TotalQty = item.totalQty;
                            resultItem.ProductConversion_Index = item.productConversion_Index;
                            resultItem.ProductConversion_Id = item.productConversion_Id;
                            resultItem.ProductConversion_Name = item.productConversion_Name;
                            resultItem.MFG_Date = item.mFG_Date.toDate();
                            resultItem.EXP_Date = item.eXP_Date.toDate();

                            resultItem.WeightRatio = item.weightRatio;
                            resultItem.UnitWeight = item.unitWeight;
                            resultItem.Weight = item.qty * (item.unitWeight * item.weightRatio);
                            resultItem.Weight_Index = item.weight_Index;
                            resultItem.Weight_Id = item.weight_Id;
                            resultItem.Weight_Name = item.weight_Name;
                            resultItem.NetWeight = resultItem.Weight * item.qty;

                            resultItem.GrsWeightRatio = item.grsWeightRatio;
                            resultItem.UnitGrsWeight = item.unitGrsWeight;
                            resultItem.GrsWeight = item.qty * (item.unitGrsWeight * item.grsWeightRatio);
                            resultItem.GrsWeight_Index = item.grsWeight_Index;
                            resultItem.GrsWeight_Id = item.grsWeight_Id;
                            resultItem.GrsWeight_Name = item.grsWeight_Name;

                            resultItem.WidthRatio = item.widthRatio;
                            resultItem.UnitWidth = item.unitWidth;
                            resultItem.Width = item.unitWidth * item.qty;
                            resultItem.Width_Index = item.width_Index;
                            resultItem.Width_Id = item.width_Id;
                            resultItem.Width_Name = item.width_Name;

                            resultItem.LengthRatio = item.lengthRatio;
                            resultItem.UnitLength = item.unitLength;
                            resultItem.Length = item.unitLength * item.qty;
                            resultItem.Length_Index = item.length_Index;
                            resultItem.Length_Id = item.length_Id;
                            resultItem.Length_Name = item.length_Name;

                            resultItem.HeightRatio = item.heightRatio;
                            resultItem.UnitHeight = item.unitHeight;
                            resultItem.Height = item.unitHeight * item.qty;
                            resultItem.Height_Index = item.height_Index;
                            resultItem.Height_Id = item.height_Id;
                            resultItem.Height_Name = item.height_Name;

                            //resultItem.UnitVolume = item.unitVolume;
                            //resultItem.Volume = item.volume;

                            resultItem.UnitVolume = (resultItem.UnitWidth * resultItem.UnitLength * resultItem.UnitHeight) / item.volume_Ratio;
                            resultItem.Volume = resultItem.Qty * resultItem.UnitVolume;

                            resultItem.UnitPrice = item.unitPrice;
                            resultItem.Price = item.unitPrice * item.qty;
                            resultItem.TotalPrice = resultItem.Price * resultItem.Qty;

                            resultItem.Currency_Index = item.currency_Index;
                            resultItem.Currency_Id = item.currency_Id;
                            resultItem.Currency_Name = item.currency_Name;

                            resultItem.Ref_Code1 = item.ref_Code1;
                            resultItem.Ref_Code2 = item.ref_Code2;
                            resultItem.Ref_Code3 = item.ref_Code3;
                            resultItem.Ref_Code4 = item.ref_Code4;
                            resultItem.Ref_Code5 = item.ref_Code5;

                            resultItem.DocumentRef_No1 = item.documentRef_No1;
                            resultItem.DocumentRef_No2 = item.documentRef_No2;
                            resultItem.DocumentRef_No3 = item.documentRef_No3;
                            resultItem.DocumentRef_No4 = item.documentRef_No4;
                            resultItem.DocumentRef_No5 = item.documentRef_No5;
                            resultItem.Document_Status = 0;
                            resultItem.DocumentItem_Remark = item.documentItem_Remark;
                            resultItem.UDF_1 = item.uDF_1;
                            resultItem.UDF_2 = item.uDF_2;
                            resultItem.UDF_3 = item.uDF_3;
                            resultItem.UDF_4 = item.uDF_4;
                            resultItem.UDF_5 = item.uDF_5;
                            resultItem.Update_By = userName;
                            resultItem.Update_Date = DateTime.Now;

                            db.im_PurchaseOrderItem.Add(resultItem);
                        }


                    }

                    var deleteItem = db.im_PurchaseOrderItem.Where(c => !data.listPurchaseOrderItemViewModel.Select(s => s.purchaseOrderItem_Index).Contains(c.PurchaseOrderItem_Index)
                                        && c.PurchaseOrder_Index == PurchaseOrderOld.PurchaseOrder_Index).ToList();

                    foreach (var c in deleteItem)
                    {
                        var deletePurchaseOrderItem = db.im_PurchaseOrderItem.Find(c.PurchaseOrderItem_Index);

                        deletePurchaseOrderItem.Document_Status = -1;
                        deletePurchaseOrderItem.Update_By = userName;
                        deletePurchaseOrderItem.Update_Date = DateTime.Now;

                    }
                }

                var transactionx = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    transactionx.Commit();
                }

                catch (Exception exy)
                {
                    msglog = State + " ex Rollback " + exy.Message.ToString();
                    olog.logging("SavePO", msglog);
                    transactionx.Rollback();

                    throw exy;

                }

                actionResult.document_No = PurchaseOrderNo;
                actionResult.Message = true;

                return actionResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region find
        public PurchaseOrderDocViewModel find(Guid id)
        {

            try
            {
                var queryResult = db.im_PurchaseOrder.Where(c => c.PurchaseOrder_Index == id).FirstOrDefault();

                var resultItem = new PurchaseOrderDocViewModel();


                var ProcessStatus = new List<ProcessStatusViewModel>();

                var filterModel = new ProcessStatusViewModel();

                filterModel.process_Index = new Guid("E6386717-A2F3-4DC0-837B-B94A2B44D274");

                //GetConfig
                ProcessStatus = utils.SendDataApi<List<ProcessStatusViewModel>>(new AppSettingConfig().GetUrl("processStatus"), filterModel.sJson());


                resultItem.purchaseOrder_Index = queryResult.PurchaseOrder_Index;
                resultItem.purchaseOrder_No = queryResult.PurchaseOrder_No;
                resultItem.documentType_Index = queryResult.DocumentType_Index.GetValueOrDefault();
                resultItem.documentType_Name = queryResult.DocumentType_Name;
                resultItem.documentType_Id = queryResult.DocumentType_Id;
                resultItem.vendor_Index = queryResult.Vendor_Index.GetValueOrDefault();
                resultItem.vendor_Id = queryResult.Vendor_Id;
                resultItem.vendor_Name = queryResult.Vendor_Name;
                resultItem.owner_Index = queryResult.Owner_Index.GetValueOrDefault();
                resultItem.owner_Id = queryResult.Owner_Id;
                resultItem.owner_Name = queryResult.Owner_Name;
                resultItem.uDF_1 = queryResult.UDF_1;
                resultItem.documentRef_No1 = queryResult.DocumentRef_No1;
                resultItem.documentRef_No2 = queryResult.DocumentRef_No2;
                resultItem.documentRef_No3 = queryResult.DocumentRef_No3;
                resultItem.documentRef_No4 = queryResult.DocumentRef_No4;
                resultItem.documentRef_No5 = queryResult.DocumentRef_No5;
                resultItem.document_Status = queryResult.Document_Status;
                resultItem.warehouse_Index = queryResult.Warehouse_Index;
                resultItem.warehouse_Index_To = queryResult.Warehouse_Index_To;
                resultItem.warehouse_Id = queryResult.Warehouse_Id;
                resultItem.warehouse_Id_To = queryResult.Warehouse_Id_To;
                resultItem.warehouse_Name = queryResult.Warehouse_Name;
                resultItem.warehouse_Name_To = queryResult.Warehouse_Name_To;
                resultItem.document_Remark = queryResult.Document_Remark;
                resultItem.purchaseOrder_Date = queryResult.PurchaseOrder_Date.toString();
                resultItem.purchaseOrder_Due_Date = queryResult.PurchaseOrder_Due_Date.toString();
                resultItem.userAssign = queryResult.UserAssign;

                resultItem.purchaseOrder_Time = queryResult.PurchaseOrder_Time;
                resultItem.dock_Index = queryResult.Dock_Index;
                resultItem.dock_Id = queryResult.Dock_Id;
                resultItem.dock_Name = queryResult.Dock_Name;
                resultItem.vehicleType_Index = queryResult.VehicleType_Index;
                resultItem.vehicleType_Id = queryResult.VehicleType_Id;
                resultItem.vehicleType_Name = queryResult.VehicleType_Name;
                resultItem.transport_Index = queryResult.Transport_Index;
                resultItem.transport_Id = queryResult.Transport_Id;
                resultItem.transport_Name = queryResult.Transport_Name;
                resultItem.driver_Name = queryResult.Driver_Name;
                resultItem.round_Index = queryResult.Round_Index;
                resultItem.round_Id = queryResult.Round_Id;
                resultItem.round_Name = queryResult.Round_Name;
                resultItem.license_Name = queryResult.License_Name;

                resultItem.dock_Index = queryResult.Dock_Index;
                resultItem.dock_Id = queryResult.Dock_Id;
                resultItem.dock_Name = queryResult.Dock_Name;
                resultItem.vehicleType_Index = queryResult.VehicleType_Index;
                resultItem.vehicleType_Id = queryResult.VehicleType_Id;
                resultItem.vehicleType_Name = queryResult.VehicleType_Name;
                resultItem.transport_Index = queryResult.Transport_Index;
                resultItem.transport_Id = queryResult.Transport_Id;
                resultItem.transport_Name = queryResult.Transport_Name;
                resultItem.driver_Name = queryResult.Driver_Name;
                resultItem.round_Index = queryResult.Round_Index;
                resultItem.round_Id = queryResult.Round_Id;
                resultItem.round_Name = queryResult.Round_Name;
                resultItem.license_Name = queryResult.License_Name;
                resultItem.forwarder_Index = queryResult.Forwarder_Index;
                resultItem.forwarder_Id = queryResult.Forwarder_Id;
                resultItem.forwarder_Name = queryResult.Forwarder_Name;
                resultItem.shipmentType_Index = queryResult.ShipmentType_Index;
                resultItem.shipmentType_Id = queryResult.ShipmentType_Id;
                resultItem.shipmentType_Name = queryResult.ShipmentType_Name;
                resultItem.cargoType_Index = queryResult.CargoType_Index;
                resultItem.cargoType_Id = queryResult.CargoType_Id;
                resultItem.cargoType_Name = queryResult.CargoType_Name;
                resultItem.unloadingType_Index = queryResult.UnloadingType_Index;
                resultItem.unloadingType_Id = queryResult.UnloadingType_Id;
                resultItem.unloadingType_Name = queryResult.UnloadingType_Name;
                resultItem.containerType_Index = queryResult.ContainerType_Index;
                resultItem.containerType_Id = queryResult.ContainerType_Id;
                resultItem.containerType_Name = queryResult.ContainerType_Name;
                resultItem.container_No1 = queryResult.Container_No1;
                resultItem.container_No2 = queryResult.Container_No2;
                resultItem.labur = queryResult.Labur;

                String Statue = "";

                Statue = queryResult.Document_Status.ToString();
                var ProcessStatusName = ProcessStatus.Where(c => c.processStatus_Id == Statue).FirstOrDefault();
                resultItem.processStatus_Name = ProcessStatusName.processStatus_Name;


                var owner = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoOwnerFilter"), new { key = queryResult.Owner_Id }.sJson()).FirstOrDefault();
                resultItem.ownerDocumentRef_No1 = owner.value1;
                resultItem.ownerDocumentRef_No2 = owner.value2;
                resultItem.ownerDocumentRef_No3 = owner.value3;

                var Listdocuments = new List<document>();
                var DocumentFile = db.im_DocumentFile.Where(c => c.Ref_Index == queryResult.PurchaseOrder_Index && c.DocumentFile_Status == 0).ToList();
                foreach (var d in DocumentFile)
                {
                    var documents = new document();
                    documents.index = d.DocumentFile_Index;
                    documents.filename = d.DocumentFile_Name;
                    documents.path = d.DocumentFile_Path;
                    documents.urlAttachFile = d.DocumentFile_Url;
                    Listdocuments.Add(documents);
                }
                resultItem.documents = Listdocuments;

                return resultItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region findbyYard
        public PurchaseOrderDocViewModel findbyYard(string id)
        {

            try
            {
                var queryResult = db.im_PurchaseOrder.Where(c => c.PurchaseOrder_No == id).FirstOrDefault();

                var resultItem = new PurchaseOrderDocViewModel();


                var ProcessStatus = new List<ProcessStatusViewModel>();

                var filterModel = new ProcessStatusViewModel();

                filterModel.process_Index = new Guid("E6386717-A2F3-4DC0-837B-B94A2B44D274");

                //GetConfig
                ProcessStatus = utils.SendDataApi<List<ProcessStatusViewModel>>(new AppSettingConfig().GetUrl("processStatus"), filterModel.sJson());


                resultItem.purchaseOrder_Index = queryResult.PurchaseOrder_Index;
                resultItem.purchaseOrder_No = queryResult.PurchaseOrder_No;
                resultItem.documentType_Index = queryResult.DocumentType_Index.GetValueOrDefault();
                resultItem.documentType_Name = queryResult.DocumentType_Name;
                resultItem.documentType_Id = queryResult.DocumentType_Id;
                resultItem.vendor_Index = queryResult.Vendor_Index.GetValueOrDefault();
                resultItem.vendor_Id = queryResult.Vendor_Id;
                resultItem.vendor_Name = queryResult.Vendor_Name;
                resultItem.owner_Index = queryResult.Owner_Index.GetValueOrDefault();
                resultItem.owner_Id = queryResult.Owner_Id;
                resultItem.owner_Name = queryResult.Owner_Name;
                resultItem.uDF_1 = queryResult.UDF_1;
                resultItem.documentRef_No1 = queryResult.DocumentRef_No1;
                resultItem.documentRef_No2 = queryResult.DocumentRef_No2;
                resultItem.documentRef_No3 = queryResult.DocumentRef_No3;
                resultItem.documentRef_No4 = queryResult.DocumentRef_No4;
                resultItem.documentRef_No5 = queryResult.DocumentRef_No5;
                resultItem.document_Status = 0;
                resultItem.warehouse_Index = queryResult.Warehouse_Index;
                resultItem.warehouse_Index_To = queryResult.Warehouse_Index_To;
                resultItem.warehouse_Id = queryResult.Warehouse_Id;
                resultItem.warehouse_Id_To = queryResult.Warehouse_Id_To;
                resultItem.warehouse_Name = queryResult.Warehouse_Name;
                resultItem.warehouse_Name_To = queryResult.Warehouse_Name_To;
                resultItem.document_Remark = queryResult.Document_Remark;
                resultItem.purchaseOrder_Date = queryResult.PurchaseOrder_Date.toString();
                resultItem.purchaseOrder_Due_Date = queryResult.PurchaseOrder_Due_Date.toString();
                resultItem.userAssign = queryResult.UserAssign;

                resultItem.purchaseOrder_Time = queryResult.PurchaseOrder_Time;
                resultItem.dock_Index = queryResult.Dock_Index;
                resultItem.dock_Id = queryResult.Dock_Id;
                resultItem.dock_Name = queryResult.Dock_Name;
                resultItem.vehicleType_Index = queryResult.VehicleType_Index;
                resultItem.vehicleType_Id = queryResult.VehicleType_Id;
                resultItem.vehicleType_Name = queryResult.VehicleType_Name;
                resultItem.transport_Index = queryResult.Transport_Index;
                resultItem.transport_Id = queryResult.Transport_Id;
                resultItem.transport_Name = queryResult.Transport_Name;
                resultItem.driver_Name = queryResult.Driver_Name;
                resultItem.round_Index = queryResult.Round_Index;
                resultItem.round_Id = queryResult.Round_Id;
                resultItem.round_Name = queryResult.Round_Name;
                resultItem.license_Name = queryResult.License_Name;

                resultItem.dock_Index = queryResult.Dock_Index;
                resultItem.dock_Id = queryResult.Dock_Id;
                resultItem.dock_Name = queryResult.Dock_Name;
                resultItem.vehicleType_Index = queryResult.VehicleType_Index;
                resultItem.vehicleType_Id = queryResult.VehicleType_Id;
                resultItem.vehicleType_Name = queryResult.VehicleType_Name;
                resultItem.transport_Index = queryResult.Transport_Index;
                resultItem.transport_Id = queryResult.Transport_Id;
                resultItem.transport_Name = queryResult.Transport_Name;
                resultItem.driver_Name = queryResult.Driver_Name;
                resultItem.round_Index = queryResult.Round_Index;
                resultItem.round_Id = queryResult.Round_Id;
                resultItem.round_Name = queryResult.Round_Name;
                resultItem.license_Name = queryResult.License_Name;
                resultItem.forwarder_Index = queryResult.Forwarder_Index;
                resultItem.forwarder_Id = queryResult.Forwarder_Id;
                resultItem.forwarder_Name = queryResult.Forwarder_Name;
                resultItem.shipmentType_Index = queryResult.ShipmentType_Index;
                resultItem.shipmentType_Id = queryResult.ShipmentType_Id;
                resultItem.shipmentType_Name = queryResult.ShipmentType_Name;
                resultItem.cargoType_Index = queryResult.CargoType_Index;
                resultItem.cargoType_Id = queryResult.CargoType_Id;
                resultItem.cargoType_Name = queryResult.CargoType_Name;
                resultItem.unloadingType_Index = queryResult.UnloadingType_Index;
                resultItem.unloadingType_Id = queryResult.UnloadingType_Id;
                resultItem.unloadingType_Name = queryResult.UnloadingType_Name;
                resultItem.containerType_Index = queryResult.ContainerType_Index;
                resultItem.containerType_Id = queryResult.ContainerType_Id;
                resultItem.containerType_Name = queryResult.ContainerType_Name;
                resultItem.container_No1 = queryResult.Container_No1;
                resultItem.container_No2 = queryResult.Container_No2;
                resultItem.labur = queryResult.Labur;

                String Statue = "";

                Statue = queryResult.Document_Status.ToString();
                var ProcessStatusName = ProcessStatus.Where(c => c.processStatus_Id == Statue).FirstOrDefault();
                resultItem.processStatus_Name = ProcessStatusName.processStatus_Name;


                var owner = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoOwnerFilter"), new { key = queryResult.Owner_Id }.sJson()).FirstOrDefault();
                resultItem.ownerDocumentRef_No1 = owner.value1;
                resultItem.ownerDocumentRef_No2 = owner.value2;
                resultItem.ownerDocumentRef_No3 = owner.value3;

                var Listdocuments = new List<document>();
                var DocumentFile = db.im_DocumentFile.Where(c => c.Ref_Index == queryResult.PurchaseOrder_Index && c.DocumentFile_Status == 0).ToList();
                foreach (var d in DocumentFile)
                {
                    var documents = new document();
                    documents.index = d.DocumentFile_Index;
                    documents.filename = d.DocumentFile_Name;
                    documents.path = d.DocumentFile_Path;
                    documents.urlAttachFile = d.DocumentFile_Url;
                    Listdocuments.Add(documents);
                }
                resultItem.documents = Listdocuments;

                return resultItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Delete
        public Boolean Delete(PurchaseOrderDocViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var PlanGoodsReceive = db.im_PurchaseOrder.Find(data.purchaseOrder_Index);

                if (PlanGoodsReceive != null)
                {
                    PlanGoodsReceive.Document_Status = -1;
                    PlanGoodsReceive.Cancel_By = data.update_By;
                    PlanGoodsReceive.Cancel_Date = DateTime.Now;

                    var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("DeletePO", msglog);
                        transaction.Rollback();
                        throw exy;
                    }

                }


                return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ConfirmStatus
        public Boolean confirmStatus(PurchaseOrderDocViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var PlanGoodsReceive = db.im_PurchaseOrder.Find(data.purchaseOrder_Index);

                if (PlanGoodsReceive != null)
                {
                    PlanGoodsReceive.Document_Status = 1;
                    PlanGoodsReceive.Update_By = data.update_By;
                    PlanGoodsReceive.Update_Date = DateTime.Now;

                    var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("confirmStatus", msglog);
                        transaction.Rollback();
                        throw exy;
                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region updateUserAssign
        public String updateUserAssign(PurchaseOrderDocViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                var PurchaseOrder = db.im_PurchaseOrder.Find(data.purchaseOrder_Index);

                if (PurchaseOrder != null)
                {
                    PurchaseOrder.UserAssign = data.userAssign;

                    var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("UpdateUserAssign", msglog);
                        transaction.Rollback();
                        throw exy;
                    }
                }

                var FindUser = db.im_PurchaseOrder.Find(data.purchaseOrder_Index);

                return FindUser.UserAssign.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region deleteUserAssign
        public String deleteUserAssign(PurchaseOrderDocViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                if (!string.IsNullOrEmpty(data.purchaseOrder_Index.ToString().Replace("00000000-0000-0000-0000-000000000000", "")))
                {
                    var PurchaseOrder = db.im_PurchaseOrder.Find(data.purchaseOrder_Index);

                    if (PurchaseOrder != null)
                    {
                        PurchaseOrder.UserAssign = "";

                        var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                        try
                        {
                            db.SaveChanges();
                            transaction.Commit();
                        }

                        catch (Exception exy)
                        {
                            msglog = State + " ex Rollback " + exy.Message.ToString();
                            olog.logging("deleteUserAssign", msglog);
                            transaction.Rollback();
                            throw exy;
                        }
                    }

                    var FindUser = db.im_PurchaseOrder.Find(data.purchaseOrder_Index);

                    return FindUser.UserAssign.ToString();
                }
                else
                {
                    return "";
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region AutobasicSuggestion
        public List<ItemListViewModel> autobasicSuggestion(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.View_PO.Where(c => c.PurchaseOrder_No.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.PurchaseOrder_No,
                        key = s.PurchaseOrder_No
                    }).Distinct();

                    var query2 = db.View_PO.Where(c => c.Owner_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        name = s.Owner_Name,
                        key = s.Owner_Name
                    }).Distinct();

                    var query = query1.Union(query2).Union(query2);

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {

            }

            return items;
        }

        #endregion


        #region AutobasicSuggestion
        public List<ItemListViewModel> autobasicSuggestionPO(ItemListViewModel data)
        {
            var query = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = db.im_PurchaseOrder.Where(c => c.PurchaseOrder_No.Contains(data.key) && !new List<int?> { -1, -2 }.Contains(c.Document_Status)).OrderBy(o => o.PurchaseOrder_No).Select(s => new ItemListViewModel
                    {
                        name = s.PurchaseOrder_No,
                        key = s.PurchaseOrder_No
                    }).Distinct();


                    query = query1.Take(10).ToList();

                }

            }
            catch (Exception ex)
            {

            }

            return query;
        }

        #endregion


        #region AutobasicSuggestion
        public List<ItemListViewModel> autobasicSuggestionVender(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(data.key))
                {
                    List<int?> status = new List<int?> { -1, -2 };
                    var query1 = db.im_PurchaseOrder.Where(c => c.Vendor_Name.Contains(data.key) || c.Vendor_Id.Contains(data.key) && !new List<int?> { -1, -2 }.Contains(c.Document_Status)).Select(s => new ItemListViewModel
                    {
                        name = s.Vendor_Name,
                        id = s.Vendor_Id,
                        index = s.Vendor_Index
                    }).Distinct();


                    //var query3 = db.View_PlanGrProcessStatus.Where(c => c.Vendor_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    //{
                    //    name = s.Vendor_Name,
                    //    key = s.Vendor_Name

                    //}).Distinct();

                    var query = query1;

                    items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {

            }

            return items;
        }

        #endregion

        #region AutoOwnerfilter
        public List<ItemListViewModel> autoOwnerfilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();

                var filterModel = new ItemListViewModel();
                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoOwnerFilter"), filterModel.sJson());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoVenderfilter
        public List<ItemListViewModel> autoVenderfilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();

                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoVendorFilter"), filterModel.sJson());

                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoStatusfilter
        public List<ItemListViewModel> autoStatusfilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }
                if (data.chk != null)
                {
                    filterModel.chk = data.chk;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoStatusFilter"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoWarehousefilter
        public List<ItemListViewModel> autoWarehousefilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();

                var filterModel = new ItemListViewModel();
                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoWarehousefilter"), filterModel.sJson());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoDocumentTypefilter
        public List<ItemListViewModel> autoDocumentTypefilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();

                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                filterModel.index = new Guid("E6386717-A2F3-4DC0-837B-B94A2B44D274");


                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoDocumentTypefilter"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoPurchaseOrderNo
        public List<ItemListViewModel> autoPurchaseOrderNo(ItemListViewModel data)
        {
            try
            {
                var query = db.View_PO.AsQueryable();

                if (data.key == "-")
                {


                }
                else if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.PurchaseOrder_No.Contains(data.key));

                }

                //if (!string.IsNullOrEmpty(data.key))
                //{
                //    query = query.Where(c => c.PurchaseOrder_No.Contains(data.key));

                //}

                var items = new List<ItemListViewModel>();

                var result = query.Select(c => new { c.PurchaseOrder_Index, c.PurchaseOrder_No }).Distinct().Take(10).ToList();


                foreach (var item in result)
                {
                    var resultItem = new ItemListViewModel
                    {
                        index = item.PurchaseOrder_Index,
                        name = item.PurchaseOrder_No
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


        #region AutoUser
        public List<ItemListViewModel> autoUser(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }


                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoUserfilter"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoSku
        public List<ItemListViewModel> autoSkufilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }
                if (!string.IsNullOrEmpty(data.key2))
                {
                    filterModel.key2 = data.key2;
                }
                else
                {
                    filterModel.key2 = "00000000-0000-0000-0000-000000000000";
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoSkufilter"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoProduct
        public List<ItemListViewModel> autoProductfilter(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();


                var filterModel = new ItemListViewModel();

                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoProductfilter"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region DropdownDocumentType
        public List<DocumentTypeViewModel> DropdownDocumentType(DocumentTypeViewModel data)
        {
            try
            {
                var result = new List<DocumentTypeViewModel>();

                var filterModel = new DocumentTypeViewModel();


                filterModel.process_Index = new Guid("E6386717-A2F3-4DC0-837B-B94A2B44D274");

                //GetConfig 
                result = utils.SendDataApi<List<DocumentTypeViewModel>>(new AppSettingConfig().GetUrl("dropDownDocumentType"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DropdownStatus
        public List<ProcessStatusViewModel> dropdownStatus(ProcessStatusViewModel data)
        {
            try
            {
                var result = new List<ProcessStatusViewModel>();

                var filterModel = new ProcessStatusViewModel();


                filterModel.process_Index = new Guid("E6386717-A2F3-4DC0-837B-B94A2B44D274");

                //GetConfig
                result = utils.SendDataApi<List<ProcessStatusViewModel>>(new AppSettingConfig().GetUrl("dropdownStatus"), filterModel.sJson());


                //var resultStatus = result.Where(c => c.processStatus_Id.Contains("0") || c.processStatus_Id.Contains("1") || c.processStatus_Id.Contains("-1") || c.processStatus_Id.Contains("-2")).ToList();
                var resultStatus = result.Where(c => c.processStatus_Id == "0" || c.processStatus_Id == "1" || c.processStatus_Id == "-1" || c.processStatus_Id == "-2").ToList();

                return resultStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DropdownWarehouse
        public List<warehouseDocViewModel> dropdownWarehouse(warehouseDocViewModel data)
        {
            try
            {
                var result = new List<warehouseDocViewModel>();

                var filterModel = new warehouseDocViewModel();

                //GetConfig
                result = utils.SendDataApi<List<warehouseDocViewModel>>(new AppSettingConfig().GetUrl("dropdownWarehouse"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DropdownRound
        public List<roundDocViewModel> dropdownRound(roundDocViewModel data)
        {
            try
            {
                var result = new List<roundDocViewModel>();

                var filterModel = new roundDocViewModel();

                //GetConfig
                result = utils.SendDataApi<List<roundDocViewModel>>(new AppSettingConfig().GetUrl("dropdownRound"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region DropdownProductconversion
        public List<ProductConversionViewModelDoc> dropdownProductconversion(ProductConversionViewModelDoc data)
        {
            try
            {
                var result = new List<ProductConversionViewModelDoc>();

                var filterModel = new ProductConversionViewModelDoc();

                if (!string.IsNullOrEmpty(data.product_Index.ToString()))
                {
                    filterModel.product_Index = data.product_Index;
                }
                //GetConfig
                result = utils.SendDataApi<List<ProductConversionViewModelDoc>>(new AppSettingConfig().GetUrl("dropdownProductconversion"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region dropdownItemStatus
        public List<ItemStatusDocViewModel> dropdownItemStatus(ItemStatusDocViewModel data)
        {
            try
            {
                var result = new List<ItemStatusDocViewModel>();

                var filterModel = new ItemStatusDocViewModel();

                //GetConfig
                result = utils.SendDataApi<List<ItemStatusDocViewModel>>(new AppSettingConfig().GetUrl("dropdownItemStatus"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region dropdownVehicle
        public List<VehicleViewModel> dropdownVehicle(VehicleViewModel data)
        {
            try
            {
                var result = new List<VehicleViewModel>();

                var filterModel = new VehicleViewModel();

                //GetConfig
                result = utils.SendDataApi<List<VehicleViewModel>>(new AppSettingConfig().GetUrl("dropdownVehicle"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region dropdownTransport
        public List<TransportViewModel> dropdownTransport(TransportViewModel data)
        {
            try
            {
                var result = new List<TransportViewModel>();

                var filterModel = new TransportViewModel();

                //GetConfig
                result = utils.SendDataApi<List<TransportViewModel>>(new AppSettingConfig().GetUrl("dropdownTransport"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Cancel
        public Boolean Cancel(PurchaseOrderDocViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {

                #region Check

                var resultGRItem = new List<GoodsReceiveItemViewModel>();
                var list = new List<DocumentViewModel> { new DocumentViewModel { ref_document_Index = data.purchaseOrder_Index } };
                var Item = new DocumentViewModel();
                Item.listDocumentViewModel = list;

                resultGRItem = utils.SendDataApi<List<GoodsReceiveItemViewModel>>(new AppSettingConfig().GetUrl("FindGR"), Item.sJson());

                if (resultGRItem.Count > 0)
                {
                    return false;
                }
                #endregion



                var PurchaseOrder = db.im_PurchaseOrder.Find(data.purchaseOrder_Index);

                if (PurchaseOrder != null)
                {
                    PurchaseOrder.Document_Status = -1;
                    PurchaseOrder.Cancel_By = data.cancel_By;
                    PurchaseOrder.Cancel_Date = DateTime.Now;

                    var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }

                    catch (Exception exy)
                    {
                        msglog = State + " ex Rollback " + exy.Message.ToString();
                        olog.logging("CancelPO", msglog);
                        transaction.Rollback();
                        throw exy;
                    }

                }


                return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region dropdownCostCenter
        public List<CostCenterViewModel> dropdownCostCenter(CostCenterViewModel data)
        {
            try
            {
                var result = new List<CostCenterViewModel>();

                var filterModel = new CostCenterViewModel();

                //GetConfig
                result = utils.SendDataApi<List<CostCenterViewModel>>(new AppSettingConfig().GetUrl("dropdownCostCenter"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region dropdownWeight
        public List<WeightViewModel> dropdownWeight(WeightViewModel data)
        {
            try
            {
                var result = new List<WeightViewModel>();

                var filterModel = new WeightViewModel();

                //GetConfig
                result = utils.SendDataApi<List<WeightViewModel>>(new AppSettingConfig().GetUrl("dropdownWeight"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region dropdownCurrency
        public List<CurrencyViewModel> dropdownCurrency(CurrencyViewModel data)
        {
            try
            {
                var result = new List<CurrencyViewModel>();

                var filterModel = new CurrencyViewModel();

                //GetConfig
                result = utils.SendDataApi<List<CurrencyViewModel>>(new AppSettingConfig().GetUrl("dropdownCurrency"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region dropdownVolume
        public List<VolumeViewModel> dropdownVolume(VolumeViewModel data)
        {
            try
            {
                var result = new List<VolumeViewModel>();

                var filterModel = new VolumeViewModel();

                //GetConfig
                result = utils.SendDataApi<List<VolumeViewModel>>(new AppSettingConfig().GetUrl("dropdownVolume"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region dropdownShipmentType
        public List<ShipmentTypeViewModel> dropdownShipmentType(ShipmentTypeViewModel data)
        {
            try
            {
                var result = new List<ShipmentTypeViewModel>();

                var filterModel = new ShipmentTypeViewModel();

                //GetConfig
                result = utils.SendDataApi<List<ShipmentTypeViewModel>>(new AppSettingConfig().GetUrl("dropdownShipmentType"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region dropdownContainerType
        public List<ContainerTypeViewModelV2> dropdownContainerType(ContainerTypeViewModelV2 data)
        {
            try
            {
                var result = new List<ContainerTypeViewModelV2>();

                var filterModel = new ContainerTypeViewModelV2();

                //GetConfig
                result = utils.SendDataApi<List<ContainerTypeViewModelV2>>(new AppSettingConfig().GetUrl("dropdownContainerType"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region dropdownDockDoor
        public List<DockDoorViewModelV2> dropdownDockDoor(DockDoorViewModelV2 data)
        {
            try
            {
                var result = new List<DockDoorViewModelV2>();

                var filterModel = new DockDoorViewModelV2();

                //GetConfig
                result = utils.SendDataApi<List<DockDoorViewModelV2>>(new AppSettingConfig().GetUrl("dropdownDockDoor"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region dropdownVehicleType
        public List<VehicleTypeViewModel> dropdownVehicleType(VehicleTypeViewModel data)
        {
            try
            {
                var result = new List<VehicleTypeViewModel>();

                var filterModel = new VehicleTypeViewModel();

                //GetConfig
                result = utils.SendDataApi<List<VehicleTypeViewModel>>(new AppSettingConfig().GetUrl("dropdownVehicleType"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region dropdownUnloadingType
        public List<UnloadingTypeViewModel> dropdownUnloadingType(UnloadingTypeViewModel data)
        {
            try
            {
                var result = new List<UnloadingTypeViewModel>();

                var filterModel = new UnloadingTypeViewModel();

                //GetConfig
                result = utils.SendDataApi<List<UnloadingTypeViewModel>>(new AppSettingConfig().GetUrl("dropdownUnloadingType"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region dropdownCargoType
        public List<CargoTypeViewModel> dropdownCargoType(CargoTypeViewModel data)
        {
            try
            {
                var result = new List<CargoTypeViewModel>();

                var filterModel = new CargoTypeViewModel();

                //GetConfig
                result = utils.SendDataApi<List<CargoTypeViewModel>>(new AppSettingConfig().GetUrl("dropdownCargoType"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region dropdownDocumentPriority
        public List<DocumentPriorityViewModel> dropdownDocumentPriority(DocumentPriorityViewModel data)
        {
            try
            {
                var result = new List<DocumentPriorityViewModel>();

                var filterModel = new DocumentPriorityViewModel();

                //GetConfig
                result = utils.SendDataApi<List<DocumentPriorityViewModel>>(new AppSettingConfig().GetUrl("dropdownDocumentPriority"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region autoOwnerfilterName
        public List<ItemListViewModel> autoOwnerfilterName(ItemListViewModel data)
        {
            try
            {
                var result = new List<ItemListViewModel>();

                var filterModel = new ItemListViewModel();
                if (!string.IsNullOrEmpty(data.key))
                {
                    filterModel.key = data.key;
                }

                //GetConfig
                result = utils.SendDataApi<List<ItemListViewModel>>(new AppSettingConfig().GetUrl("autoOwnerfilterName"), filterModel.sJson());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoPurchaseOrderNoAndOwner
        public List<ItemListViewModel> autoPurchaseOrderNoAndOwner(ItemListViewModel data)
        {
            try
            {
                var query = db.im_PurchaseOrder.AsQueryable();

                if (data.key == "-")
                {


                }
                else if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.PurchaseOrder_No.Contains(data.key));

                }

                var items = new List<ItemListViewModel>();

                var result = query.Select(c => new { c.PurchaseOrder_No, c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();


                foreach (var item in result)
                {
                    var resultItem = new ItemListViewModel
                    {
                        index = item.Owner_Index,
                        name = item.PurchaseOrder_No,
                        value1 = item.Owner_Id,
                        value2 = item.Owner_Name

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

    }
}
