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
                    record.Weight = Math.Round((double)(record.Count * Convert.ToDouble(res.UnitWeight)),2);
                }
                var res2 = CurrentDBSession.LocationDal.LoadEntities(a => a.Id == record.ItemLocationId).FirstOrDefault();
                if (res2 != null)
                {
                    record.ItemLocation = res2.Name;
                }
            }
            return records;
        }
    }
}
