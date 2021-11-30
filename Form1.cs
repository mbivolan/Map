using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map
{
    public enum KpZoom
    {
        ZoomIn,
        ZoomOut
    }


    public partial class Form1 : Form
    {
        private DrawObject drawing;
        private DrawEngine drawEngine;
        private bool selectMode = false;
        private bool panelDragging = false;
        private bool isScrolling = false;
        private bool scrollbars = false;
        
        private bool shiftSelecting = false;
        private Point ptSelectionStart = new Point();
        private Point ptSelectionEnd = new Point();

        private Cursor grabCursor = null;
        private Cursor dragCursor = null;


        public Form1()
        {
            drawEngine = new DrawEngine();
            drawing = new DrawObject(this);
            InitializeComponent();
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
                //ToggleMultiPage();

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

                UpdatePanels(true);
                //ToggleMultiPage();

                // scrollbars
                DisplayScrollbars();
                SetScrollbarValues();
            }
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
            if (e.Button == MouseButtons.Left)
            {
                // Left Shift or Right Shift pressed? Or is select mode one?
                if (this.IsKeyPressed(0xA0) || this.IsKeyPressed(0xA1) || selectMode == true)
                {
                    // Fancy cursor
                    pbFull.Cursor = Cursors.Cross;

                    shiftSelecting = true;

                    // Initial seleciton
                    ptSelectionStart.X = e.X;
                    ptSelectionStart.Y = e.Y;

                    // No selection end
                    ptSelectionEnd.X = -1;
                    ptSelectionEnd.Y = -1;
                }
                else
                {
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

        private void KP_ImageViewerV2_MouseWheel(object sender, MouseEventArgs e)
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

        private void KpImageViewer_Click(object sender, EventArgs e)
        {
            FocusOnMe();
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

                /*
                if (updatePreview && drawing.PreviewImage != null && pbPanel.Visible == true)
                {
                    // No memory leaks here
                    if (preview != null)
                    {
                        preview.Dispose();
                        preview = null;
                    }

                    // New preview
                    preview = new Bitmap(drawing.PreviewImage.Size.Width, drawing.PreviewImage.Size.Height);

                    // Make sure panel is the same size as the bitmap
                    if (pbPanel.Size != drawing.PreviewImage.Size)
                    {
                        pbPanel.Size = drawing.PreviewImage.Size;
                    }

                    // New Graphics from the new bitmap we created (Empty)
                    using (Graphics g = Graphics.FromImage(preview))
                    {
                        // Draw the image on the bitmap
                        g.DrawImage(drawing.PreviewImage, 0, 0, drawing.PreviewImage.Size.Width, drawing.PreviewImage.Size.Height);

                        double ratioX = (double)drawing.PreviewImage.Size.Width / (double)drawing.CurrentSize.Width;
                        double ratioY = (double)drawing.PreviewImage.Size.Height / (double)drawing.CurrentSize.Height;

                        double boxWidth = pbFull.Width * ratioX;
                        double boxHeight = pbFull.Height * ratioY;
                        double positionX = ((drawing.BoundingBox.X - (drawing.BoundingBox.X * 2)) * ratioX);
                        double positionY = ((drawing.BoundingBox.Y - (drawing.BoundingBox.Y * 2)) * ratioY);

                        // Making the red pen
                        Pen pen = new Pen(Color.Red, 1);

                        if (boxHeight >= drawing.PreviewImage.Size.Height)
                        {
                            boxHeight = drawing.PreviewImage.Size.Height - 1;
                        }
                        else if ((boxHeight + positionY) > drawing.PreviewImage.Size.Height)
                        {
                            boxHeight = drawing.PreviewImage.Size.Height - (positionY);
                        }

                        if (boxWidth >= drawing.PreviewImage.Size.Width)
                        {
                            boxWidth = drawing.PreviewImage.Size.Width - 1;
                        }
                        else if ((boxWidth + positionX) > drawing.PreviewImage.Size.Width)
                        {
                            boxWidth = drawing.PreviewImage.Size.Width - (positionX);
                        }

                        // Draw the rectangle on the bitmap
                        g.DrawRectangle(pen, new Rectangle((int)positionX, (int)positionY, (int)boxWidth, (int)boxHeight));
                    }

                    // Display the bitmap
                    pbPanel.Image = preview;
                }
                */
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

    }
}
