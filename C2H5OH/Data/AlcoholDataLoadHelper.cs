using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace C2H5OH.Data
{
    internal static class AlcoholDataLoadHelper
    {
        /*
         * Credit to https://immortalcoder.blogspot.com/2013/12/convert-csv-file-to-datatable-in-c.html
         */
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath, Encoding.UTF8);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}