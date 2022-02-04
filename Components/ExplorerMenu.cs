using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Map.Components.Tools;

namespace Map
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.FlowLayoutPanel explorerMenuPanel;

        private List<Button> buttons = new List<Button>();

        private FileExplorer locationExplorer = new FileExplorer("D:\\Cerealcom");

        // TEST ONLY
        private List<string> locations = new List<string>();

        private void InitializeExplorerMenu()
        {
            this.explorerMenuPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.explorerMenuPanel.SuspendLayout();

            this.explorerMenuPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top 
                | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.explorerMenuPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.explorerMenuPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.explorerMenuPanel.Location = new System.Drawing.Point(4, 41);
            this.explorerMenuPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.explorerMenuPanel.Name = "explorerMenuPanel";
            this.explorerMenuPanel.Size = new System.Drawing.Size(1187, 700);
            this.explorerMenuPanel.TabIndex = 13;
            this.explorerMenuPanel.AutoScroll = true;
            this.explorerMenuPanel.WrapContents = false;
            this.explorerMenuPanel.FlowDirection = FlowDirection.TopDown;

            this.Controls.Add(this.explorerMenuPanel);

            this.explorerMenuPanel.ResumeLayout(false);
            this.explorerMenuPanel.PerformLayout();

            CreateLocationButtons();
        }

        private void locationButton_Click(object sender, EventArgs e)
        {
            Button senderBtn = (Button)sender;

            locationExplorer.UpdatePath((senderBtn.Tag as List<string>).First());

            if (locationExplorer.IsDocumentDir())
            {
                RemoveLocationButtons();

                ShowMap();

                this.ImagePath = locationExplorer.GetConfigFile();
                UpdatePanels(true);
                LoadPointsData();
            }
            else
            {
                RemoveLocationButtons();
                CreateLocationButtons();
            }


        }

        private void RemoveLocationButtons()
        {
            List<Control> foundObjects = new List<Control>();

            foreach (Control newContr in this.explorerMenuPanel.Controls)
            {
                // TODO: Pun them in enums
                if ((newContr.Tag is List<string>) && ((newContr.Tag as List<string>).Contains("locationButton")))
                {
                    foundObjects.Add(newContr);
                }
            }

            foreach(Control newContr in foundObjects)
            {
                explorerMenuPanel.Controls.OfType<Button>().ToList().ForEach(btn => btn.Dispose());
                this.explorerMenuPanel.Controls.Remove(newContr);
            }

        }

        private void CreateLocationButtons()
        {
            UpdateButtonsFromPath();

            foreach (string placeName in locations)
            {
                Button newButton = new System.Windows.Forms.Button();

                // TODO: Beutify buttons
                newButton.Margin = new System.Windows.Forms.Padding(5);
                newButton.Size = new System.Drawing.Size(300, 50);

                newButton.Name = "btnLocation" + placeName;
                newButton.Text = placeName;
                newButton.Tag = new List<string>();
                // TODO: Pun them in enums
                (newButton.Tag as List<string>).Add(placeName);
                (newButton.Tag as List<string>).Add("locationButton");
                newButton.Click += new System.EventHandler(this.locationButton_Click);

                buttons.Add(newButton);
                this.explorerMenuPanel.Controls.Add(newButton);
            }

            CreateBackButton();
        }
        private void CreateBackButton()
        {
            Button btnBack = new System.Windows.Forms.Button();
            btnBack.Margin = new System.Windows.Forms.Padding(5);
            btnBack.Size = new System.Drawing.Size(100, 50);
            btnBack.Name = "btnBack";
            btnBack.Text = "Back";
            btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Right)));
            buttons.Add(btnBack);
            btnBack.Click += new System.EventHandler(this.getBack_Click);
            this.explorerMenuPanel.Controls.Add(btnBack);
        }

        private void getBack_Click(object sender, EventArgs e)
        {
            Button senderBtn = (Button)sender;

            senderBtn.Dispose();
            this.explorerMenuPanel.Controls.Remove(senderBtn);


            locationExplorer.goToBase();

            RemoveLocationButtons();
            CreateLocationButtons();
        }


        private void UpdateButtonsFromPath()
        {
            this.locations = locationExplorer.GetDirNames();
        }

        // TODO: These methods need to be moved in Designer
        private void ShowExplorerMenu()
        {
            this.locationExplorer.goToBase();

            RemoveLocationButtons();
            CreateLocationButtons();

            this.pbFull.Hide();
            this.explorerMenuPanel.Show();

            this.searchTextBox.Enabled = false;
            this.button2.Enabled = false;
        }

        private void ShowMap()
        {
            this.pbFull.Show();
            this.explorerMenuPanel.Hide();

            this.searchTextBox.Enabled = true;
            this.button2.Enabled = true;
        }


    }
}
