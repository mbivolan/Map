
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
            this.label1 = new System.Windows.Forms.Label();
            this.pbFull = new Map.PanelDoubleBuffered();
            this.sbVert = new System.Windows.Forms.VScrollBar();
            this.sbHoriz = new System.Windows.Forms.HScrollBar();
            this.sbPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbPanel)).BeginInit();
            this.pbFull.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(11, 6);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(44, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cbZoom
            // 
            this.cbZoom.FormattingEnabled = true;
            this.cbZoom.Location = new System.Drawing.Point(193, 7);
            this.cbZoom.Margin = new System.Windows.Forms.Padding(2);
            this.cbZoom.Name = "cbZoom";
            this.cbZoom.Size = new System.Drawing.Size(48, 21);
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
            this.btnMode.Location = new System.Drawing.Point(158, 4);
            this.btnMode.Margin = new System.Windows.Forms.Padding(2);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(31, 24);
            this.btnMode.TabIndex = 16;
            this.btnMode.UseVisualStyleBackColor = true;
            this.btnMode.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnFitToScreen
            // 
            this.btnFitToScreen.Image = global::Map.Properties.Resources.btnFitToScreen;
            this.btnFitToScreen.Location = new System.Drawing.Point(126, 4);
            this.btnFitToScreen.Margin = new System.Windows.Forms.Padding(2);
            this.btnFitToScreen.Name = "btnFitToScreen";
            this.btnFitToScreen.Size = new System.Drawing.Size(28, 24);
            this.btnFitToScreen.TabIndex = 13;
            this.btnFitToScreen.UseVisualStyleBackColor = true;
            this.btnFitToScreen.Click += new System.EventHandler(this.btnFitToScreen_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Image = global::Map.Properties.Resources.btnZoomIn;
            this.btnZoomIn.Location = new System.Drawing.Point(59, 5);
            this.btnZoomIn.Margin = new System.Windows.Forms.Padding(2);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(28, 24);
            this.btnZoomIn.TabIndex = 12;
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Image = global::Map.Properties.Resources.btnZoomOut;
            this.btnZoomOut.Location = new System.Drawing.Point(91, 4);
            this.btnZoomOut.Margin = new System.Windows.Forms.Padding(2);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(29, 24);
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
            this.tbNavigation.Size = new System.Drawing.Size(100, 20);
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
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(949, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(163, 569);
            this.panel1.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(946, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Detalii";
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
            this.pbFull.Location = new System.Drawing.Point(3, 33);
            this.pbFull.Margin = new System.Windows.Forms.Padding(2);
            this.pbFull.Name = "pbFull";
            this.pbFull.Size = new System.Drawing.Size(931, 569);
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
            this.sbVert.Location = new System.Drawing.Point(1005, 0);
            this.sbVert.Name = "sbVert";
            this.sbVert.Size = new System.Drawing.Size(17, 720);
            this.sbVert.TabIndex = 0;
            this.sbVert.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SbVert_Scroll);
            // 
            // sbHoriz
            // 
            this.sbHoriz.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sbHoriz.Location = new System.Drawing.Point(0, 719);
            this.sbHoriz.Name = "sbHoriz";
            this.sbHoriz.Size = new System.Drawing.Size(1005, 17);
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
            this.sbPanel.Name = "sbPanel";
            this.sbPanel.Size = new System.Drawing.Size(931, 568);
            this.sbPanel.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 603);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.pbFull);
            this.Controls.Add(this.cbZoom);
            this.Controls.Add(this.btnMode);
            this.Controls.Add(this.btnFitToScreen);
            this.Controls.Add(this.btnZoomIn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Cerealcom";
            ((System.ComponentModel.ISupportInitialize)(this.pbPanel)).EndInit();
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
    }
}

