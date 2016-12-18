using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Excel导出数据
    /// </summary>
    public class NPIOHelper
    {
        public NPIOHelper()
        {

        }

        /// <summary>
        /// 将整个DataSet导出
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static MemoryStream ReaderToMemory(DataSet ds)
        {
            MemoryStream ms = new MemoryStream();

            using (ds)
            {
                IWorkbook workbook = new HSSFWorkbook();

                ISheet sheet = workbook.CreateSheet();

                IRow headerRow = sheet.CreateRow(0);

                // handling header.
                foreach (DataColumn column in ds.Tables[0].Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value

                // handling value.
                int rowIndex = 1;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        ICell cell = dataRow.CreateCell(column.Ordinal);
                        string strVale = row[column].ToString();

                        if (column.DataType == typeof(decimal))
                        {
                            cell.SetCellValue(double.Parse(strVale == "" ? "0" : strVale));
                        }
                        else
                        {
                            cell.SetCellValue(strVale);
                        }
                    }

                    rowIndex++;
                }

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }

        /// <summary>
        /// 将整个DataSet导出=>指定标题
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="ExcelHeaderList"></param>
        /// <returns></returns>
        public static MemoryStream RenderToMemory(DataSet ds, string[] ExcelHeaderList)
        {
            MemoryStream ms = new MemoryStream();

            using (ds)
            {
                IWorkbook workbook = new HSSFWorkbook();

                ISheet sheet = workbook.CreateSheet();

                IRow headerRow = sheet.CreateRow(0);

                // handling header.
                for (int c = 0; c < ExcelHeaderList.Length; c++)
                {
                    headerRow.CreateCell(c).SetCellValue(ExcelHeaderList[c]);//If Caption not set, returns the ColumnName value
                }
                // handling value.
                int rowIndex = 1;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        ICell cell = dataRow.CreateCell(column.Ordinal);
                        string strVale = row[column].ToString();

                        if (column.DataType == typeof(decimal))
                        {
                            cell.SetCellValue(double.Parse(strVale == "" ? "0" : strVale));
                        }
                        else
                        {
                            cell.SetCellValue(strVale);
                        }

                    }

                    rowIndex++;
                }

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }

        /// <summary>
        /// 按照指定的列来导出
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="ExcelHeaderList"></param>
        /// <param name="ColsNameList"></param>
        /// <returns></returns>
        public static MemoryStream RenderToMemory(DataSet ds, string[] ExcelHeaderList, string[] ColsNameList, string SheetName, Func<IWorkbook, ISheet, int> fnSetHeader = null, Action<IWorkbook, ISheet, int, int> fnSetFooter = null)
        {
            MemoryStream ms = new MemoryStream();

            using (ds)
            {
                IWorkbook workbook = new HSSFWorkbook();

                ISheet sheet = workbook.CreateSheet(SheetName);
                sheet.DisplayGridlines = false;


                int begindex = 0;
                if (fnSetHeader != null)
                    begindex = fnSetHeader(workbook, sheet);

                IRow headerRow = sheet.CreateRow(begindex);
                //设置行高度
                headerRow.HeightInPoints = 25;
                ICellStyle style = workbook.CreateCellStyle();
                //设置字体为粗体
                IFont font = workbook.CreateFont();
                font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                font.Color = NPOI.HSSF.Util.HSSFColor.BlueGrey.Index;
                //设置居中
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                //设置边框
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                style.SetFont(font);
                //设置背景颜色
                //style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.RED.index;
                ////style.FillPattern = FillPatternType.SQUARES;
                //style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.RED.index; 
                for (int c = 0; c < ExcelHeaderList.Length; c++)
                {
                    ICell headercell = headerRow.CreateCell(c);
                    headercell.SetCellValue(ExcelHeaderList[c]);
                    headercell.CellStyle = style;
                }
                // handling value.
                int rowIndex = begindex + 1;
                ICellStyle styledt = workbook.CreateCellStyle();
                styledt.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                styledt.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                styledt.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                styledt.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                styledt.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                styledt.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                styledt.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                styledt.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    dataRow.HeightInPoints = 19;
                    for (int c = 0; c < ColsNameList.Length; c++)
                    {
                        ICell cell = dataRow.CreateCell(c);
                        string strVale = row[ColsNameList[c]].ToString();

                        if (ds.Tables[0].Columns[ColsNameList[c]].DataType == typeof(decimal) || ds.Tables[0].Columns[ColsNameList[c]].DataType == typeof(int))
                        {
                            cell.SetCellValue(double.Parse(strVale == "" ? "0" : strVale));
                        }
                        else
                        {
                            cell.SetCellValue(strVale);
                        }

                        cell.CellStyle = styledt;
                    }
                    rowIndex++;
                }


                if (fnSetFooter != null)
                    fnSetFooter(workbook, sheet, begindex, rowIndex);
                /*为了导出列为中文*/
                int colcount = ExcelHeaderList.Length;
                if (colcount == 13) colcount = colcount - 7;
                for (int c = 0; c < colcount; c++)
                {
                    sheet.AutoSizeColumn(c);
                }
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }

        /// <summary>
        /// 传递dataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="SheetName"></param>
        /// <returns></returns>
        public static MemoryStream RenderToMemory(DataTable dt, string SheetName, bool isShowTitle = true)
        {
            MemoryStream ms = new MemoryStream();

            using (dt)
            {
                IWorkbook workbook = new HSSFWorkbook();

                ISheet sheet = workbook.CreateSheet(SheetName);
                int rowIndex = 0;
                if (isShowTitle)
                {
                    //sheet.DisplayGridlines = false;
                    IRow headerRow = sheet.CreateRow(0);
                    //设置行高度
                    headerRow.HeightInPoints = 25;

                    //表头取值Datatable列头
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        ICell headercell = headerRow.CreateCell(c);
                        headercell.SetCellValue(dt.Columns[c].ColumnName);

                        ICellStyle style = workbook.CreateCellStyle();
                        //设置字体为粗体
                        IFont font = workbook.CreateFont();
                        font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                        font.Color = NPOI.HSSF.Util.HSSFColor.BlueGrey.Index;
                        //设置居中
                        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                        style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                        //设置边框
                        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                        style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                        style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                        style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                        style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                        style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                        style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                        style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                        style.SetFont(font);
                        //设置背景颜色
                        //style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.RED.index;
                        ////style.FillPattern = FillPatternType.SQUARES;
                        //style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.RED.index; 
                        headercell.CellStyle = style;
                    }

                    rowIndex = 1;
                }
                ICellStyle styledt = workbook.CreateCellStyle();
                styledt.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                styledt.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                styledt.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                styledt.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                styledt.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                styledt.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                styledt.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                styledt.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                foreach (DataRow row in dt.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    dataRow.HeightInPoints = 19;
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        ICell cell = dataRow.CreateCell(c);
                        var currField = dt.Columns[c].ColumnName;
                        string strVale = row[currField].ToString();

                        if (dt.Columns[currField].DataType == typeof(decimal) || dt.Columns[currField].DataType == typeof(int))
                        {
                            cell.SetCellValue(double.Parse(strVale == "" ? "0" : strVale));
                        }
                        else
                        {
                            cell.SetCellValue(strVale);
                        }

                        cell.CellStyle = styledt;
                    }
                    rowIndex++;
                }
                /*为了导出列为中文*/
                int colcount = dt.Columns.Count;
                for (int c = 0; c < colcount; c++)
                {
                    sheet.AutoSizeColumn(c);
                }
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }


        public static void SaveTableToExcel(DataTable dt, string sheetName, string filepath, string postilColName = null, string postilText = null)
        {
            var fs = new FileStream(filepath, FileMode.CreateNew);
            //MemoryStream ms = new MemoryStream();
            IWorkbook workBook = new XSSFWorkbook();//创建工作薄
            ISheet sheet = workBook.CreateSheet(sheetName);
            IRow headerRow = sheet.CreateRow(0);
            //设置行高度
            headerRow.HeightInPoints = 25;

            //表头取值Datatable列头
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                ICell headercell = headerRow.CreateCell(c);
                headercell.SetCellValue(dt.Columns[c].ColumnName);
                if (!string.IsNullOrEmpty(postilColName) && dt.Columns[c].ColumnName == postilColName)
                {
                    var patr = sheet.CreateDrawingPatriarch();
                    var comment1 = patr.CreateCellComment(new XSSFClientAnchor(0, 0, 0, 0, 0, 0, 0, 0));
                    comment1.String = new XSSFRichTextString(postilText);
                    headercell.CellComment = comment1;
                }

                ICellStyle style = workBook.CreateCellStyle();
                //设置字体为粗体
                IFont font = workBook.CreateFont();
                font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
                font.Color = NPOI.HSSF.Util.HSSFColor.BlueGrey.Index;
                //设置居中
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                //设置边框
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
                style.SetFont(font);
                //设置背景颜色
                //style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.RED.index;
                ////style.FillPattern = FillPatternType.SQUARES;
                //style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.RED.index; 
                headercell.CellStyle = style;
            }

            int rowIndex = 1;
            ICellStyle styledt = workBook.CreateCellStyle();
            styledt.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            styledt.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            styledt.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            styledt.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            styledt.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            styledt.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            styledt.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            styledt.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            foreach (DataRow row in dt.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                dataRow.HeightInPoints = 19;
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    ICell cell = dataRow.CreateCell(c);
                    var currField = dt.Columns[c].ColumnName;
                    string strVale = row[currField].ToString();

                    if (dt.Columns[currField].DataType == typeof(decimal) || dt.Columns[currField].DataType == typeof(int))
                    {
                        cell.SetCellValue(double.Parse(strVale == "" ? "0" : strVale));
                    }
                    else
                    {
                        cell.SetCellValue(strVale);
                    }

                    cell.CellStyle = styledt;
                }
                rowIndex++;
            }
            /*为了导出列为中文*/
            int colcount = dt.Columns.Count;
            for (int c = 0; c < colcount; c++)
            {
                sheet.AutoSizeColumn(c);
            }
            //workBook.Write(ms);
            //ms.Flush();
            //ms.Position = 0;
            //return ms;
            workBook.Write(fs);
        }

        /// <summary>
        /// 按照指定的列来导出,可以设定列的宽度
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="ExcelHeaderList"></param>
        /// <param name="ColsNameList"></param>
        /// <param name="ColsWidthList"></param>
        /// <returns></returns>
        public static MemoryStream RenderToMemory(DataSet ds, string[] ExcelHeaderList, string[] ColsNameList, int[] ColsWidthList)
        {
            MemoryStream ms = new MemoryStream();

            using (ds)
            {
                IWorkbook workbook = new HSSFWorkbook();

                ISheet sheet = workbook.CreateSheet();

                IRow headerRow = sheet.CreateRow(0);

                // handling header.
                for (int c = 0; c < ExcelHeaderList.Length; c++)
                {
                    ICell cell = headerRow.CreateCell(c);
                    cell.SetCellValue(ExcelHeaderList[c]);//If Caption not set, returns the ColumnName value
                    if (ColsWidthList[c] > 0)
                    {
                        sheet.SetColumnWidth(c, ColsWidthList[c] * 100);
                    }
                }
                // handling value.
                int rowIndex = 1;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    for (int c = 0; c < ColsNameList.Length; c++)
                    {
                        ICell cell = dataRow.CreateCell(c);
                        string strVale = row[ColsNameList[c]].ToString();

                        if (ds.Tables[0].Columns[ColsNameList[c]].DataType == typeof(decimal))
                        {
                            cell.SetCellValue(double.Parse(strVale == "" ? "0" : strVale));
                        }
                        else
                        {
                            cell.SetCellValue(strVale);
                        }
                    }
                    rowIndex++;
                }

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }

        public static DataTable RenderDataTableFromExcel(Stream excelFileStream)
        {
            using (excelFileStream)
            {
                IWorkbook workbook = new HSSFWorkbook(excelFileStream);

                ISheet sheet = workbook.GetSheetAt(0);//取第一个表 

                DataTable table = new DataTable();

                IRow headerRow = sheet.GetRow(0);//第一行为标题行 
                int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells 
                int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1 

                //handling header. 
                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }

                for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null)
                        break;

                    if (row != null)
                    {
                        if (row.GetCell(0) == null)
                        {
                            break;
                        }
                        if (row.GetCell(0).ToString().Trim() == "")
                        {
                            break;
                        }
                        DataRow dataRow = table.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        table.Rows.Add(dataRow);
                    }
                }
                workbook = null;
                sheet = null;
                return table;

            }

        }

        #region 将Excel数据转DataTable数据
        public static DataTable RenderDataTableFromExcel2007(Stream excelFileStream)
        {
            DataTable table = new DataTable();
            try
            {
                using (excelFileStream)
                {
                    IWorkbook workbook = new XSSFWorkbook(excelFileStream);

                    ISheet sheet = workbook.GetSheetAt(0);//取第一个表 

                    IRow headerRow = sheet.GetRow(0);//第一行为标题行 
                    int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells 
                    int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1 

                    //handling header. 
                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        string columnname = headerRow.GetCell(i).StringCellValue;
                        if (columnname == "")
                            continue;
                        DataColumn column = new DataColumn(columnname);
                        table.Columns.Add(column);
                    }

                    for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null)
                            break;
                        if (row.FirstCellNum < 0)
                        {
                            continue;
                        }
                        else if (row.GetCell(row.FirstCellNum).ToString().Trim() == "")
                        {
                            continue;
                        }

                        DataRow dataRow = table.NewRow();

                        if (row != null)
                        {
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    { //空数据类型处理
                                        case CellType.Blank:
                                            dataRow[j] = "";
                                            break;
                                        case CellType.String:
                                            dataRow[j] = row.GetCell(j).StringCellValue;
                                            break;
                                        case CellType.Numeric: //数字类型  
                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = row.GetCell(j).DateCellValue;
                                            }
                                            else
                                            {
                                                dataRow[j] = row.GetCell(j).NumericCellValue;
                                            }
                                            break;
                                        case CellType.Formula:
                                            dataRow[j] = row.GetCell(j).NumericCellValue;
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }
                        }

                        table.Rows.Add(dataRow);
                    }
                    workbook = null;
                    sheet = null;
                    return table;

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 特殊表格转化，进口关务使用
        /// </summary>
        /// <param name="excelFileStream"></param>
        /// <param name="RowIndex">从第几行开始读起</param>
        /// <returns></returns>
        public static DataTable RenderDataTableFromExcel2007(Stream excelFileStream, int RowIndex)
        {
            DataTable table = new DataTable();
            try
            {
                using (excelFileStream)
                {
                    IWorkbook workbook = new XSSFWorkbook(excelFileStream);

                    ISheet sheet = workbook.GetSheetAt(0);//取第一个表 

                    IRow headerRow = sheet.GetRow(RowIndex);
                    int cellCount = headerRow.LastCellNum;
                    int rowCount = sheet.LastRowNum;

                    //获取载货清单号及车牌号
                    string TruckNo = "", PackingListNo = "";
                    IRow PackingListRow = sheet.GetRow(2);
                    for (int i = PackingListRow.FirstCellNum; i < cellCount; i++)
                    {
                        if (PackingListRow.GetCell(i).StringCellValue.Contains("车牌号码"))
                        {
                            TruckNo = PackingListRow.GetCell(i + 2).StringCellValue;
                            i = i + 2;
                        }
                        else if (PackingListRow.GetCell(i).StringCellValue.Contains("载货清单"))
                        {
                            PackingListNo = PackingListRow.GetCell(i + 2).StringCellValue;
                            i = cellCount;
                        }
                    }


                    //handling header. 
                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        string columnname = headerRow.GetCell(i).StringCellValue;
                        if (columnname == "")
                            continue;
                        DataColumn column = new DataColumn(columnname);
                        table.Columns.Add(column);
                    }
                    //添加两列，车牌号与载货清单号
                    table.Columns.Add(new DataColumn("载货清单号"));
                    table.Columns.Add(new DataColumn("车牌号"));

                    for (int i = (RowIndex + 1); i <= rowCount; i++)//从第四行开始读取数据
                    {
                        IRow row = sheet.GetRow(i);
                        if (row.FirstCellNum < 0)
                        {
                            continue;
                        }
                        else if (row.GetCell(row.FirstCellNum).ToString().Trim() == "" || row.GetCell(row.FirstCellNum).ToString().Contains("合计"))
                        {
                            i = rowCount;
                            continue;
                        }

                        DataRow dataRow = table.NewRow();

                        if (row != null)
                        {
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    { //空数据类型处理
                                        case CellType.Blank:
                                            dataRow[j] = "";
                                            break;
                                        case CellType.String:
                                            dataRow[j] = row.GetCell(j).StringCellValue;
                                            break;
                                        case CellType.Numeric: //数字类型  
                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = row.GetCell(j).DateCellValue;
                                            }
                                            else
                                            {
                                                dataRow[j] = row.GetCell(j).NumericCellValue;
                                            }
                                            break;
                                        case CellType.Formula:
                                            dataRow[j] = row.GetCell(j).NumericCellValue;
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }

                            //添加车牌号及载货清单号
                            dataRow[cellCount] = PackingListNo;
                            dataRow[cellCount + 1] = TruckNo;
                        }

                        table.Rows.Add(dataRow);
                    }
                    workbook = null;
                    sheet = null;
                    return table;

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 特殊表格转化，出口关务使用
        /// </summary>
        /// <param name="excelFileStream"></param>
        /// <param name="RowIndex">从第几行开始读起</param>
        /// <returns></returns>
        public static DataTable RenderDataTableFromExcel2007_CtmOut(Stream excelFileStream, int RowIndex)
        {
            DataTable table = new DataTable();
            try
            {
                using (excelFileStream)
                {
                    IWorkbook workbook = new XSSFWorkbook(excelFileStream);

                    ISheet sheet = workbook.GetSheetAt(0);//取第一个表 

                    IRow headerRow = sheet.GetRow(RowIndex);
                    int cellCount = headerRow.LastCellNum;
                    int rowCount = sheet.LastRowNum;

                    //获取载货清单号及车牌号
                    string TruckNo = "", PackingListNo = "", CompanyCode = "";
                    IRow PackingListRow = sheet.GetRow(2);
                    for (int i = PackingListRow.FirstCellNum; i < cellCount; i++)
                    {
                        if (PackingListRow.GetCell(i).StringCellValue.Contains("车牌号码"))
                        {
                            TruckNo = PackingListRow.GetCell(i + 2).StringCellValue;
                            i = i + 2;
                        }
                        else if (PackingListRow.GetCell(i).StringCellValue.Contains("载货清单"))
                        {
                            PackingListNo = PackingListRow.GetCell(i + 2).StringCellValue;
                            i = i + 2;
                        }
                        else if (PackingListRow.GetCell(i).StringCellValue.Contains("企业代码"))
                        {
                            CompanyCode = PackingListRow.GetCell(i + 2).StringCellValue;
                            i = i + 2;
                        }
                    }


                    //handling header. 
                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        string columnname = headerRow.GetCell(i).StringCellValue;
                        if (columnname == "")
                            continue;
                        DataColumn column = new DataColumn(columnname);
                        table.Columns.Add(column);
                    }
                    //添加两列，车牌号与载货清单号
                    table.Columns.Add(new DataColumn("载货清单号"));
                    table.Columns.Add(new DataColumn("车牌号"));
                    table.Columns.Add(new DataColumn("企业代码"));

                    for (int i = (RowIndex + 1); i <= rowCount; i++)//从第四行开始读取数据
                    {
                        IRow row = sheet.GetRow(i);
                        if (row.FirstCellNum < 0)
                        {
                            continue;
                        }
                        else if (row.GetCell(row.FirstCellNum).ToString().Trim() == "" || row.GetCell(row.FirstCellNum).ToString().Contains("合计"))
                        {
                            i = rowCount;
                            continue;
                        }

                        DataRow dataRow = table.NewRow();

                        if (row != null)
                        {
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    { //空数据类型处理
                                        case CellType.Blank:
                                            dataRow[j] = "";
                                            break;
                                        case CellType.String:
                                            dataRow[j] = row.GetCell(j).StringCellValue;
                                            break;
                                        case CellType.Numeric: //数字类型  
                                            if (HSSFDateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = row.GetCell(j).DateCellValue;
                                            }
                                            else
                                            {
                                                dataRow[j] = row.GetCell(j).NumericCellValue;
                                            }
                                            break;
                                        case CellType.Formula:
                                            dataRow[j] = row.GetCell(j).NumericCellValue.ToString();
                                            break;
                                        default:
                                            dataRow[j] = "";
                                            break;
                                    }
                                }
                            }

                            //添加车牌号及载货清单号
                            dataRow[cellCount] = PackingListNo;
                            dataRow[cellCount + 1] = TruckNo;
                            dataRow[cellCount + 2] = CompanyCode;
                        }

                        table.Rows.Add(dataRow);
                    }
                    workbook = null;
                    sheet = null;
                    return table;

                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }
        #endregion

        //读取本地文件Excel的标题
        public static DataTable RenderDataTableFormExcelHeader2007(string filePath)
        {

            DataTable table = new DataTable();
            try
            {
                IWorkbook workbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(file);
                }

                ISheet sheet = workbook.GetSheetAt(0);//取第一个表 

                IRow headerRow = sheet.GetRow(0);//第一行为标题行 
                int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells 
                int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1 

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    string colName = headerRow.GetCell(i).StringCellValue;
                    if (colName == "")
                        continue;
                    DataColumn column = new DataColumn(colName);
                    table.Columns.Add(column);
                }

                for (int i = 1; i <= 1; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    if (row != null)
                    {
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                dataRow[j] = "";
                        }
                    }

                    table.Rows.Add(dataRow);
                }

                workbook = null;
                sheet = null;
                return table;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 读取Excel列
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="rowIndex">读取Excel第几行数据，Index从零开始</param>
        /// <returns></returns>
        public static DataTable RenderDataTableFormExcelHeader2007(string filePath, int rowIndex)
        {

            DataTable table = new DataTable();
            try
            {
                IWorkbook workbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(file);
                }

                ISheet sheet = workbook.GetSheetAt(0);//取第一个表 

                IRow headerRow = sheet.GetRow(rowIndex);//第一行为标题行 
                int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells 
                int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1 

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    string colName = headerRow.GetCell(i).StringCellValue;
                    if (colName == "")
                        continue;
                    DataColumn column = new DataColumn(colName);
                    table.Columns.Add(column);
                }

                for (int i = 1; i <= 1; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    if (row != null)
                    {
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                dataRow[j] = "";
                        }
                    }

                    table.Rows.Add(dataRow);
                }

                workbook = null;
                sheet = null;
                return table;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }


        #region 同时导出多个SheetName页，并且第一行为标题
        /// <summary>
        /// 根据DataSet导出Excel，多个SheetName
        /// </summary>
        /// <param name="ds">DataSet数据源</param>
        /// <param name="SheetName">SheetName数组</param>
        /// <param name="TitleName">每个SheetName第一行标题栏名称，数组类型</param>
        /// <param name="isShowTitle">是否显示第一行标题，默认为显示</param>
        /// <returns></returns>
        public static MemoryStream RenderToMemory(DataSet ds, string[] SheetName, string[] TitleName, bool isShowTitle = true)
        {
            MemoryStream ms = new MemoryStream();

            using (ds)
            {
                IWorkbook workbook = new HSSFWorkbook();

                //第一行样式
                ICellStyle titleStyle = TitleStyle(workbook);
                //标题栏位样式
                ICellStyle headerStyle = HeaderStyle(workbook);
                //数据单元格
                ICellStyle cellStyle = BodyStyle(workbook);
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    DataTable dt = ds.Tables[i] as DataTable;
                    ISheet sheet = workbook.CreateSheet(SheetName[i]);

                    int rowIndex = 0;
                    if (isShowTitle)
                    {
                        IRow title = sheet.CreateRow(rowIndex);
                        title.HeightInPoints = 35;//设置行高度
                        ICell headercell = title.CreateCell(0);
                        headercell.SetCellValue(TitleName[i]);
                        SetCellRangeAddress(sheet, 0, 0, 0, dt.Columns.Count - 1);
                        headercell.CellStyle = titleStyle;
                        rowIndex++;
                    }

                    //标题栏位
                    IRow headerRow = sheet.CreateRow(rowIndex);
                    headerRow.HeightInPoints = 25;
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        ICell headercell = headerRow.CreateCell(c);
                        headercell.SetCellValue(dt.Columns[c].ColumnName);
                        headercell.CellStyle = headerStyle;
                    }
                    rowIndex++;

                    foreach (DataRow row in dt.Rows)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        dataRow.HeightInPoints = 19;
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            ICell cell = dataRow.CreateCell(c);
                            var currField = dt.Columns[c].ColumnName;
                            string strVale = row[currField].ToString();

                            if (dt.Columns[currField].DataType == typeof(decimal) || dt.Columns[currField].DataType == typeof(int))
                            {
                                cell.SetCellValue(double.Parse(strVale == "" ? "0" : strVale));
                            }
                            else
                            {
                                cell.SetCellValue(strVale);
                            }
                            cell.CellStyle = cellStyle;
                        }
                        rowIndex++;
                    }
                    /*为了导出列为中文*/
                    int colcount = dt.Columns.Count;
                    for (int c = 0; c < colcount; c++)
                    {
                        sheet.AutoSizeColumn(c);
                    }
                }
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
            }
            return ms;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }

        public static ICellStyle TitleStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            //设置字体为粗体
            IFont font = workbook.CreateFont();
            font.FontHeight = 20 * 20;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            //设置居中
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            //设置边框
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Grey50Percent.Index;
            style.SetFont(font);
            return style;
        }

        public static ICellStyle HeaderStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            //设置字体为粗体
            IFont font = workbook.CreateFont();
            //font.IsItalic = true;//斜体
            font.FontHeightInPoints = (short)10;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            //设置居中
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            //设置边框
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Grey80Percent.Index;
            style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Grey80Percent.Index;
            style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Grey80Percent.Index;
            style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Grey80Percent.Index;
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Orange.Index;
            style.FillPattern = FillPattern.Squares;
            style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Orange.Index;
            style.SetFont(font);
            return style;
        }

        public static ICellStyle BodyStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            //设置字体为粗体
            IFont font = workbook.CreateFont();
            //font.IsItalic = true;//斜体
            font.FontHeightInPoints = (short)10;
            //font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            //设置居中
            //style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            //设置边框
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Grey80Percent.Index;
            style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Grey80Percent.Index;
            style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Grey80Percent.Index;
            style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Grey80Percent.Index;
            style.SetFont(font);
            return style;
        }
        #endregion
    }
}
