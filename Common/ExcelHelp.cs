using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Common
{
    public class ExcelHelp
    {
        /// <summary>
        /// Excel转DataTable
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static DataTable ExcelToDT(string Path)
        {
            //string strConn = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source =" + Path + ";Extended Properties=Excel 8.0";
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
            DataSet ds = null;
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                string strExcel = "";
                OleDbDataAdapter myCommand = null;
                strExcel = "select * from [sheet1$]";
                myCommand = new OleDbDataAdapter(strExcel, strConn);
                ds = new DataSet(); myCommand.Fill(ds, "table1");
            }
            RemoveEmpty(ds.Tables[0]);//删除空行
            return ds.Tables[0];
        }
        /// <summary>
        /// 删除空行
        /// </summary>
        /// <param name="dt"></param>
        private static void RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
        }
    }
}
