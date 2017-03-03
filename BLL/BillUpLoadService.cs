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
                    return ExportBackOutput(billId, TemplateUrl);
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
            return NPIOHelper.RenderToMemory(dt, "sheet1");
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
            return NPIOHelper.RenderToMemory(dt, "sheet1");

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
            return NPIOHelper.RenderToMemory(dt, "sheet1");
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
                string ColumnName = dt.Columns[0].ColumnName;//第一个列名
                //如果是批量导入交货单
                if (ColumnName == "交货")
                {
                    billCodes = AddManyGiveBills(dt);
                }
                else
                {
                    //单张单据
                    dt.Rows.RemoveAt(0); //删除标题行
                    switch (typeTemp)
                    {
                        case "转仓单号":
                            AddTransferBill(dt);
                            break;
                        case "退仓单号":
                            AddBackInput(dt);
                            break;
                        case "交货单号":
                            AddGiveBill(dt);
                            break;
                        case "退货单号":
                            AddBackOutput(dt);
                            break;
                    }
                }
                //删除文件
                File.Delete(item);
            }
            return billCodes == "" ? "导入成功" : "导入异常：" + billCodes;
            //}
            //catch(Exception e)
            //{
            //    return e.ToString();
            //}
        }
        /// <summary>
        /// 批量导入交货单
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string AddManyGiveBills(DataTable dt)
        {
            string res = "";
            GiveBillService Service = new GiveBillService();
            //转成集合
            List<ManyGiveBillsModel> list = new List<ManyGiveBillsModel>();
            foreach (DataRow row in dt.Rows)
            {
                ManyGiveBillsModel model = new ManyGiveBillsModel()
                {
                    LBBillCode = row[0].ToString(),
                    LBCustomerCode = row[1].ToString(),
                    LBCustomerName = row[2].ToString(),
                    ItemCode = row[4].ToString(),
                    ItemName = row[5].ToString(),
                    ItemBatch = row[7].ToString(),
                    Count = Convert.ToDouble(row[8]),
                    Weight = Convert.ToDouble(row[9]),
                    ItemLocationId=row[10].ToString(),
                    ItemLocation=row[11].ToString(),
                    LBContacts = row[12].ToString(),
                    LBPhone = row[13].ToString(),
                    LBSendAddress = row[14].ToString(),
                    LBMailCode = row[15].ToString(),
                    LBBillDate = Convert.ToDateTime(row[16]),
                    WarehouseId = Convert.ToInt32(row[17]),
                    Warehouse = row[18].ToString(),
                    ChargePerson = row[19].ToString()
                };
                list.Add(model);
            }
            //按立邦单号排序
            list.Sort((x, y) =>
            {
                int value = x.LBBillCode.CompareTo(y.LBBillCode);
                if (value == 0)
                    value = x.ItemCode.CompareTo(y.ItemCode);
                return value;
            });
            string CurrentLBBillCode = "";//当前立邦单号
            GiveBill bill = null;//当前交货单
            foreach (var item in list)
            {
                //新的一条单据
                if (item.LBBillCode != CurrentLBBillCode)
                {
                    //保存上一张单据
                    if (CurrentLBBillCode != "")
                    {
                        res += Service.SaveImport(bill);
                    }
                    //新建下一张单据
                    bill = new GiveBill()
                    {
                        LBBillCode = item.LBBillCode,
                        LBCustomerCode = item.LBCustomerCode,
                        LBCustomerName = item.LBCustomerName,
                        LBContacts = item.LBContacts,
                        LBPhone = item.LBPhone,
                        LBSendAddress = item.LBSendAddress,
                        LBMailCode = item.LBMailCode,
                        LBBillDate = item.LBBillDate,
                        WarehouseId = item.WarehouseId,
                        Warehouse = item.Warehouse,
                        ChargePerson = item.ChargePerson
                    };
                }
                Record record = new Record() { ItemCode = item.ItemCode, ItemName = item.ItemName, ItemBatch = item.ItemBatch, Count = item.Count, Weight = item.Weight,ItemLocation=item.ItemLocation, ItemLocationId=item.ItemLocationId};
                bill.Record.Add(record);
                CurrentLBBillCode = item.LBBillCode;
            }
            //循环结束 保存最后一条
            if (bill != null)
            {
                res += Service.SaveImport(bill);
            }
            return res;
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
