using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;

namespace Map
{
    public struct ColumnData
    {
        public string Nume { get; set; }
        public string Tarla { get; set; }
        public string Parcela { get; set; }
        public string Suprafata { get; set; }
    }
    public enum KpZoom
    {
        ZoomIn,
        ZoomOut
    }

    public partial class Form1 : Form
    {
        private DrawObject drawing;
        private DrawEngine drawEngine;
        private List<Map.CheckPoint> checkPoints;
        private List<Map.CheckPoint> foundPoints;
        private Map.CheckPoint lastPoint;
        private bool selectMode = false;
        private bool panelDragging = false;
        private bool isScrolling = false;
        private bool scrollbars = false;
        private bool enableNewPoint = true;
        private string imagePath;
        private string configPath;
        private List<ColumnData> DataItems;

        private bool deleteDocs
        {
            get
            {
                return _deleteDocs;
            }
            set
            {
                _deleteDocs = value;
                if (value)
                {
                    this.removeDocBtn.Text = "Anuleaza";
                } else
                {
                    this.removeDocBtn.Text = "Sterge Document";
                }
                this.Refresh();
            }
        }

        private bool _deleteDocs = false;

        private int selectedPointNumber = -1;
        private int lastSelectedPoint = -1;

        private bool shiftSelecting = false;
        private Point ptSelectionStart = new Point();
        private Point ptSelectionEnd = new Point();

        private Cursor grabCursor = null;
        private Cursor dragCursor = null;


        public Form1()
        {
            drawEngine = new DrawEngine();
            drawing = new DrawObject(this);
            checkPoints = new List<CheckPoint>();
            foundPoints = new List<CheckPoint>();
            DataItems = new List<ColumnData>();
            InitializeWindow();
            InitControl();
        }

        public Size OriginalSize
        {
            get { return drawing.OriginalSize; }
        }

        public Size CurrentSize
        {
            get { return drawing.CurrentSize; }
        }

