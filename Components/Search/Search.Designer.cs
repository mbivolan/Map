namespace Map.Components.Search
{
    partial class Search
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.filterJudet = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.filterFirma = new System.Windows.Forms.ComboBox();
            this.filterLocalitate = new System.Windows.Forms.ComboBox();
            this.filterTarla = new System.Windows.Forms.ComboBox();
            this.filterStatus = new System.Windows.Forms.ComboBox();
            this.filterData = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 10);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(797, 403);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(810, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Judet";
            // 
            // filterJudet
            // 
            this.filterJudet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterJudet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterJudet.Location = new System.Drawing.Point(847, 7);
            this.filterJudet.Margin = new System.Windows.Forms.Padding(2);
            this.filterJudet.Name = "filterJudet";
            this.filterJudet.Size = new System.Drawing.Size(139, 21);
            this.filterJudet.TabIndex = 2;
            this.filterJudet.SelectedIndexChanged += new System.EventHandler(this.filterJudet_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(811, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Firma";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(810, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Localitate";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(811, 86);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Tarla";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(810, 111);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Status dosar";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(810, 136);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Data contract";
            // 
            // filterFirma
            // 
            this.filterFirma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterFirma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterFirma.Location = new System.Drawing.Point(847, 33);
            this.filterFirma.Margin = new System.Windows.Forms.Padding(2);
            this.filterFirma.Name = "filterFirma";
            this.filterFirma.Size = new System.Drawing.Size(139, 21);
            this.filterFirma.TabIndex = 8;
            this.filterFirma.SelectedIndexChanged += new System.EventHandler(this.filterFirma_SelectedIndexChanged);
            // 
            // filterLocalitate
            // 
            this.filterLocalitate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterLocalitate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterLocalitate.Location = new System.Drawing.Point(867, 58);
            this.filterLocalitate.Margin = new System.Windows.Forms.Padding(2);
            this.filterLocalitate.Name = "filterLocalitate";
            this.filterLocalitate.Size = new System.Drawing.Size(119, 21);
            this.filterLocalitate.TabIndex = 9;
            this.filterLocalitate.SelectedIndexChanged += new System.EventHandler(this.filterLocalitate_SelectedIndexChanged);
            // 
            // filterTarla
            // 
            this.filterTarla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTarla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterTarla.Location = new System.Drawing.Point(847, 83);
            this.filterTarla.Margin = new System.Windows.Forms.Padding(2);
            this.filterTarla.Name = "filterTarla";
            this.filterTarla.Size = new System.Drawing.Size(139, 21);
            this.filterTarla.TabIndex = 10;
            this.filterTarla.SelectedIndexChanged += new System.EventHandler(this.filterTarla_SelectedIndexChanged);
            // 
            // filterStatus
            // 
            this.filterStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterStatus.Location = new System.Drawing.Point(880, 108);
            this.filterStatus.Margin = new System.Windows.Forms.Padding(2);
            this.filterStatus.Name = "filterStatus";
            this.filterStatus.Size = new System.Drawing.Size(106, 21);
            this.filterStatus.TabIndex = 11;
            this.filterStatus.SelectedIndexChanged += new System.EventHandler(this.filterStatus_SelectedIndexChanged);
            // 
            // filterData
            // 
            this.filterData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterData.Location = new System.Drawing.Point(886, 133);
            this.filterData.Margin = new System.Windows.Forms.Padding(2);
            this.filterData.Name = "filterData";
            this.filterData.Size = new System.Drawing.Size(100, 21);
            this.filterData.TabIndex = 12;
            this.filterData.SelectedIndexChanged += new System.EventHandler(this.filterData_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(811, 162);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 24);
            this.button1.TabIndex = 13;
            this.button1.Text = "Creaza raport";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 424);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.filterData);
            this.Controls.Add(this.filterStatus);
            this.Controls.Add(this.filterTarla);
            this.Controls.Add(this.filterLocalitate);
            this.Controls.Add(this.filterFirma);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filterJudet);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Search";
            this.Text = "Rapoarte";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox filterJudet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox filterFirma;
        private System.Windows.Forms.ComboBox filterLocalitate;
        private System.Windows.Forms.ComboBox filterTarla;
        private System.Windows.Forms.ComboBox filterStatus;
        private System.Windows.Forms.ComboBox filterData;
        private System.Windows.Forms.Button button1;
    }
}