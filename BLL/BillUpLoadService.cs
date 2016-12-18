using Common;
using DALFactory;
using DBModel;
using IBLL;
using IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL
{
    public partial class BillUpLoadService : IBillUpLoadService
    {
        public IDBSession CurrentDBSession
        {
            get
            {
                return DBSessionFactory.CreateDBSession();//创建线程内唯一对象
            }
        }
        #region 导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="billType">单据类型</param>
        /// <param name="billId">单据ID</param>
        /// <param name="TemplateUrl">模板地址</param>
        /// <returns></returns>
        public MemoryStream ExprotExcel(string billType, Guid billId, string TemplateUrl)
        {
            switch (billType)
            {
                case "transferBill":
                    return ExportTransferBill(billId, TemplateUrl);
                case "backOutput":
                  return  ExportBackOutput(billId, TemplateUrl);
                case "backInput":
                    return ExportBackInput(billId, TemplateUrl);
                case "giveBill":
                    return ExportGiveBill(billId, TemplateUrl);
            }
            return null;
        }
        /// <summary>
        /// 导出退货单
        /// </summary>
        /// <param name="billId"></param>
        /// <param name="TemplateUrl"></param>
        /// <returns></returns>
        private MemoryStream ExportBackOutput(Guid billId, string TemplateUrl)
        {
            BackOutput bill = CurrentDBSession.BackOutputDal.LoadEntities(a => a.Id == billId).FirstOrDefault();
            if (bill == null) { return null; }
            DataTable dt = ExcelHelp.ExcelToDT(TemplateUrl);
            foreach (var record in bill.Record)
            {
                DataRow dr = dt.NewRow();
                dr["LBBillCode"] = bill.LBBillCode;
                dr["LBBillDate"] = bill.LBBillDate;
                dr["LBCustomerCode"] = bill.LBCustomerCode;
                dr["LBCustomerName"] = bill.LBCustomerName;
                dr["LBSendAddress"] = bill.LBSendAddress;
                dr["LBContacts"] = bill.LBContacts;
                dr["LBPhone"] = bill.LBPhone;
                dr["LBMailCode"] = bill.LBMailCode;
                dr["LBReson"] = bill.LBReson;
                dr["LBRemark"] = bill.LBRemark;
                dr["CreateDate"] = bill.CreateDate;
                dr["Warehouse"] = bill.Warehouse;
                dr["WarehouseId"] = bill.WarehouseId;
                dr["LoadGoodsType"] = bill.LoadGoodsType;
                dr["InputType"] = bill.InputType;
                dr["InputTypeId"] = bill.InputTypeId;
                dr["ChargePerson"] = bill.ChargePerson;
                dr["MakePerson"] = bill.MakePerson;
                dr["BusinessType"] = bill.BusinessType;
                dr["LineWay"] = bill.LineWay;
                dr["Remark"] = bill.Remark;
                dr["Department"] = bill.Department;
                dr["DepartmentId"] = bill.DepartmentId;
                dr["Company"] = bill.Company;
                dr["CompanyId"] = bill.CompanyId;
             
                //子表
                dr["ItemCode"] = record.ItemCode;
                dr["ItemLocationId"] = record.ItemLocationId;
                dr["ItemBatch"] = record.ItemBatch;
                dr["Count"] = record.Count;
                dt.Rows.Add(dr);
            }
            return NPIOHelper.RenderToMemory(dt, "backoutput");
        }


        /// <summary>
        /// 导出退仓单
        /// </summary>
        /// <param name="billId"></param>
        /// <param name="TemplateUrl"></param>
        /// <returns></returns>
        private MemoryStream ExportBackInput(Guid billId, string TemplateUrl)
        {
            BackInput bill = CurrentDBSession.BackInputDal.LoadEntities(a => a.Id == billId).FirstOrDefault();
            if (bill == null) { return null; }
            DataTable dt = ExcelHelp.ExcelToDT(TemplateUrl);
            foreach (var record in bill.Record)
            {
                DataRow dr = dt.NewRow();
                dr["LBBillCode"] = bill.LBBillCode;
                dr["LBBillDate"] = bill.LBBillDate;
                dr["LBBackWarehouse"] = bill.LBBackWarehouse;
                dr["LBMoveType"] = bill.LBMoveType;
                dr["LBBackAddress"] = bill.LBBackAddress;
                dr["LBContacts"] = bill.LBContacts;
                dr["LBPhone"] = bill.LBPhone;
                dr["LBRemark"] = bill.LBRemark;
                dr["CreateDate"] = bill.CreateDate;
                dr["ChargePerson"] = bill.ChargePerson;
                dr["Warehouse"] = bill.Warehouse;
                dr["WarehouseId"] = bill.WarehouseId;
                dr["LoadGoodsType"] = bill.LoadGoodsType;
                dr["OutputType"] = bill.OutputType;
                dr["OutputTypeId"] = bill.OutputTypeId;
                dr["BusinessType"] = bill.BusinessType;
                dr["LineWay"] = bill.LineWay;
                dr["MakePerson"] = bill.MakePerson;
                dr["Remark"] = bill.Remark;
                dr["Department"] = bill.Department;
                dr["DepartmentId"] = bill.DepartmentId;
                dr["Company"] = bill.Company;
                dr["CompanyId"] = bill.CompanyId;
                //子表
                dr["ItemCode"] = record.ItemCode;
                dr["ItemLocationId"] = record.ItemLocationId;
                dr["ItemBatch"] = record.ItemBatch;
                dr["Count"] = record.Count;
                dt.Rows.Add(dr);
            }
            return NPIOHelper.RenderToMemory(dt, "backinput");

        }


        /// <summary>
        /// 导出入库单
        /// </summary>
        /// <param name="billId"></param>
        /// <param name="TemplateUrl"></param>
        /// <returns></returns>
        private MemoryStream ExportTransferBill(Guid billId, string TemplateUrl)
        {
            TransferBill bill = CurrentDBSession.TransferBillDal.LoadEntities(a => a.Id == billId).FirstOrDefault();
            if (bill == null) { return null; }
            DataTable dt = ExcelHelp.ExcelToDT(TemplateUrl);
            foreach (var record in bill.Record)
            {
                DataRow dr = dt.NewRow();
                dr["LBBillCode"] = bill.LBBillCode;
                dr["LBBillDate"] = bill.LBBillDate;
                dr["LBFrom"] = bill.LBFrom;
                dr["LBMoveType"] = bill.LBMoveType;
                dr["LBRemark"] = bill.LBRemark;
                dr["MakePerson"] = bill.MakePerson;
                dr["CreateDate"] = bill.CreateDate;
                dr["ChargePerson"] = bill.ChargePerson;
                dr["Warehouse"] = bill.Warehouse;
                dr["WarehouseId"] = bill.WarehouseId;
                dr["LoadGoodsType"] = bill.LoadGoodsType;
                dr["InputType"] = bill.InputType;
                dr["InputTypeId"] = bill.InputTypeId;
                dr["BusinessType"] = bill.BusinessType;
                dr["Remark"] = bill.Remark;
                dr["Department"] = bill.Department;
                dr["DepartmentId"] = bill.DepartmentId;
                dr["Company"] = bill.Company;
                dr["CompanyId"] = bill.CompanyId;
                //子表
                dr["ItemCode"] = record.ItemCode;
                dr["ItemLocationId"] = record.ItemLocationId;
                dr["ItemBatch"] = record.ItemBatch;
                dr["Count"] = record.Count;
                dt.Rows.Add(dr);
            }
            return NPIOHelper.RenderToMemory(dt, "sheet1");
        }


        /// <summary>
        /// 导出交货单
        /// </summary>
        /// <param name="billId"></param>
        /// <returns></returns>
        private MemoryStream ExportGiveBill(Guid billId, string TemplateUrl)
        {
            GiveBill bill = CurrentDBSession.GiveBillDal.LoadEntities(a => a.Id == billId).FirstOrDefault();
            if (bill == null) { return null; }
            //根据模板创建Datatable
            DataTable dt = ExcelHelp.ExcelToDT(TemplateUrl);
            foreach (var record in bill.Record)
            {
                DataRow dr = dt.NewRow();
                dr["LBBillCode"] = bill.LBBillCode;
                dr["LBBillDate"] = bill.LBBillDate;
                dr["LBTaskBillCode"] = bill.LBTaskBillCode;
                dr["LBLine"] = bill.LBLine;
                dr["LBContacts"] = bill.LBContacts;
                dr["LBPhone"] = bill.LBPhone;
                dr["LBMailCode"] = bill.LBMailCode;
                dr["LBCustomerCode"] = bill.LBCustomerCode;
                dr["LBCustomerName"] = bill.LBCustomerName;
                dr["LBSendAddress"] = bill.LBSendAddress;
                dr["LBRemark"] = bill.LBRemark;
                dr["CreateDate"] = bill.CreateDate;
                dr["ChargePerson"] = bill.ChargePerson;
                dr["MakePerson"] = bill.MakePerson;
                dr["Warehouse"] = bill.Warehouse;
                dr["WarehouseId"] = bill.WarehouseId;
                dr["LoadGoodsType"] = bill.LoadGoodsType;
                dr["OutputType"] = bill.OutputType;
                dr["OutputTypeId"] = bill.OutputTypeId;
                dr["BusinessType"] = bill.BusinessType;
                dr["LineWay"] = bill.LineWay;
                dr["Remark"] = bill.Remark;
                dr["Department"] = bill.Department;
                dr["DepartmentId"] = bill.DepartmentId;
                dr["Company"] = bill.Company;
                dr["CompanyId"] = bill.CompanyId;
                //子表
                dr["ItemCode"] = record.ItemCode;
                dr["ItemLocationId"] = record.ItemLocationId;
                dr["ItemBatch"] = record.ItemBatch;
                dr["Count"] = record.Count;
                dt.Rows.Add(dr);
            }
            return NPIOHelper.RenderToMemory(dt, "givebill");
        }

        #endregion
        #region 上传Excel
        /// <summary>
        ///上传时候保存Excel
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public string Save(string[] files)
        {
            if (files == null || files.Length == 0)
            {
                return "未选择文件！";
            }
            string billCodes = "";
            //try
            //{
            foreach (var item in files)
            {
                DataTable dt = ExcelHelp.ExcelToDT(item);
                string typeTemp = dt.Rows[0][0].ToString();//首行首列
                                                           //删除第一行
                dt.Rows.RemoveAt(0);
                switch (typeTemp)
                {
                    case "转仓单号":
                        billCodes += AddTransferBill(dt) + "  ";
                        break;
                    case "退仓单号":
                        billCodes += AddBackInput(dt) + "  ";
                        break;
                    case "交货单号":
                        billCodes += AddGiveBill(dt) + "  ";
                        break;
                    case "退货单号":
                        billCodes += AddBackOutput(dt) + "  ";
                        break;
                }
                //删除文件
                File.Delete(item);
            }

            return "导入成功，单号为：" + billCodes;
            //}
            //catch(Exception e)
            //{
            //    return e.ToString();
            //}
        }
        private string AddTransferBill(DataTable dt)
        {
            TransferBill bill = DataTableToEntites.GetEntity<TransferBill>(dt.Rows[0]);
            bill.Record = HandleRecords(dt);
            TransferBillService Service = new TransferBillService();
            Service.SaveData(bill);
            return bill.BillCode;
        }
        private string AddBackInput(DataTable dt)
        {
            BackInput bill = DataTableToEntites.GetEntity<BackInput>(dt.Rows[0]);
            bill.Record = HandleRecords(dt);
            BackInputService Service = new BackInputService();
            Service.SaveData(bill);
            return bill.BillCode;
        }
        private string AddGiveBill(DataTable dt)
        {
            GiveBill bill = DataTableToEntites.GetEntity<GiveBill>(dt.Rows[0]);
            bill.Record = HandleRecords(dt);
            GiveBillService Service = new GiveBillService();
            Service.SaveData(bill);
            return bill.BillCode;
        }
        private string AddBackOutput(DataTable dt)
        {
            BackOutput bill = DataTableToEntites.GetEntity<BackOutput>(dt.Rows[0]);
            bill.Record = HandleRecords(dt);
            BackOutputService Service = new BackOutputService();
            Service.SaveData(bill);
            return bill.BillCode;
        }
        /// <summary>
        /// 处理子表数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private ICollection<Record> HandleRecords(DataTable dt)
        {
            ICollection<Record> records = DataTableToEntites.GetEntities<Record>(dt);
            foreach (var record in records)
            {
                var res = CurrentDBSession.GoodItemDal.LoadEntities(a => a.ItemCode == record.ItemCode).FirstOrDefault();
                if (res != null)
                {
                    record.ItemName = res.ItemName;
                    record.ItemLine = res.ItemLine;
                    record.ItemSpecifications = res.ItemSpecifications;
                    record.ItemUnit = res.ItemUnit;
                    record.UnitWeight = res.UnitWeight;
                    record.Weight = Math.Round((double)(record.Count * Convert.ToDouble(res.UnitWeight)), 2);
                }
                var res2 = CurrentDBSession.LocationDal.LoadEntities(a => a.Id == record.ItemLocationId).FirstOrDefault();
                if (res2 != null)
                {
                    record.ItemLocation = res2.Name;
                }
            }
            return records;
        }

        #endregion

    }
}
