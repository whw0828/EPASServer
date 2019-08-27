using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAS.Component.Excel
{
    /// <summary>
    /// EXCEL 读写类
    /// </summary>
   public class ExcelHelper
    {

        /// <summary>
        /// 获取指定单元格数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="row"></param>
        /// <param name="colum"></param>
        /// <returns></returns>
       public static object GetDataTableData(DataTable data,int row,int colum)
        {
            if (data == null) return null;
          
              return  data.Rows[row][colum];
         
        }

        private static string TransICell(ICell cell)
        {
            string cellValue = string.Empty;
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    cellValue = cell.NumericCellValue.ToString();
                    break;

                case CellType.String:
                    cellValue = cell.StringCellValue;
                    break;
            }

            return cellValue;
        }
        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="filePath">Excel文件</param>
        /// <param name="strSheetName">Sheet名</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns></returns>
        public static DataTable ReadExcelFileToDataTable(string fileName, string strSheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;

            IWorkbook workbook = null;
            #region  初始化信息
            try
            {
                //打开并读取指定路径Excel文件
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
            }
            catch (Exception e)
            {
                throw e;
            }

            #endregion

            //获取文件的指定Sheet
            if(strSheetName!=null)
            {
                sheet = workbook.GetSheet(strSheetName);

                if (sheet == null)//如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                {
                    sheet = workbook.GetSheetAt(0);//读取第一张Sheet页中数据
                }
            }else
            {
                sheet = workbook.GetSheetAt(0);//读取第一张Sheet页中数据
            }

            if (sheet!=null)
            {
                IRow firstRow = sheet.GetRow(0);
                int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                if (isFirstRowColumn)
                {
                    //读取第一行列名
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if(cell!=null && cell.CellType!= CellType.Blank)
                        {
                            string cellValue = TransICell(cell);
                            

                            DataColumn column = new DataColumn(cellValue);
                            data.Columns.Add(column);
                        }
                    }
                    startRow = sheet.FirstRowNum + 1;
                }
                else
                {
                    startRow = sheet.FirstRowNum;
                }

                //最后一列的标号
                int rowCount = sheet.LastRowNum;
                for (int i = startRow; i <= rowCount; ++i)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue; //没有数据的行默认是null　　　　　　　

                    DataRow dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null && cell.CellType != CellType.Blank)
                        {
                            string cellValue = TransICell(cell); 
                            
                            dataRow[j] = cellValue;
                        }
  
                            
                    }
                    data.Rows.Add(dataRow);
                }
            }

            return data;
        }

        

    }    
}
