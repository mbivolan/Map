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
            this.explorerMenuPanel.Location = new System.Drawing.Point(3, 33);
            this.explorerMenuPanel.Margin = new System.Windows.Forms.Padding(2);
            this.explorerMenuPanel.Name = "explorerMenuPanel";
            this.explorerMenuPanel.Size = new System.Drawing.Size(891, 569);
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
                var list = Directory.GetFiles(locationExplorer.GetExplorerPath(), "*.png");
                if (list.Length > 0)
                {
                    Console.WriteLine(list.Length);
                    RemoveLocationButtons();
                    CreateImageButton();
                    //CreateBackButton();
                }
                //RemoveLocationButtons();
                //ShowMap();

                //this.ImagePath = locationExplorer.GetConfigFile();
                //UpdatePanels(true);
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

            locationExplorer.goToBase();

            if (locationExplorer.IsDocumentDir())
            {
                RemoveLocationButtons();
                ShowMap();

                this.ImagePath = locationExplorer.GetConfigFile();
                UpdatePanels(true);
            }
            else
            {
                RemoveLocationButtons();
                CreateLocationButtons();
            }
        }

        private void CreateImageButton()
        {
            var list = Directory.GetFiles(locationExplorer.GetExplorerPath(), "*.png");
            foreach (string imagPath in list)
            {
                Button btnImg = new System.Windows.Forms.Button();
                btnImg.Margin = new System.Windows.Forms.Padding(5);
                btnImg.Size = new System.Drawing.Size(300, 50);
                btnImg.Name = "btnImg";
                btnImg.Text = Path.GetFileNameWithoutExtension(imagPath);
                btnImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top
                    | System.Windows.Forms.AnchorStyles.Right)));
                buttons.Add(btnImg);
                btnImg.Tag = imagPath;
                btnImg.Click += new System.EventHandler(this.imageButton_Click);
                this.explorerMenuPanel.Controls.Add(btnImg);
                
                this.ImagePath = locationExplorer.MapPath(imagPath);

                Console.WriteLine(imagPath);
            }
        }

        private void imageButton_Click(object sender, EventArgs e)
        {
            RemoveLocationButtons();
            ShowMap();

            this.ImagePath = locationExplorer.GetConfigFile();
            Console.WriteLine(locationExplorer);
            UpdatePanels(true);
        }

        private void UpdateButtonsFromPath()
        {
            this.locations = locationExplorer.GetDirNames();
        }

        // TODO: These methods need to be moved in Designer
        private void ShowExplorerMenu()
        {
            this.pbFull.Hide();
            this.explorerMenuPanel.Show();
        }

        private void ShowMap()
        {
            this.pbFull.Show();
            this.explorerMenuPanel.Hide();
        }
    }
}
