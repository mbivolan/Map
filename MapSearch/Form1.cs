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
using RenderedDataList = System.Collections.Generic.List<System.Collections.Generic.List<System.String>>;

namespace MapSearch
{
    public partial class Form1 : Form
    {
        DataSource newDataSource;
        List<DataEntry> currentData;
        Dictionary<String, String> searchQuery;

        Dictionary<String, ComboBox> filterBoxes;  

        public Form1()
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
            dataGridView1.Columns[1].Name = "Nr. Parcele";
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

            newDataSource = new DataSource(new List<String>());
            currentData = newDataSource.rawData;


            /*
            { "NumarParcela", this.NumarParcela} ,
                { "Judet", this.Judet },
                { "Firma", this.Firma },
                { "Oras", this.Oras },
                { "Nume", this.NumeProprietar },
                { "Tarla", this.Tarla },
                { "Parcela", this.Parcela },
                { "Status", this.StatusDosar },
                { "Data", this.DataContract },
                { "Suprafata", this.Suprafata }
            */
            // private System.Windows.Forms.ComboBox filterFirma;
            //private System.Windows.Forms.ComboBox filterLocalitate;
            //private System.Windows.Forms.ComboBox filterTarla;
            //private System.Windows.Forms.ComboBox filterStatus;
            //private System.Windows.Forms.ComboBox filterData;

            // Baga si pe restu :D

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

        private void filterList(List<string> list, List<string> filter)
        {
            foreach (string s in list)
            {
                filter.Add(s);
            }
        }

        private void ResetFilters(List<string> filter)
        {
            List<String> firma = new List<String>();
            List<String> localitate = new List<String>();
            List<String> tarla = new List<String>();
            List<String> status = new List<String>();
            List<String> data = new List<String>();
            
            filterList(filter, firma);
            filterList(filter, localitate);
            filterList(filter, tarla);
            filterList(filter, status);
            filterList(filter, data);
        }

        /*
                private void resetTable(string resetField)
                {
                    filterLocalitate.Items.Clear();
                    filterLocalitate.Items.AddRange(
                        newDataSource.getUniqueValues(currentData, "Oras").ToArray()
                    );

                    filterTarla.Items.Clear();
                    filterTarla.Items.AddRange(
                        newDataSource.getUniqueValues(currentData, "Tarla").ToArray()
                    );


                    filterData.Items.Clear();
                    filterData.Items.AddRange(
                        newDataSource.getUniqueValues(currentData, "Data").ToArray()
                    );



                }
        */

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
            foreach (List<String> entry in newDataSource.renderData(currentData))
            {
                dataGridView1.Rows.Add(entry.ToArray());
            }

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
            //currentData = newDataSource.filterData(currentData, "Judet", filterJudet.Text);
            searchQuery["Judet"] = filterJudet.Text;
            resetQuery("Judet");
            search();
            resetTable("Judet");
        }

        private void filterLocalitate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //currentData = newDataSource.filterData(currentData, "Oras", filterLocalitate.Text);
            searchQuery["Oras"] = filterLocalitate.Text;
            search();
            resetTable("Oras");
        }

        private void filterFirma_SelectedIndexChanged(object sender, EventArgs e)
        {
            //currentData = newDataSource.filterData(currentData, "Firma", filterFirma.Text);
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
            StreamWriter sw = new StreamWriter(@"C:\Users\mabiv\Workspace\MapProject\test.csv", false);

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

                for (int i = 0; i < newList.Count; i++)
                {
                    String value = newList[i];
                    if (i == 0)
                    {
                        sw.Write(index.ToString() + ',');
                        continue;
                    }

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
