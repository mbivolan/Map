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

            addToCombo();
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
        }
        private void addToCombo()
        {
            {
                var listaJudet = new List<string> {
                    ("DOLJ"),
                    ("OLT"),
                };
                var listaFirma = new List<string> {
                    ("ANM"),
                    ("CEREALCOM"),
                    ("CERVINA"),
                    ("OLTYRE"),
                    ("REDIAS"),
                };
                var listaLocalitate = new List<string> {
                    ("AMARASTII DE SUS"),
                    ("BIRCA"),
                    ("CALOPAR"),
                    ("CARACAL"),
                    ("CERAT"),
                    ("DRANIC"),
                    ("GINGIOVA"),
                    ("GIURGITA"),
                    ("GOICEA"),
                    ("LIVPOVU"),
                    ("MACESU DE JOS"),
                    ("MACESU DE SUS"),
                    ("REDEA"),
                    ("SEGARCEA"),
                    ("VALEA STANCIULUI"),
                };
                var listaTarla = new List<string> {
                    ("1"),
                    ("2"),
                };
                var listaStatus = new List<string> {
                    ("ANTECONTRACT"),
                    ("CESIUNE"),
                    ("CVC"),
                    ("SENTINTA"),

                };
                var listaData = new List<string> {
                    ("PENDING"),
                    ("PENDING"),
                };
                foreach (string entry in listaJudet)
                {
                    this.filterJudet.Items.Add(entry);
                }
                foreach (string entry in listaFirma)
                {
                    this.filterFirma.Items.Add(entry);
                }
                foreach (string entry in listaLocalitate)
                {
                    this.filterLocalitate.Items.Add(entry);
                }
                foreach (string entry in listaTarla)
                {
                    this.filterTarla.Items.Add(entry);
                }
                foreach (string entry in listaStatus)
                {
                    this.filterStatus.Items.Add(entry);
                }
                foreach (string entry in listaData)
                {
                    this.filterData.Items.Add(entry);
                }
            }
        }
    }
}