        public int PanelWidth
        {
            get
            {
                if (pbFull != null)
                {
                    return pbFull.Width;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int PanelHeight
        {
            get
            {
                if (pbFull != null)
                {
                    return pbFull.Height;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Bitmap Image
        {
            get
            {
                return drawing.Image;
            }
            set
            {
                drawing.Image = value;

                UpdatePanels(true);

                // scrollbars
                DisplayScrollbars();
                SetScrollbarValues();
            }
        }

        public string ImagePath
        {
            set
            {
                drawing.ImagePath = value;
                this.imagePath = value;

                var imageDir = Path.GetDirectoryName(imagePath);
                this.configPath = Path.Combine(imageDir, "config.json");

                UpdatePanels(true);

                // scrollbars
                DisplayScrollbars();
                SetScrollbarValues();
            }

            get { return this.imagePath; }
        }

        public delegate void ImageViewerZoomEventHandler(object sender, ImageViewerZoomEventArgs e);
        public event ImageViewerZoomEventHandler AfterZoom;

        protected virtual void OnZoom(ImageViewerZoomEventArgs e)
        {
            if (AfterZoom != null)
            {
                AfterZoom(this, e);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.tif;*.tiff;*.wmf;*.emf|JPEG Files (*.jpg)|*.jpg;*.jpeg|GIF Files (*.gif)|*.gif|BMP Files (*.bmp)|*.bmp|PNG Files (*.png)|*.png|TIF files (*.tif;*.tiff)|*.tif;*.tiff|EMF/WMF Files (*.wmf;*.emf)|*.wmf;*.emf|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.ImagePath = openFileDialog.FileName;
            }

            UpdatePanels(true);

            LoadPointsData();
        }

        private void cleanDocumentView()
        {
            int i = 0; 
            while(i < this.addDocumentMenu.Controls.Count)
            {
                if (this.addDocumentMenu.Controls[i].Name == "linkDocumentText")
                {
                    this.addDocumentMenu.Controls.RemoveAt(i);
                } else
                {
                    i++;
                }
            }

            this.addDocumentMenu.PerformLayout();
        }

        private void refreshDocumentView(CheckPoint ck)
        {
            cleanDocumentView();
            foreach (string fileName in ck.AdditionalDocument)
            {
                System.Windows.Forms.LinkLabel linkToPoint = new System.Windows.Forms.LinkLabel();

                linkToPoint.AutoSize = true;
                linkToPoint.Location = new System.Drawing.Point(7, 230);
                linkToPoint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
                linkToPoint.Name = "linkDocumentText";
                linkToPoint.Size = new System.Drawing.Size(36, 17);
                linkToPoint.TabIndex = 12;
                linkToPoint.TabStop = true;
                linkToPoint.Text = Path.GetFileName(fileName);
                linkToPoint.Tag = fileName;
                linkToPoint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDoc_Click);

                this.addDocumentMenu.Controls.Add(linkToPoint);
            }
        }

        private void removeDocBtn_Click(object sender, EventArgs e)
        {
            if (this._deleteDocs)
            {
                refreshDocumentView(this.checkPoints[lastSelectedPoint]);
                this.deleteDocs = false;
            } else
            {
                this.deleteDocs = true;
            }
        }

        private void linkDoc_Click(object sender, EventArgs e)
        {
            if (this._deleteDocs)
            {
                this.addDocumentMenu.Controls.Remove((LinkLabel)sender);
            } else
            {
                System.Diagnostics.Process.Start((string)((LinkLabel)sender).Tag);
            }
        }

        /// ///////////////////////////////////////////////////////
        // CSV reader and autofill

        private void cleanTarlaView()
        {
            int i = 0;
            while (i < this.panelTarla.Controls.Count)
            {
                if (this.panelTarla.Controls[i].Name == "numarTarla")
                {
                    this.panelTarla.Controls.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }


        /*
        private void populateFields(string docName)
        {
            bool checkForMultiple = false;
            List<string> listaTarlale = new List<string>();
            string name = docName.Replace(".pdf", "");
            char ch = '-';
            int count;
            count = name.Count(f => (f == ch));
            if (count > 1)
            {
                string[] subs = name.Split('-');
                Console.WriteLine("/////////////////////");
                Console.WriteLine(subs.Length);

                if (subs.Length > 3)
                {
                    checkForMultiple = true;
                }

                foreach (string sub in subs)
                {
                    Console.WriteLine($"Substring: {sub}");
                    bool letter = sub.All(c => Char.IsLetter(c) || c == ' ');
                    bool digit = sub.All(Char.IsLetterOrDigit);
                    if (letter == true && sub.Length > 1 || sub.Contains('+'))
                    {
                        this.numePropText.Text = sub; // Completeaza numele
                    }
                    else if(sub.Contains('t')) // Completeaza tarla si parcela
                    {
                        var matchTarla = Regex.Match(sub, @"t(.+?)p").Groups[1].Value; // Atribuie numarul tarlalei
                        var matchParcela = Regex.Match(sub, @"p(.*)").Groups[1].Value; // Atribuie  numarul parcelei
                        listaTarlale.Add("T" + matchTarla + " P" + matchParcela);
                        
                        this.tarlaText.Text = this.tarlaText.Text + ("//" + matchTarla);
                        this.parcelaText.Text = this.parcelaText.Text + ("//" + matchParcela);
                        //Console.WriteLine(1110);
                    }
                    else if(digit == true && sub.Contains('c')) // Completeaza status dosar
                    {
                        this.statusDosarText.Text = sub;
                    }
                    else if (sub.Contains('s')) // Completeaza status dosar
                    {
                        this.statusDosarText.Text = sub;
                    }
                }
            }
            else
            {
                this.parcelaText.Text = "false";
                //Console.WriteLine(count);
            }
            cleanTarlaView();
            

            var path = @"D:\test.csv";
            using (TextFieldParser csvReader = new TextFieldParser(path))
            {
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvReader.ReadLine();

                List<ColumnData> DataItems = new List<ColumnData>();
                while (!csvReader.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvReader.ReadFields();

                    var cd = new ColumnData();
                    cd.Nume = fields[2];
                    cd.Tarla = fields[6];
                    cd.Parcela = fields[7];
                    cd.Suprafata = fields[4];
                    DataItems.Add(cd);

                }
                foreach (ColumnData data in DataItems)
                {
                    string tarla = this.tarlaText.Text;
                    string parcela = this.parcelaText.Text;
                    tarla = tarla.Replace("/", "");
                    parcela = parcela.Replace("/", "");
                    //Console.WriteLine(tarla);
                    //Console.WriteLine(parcela);


                    List<Button> buttons = new List<Button>();
                    
                    if (checkForMultiple == true)
                    {
                        this.panel1.Hide();
                        this.panelTarla.Show();
                        foreach (string item in listaTarlale)
                        {
                            
                            //Create buttons
                            Button tarlaButton = new System.Windows.Forms.Button();
                            // TODO: Beutify buttons
                            tarlaButton.Margin = new System.Windows.Forms.Padding(3);
                            tarlaButton.Size = new System.Drawing.Size(139, 25);
                            tarlaButton.Name = "numarTarla";
                            tarlaButton.Text = item;
                            tarlaButton.Tag = new List<string>();
                            // TODO: Pun them in enums
                            (tarlaButton.Tag as List<string>).Add(item);
                            (tarlaButton.Tag as List<string>).Add("numarTarla" + item);
                            tarlaButton.Click += new System.EventHandler(this.chooseButton_Click);

                            buttons.Add(tarlaButton);
                            this.panelTarla.Controls.Add(tarlaButton);
                            Console.WriteLine(item); //
                            Console.WriteLine("/////////////\\\\\\\\\\\\"); //

                        }

                    }

                    if (data.Nume.ToLower() == this.numePropText.Text && data.Tarla == tarla && data.Parcela == parcela)
                    {
                        Console.WriteLine("Found person"); //
                        Console.WriteLine("Nume: " + data.Nume); //
                        this.suprafataText.Text = data.Suprafata;
                        Console.WriteLine("Suprafata: " + data.Suprafata); //
                        break;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
        */

        private void populateFields(string docName)
        {
            bool checkForMultiple = false;
            List<string> listaTarlale = new List<string>();
            string name = docName.Replace(".pdf", "");
            char ch = '-';
            int count = name.Count(f => (f == ch));
            if (count > 1)
            {
                string[] subs = name.Split('-');
                Console.WriteLine("/////////////////////");
                Console.WriteLine(subs.Length);

                if (subs.Length > 3)
                {
                    checkForMultiple = true;
                }

                foreach (string sub in subs)
                {
                    Console.WriteLine($"Substring: {sub}");
                    bool letter = sub.All(c => Char.IsLetter(c) || c == ' ');
                    bool digit = sub.All(Char.IsLetterOrDigit);
                    if (letter == true && sub.Length > 2 && sub != "cesiune" || sub.Contains('+'))
                    {
                        this.numePropText.Text = sub; // Completeaza numele
                    }
                    else if (sub.Contains('t')) // Completeaza tarla si parcela
                    {
                        var matchTarla = Regex.Match(sub, @"t(.+?)p").Groups[1].Value; // Atribuie numarul tarlalei
                        var matchParcela = Regex.Match(sub, @"p(.*)").Groups[1].Value; // Atribuie  numarul parcelei
                        listaTarlale.Add("T" + matchTarla + " P" + matchParcela);

                        this.tarlaText.Text = this.tarlaText.Text + ("//" + matchTarla);
                        this.parcelaText.Text = this.parcelaText.Text + ("//" + matchParcela);
                    }
                    else if (digit == true && sub.Contains('c')) // Completeaza status dosar
                    {
                        this.statusDosarText.Text = sub;
                    }
                    else if (sub.Contains('s') || sub.Contains("sc") || sub.Contains("cesiune")) // Completeaza status dosar
                    {
                        this.statusDosarText.Text = sub;
                    }
                }
            }
            else
            {
                this.parcelaText.Text = "false";
                return;
            }
            cleanTarlaView();

            List<Button> buttons = new List<Button>();

            this.panel1.Hide();
            this.panelTarla.Show();
            foreach (string item in listaTarlale)
            {

                //Create buttons
                Button tarlaButton = new System.Windows.Forms.Button();
                // TODO: Beutify buttons
                tarlaButton.Margin = new System.Windows.Forms.Padding(3);
                tarlaButton.Size = new System.Drawing.Size(139, 25);
                tarlaButton.Name = "numarTarla";
                tarlaButton.Text = item;
                tarlaButton.Tag = new List<string>();
                // TODO: Pun them in enums
                (tarlaButton.Tag as List<string>).Add(item);
                (tarlaButton.Tag as List<string>).Add("numarTarla" + item);
                tarlaButton.Click += new System.EventHandler(this.chooseButton_Click);

                buttons.Add(tarlaButton);
                this.panelTarla.Controls.Add(tarlaButton);
                Console.WriteLine(item); //
                Console.WriteLine("/////////////\\\\\\\\\\\\"); //

            }
        }

        private void chooseButton_Click(object sender, EventArgs e)
        {
            string s = (sender as Button).Text;
            s = s.Replace(" ", "");
            var matchTarla = Regex.Match(s, @"T(.+?)P").Groups[1].Value; // Atribuie numarul tarlalei
            var matchParcela = Regex.Match(s, @"P(.*)").Groups[1].Value; // Atribuie  numarul parcelei

            char c = '+';
            int cnt = this.numePropText.Text.Count(f => (f == c));
            Console.Write(cnt);
            Console.Write("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");




            var path = @"D:\test.csv";
            using (TextFieldParser csvReader = new TextFieldParser(path))
            {
                csvReader.CommentTokens = new string[] { "#" };
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvReader.ReadLine();

                this.DataItems.Clear();
                while (!csvReader.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvReader.ReadFields();

                    var cd = new ColumnData();
                    cd.Nume = fields[2];
                    cd.Tarla = fields[6];
                    cd.Parcela = fields[7];
                    cd.Suprafata = fields[4];
                    DataItems.Add(cd);

                }

                if (cnt > 0)
                {
                    string[] subs = this.numePropText.Text.Split('+');
                    foreach (string nume in subs)
                    {
                        foreach (ColumnData data in DataItems)
                        {
                            if (data.Nume.ToLower() == nume && data.Tarla == matchTarla && data.Parcela == matchParcela)
                            {
                                this.suprafataText.Text = data.Suprafata;
                                break;
                            }
                        }
                    }
                    Console.WriteLine("is ok");
                }
                else{
                    foreach (ColumnData data in DataItems)
                    {
                        if (data.Nume.ToLower() == this.numePropText.Text && data.Tarla == matchTarla && data.Parcela == matchParcela)
                        {
                            this.suprafataText.Text = data.Suprafata;
                            break;
                        }
                    }
                }

            }
            Console.WriteLine(s);
            this.panel1.Show();
            this.panelTarla.Hide();
            cleanTarlaView();
        }

        /// ///////////////////////////////////////////////////////

        private void btnAddDocument_Click(object sender, EventArgs e)
        {
            if (lastSelectedPoint == -1)
            {
                return;
            }

            CheckPoint ck = this.checkPoints[lastSelectedPoint];
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fisiere PDF|*.pdf|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                ck.AdditionalDocument.Add(openFileDialog.FileName);
                populateFields(Path.GetFileName(openFileDialog.FileName));
                refreshDocumentView(ck);
            }

        }

        private void additionalDocument_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Fisiere PDF|*.pdf|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                additionalDocumentText.Text = Path.GetFileName(openFileDialog.FileName);
                additionalDocumentText.Tag = openFileDialog.FileName;
            }

            UpdatePanels(true);
        }

        private void SbVert_Scroll(object sender, ScrollEventArgs e)
        {
            if (!isScrolling)
            {
                double perPercent = (double)this.CurrentSize.Height / 101.0;

                double value = e.NewValue * perPercent;

                this.drawing.SetPositionY(Convert.ToInt32(Math.Round(value, 0)));

                this.drawing.AvoidOutOfScreen();

                pbFull.Invalidate();

                UpdatePanels(true);
            }
        }

        private void SbHoriz_Scroll(object sender, ScrollEventArgs e)
        {
            if (!isScrolling)
            {
                double perPercent = (double)this.CurrentSize.Width / 101.0;

                double value = e.NewValue * perPercent;

                this.drawing.SetPositionX(Convert.ToInt32(Math.Round(value, 0)));

                this.drawing.AvoidOutOfScreen();

                pbFull.Invalidate();

                UpdatePanels(true);
            }
        }
        private void DisplayScrollbars()
        {
            if (scrollbars)
            {
                if (this.Image != null)
                {
                    int perPercent = this.CurrentSize.Width / 100;

                    if (this.CurrentSize.Width - perPercent > this.pbFull.Width)
                    {
                        this.sbHoriz.Visible = true;
                    }
                    else
                    {
                        this.sbHoriz.Visible = false;
                    }

                    if (this.CurrentSize.Height - perPercent > this.pbFull.Height)
                    {
                        this.sbVert.Visible = true;
                    }
                    else
                    {
                        this.sbVert.Visible = false;
                    }

                    if (this.sbVert.Visible == true && this.sbHoriz.Visible == true)
                    {
                        this.sbPanel.Visible = true;
                        this.sbVert.Height = this.pbFull.Height - 18;
                        this.sbHoriz.Width = this.pbFull.Width - 18;
                    }
                    else
                    {
                        this.sbPanel.Visible = false;

                        if (this.sbVert.Visible)
                        {
                            this.sbVert.Height = this.pbFull.Height;
                        }
                        else
                        {
                            this.sbHoriz.Width = this.pbFull.Width;
                        }
                    }
                }
                else
                {
                    this.sbHoriz.Visible = false;
                    this.sbVert.Visible = false;
                    this.sbPanel.Visible = false;
                }
            }
            else
            {
                this.sbHoriz.Visible = false;
                this.sbVert.Visible = false;
                this.sbPanel.Visible = false;
            }
        }

        private void SetScrollbarValues()
        {
            if (scrollbars)
            {
                if (sbHoriz.Visible)
                {
                    isScrolling = true;
                    double perPercent = (double)this.CurrentSize.Width / 101.0;
                    double totalPercent = (double)this.pbFull.Width / perPercent;

                    sbHoriz.Minimum = 0;
                    sbHoriz.Maximum = 100;
                    sbHoriz.LargeChange = Convert.ToInt32(Math.Round(totalPercent, 0));

                    double value = (double)((-this.drawing.BoundingBox.X) / perPercent);

                    if (value > sbHoriz.Maximum) { sbHoriz.Value = (sbHoriz.Maximum - sbHoriz.LargeChange) + ((sbHoriz.LargeChange > 0) ? 1 : 0); }
                    else if (value < 0) { sbHoriz.Value = 0; }
                    else
                    {
                        sbHoriz.Value = Convert.ToInt32(Math.Round(value, 0));
                    }
                    isScrolling = false;
                }

                if (sbVert.Visible)
                {
                    isScrolling = true;
                    double perPercent = (double)this.CurrentSize.Height / 101.0;
                    double totalPercent = (double)this.pbFull.Height / perPercent;

                    sbVert.Minimum = 0;
                    sbVert.Maximum = 100;
                    sbVert.LargeChange = Convert.ToInt32(Math.Round(totalPercent, 0));

                    double value = (double)((-this.drawing.BoundingBox.Y) / perPercent);

                    if (value > sbVert.Maximum) { sbVert.Value = (sbVert.Maximum - sbVert.LargeChange) + ((sbVert.LargeChange > 0) ? 1 : 0); }
                    else if (value < 0) { sbVert.Value = 0; }
                    else
                    {
                        sbVert.Value = Convert.ToInt32(Math.Round(value, 0));
                    }
                    isScrolling = false;
                }
            }
            else
            {
                sbHoriz.Visible = false;
                sbVert.Visible = false;
            }
        }

        public bool Scrollbars
        {
            get { return scrollbars; }
            set
            {
                scrollbars = value;
                DisplayScrollbars();
                SetScrollbarValues();
            }
        }

        private void pbFull_MouseDown(object sender, MouseEventArgs e)
        {
            selectedPointNumber = getClickedCircle(drawing.getScaledPoint(new FloatPoint(e.X, e.Y)));
            cleanDocumentView();

            if (lastSelectedPoint != -1)
            {
                var ck = checkPoints.ElementAt(lastSelectedPoint);
                drawing.DrawCircle((int)ck.coord_x, (int)ck.coord_y, Brushes.DeepSkyBlue);
            }

            if (e.Button == MouseButtons.Left)
            {
                // Left Shift or Right Shift pressed? Or is select mode one?
                if (this.IsKeyPressed(0xA0) || this.IsKeyPressed(0xA1) || selectMode == true)
                {
                    if (!enableNewPoint)
                    {
                        return;
                    }

                    enableNewPoint = false;

                    FloatPoint pointCoord = drawing.getScaledPoint(new FloatPoint(e.X, e.Y));

                    drawing.DrawCircle((int)pointCoord.X, (int)pointCoord.Y, Brushes.Black);

                    checkPoints.Add(new CheckPoint());
                    selectedPointNumber = checkPoints.Count() - 1;
                    lastSelectedPoint = selectedPointNumber;

                    var ck = checkPoints.ElementAt(selectedPointNumber);

                    ck.coord_x = (int)pointCoord.X;
                    ck.coord_y = (int)pointCoord.Y;

                    selectMode = false;
                    this.btnMode.Image = Map.Properties.Resources.btnSelect;
                    cleanDocumentView();
                    SavePointsData(this.ImagePath, checkPoints);
                }
                else if (selectedPointNumber != -1)
                {
                    numePropText.Text = checkPoints.ElementAt(selectedPointNumber).NumeProprietar;
                    tarlaText.Text = checkPoints.ElementAt(selectedPointNumber).Tarla;
                    parcelaText.Text = checkPoints.ElementAt(selectedPointNumber).Parcela;
                    suprafataText.Text = checkPoints.ElementAt(selectedPointNumber).Suprafata;
                    statusDosarText.Text = checkPoints.ElementAt(selectedPointNumber).StatusDosar;
                    linkDocumentText.Text = checkPoints.ElementAt(selectedPointNumber).LinkDocument;

                    lastSelectedPoint = selectedPointNumber;
                    refreshDocumentView(checkPoints.ElementAt(selectedPointNumber));
                    var ck = checkPoints.ElementAt(lastSelectedPoint);
                    drawing.DrawCircle((int)ck.coord_x, (int)ck.coord_y, Brushes.Black);
                }
                else
                {
                    cleanDocumentView();
                    selectedPointNumber = -1;
                    lastSelectedPoint = -1;
                    PopulateMetaFields();
                    // Start dragging
                    drawing.BeginDrag(new Point(e.X, e.Y));

                    // Fancy cursor
                    if (grabCursor != null)
                    {
                        pbFull.Cursor = grabCursor;
                    }
                }
            }
        }

        private void pbFull_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            drawing.JumpToOrigin(e.X + (drawing.BoundingBox.X - (drawing.BoundingBox.X * 2)), e.Y + (drawing.BoundingBox.Y - (drawing.BoundingBox.Y * 2)), pbFull.Width, pbFull.Height);
            UpdatePanels(true);
        }

        private void pbFull_MouseHover(object sender, EventArgs e)
        {
            // Left shift or Right shift!
            if (this.IsKeyPressed(0xA0) || this.IsKeyPressed(0xA1))
            {
                // Fancy cursor
                pbFull.Cursor = Cursors.Cross;
            }
            else
            {
                // Fancy cursor if not dragging
                if (!drawing.IsDragging)
                {
                    pbFull.Cursor = dragCursor;
                }
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetKeyState(int key);

        private bool IsKeyPressed(int key)
        {
            bool keyPressed = false;
            short result = GetKeyState(key);

            switch (result)
            {
                case 0:
                    // Not pressed and not toggled
                    keyPressed = false;
                    break;

                case 1:
                    // Not presses but toggled
                    keyPressed = false;
                    break;

                default:
                    // Pressed
                    keyPressed = true;
                    break;
            }

            return keyPressed;
        }

        private void pbFull_MouseUp(object sender, MouseEventArgs e)
        {
            // Am i dragging or selecting?
            if (shiftSelecting == true)
            {
                // Calculate my selection rectangle
                Rectangle rect = CalculateReversibleRectangle(ptSelectionStart, ptSelectionEnd);

                // Clear the selection rectangle
                ptSelectionEnd.X = -1;
                ptSelectionEnd.Y = -1;
                ptSelectionStart.X = -1;
                ptSelectionStart.Y = -1;

                // Stop selecting
                shiftSelecting = false;

                // Position of the panel to the screen
                Point ptPbFull = PointToScreen(pbFull.Location);

                // Zoom to my selection
                drawing.ZoomToSelection(rect, ptPbFull);

                // Refresh my screen & update my preview panel
                pbFull.Refresh();
                UpdatePanels(true);
            }
            else
            {
                // Stop dragging and update my panels
                drawing.EndDrag();
                UpdatePanels(true);

                // Fancy cursor
                if (dragCursor != null)
                {
                    pbFull.Cursor = dragCursor;
                }
            }
        }

        private Rectangle CalculateReversibleRectangle(Point ptSelectStart, Point ptSelectEnd)
        {
            Rectangle rect = new Rectangle();

            ptSelectStart = pbFull.PointToScreen(ptSelectStart);
            ptSelectEnd = pbFull.PointToScreen(ptSelectEnd);

            if (ptSelectStart.X < ptSelectEnd.X)
            {
                rect.X = ptSelectStart.X;
                rect.Width = ptSelectEnd.X - ptSelectStart.X;
            }
            else
            {
                rect.X = ptSelectEnd.X;
                rect.Width = ptSelectStart.X - ptSelectEnd.X;
            }
            if (ptSelectStart.Y < ptSelectEnd.Y)
            {
                rect.Y = ptSelectStart.Y;
                rect.Height = ptSelectEnd.Y - ptSelectStart.Y;
            }
            else
            {
                rect.Y = ptSelectEnd.Y;
                rect.Height = ptSelectStart.Y - ptSelectEnd.Y;
            }

            return rect;
        }

        private void DrawReversibleRectangle(Rectangle rect)
        {
            pbFull.Refresh();
            ControlPaint.DrawReversibleFrame(rect, Color.LightGray, FrameStyle.Dashed);
        }

        private void pbFull_MouseMove(object sender, MouseEventArgs e)
        {
            // Am I dragging or selecting?
            if (shiftSelecting == true)
            {
                // Keep selecting
                ptSelectionEnd.X = e.X;
                ptSelectionEnd.Y = e.Y;

                Rectangle pbFullRect = new Rectangle(0, 0, pbFull.Width - 1, pbFull.Height - 1);

                // Am I still selecting within my panel?
                if (pbFullRect.Contains(new Point(e.X, e.Y)))
                {
                    // If so, draw my Rubber Band Rectangle!
                    Rectangle rect = CalculateReversibleRectangle(ptSelectionStart, ptSelectionEnd);
                    DrawReversibleRectangle(rect);
                }
            }
            else
            {
                // Keep dragging
                drawing.Drag(new Point(e.X, e.Y));
                if (drawing.IsDragging)
                {
                    UpdatePanels(false);
                }
                else
                {
                    // I'm not dragging OR selecting
                    // Make sure if left or right shift is pressed to change cursor

                    if (this.IsKeyPressed(0xA0) || this.IsKeyPressed(0xA1) || selectMode == true)
                    {
                        // Fancy Cursor
                        if (pbFull.Cursor != Cursors.Cross)
                        {
                            pbFull.Cursor = Cursors.Cross;
                        }
                    }
                    else
                    {
                        // Fancy Cursor
                        if (pbFull.Cursor != dragCursor)
                        {
                            pbFull.Cursor = dragCursor;
                        }
                    }
                }
            }
        }

        private void MouseWheelZoom(object sender, MouseEventArgs e)
        {
            drawing.Scroll(sender, e);

            if (drawing.Image != null)
            {
                if (e.Delta < 0)
                {
                    OnZoom(new ImageViewerZoomEventArgs(drawing.Zoom, KpZoom.ZoomOut));
                }
                else
                {
                    OnZoom(new ImageViewerZoomEventArgs(drawing.Zoom, KpZoom.ZoomIn));
                }
            }

            UpdatePanels(true);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            InitControl();
            drawing.AvoidOutOfScreen();
            UpdatePanels(true);
        }

        private void pbFull_Paint(object sender, PaintEventArgs e)
        {
            // Can I double buffer?
            if (drawEngine.CanDoubleBuffer())
            {
                // Yes I can!
                drawEngine.g.FillRectangle(new SolidBrush(pbFull.BackColor), e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width, e.ClipRectangle.Height);

                // Drawing to backBuffer
                drawing.Draw(drawEngine.g);

                // Drawing to Panel
                drawEngine.Render(e.Graphics);
            }
        }
        private void btnMode_Click(object sender, EventArgs e)
        {
            if (selectMode == false)
            {
                selectMode = true;
                this.btnMode.Image = Map.Properties.Resources.btnDrag;
            }
            else
            {
                selectMode = false;
                this.btnMode.Image = Map.Properties.Resources.btnSelect;
            }
        }
        private void btnFitToScreen_Click(object sender, EventArgs e)
        {
            drawing.FitToScreen();
            UpdatePanels(true);
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            drawing.ZoomOut();

            // AfterZoom Event
            if (drawing.Image != null)
            {
                OnZoom(new ImageViewerZoomEventArgs(drawing.Zoom, KpZoom.ZoomOut));
            }
            UpdatePanels(true);
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            drawing.ZoomIn();

            // AfterZoom Event
            if (drawing.Image != null)
            {
                OnZoom(new ImageViewerZoomEventArgs(drawing.Zoom, KpZoom.ZoomIn));
            }
            UpdatePanels(true);
        }
        public void InitControl()
        {
            // Make sure panel is DoubleBuffering
            drawEngine.CreateDoubleBuffer(pbFull.CreateGraphics(), pbFull.Size.Width, pbFull.Size.Height);

            
            if (!scrollbars)
            {
                sbHoriz.Visible = false;
                sbVert.Visible = false;
                sbPanel.Visible = false;
            }
            
        }

        private void FocusOnMe()
        {
            // Do not lose focus! ("Fix" for the Scrolling issue)
            this.Focus();
        }

        private void pbFull_Click(object sender, EventArgs e)
        {
            FocusOnMe();
        }

        private void pbPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (panelDragging == false)
            {
                drawing.JumpToOrigin(e.X, e.Y, pbPanel.Width, pbPanel.Height, pbFull.Width, pbFull.Height);
                UpdatePanels(true);

                panelDragging = true;
            }
        }

        private void pbPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (panelDragging)
            {
                drawing.JumpToOrigin(e.X, e.Y, pbPanel.Width, pbPanel.Height, pbFull.Width, pbFull.Height);
                UpdatePanels(true);
            }
        }

        private void pbPanel_MouseUp(object sender, MouseEventArgs e)
        {
            panelDragging = false;
        }

        private void UpdatePanels(bool updatePreview)
        {
            if (drawing.CurrentSize.Width > 0 && drawing.OriginalSize.Width > 0)
            {
                // scrollbars
                DisplayScrollbars();
                SetScrollbarValues();

                // Make sure panel is up to date
                pbFull.Refresh();

                // Calculate zoom
                double zoom = Math.Round(((double)drawing.CurrentSize.Width / (double)drawing.OriginalSize.Width), 2);

                // Display zoom in percentages
                cbZoom.Text = (int)(zoom * 100) + "%";
            }
            
        }

        public class ImageViewerZoomEventArgs : EventArgs
        {
            private int zoom;
            public int Zoom
            {
                get { return zoom; }
            }

            private KpZoom inOut;
            public KpZoom InOut
            {
                get { return inOut; }
            }

            public ImageViewerZoomEventArgs(double zoom, KpZoom inOut)
            {
                this.zoom = Convert.ToInt32(Math.Round((zoom * 100), 0));
                this.inOut = inOut;
            }
        }

        private void saveMetaBtn_Click(object sender, EventArgs e)
        {
            enableNewPoint = true;
            var ck = checkPoints.ElementAt(selectedPointNumber);

            ck.NumeProprietar = numePropText.Text;
            ck.Tarla = tarlaText.Text;
            ck.Parcela = parcelaText.Text;
            ck.Suprafata = suprafataText.Text;
            ck.StatusDosar = statusDosarText.Text;
            ck.LinkDocument = linkDocumentText.Text;

            ck.AdditionalDocument.Clear();

            foreach (Control ctr in this.addDocumentMenu.Controls)
            {
                if (ctr.Name == "linkDocumentText")
                {
                    ck.AdditionalDocument.Add((string)ctr.Tag);
                }
            }

            this.deleteDocs = false;

            SavePointsData(this.ImagePath, checkPoints);

            PopulateMetaFields();
        }

        private int getClickedCircle(FloatPoint newPoint)
        {
            foreach (var thisCheckPoint in checkPoints)
            {
                bool isInside = ((newPoint.X - thisCheckPoint.coord_x) * (newPoint.X - thisCheckPoint.coord_x) + (newPoint.Y - thisCheckPoint.coord_y) * (newPoint.Y - thisCheckPoint.coord_y) < 400);
                if (isInside)
                {
                    return checkPoints.IndexOf(thisCheckPoint);
                }
            }

            return -1;
        }

        private void PopulateMetaFields()
        {
            numePropText.Text = "";
            tarlaText.Text = "";
            parcelaText.Text = "";
            suprafataText.Text = "";
            statusDosarText.Text = "";
            linkDocumentText.Text = "";
            additionalDocumentText.Text = "";
        }

        private void PopulateMetaFields(CheckPoint ck)
        {
            numePropText.Text = ck.NumeProprietar;
            tarlaText.Text = ck.Tarla;
            parcelaText.Text = ck.Parcela;
            suprafataText.Text = ck.Suprafata;
            statusDosarText.Text = ck.StatusDosar;
            linkDocumentText.Text = ck.LinkDocument;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void addDocument_Click(object sender, EventArgs e)
        {

        }

        private void numePropText_TextChanged(object sender, EventArgs e)
        {

        }

        private void tarlaText_TextChanged(object sender, EventArgs e)
        {

        }

        private void parcelaText_TextChanged(object sender, EventArgs e)
        {

        }

        private void suprafataText_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusDosarText_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkDocument_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }

        private void additionalDocument_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }

        private void VisitLink()
        {
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start((string)linkDocumentText.Tag);
        }

        private void VisitLink2()
        {
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start((string)additionalDocumentText.Tag);
        }

        private void SavePointsData(string imagePath, List<CheckPoint> ckList)
        {
            if (! File.Exists(imagePath))
            {
                return;
            }

            var imageDir = Path.GetDirectoryName(imagePath);
            this.configPath = Path.Combine(imageDir, "config.json");

            string output = JsonConvert.SerializeObject(ckList, Formatting.Indented);
            File.WriteAllText(configPath, output);
        }

        private void LoadPointsData()
        {
            if (! File.Exists(this.configPath))
            {
                return;
            }

            string inputJson = File.ReadAllText(configPath);
            checkPoints = JsonConvert.DeserializeObject<List<CheckPoint>>(inputJson);

            foreach(CheckPoint ck in checkPoints)
            {
                drawing.DrawCircle((int)ck.coord_x, (int)ck.coord_y, Brushes.DeepSkyBlue);
            }

            UpdatePanels(true);
        }

        private void CenterPoint(int x, int y)
        {
            drawing.JumpToOrigin(x + (drawing.BoundingBox.X - (drawing.BoundingBox.X * 2)), y + (drawing.BoundingBox.Y - (drawing.BoundingBox.Y * 2)), pbFull.Width, pbFull.Height);
            UpdatePanels(true);
        }

        private void cleanSearchView()
        {
            int i = 0;
            while (i < this.searchMenu.Controls.Count)
            {
                if (this.searchMenu.Controls[i].Name == "linkDocumentText")
                {
                    this.searchMenu.Controls.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            if (searchTextBox.Text == null)
            {
                return;
            }

            cleanSearchView();

            this.foundPoints.Clear();


            foreach (CheckPoint ck in checkPoints)
            {
                if (ck.Tarla == searchTextBox.Text)
                {
                    this.foundPoints.Add(ck);
                }
            }

            if (this.foundPoints.Count == 1)
            {
                int index = this.checkPoints.FindIndex(new_ck => new_ck.Tarla == this.foundPoints[0].Tarla);

                CheckPoint ck = this.foundPoints[0];

                FloatPoint point = drawing.getUnScaledPoint(new FloatPoint(ck.coord_x, ck.coord_y));
                CenterPoint((int)point.X, (int)point.Y);

                numePropText.Text = ck.NumeProprietar;
                tarlaText.Text = ck.Tarla;
                parcelaText.Text = ck.Parcela;
                suprafataText.Text = ck.Suprafata;
                statusDosarText.Text = ck.StatusDosar;
                linkDocumentText.Text = ck.LinkDocument;
                //additionalDocumentText.Text = ck.AdditionalDocument;

                drawing.DrawCircle((int)ck.coord_x, (int)ck.coord_y, Brushes.Black);
                UpdatePanels(true);
                lastSelectedPoint = index;
            } else {
                foreach (CheckPoint ck in this.foundPoints) {
                    System.Windows.Forms.LinkLabel linkToPoint = new System.Windows.Forms.LinkLabel();

                    linkToPoint.AutoSize = true;
                    linkToPoint.Location = new System.Drawing.Point(7, 230);
                    linkToPoint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
                    linkToPoint.Name = "linkDocumentText";
                    linkToPoint.Size = new System.Drawing.Size(36, 17);
                    linkToPoint.TabIndex = 12;
                    linkToPoint.TabStop = true;
                    linkToPoint.Text = ck.NumeProprietar;
                    linkToPoint.Tag = this.checkPoints.FindIndex(new_ck => new_ck.NumeProprietar == ck.NumeProprietar && 
                                                                           new_ck.Tarla == ck.Tarla);
                    linkToPoint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPoint_Click);

                    this.searchMenu.Controls.Add(linkToPoint);
                }

            }
        }
        private void linkPoint_Click(object sender, EventArgs e)
        {
            LinkLabel pointLink = (LinkLabel)sender;
            int index = (int)pointLink.Tag;

            CheckPoint ck = this.checkPoints[index];

            FloatPoint point = drawing.getUnScaledPoint(new FloatPoint(ck.coord_x, ck.coord_y));
            CenterPoint((int)point.X, (int)point.Y);

            numePropText.Text = ck.NumeProprietar;
            tarlaText.Text = ck.Tarla;
            parcelaText.Text = ck.Parcela;
            suprafataText.Text = ck.Suprafata;
            statusDosarText.Text = ck.StatusDosar;
            linkDocumentText.Text = ck.LinkDocument;
            //additionalDocumentText.Text = ck.AdditionalDocument;

            if (lastSelectedPoint != -1)
            {
                CheckPoint last_ck = this.checkPoints[lastSelectedPoint];
                drawing.DrawCircle((int)last_ck.coord_x, (int)last_ck.coord_y, Brushes.DeepSkyBlue);
            }

            drawing.DrawCircle((int)ck.coord_x, (int)ck.coord_y, Brushes.Black);
            UpdatePanels(true);
            lastSelectedPoint = index;
        }

        private void showMenuBtn_Click(object sender, EventArgs e)
        {
            ShowExplorerMenu();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lastSelectedPoint == -1)
            {
                return;
            }

            CheckPoint last_ck = this.checkPoints[lastSelectedPoint];
            drawing.DrawCircle((int)last_ck.coord_x, (int)last_ck.coord_y, Brushes.White);
            UpdatePanels(true);

            this.checkPoints.RemoveAt(lastSelectedPoint);
            SavePointsData(this.ImagePath, checkPoints);

            this.lastSelectedPoint = -1;
            enableNewPoint = true;
        }

    }
}
