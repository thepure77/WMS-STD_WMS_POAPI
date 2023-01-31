using GRDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PODataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess
{
    public class PODbContext : DbContext
    {

        public PODbContext(DbContextOptions<PODbContext> options) : base(options)
        {
        }

        public DbSet<GetValueByColumn> GetValueByColumn { get; set; }

        public DbSet<im_PurchaseOrder> im_PurchaseOrder { get; set; }
        public DbSet<im_PurchaseOrderItem> im_PurchaseOrderItem { get; set; }
        public DbSet<View_PO> View_PO { get; set; }
        public DbSet<im_DocumentFile> im_DocumentFile { get; set; }
        public DbSet<View_GetPurchaseOrder_Popup> View_GetPurchaseOrder_Popup { get; set; }
        public DbSet<View_GetPurchaseOrderItem> View_GetPurchaseOrderItem { get; set; }
        public DbSet<View_PurchaseOrderItem> View_PurchaseOrderItem { get; set; }
        public DbSet<View_CheckRemainingPO_Qty> View_CheckRemainingPO_Qty { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        
    }


}
