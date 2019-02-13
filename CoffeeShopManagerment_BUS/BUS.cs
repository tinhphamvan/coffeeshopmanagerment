using CoffeeShopManagerment_DAL.DTO_DAO;
using System;
using CoffeeShopManagerment_BUS;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManagerment_BUS
{
   
    public class BUS
    {
      
            coffeemanagerDBEntities db = new coffeemanagerDBEntities();

            public void exportExel(DataGridView table)
            {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Tính Lương";

            for (int i = 1; i < table.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = "'" + table.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = table.Rows[i].Cells[j].Value.ToString();
                }
            }

            var saveFile = new SaveFileDialog();
            saveFile.FileName = "output";
            saveFile.DefaultExt = ".xlsx";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                workbook.SaveAs(saveFile.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            app.Quit();
            MessageBox.Show("Xuất file thành công. Vui lòng kiểm tra lại.");
        }

            
            public int checkRow (int t, DateTime nn)
            {
                int h = db.Schedules.Where(x => x.staffID == t && x.dateID == nn).Select(o => o.scheduleID).FirstOrDefault();
                return h;
            }
            
            public bool checkWorkedDate(DateTime date)
            {
                bool check = db.Schedules.Any(o => o.dateID == date);
                return check;
            }

            public int? checkRow2(int t, DateTime nn)
            {
                int? h = db.Schedules.Where(x => x.staffID == t && x.dateID == nn).Select(o => o.scheduleID).FirstOrDefault();
                return h;
            }


    }
}
