using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.IO;
using System.Diagnostics;
using NPOI;
using NPOI.Util;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace Enow.TZB.Utility
{
    /// <summary> 
    /// NPOIHelper类  
    /// </summary> 
    public class NPOIHelper
    {
        #region excel2003超过65535行数据用多个sheet处理
        /// <summary> 
        /// 将DataTable数据导出到Excel文件中(xls) 最大支持65535行 
        /// </summary> 
        /// <param name="dt">数据源</param> 
        /// <param name="excelName">导出文件名称</param> 
        public static void TableToExcelForXLSAny(DataTable dt, string excelName)
        {
            //Stopwatch stopWacth = new Stopwatch();
            //stopWacth.Start();// 开始计时器 
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            int rowCount = dt.Rows.Count;
            int sheetMaxRow = 65535;

            //大于65535行数据 
            if (rowCount > sheetMaxRow)
            {
                for (int k = 1; k <= rowCount / sheetMaxRow; k++)
                {
                    ISheet sheet = hssfworkbook.CreateSheet(excelName + "_" + k);
                    //表头   
                    IRow row = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet.SetColumnWidth(i, 30 * 180);// 设置所在列的宽度 
                        ICell cell = row.CreateCell(i);
                        cell.SetCellValue(dt.Columns[i].ColumnName);
                    }

                    int m = 0;
                    //数据   
                    for (int i = (k - 1) * sheetMaxRow; i < k * sheetMaxRow; i++)
                    {
                        IRow row1 = sheet.CreateRow(m + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ICell cell = row1.CreateCell(j);
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                        m++;
                    }
                }

                if (rowCount % sheetMaxRow != 0)
                {
                    ISheet sheet = hssfworkbook.CreateSheet(excelName + "_" + (rowCount / sheetMaxRow + 1));
                    //表头   
                    IRow row = sheet.CreateRow(0);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet.SetColumnWidth(i, 30 * 180);// 设置所在列的宽度 
                        ICell cell = row.CreateCell(i);
                        cell.SetCellValue(dt.Columns[i].ColumnName);
                    }

                    int m = 0;

                    //数据   
                    for (int i = (rowCount / sheetMaxRow) * sheetMaxRow; i < rowCount; i++)
                    {

                        IRow row1 = sheet.CreateRow(m + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            ICell cell = row1.CreateCell(j);
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                        m++;
                    }
                }

            }

             // 小于65536行    
            else
            {
                ISheet sheet = hssfworkbook.CreateSheet(excelName);
                //表头   
                IRow row = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sheet.SetColumnWidth(i, 30 * 180);// 设置所在列的宽度 
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(dt.Columns[i].ColumnName);
                }

                //数据   
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row1 = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row1.CreateCell(j);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
            }

            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            curContext.Response.Clear();
            curContext.Response.ContentType = "application/x-excel";
            string filename = HttpUtility.UrlEncode(excelName + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls");
            curContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            hssfworkbook.Write(curContext.Response.OutputStream);
            //File.AppendAllText(curContext.Server.MapPath("/TestMinutes/TestResult.txt"), "\nNPOI得到dataTable后导出数据速度:" + stopWacth.ElapsedMilliseconds.ToString() + "毫秒", System.Text.Encoding.UTF8);
            //stopWacth.Stop();
            curContext.Response.End();
        }
        #endregion

        #region Excel2003
        /// <summary>   
        /// 将Excel文件中的数据读出到DataTable中(xls)   
        /// </summary>   
        /// <param name="file">文件路径</param>   
        /// <returns>DataTable</returns>   
        public static DataTable ExcelToTableForXLS(string file)
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(fs);
                ISheet sheet = hssfworkbook.GetSheetAt(0);

                //表头   
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLS(header.GetCell(i) as HSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        //continue;   
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }
                //数据   
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueTypeForXLS(sheet.GetRow(i).GetCell(j) as HSSFCell);
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        /// <summary>   
        /// 将DataTable数据导出到Excel文件中(xls)   
        /// </summary>   
        /// <param name="dt">数据源</param>   
        /// <param name="excelName">excel名称</param>   
        public static void TableToExcelForXLS(DataTable dt, string excelName, string SheetName)
        {
            HSSFWorkbook hssfworkbook = new HSSFWorkbook();
            ISheet sheet = hssfworkbook.CreateSheet(SheetName);

            //表头   
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据   
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            curContext.Response.Clear();
            curContext.Response.ContentType = "application/x-excel";
            string filename = HttpUtility.UrlEncode(excelName + DateTime.Now.ToString("yyyyMMddHHmm") + ".xls");
            curContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            hssfworkbook.Write(curContext.Response.OutputStream);
            curContext.Response.End();
        }

        /// <summary>   
        /// 获取单元格类型(xls)   
        /// </summary>   
        /// <param name="cell">单元格</param>   
        /// <returns>单元格类型</returns>   
        private static object GetValueTypeForXLS(HSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:   
                    return null;
                case CellType.Boolean: //BOOLEAN:   
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:   
                    return cell.NumericCellValue;
                case CellType.String: //STRING:   
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:   
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:   
                default:
                    return "=" + cell.CellFormula;
            }
        }
        #endregion

        #region Excel2007和简单的Excel2010
        /// <summary>   
        /// 将Excel文件中的数据读出到DataTable中(xlsx)   
        /// </summary>   
        /// <param name="file">文件路径</param>   
        /// <returns>DataTable</returns>   
        public static DataTable ExcelToTableForXLSX(string file)
        {
            DataTable dt = new DataTable();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                ISheet sheet = xssfworkbook.GetSheetAt(0);

                // 表头   
                IRow header = sheet.GetRow(sheet.FirstRowNum);
                List<int> columns = new List<int>();
                for (int i = 0; i < header.LastCellNum; i++)
                {
                    object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                    if (obj == null || obj.ToString() == string.Empty)
                    {
                        dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                        // continue;   
                    }
                    else
                        dt.Columns.Add(new DataColumn(obj.ToString()));
                    columns.Add(i);
                }

                // 数据   
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    DataRow dr = dt.NewRow();
                    bool hasValue = false;
                    foreach (int j in columns)
                    {
                        dr[j] = GetValueTypeForXLSX(sheet.GetRow(i).GetCell(j) as XSSFCell);
                        if (dr[j] != null && dr[j].ToString() != string.Empty)
                        {
                            hasValue = true;
                        }
                    }
                    if (hasValue)
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

        /// <summary>   
        /// 将DataTable数据导出到Excel文件中(xlsx)   
        /// </summary>   
        /// <param name="dt">数据源</param>   
        /// <param name="excelName">文件名称</param>   
        public static void TableToExcelForXLSX(DataTable dt, string excelName)
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook();
            ISheet sheet = xssfworkbook.CreateSheet("Test");

            //表头   
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据   
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            curContext.Response.Clear();
            curContext.Response.ContentType = "application/x-excel";
            string filename = HttpUtility.UrlEncode(excelName + DateTime.Now.ToString("yyyyMMddHHmm") + ".xlsx");
            curContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            xssfworkbook.Write(curContext.Response.OutputStream);
            curContext.Response.End();
        }

        /// <summary>   
        /// 获取单元格类型(xlsx)   
        /// </summary>   
        /// <param name="cell">单元格</param>   
        /// <returns>单元格类型</returns>   
        private static object GetValueTypeForXLSX(XSSFCell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:   
                    return null;
                case CellType.Boolean: //BOOLEAN:   
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:   
                    return cell.NumericCellValue;
                case CellType.String: //STRING:   
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:   
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:   
                default:
                    return "=" + cell.CellFormula;
            }
        }
        #endregion
    }
}
