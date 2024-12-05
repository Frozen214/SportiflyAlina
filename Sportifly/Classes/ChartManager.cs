using Guna.Charts.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sportifly.Classes
{
    public class ChartManager
    {
        public bool CheckEmpty(DataTable dataTable)
        {
            return dataTable.Rows.Count > 0;
        }

        public void ChartBar(GunaChart chart, DataTable data, string nameChart)
        {
            if (!CheckEmpty(data))
            {
                MessageBox.Show("Набор данных не определен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            chart.Datasets.Clear();
            chart.Legend.Display = false;
            chart.YAxes.GridLines.Display = false;
            chart.XAxes.Display = true;
            chart.YAxes.Display = true;
            chart.Title.Text = nameChart;

            var dataset = new GunaBarDataset();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                dataset.DataPoints.Add(
                Convert.ToString(data.Rows[i][0]),
                Convert.ToDouble(data.Rows[i][1])
                );
            }
            chart.Datasets.Add(dataset);
        }
    }
}
