using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

using RenderedDataList = System.Collections.Generic.List<System.Collections.Generic.List<System.String>>;

namespace Map.Components.Search
{
    public static class ExtensionMethods
    {
      public static void DoubleBuffered(this DataGridView dgv, bool setting)
      {
          Type dgvType = dgv.GetType();
         PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
             BindingFlags.Instance | BindingFlags.NonPublic);
         pi.SetValue(dgv, setting, null);
      }
    }

    public partial class Search : Form
    {
        DataSource newDataSource;
        List<DataEntry> currentData;
        Dictionary<string, string> searchQuery;

        Dictionary<String, ComboBox> filterBoxes;
        public Search(List<String> initPath)
        {
            InitializeComponent();
            filterBoxes = new Dictionary<String, ComboBox>() {
                { "Judet", this.filterJudet},
                { "Firma", this.filterFirma},
                { "Oras", this.filterLocalitate},
                { "Tarla", this.filterTarla},
                { "Status", this.filterStatus},
                { "Data", this.filterData},

            };

            dataGridView1.ColumnCount = 11;

            dataGridView1.Columns[0].Name = "Nr. Crt.";
            dataGridView1.Columns[1].Name = "Nr. Dosar";
            dataGridView1.Columns[2].Name = "Judet";
            dataGridView1.Columns[3].Name = "Firma";
            dataGridView1.Columns[4].Name = "Localitate";
            dataGridView1.Columns[5].Name = "Nume";
            dataGridView1.Columns[6].Name = "Tarla";
            dataGridView1.Columns[7].Name = "Parcela";
            dataGridView1.Columns[8].Name = "Status";
            dataGridView1.Columns[9].Name = "Data";
            dataGridView1.Columns[10].Name = "Suprafata";

            searchQuery = new Dictionary<string, string>();

            newDataSource = new DataSource(initPath);
            currentData = newDataSource.rawData;

            filterJudet.Items.AddRange(
                newDataSource.getUniqueValues(currentData, "Judet").ToArray()
            );

            filterFirma.Items.Clear();
            filterFirma.Items.AddRange(
                newDataSource.getUniqueValues(currentData, "Firma").ToArray()
            );

            filterStatus.Items.Clear();
            filterStatus.Items.AddRange(
                newDataSource.getUniqueValues(currentData, "Status").ToArray()
            );
            resetTable("");
        }

        private void resetTable(string resetField)
        {
            List<String> keys = this.filterBoxes.Keys.ToList();

            int index = keys.IndexOf(resetField);

            for (int i = index + 1; i < this.filterBoxes.Count; i++)
            {
                this.filterBoxes[keys[i]].Items.Clear();

                if (keys[i] != "Data")
                {
                    this.filterBoxes[keys[i]].Items.AddRange(
                        newDataSource.getUniqueValues(currentData, keys[i]).ToArray()
                    );
                }
                searchQuery[keys[i]] = null;
            }


            dataGridView1.Rows.Clear();
            float sum = 0;
            int count = 0;
            foreach (List<String> entry in newDataSource.renderData(currentData))
            {
                dataGridView1.Rows.Add(entry.ToArray());
                string num = entry[entry.Count - 1];
                float number = float.Parse(num);
                sum += number;
                count++;
            }
            int antecontract = 0;
            int cesiune = 0;
            int sentinta = 0;
            int cvc = 0;
            List<String> values = new List<string>();
            List<String> nume = new List<string>();
            List<String> dosare = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                values.Add(Convert.ToString(dataGridView1.Rows[i].Cells[8].Value));
                nume.Add(Convert.ToString(dataGridView1.Rows[i].Cells[5].Value));
            }

            dosare = nume.Distinct().ToList();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            int indexx = 1;
            foreach (string name in dosare)
            {
                dict.Add(name, indexx);
                indexx++;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dosare.Contains(Convert.ToString(dataGridView1.Rows[i].Cells[5].Value)))
                {
                    dataGridView1.Rows[i].Cells[1].Value = dict[Convert.ToString(dataGridView1.Rows[i].Cells[5].Value)];
                }
            }

            foreach (string value in values)
            {
                if (value == "Antecontract")
                {
                    antecontract++;
                }
                else if (value == "Cesiune")
                {
                    cesiune++;
                }
                else if (value == "Cvc")
                {
                    cvc++;
                }
                else if (value == "Sentinta")
                {
                    sentinta++;
                }
            }

                
            dataGridView1.Rows.Add("", "Numar dosare: " + (indexx - 1), "", "", "", "", "", "Numar parcele: " + count , "Antecontracte: " + antecontract, "", "Total suprafata: " + sum);
            dataGridView1.Rows.Add("", "", "", "", "", "", "", "", "Cesiuni: " + cesiune, "", "");
            dataGridView1.Rows.Add("", "", "", "", "", "", "", "", "CVC: " + cvc, "", "");
            dataGridView1.Rows.Add("", "", "", "", "", "", "", "", "Sentinte: " + sentinta, "", "");
            ExtensionMethods.DoubleBuffered(dataGridView1, true);
        }

        private void resetQuery(string resetField)
        {
            List<String> keys = this.filterBoxes.Keys.ToList();

            int index = keys.IndexOf(resetField);

            for (int i = index + 1; i < this.filterBoxes.Count; i++)
            {
                searchQuery[keys[i]] = null;
            }
        }

        private void filterJudet_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchQuery["Judet"] = filterJudet.Text;
            resetQuery("Judet");
            search();
            resetTable("Judet");
        }

        private void filterLocalitate_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchQuery["Oras"] = filterLocalitate.Text;
            search();
            resetTable("Oras");
        }

        private void filterFirma_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchQuery["Firma"] = filterFirma.Text;
            search();
            resetTable("Firma");
        }

        private void search()
        {
            currentData = newDataSource.rawData;

            foreach (string key in searchQuery.Keys)
            {
                if (searchQuery[key] != null)
                {
                    currentData = newDataSource.filterData(currentData, key, searchQuery[key]);

                }
            }
        }

        private void filterTarla_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchQuery["Tarla"] = filterTarla.Text;
            search();
            resetTable("Tarla");
        }

        private void filterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchQuery["Status"] = filterStatus.Text;
            search();
            resetTable("Status");
        }

        private void filterData_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchQuery["Data"] = filterData.Text;
            search();
            resetTable("Data");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog result = new SaveFileDialog();
            result.InitialDirectory = @"C:\";
            result.RestoreDirectory = true;
            result.FileName = ".csv";
            result.DefaultExt = "csv";
            result.Filter = "CSV Files (*.csv) | *.csv";
            if (result.ShowDialog() == DialogResult.OK)
            {
                Stream fileStream = result.OpenFile();
                fileStream.Close();
            }


            StreamWriter sw = new StreamWriter(result.FileName, false);

            sw.Write("Nr. Crt.,");
            foreach (String key in currentData[0].toHash().Keys)
            {
                sw.Write(key);
                if (currentData[0].toHash().Keys.ToList().IndexOf(key) < currentData[0].toHash().Keys.Count - 1)
                {
                    sw.Write(",");
                }
            }

            sw.Write(sw.NewLine);
            int index = 0;
            foreach (DataEntry dr in currentData)
            {
                index++;
                List<String> newList = dr.toList();
                sw.Write(index.ToString() + ',');
                for (int i = 0; i < newList.Count; i++)
                {
                    String value = newList[i];


                    if (value != null)
                    {
                        if (value.Contains(','))
                        {
                            String newValue = String.Format("\"{0}\"", value);
                            sw.Write(newValue);
                        }
                        else
                        {
                            sw.Write(value.ToString());
                        }
                    }
                    if (i < newList.Count - 1)
                    {
                        sw.Write(",");
                    }
                }

                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
