using System.Windows.Forms;

namespace Map
{
    partial class Form1
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

        private void InitializeWindow()
        {
            InitializeExplorerMenu();
            InitializeComponent();

            this.searchTextBox.Enabled = false;
            this.button2.Enabled = false;
            
            ShowExplorerMenu();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOpen = new System.Windows.Forms.Button();
            this.cbZoom = new System.Windows.Forms.ComboBox();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnMode = new System.Windows.Forms.Button();
            this.btnFitToScreen = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.pbPanel = new System.Windows.Forms.PictureBox();
            this.tbNavigation = new System.Windows.Forms.TextBox();
            this.lblNavigation = new System.Windows.Forms.Label();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.additionalDocumentText = new System.Windows.Forms.LinkLabel();
            this.linkDocumentText = new System.Windows.Forms.LinkLabel();
            this.statusDosarText = new System.Windows.Forms.TextBox();
            this.suprafataText = new System.Windows.Forms.TextBox();
            this.parcelaText = new System.Windows.Forms.TextBox();
            this.tarlaText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numePropText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveMetaBtn = new System.Windows.Forms.Button();
            this.addDocument = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.additionalDocument = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.showMenuBtn = new System.Windows.Forms.Button();
            this.addDocumentMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.searchMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.pbFull = new Map.PanelDoubleBuffered();
            this.sbVert = new System.Windows.Forms.VScrollBar();
            this.sbHoriz = new System.Windows.Forms.HScrollBar();
            this.sbPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbPanel)).BeginInit();
            this.panel1.SuspendLayout();
            this.addDocumentMenu.SuspendLayout();
            this.pbFull.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::Map.Properties.Resources.btnOpen;
            this.btnOpen.Location = new System.Drawing.Point(4, 6);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(35, 30);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cbZoom
            // 
            this.cbZoom.FormattingEnabled = true;
            this.cbZoom.Location = new System.Drawing.Point(1046, 9);
            this.cbZoom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbZoom.Name = "cbZoom";
            this.cbZoom.Size = new System.Drawing.Size(72, 24);
            this.cbZoom.TabIndex = 14;
            // 
            // panelMenu
            // 
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(200, 100);
            this.panelMenu.TabIndex = 0;
            // 
            // btnMode
            // 
            this.btnMode.Image = global::Map.Properties.Resources.btnSelect;
            this.btnMode.Location = new System.Drawing.Point(868, 4);
            this.btnMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(41, 30);
            this.btnMode.TabIndex = 16;
            this.btnMode.UseVisualStyleBackColor = true;
            this.btnMode.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnFitToScreen
            // 
            this.btnFitToScreen.Image = global::Map.Properties.Resources.btnFitToScreen;
            this.btnFitToScreen.Location = new System.Drawing.Point(1003, 6);
            this.btnFitToScreen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFitToScreen.Name = "btnFitToScreen";
            this.btnFitToScreen.Size = new System.Drawing.Size(37, 30);
            this.btnFitToScreen.TabIndex = 13;
            this.btnFitToScreen.UseVisualStyleBackColor = true;
            this.btnFitToScreen.Click += new System.EventHandler(this.btnFitToScreen_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Image = global::Map.Properties.Resources.btnZoomIn;
            this.btnZoomIn.Location = new System.Drawing.Point(915, 4);
            this.btnZoomIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(37, 30);
            this.btnZoomIn.TabIndex = 12;
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Image = global::Map.Properties.Resources.btnZoomOut;
            this.btnZoomOut.Location = new System.Drawing.Point(958, 6);
            this.btnZoomOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(39, 30);
            this.btnZoomOut.TabIndex = 11;
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // pbPanel
            // 
            this.pbPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPanel.Location = new System.Drawing.Point(302, 36);
            this.pbPanel.Name = "pbPanel";
            this.pbPanel.Size = new System.Drawing.Size(148, 117);
            this.pbPanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPanel.TabIndex = 10;
            this.pbPanel.TabStop = false;
            this.pbPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbPanel_MouseDown);
            this.pbPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbPanel_MouseMove);
            this.pbPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbPanel_MouseUp);
            // 
            // tbNavigation
            // 
            this.tbNavigation.Location = new System.Drawing.Point(0, 0);
            this.tbNavigation.Name = "tbNavigation";
            this.tbNavigation.Size = new System.Drawing.Size(100, 22);
            this.tbNavigation.TabIndex = 0;
            // 
            // lblNavigation
            // 
            this.lblNavigation.Location = new System.Drawing.Point(0, 0);
            this.lblNavigation.Name = "lblNavigation";
            this.lblNavigation.Size = new System.Drawing.Size(100, 23);
            this.lblNavigation.TabIndex = 0;
            // 
            // panelNavigation
            // 
            this.panelNavigation.Location = new System.Drawing.Point(0, 0);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(200, 100);
            this.panelNavigation.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.additionalDocumentText);
            this.panel1.Controls.Add(this.linkDocumentText);
            this.panel1.Controls.Add(this.statusDosarText);
            this.panel1.Controls.Add(this.suprafataText);
            this.panel1.Controls.Add(this.parcelaText);
            this.panel1.Controls.Add(this.tarlaText);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numePropText);
            this.panel1.Location = new System.Drawing.Point(1198, 41);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(297, 132);
            this.panel1.TabIndex = 17;
            // 
            // additionalDocumentText
            // 
            this.additionalDocumentText.AutoSize = true;
            this.additionalDocumentText.Location = new System.Drawing.Point(7, 258);
            this.additionalDocumentText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.additionalDocumentText.Name = "additionalDocumentText";
            this.additionalDocumentText.Size = new System.Drawing.Size(102, 17);
            this.additionalDocumentText.TabIndex = 13;
            this.additionalDocumentText.TabStop = true;
            this.additionalDocumentText.Text = "Acte Aditionale";
            this.additionalDocumentText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.additionalDocument_LinkClicked);
            // 
            // linkDocumentText
            // 
            this.linkDocumentText.AutoSize = true;
            this.linkDocumentText.Location = new System.Drawing.Point(7, 230);
            this.linkDocumentText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkDocumentText.Name = "linkDocumentText";
            this.linkDocumentText.Size = new System.Drawing.Size(36, 17);
            this.linkDocumentText.TabIndex = 12;
            this.linkDocumentText.TabStop = true;
            this.linkDocumentText.Text = "Acte";
            this.linkDocumentText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDocument_LinkClicked);
            // 
            // statusDosarText
            // 
            this.statusDosarText.Location = new System.Drawing.Point(112, 105);
            this.statusDosarText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.statusDosarText.Name = "statusDosarText";
            this.statusDosarText.Size = new System.Drawing.Size(180, 22);
            this.statusDosarText.TabIndex = 11;
            this.statusDosarText.TextChanged += new System.EventHandler(this.statusDosarText_TextChanged);
            // 
            // suprafataText
            // 
            this.suprafataText.Location = new System.Drawing.Point(112, 80);
            this.suprafataText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.suprafataText.Name = "suprafataText";
            this.suprafataText.Size = new System.Drawing.Size(180, 22);
            this.suprafataText.TabIndex = 10;
            this.suprafataText.TextChanged += new System.EventHandler(this.suprafataText_TextChanged);
            // 
            // parcelaText
            // 
            this.parcelaText.Location = new System.Drawing.Point(112, 54);
            this.parcelaText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.parcelaText.Name = "parcelaText";
            this.parcelaText.Size = new System.Drawing.Size(180, 22);
            this.parcelaText.TabIndex = 9;
            this.parcelaText.TextChanged += new System.EventHandler(this.parcelaText_TextChanged);
            // 
            // tarlaText
            // 
            this.tarlaText.Location = new System.Drawing.Point(112, 28);
            this.tarlaText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tarlaText.Name = "tarlaText";
            this.tarlaText.Size = new System.Drawing.Size(180, 22);
            this.tarlaText.TabIndex = 8;
            this.tarlaText.TextChanged += new System.EventHandler(this.tarlaText_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Status dosar";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Suprafata";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Parcela";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tarla";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nume";
            // 
            // numePropText
            // 
            this.numePropText.Location = new System.Drawing.Point(112, 2);
            this.numePropText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numePropText.Name = "numePropText";
            this.numePropText.Size = new System.Drawing.Size(180, 22);
            this.numePropText.TabIndex = 0;
            this.numePropText.TextChanged += new System.EventHandler(this.numePropText_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1140, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Detalii";
            // 
            // saveMetaBtn
            // 
            this.saveMetaBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveMetaBtn.Location = new System.Drawing.Point(1202, 709);
            this.saveMetaBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveMetaBtn.Name = "saveMetaBtn";
            this.saveMetaBtn.Size = new System.Drawing.Size(117, 30);
            this.saveMetaBtn.TabIndex = 19;
            this.saveMetaBtn.Text = "Save";
            this.saveMetaBtn.UseVisualStyleBackColor = true;
            this.saveMetaBtn.Click += new System.EventHandler(this.saveMetaBtn_Click);
            // 
            // addDocument
            // 
            this.addDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addDocument.Location = new System.Drawing.Point(1326, 711);
            this.addDocument.Margin = new System.Windows.Forms.Padding(4);
            this.addDocument.Name = "addDocument";
            this.addDocument.Size = new System.Drawing.Size(169, 28);
            this.addDocument.TabIndex = 20;
            this.addDocument.Text = "Incarca document";
            this.addDocument.UseVisualStyleBackColor = true;
            this.addDocument.Click += new System.EventHandler(this.btnAddDocument_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1326, 677);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 28);
            this.button1.TabIndex = 13;
            this.button1.Text = "Modificare status";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // additionalDocument
            // 
            this.additionalDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.additionalDocument.Location = new System.Drawing.Point(1326, 648);
            this.additionalDocument.Margin = new System.Windows.Forms.Padding(4);
            this.additionalDocument.Name = "additionalDocument";
            this.additionalDocument.Size = new System.Drawing.Size(169, 28);
            this.additionalDocument.TabIndex = 21;
            this.additionalDocument.Text = "Adauga Act Aditional";
            this.additionalDocument.UseVisualStyleBackColor = true;
            this.additionalDocument.Click += new System.EventHandler(this.additionalDocument_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Enabled = false;
            this.searchTextBox.Location = new System.Drawing.Point(1299, 405);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(196, 22);
            this.searchTextBox.TabIndex = 22;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(1198, 400);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 33);
            this.button2.TabIndex = 23;
            this.button2.Text = "Cauta";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // showMenuBtn
            // 
            this.showMenuBtn.Location = new System.Drawing.Point(45, 6);
            this.showMenuBtn.Name = "showMenuBtn";
            this.showMenuBtn.Size = new System.Drawing.Size(75, 29);
            this.showMenuBtn.TabIndex = 0;
            this.showMenuBtn.Text = "Meniu";
            this.showMenuBtn.UseVisualStyleBackColor = true;
            this.showMenuBtn.Click += new System.EventHandler(this.showMenuBtn_Click);
            // 
            // addDocumentMenu
            // 
            this.addDocumentMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addDocumentMenu.BackColor = System.Drawing.SystemColors.ControlLight;
            this.addDocumentMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addDocumentMenu.Controls.Add(this.label3);
            this.addDocumentMenu.Location = new System.Drawing.Point(1198, 180);
            this.addDocumentMenu.Name = "addDocumentMenu";
            this.addDocumentMenu.Size = new System.Drawing.Size(297, 201);
            this.addDocumentMenu.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Documente:";
            // 
            // searchMenu
            // 
            this.searchMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchMenu.AutoScroll = true;
            this.searchMenu.BackColor = System.Drawing.SystemColors.ControlLight;
            this.searchMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchMenu.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.searchMenu.Location = new System.Drawing.Point(1198, 440);
            this.searchMenu.Name = "searchMenu";
            this.searchMenu.Size = new System.Drawing.Size(297, 201);
            this.searchMenu.TabIndex = 25;
            this.searchMenu.WrapContents = false;
            // 
            // pbFull
            // 
            this.pbFull.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbFull.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbFull.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFull.Controls.Add(this.sbVert);
            this.pbFull.Controls.Add(this.sbHoriz);
            this.pbFull.Controls.Add(this.sbPanel);
            this.pbFull.Location = new System.Drawing.Point(4, 41);
            this.pbFull.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbFull.Name = "pbFull";
            this.pbFull.Size = new System.Drawing.Size(1187, 700);
            this.pbFull.TabIndex = 13;
            this.pbFull.Click += new System.EventHandler(this.pbFull_Click);
            this.pbFull.Paint += new System.Windows.Forms.PaintEventHandler(this.pbFull_Paint);
            this.pbFull.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbFull_MouseDoubleClick);
            this.pbFull.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbFull_MouseDown);
            this.pbFull.MouseHover += new System.EventHandler(this.pbFull_MouseHover);
            this.pbFull.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbFull_MouseMove);
            this.pbFull.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbFull_MouseUp);
            // 
            // sbVert
            // 
            this.sbVert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sbVert.Location = new System.Drawing.Point(966, 0);
            this.sbVert.Name = "sbVert";
            this.sbVert.Size = new System.Drawing.Size(21, 842);
            this.sbVert.TabIndex = 0;
            this.sbVert.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SbVert_Scroll);
            // 
            // sbHoriz
            // 
            this.sbHoriz.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sbHoriz.Location = new System.Drawing.Point(0, 840);
            this.sbHoriz.Name = "sbHoriz";
            this.sbHoriz.Size = new System.Drawing.Size(966, 21);
            this.sbHoriz.TabIndex = 1;
            this.sbHoriz.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SbHoriz_Scroll);
            // 
            // sbPanel
            // 
            this.sbPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sbPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sbPanel.Location = new System.Drawing.Point(-1, 0);
            this.sbPanel.Margin = new System.Windows.Forms.Padding(4);
            this.sbPanel.Name = "sbPanel";
            this.sbPanel.Size = new System.Drawing.Size(841, 608);
            this.sbPanel.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 742);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.searchMenu);
            this.Controls.Add(this.addDocumentMenu);
            this.Controls.Add(this.showMenuBtn);
            this.Controls.Add(this.additionalDocument);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addDocument);
            this.Controls.Add(this.saveMetaBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.pbFull);
            this.Controls.Add(this.cbZoom);
            this.Controls.Add(this.btnMode);
            this.Controls.Add(this.btnFitToScreen);
            this.Controls.Add(this.btnZoomIn);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(1499, 742);
            this.Name = "Form1";
            this.Text = "Cerealcom";
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MouseWheelZoom);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbPanel)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.addDocumentMenu.ResumeLayout(false);
            this.addDocumentMenu.PerformLayout();
            this.pbFull.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private PanelDoubleBuffered pbFull;
        private System.Windows.Forms.ComboBox cbZoom;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.PictureBox pbPanel;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnFitToScreen;
        private System.Windows.Forms.Button btnMode;
        private System.Windows.Forms.Panel sbPanel;
        private System.Windows.Forms.HScrollBar sbHoriz;
        private System.Windows.Forms.VScrollBar sbVert;
        private System.Windows.Forms.TextBox tbNavigation;
        private System.Windows.Forms.Label lblNavigation;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox numePropText;
        private System.Windows.Forms.Button saveMetaBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox statusDosarText;
        private System.Windows.Forms.TextBox suprafataText;
        private System.Windows.Forms.TextBox parcelaText;
        private System.Windows.Forms.TextBox tarlaText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkDocumentText;
        private System.Windows.Forms.Button addDocument;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button additionalDocument;
        private System.Windows.Forms.LinkLabel additionalDocumentText;

        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button showMenuBtn;
        private System.Windows.Forms.FlowLayoutPanel addDocumentMenu;
        private System.Windows.Forms.FlowLayoutPanel searchMenu;
        private System.Windows.Forms.Label label3;
    }
}

