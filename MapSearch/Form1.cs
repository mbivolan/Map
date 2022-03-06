using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 8;

            dataGridView1.Columns[0].Name = "Judet";
            dataGridView1.Columns[1].Name = "Localitate";
            dataGridView1.Columns[2].Name = "Firma";
            dataGridView1.Columns[3].Name = "Tarla";
            dataGridView1.Columns[4].Name = "Status";
            dataGridView1.Columns[5].Name = "Nume";
            dataGridView1.Columns[6].Name = "Data";
            dataGridView1.Columns[7].Name = "Suprafata";

            List<String> test = new List<string>();
            test.Add("lol1");

            DataSource newDataSource = new DataSource(new List<String>());

            foreach (List<String> entry in newDataSource.data)
            {
                dataGridView1.Rows.Add(entry.ToArray());
            }

            foreach (List<String> entry in newDataSource.data)
            {
                // Faci o lista pentru oras 
                // daca newDataSource.data[1] nu exista in lista, il bagi

            }

            // bagi lista in combobox

        }
    }
}
