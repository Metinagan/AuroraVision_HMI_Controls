using HMI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(cameraView),"Icons.3d-design.png")]
    public partial class cameraView : UserControl
    {
        //inputValues
        private Image _inputImage;
        private int _mpWidth = 1300;
        private int _printWidth = 800;
        private int _repeatHeight = 300;

        //Output Values
        private int _cameraX;
        private int _cameraY;
        private int _outputCenterX;
        private int _outputNewWidth;

        //MoveLine
        private bool isDragging = false;
        private int originalX;

        //SizeLine
        private bool isResizing = false;
        private int originalWidth;
        private int minWidth = 50;

        private Point actionStart;
        private Point clickPoint = Point.Empty;


        private bool isFirstLoad = true; // İlk yükleme kontrolü

        public cameraView()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        public void SetupEventHandlers()
        {
            panel1.Paint += Panel1_Paint;
            panel1.MouseDown += Panel1_MouseDown;
            panel1.MouseMove += Panel1_MouseMove;
            panel1.MouseUp += Panel1_MouseUp;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.Paint += PictureBox1_Paint;
        }
        private void calculateCenter()
        {
            decimal ratio = (decimal)_mpWidth / (decimal)panel1.Width;
            //decimal pbCenter = 
            _outputCenterX = (int)Math.Round((pictureBox1.Right - ((pictureBox1.Width) / 2m)) * ratio);
        }
        private void calculateWidth()
        {
            decimal ratio = (decimal)_mpWidth / (decimal)panel1.Width;
            _outputNewWidth = ((int)Math.Ceiling(pictureBox1.Width * ratio));
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            cameraX = 0;
            cameraY = 0;
        }
        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            int x = pictureBox1.Location.X + e.X;
            int y = pictureBox1.Location.Y + e.Y;
            decimal ratio = (decimal)_mpWidth / (decimal)panel1.Width;
            
            int realX = (int)Math.Round(x * ratio);
            int realY = (int)Math.Round(y * ratio);

            if(realX>= _mpWidth) { cameraX = _mpWidth; }
            else if (realX <= 0) { cameraX = 0; }
            else { cameraX = realX; }
            if(realY >= _repeatHeight) { cameraY = _repeatHeight; }
            else if (realY <= 0) { cameraY = 0; }
            else { cameraY = realY; }

            if (e.Button == MouseButtons.Left)
            {
                if (pictureBox1.Location.X < 10)
                {
                    if (e.X < 10)
                    {
                        pictureBox1.Location = new Point(15, 0);
                        panel1.Invalidate();
                    }

                }
                else if(pictureBox1.Location.X + pictureBox1.Width > panel1.Width - 10)
                {
                    if (e.X > pictureBox1.Width - 10)
                    {
                        pictureBox1.Width -= 10;
                        panel1.Invalidate();
                    }
                }

                clickPoint = e.Location;
                pictureBox1.Invalidate();
            }
        }
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (clickPoint != Point.Empty)
            {
                int boxSize = 50; // Kare boyutu
                int radius = 10; // Köşe yuvarlaklığı

                Rectangle rect = new Rectangle(clickPoint.X - boxSize / 2,
                                               clickPoint.Y - boxSize / 2,
                                               boxSize, boxSize);

                using (GraphicsPath path = new GraphicsPath())
                {
                    // Yuvarlatılmış kare oluştur
                    path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
                    path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
                    path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                    path.CloseFigure();

                    // Çizgi ve dolgu rengi
                    using (Pen pen = new Pen(Color.Red, 3))
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, Color.Red))) // Şeffaf iç renk
                    {
                        e.Graphics.FillPath(brush, path);
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            }
        }


        private void CenterPictureBox()
        {
            if (_mpWidth == 0 || _printWidth == 0 || _repeatHeight == 0 || this.Width == 0)
            {
                return;
            }
            if (_inputImage != null && isFirstLoad)
            {
                pictureBox1.Width = (_printWidth * panel1.Width) / _mpWidth;
                pictureBox1.Height = ((_repeatHeight * panel1.Width) / _mpWidth);
                panel1.Height = pictureBox1.Height;
                this.Height = pictureBox1.Height;
                pictureBox1.Location = new Point((panel1.Width - pictureBox1.Width) / 2, 0);

                _outputCenterX = pictureBox1.Location.X + pictureBox1.Width / 2;
                calculateCenter();
                calculateWidth();
                panel1.Invalidate();
                isFirstLoad = false; // Bir daha çalışmaması için bayrağı false yap
            }
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            int rightLineX = pictureBox1.Location.X + pictureBox1.Width;
            int leftLineX = pictureBox1.Location.X;

            if (Math.Abs(e.X - rightLineX) <= 15)
            {
                // Sağ çizgiye tıklandı (resize)
                isResizing = true;
                actionStart = e.Location;
                originalWidth = pictureBox1.Width;
            }
            else if (Math.Abs(e.X - leftLineX) <= 15)
            {
                // Sol çizgiye tıklandı (drag)
                isDragging = true;
                actionStart = e.Location;
                originalX = pictureBox1.Location.X;
            }
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing)
            {
                int newWidth = originalWidth + (e.X - actionStart.X);

                // Yeni genişlik minWidth'ten büyük ve panel sınırları içinde mi?
                if (newWidth >= minWidth && pictureBox1.Location.X + newWidth <= panel1.Width)
                {
                    pictureBox1.Width = newWidth;
                    panel1.Invalidate();
                    calculateCenter();
                    calculateWidth();
                }
            }
            else if (isDragging)
            {
                int newX = originalX + (e.X - actionStart.X);

                // Yeni X pozisyonu panel sınırları içinde mi?
                if (newX >= 0 && newX + pictureBox1.Width <= panel1.Width)
                {
                    pictureBox1.Location = new Point(newX, pictureBox1.Location.Y);
                    panel1.Invalidate();
                    calculateCenter();
                }
            }
        }
        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isResizing = false;
            isDragging = false;
        }
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                new Point(0, 0),
                new Point(panel1.Width, 0),
                Color.FromArgb(200, Color.Blue), // Başlangıç rengi
                Color.FromArgb(200, Color.Blue))) // Bitiş rengi
            using (Pen pen = new Pen(gradientBrush, 8)) // Daha kalın çizgi
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                // Sağ çizgi (Resize Slider)
                int rightLineX = pictureBox1.Location.X + pictureBox1.Width;
                e.Graphics.DrawLine(pen, new Point(rightLineX, 10), new Point(rightLineX, panel1.Height - 10));

                // Sol çizgi (Drag Slider)
                int leftLineX = pictureBox1.Location.X;
                e.Graphics.DrawLine(pen, new Point(leftLineX, 10), new Point(leftLineX, panel1.Height - 10));

                // Tutacakları daha belirgin yapalım
                DrawSliderHandle(e.Graphics, new Point(leftLineX, panel1.Height / 2));
                DrawSliderHandle(e.Graphics, new Point(rightLineX, panel1.Height / 2));
            }
        }
        // Yuvarlak tutacakları daha belirgin çizme metodu
        private void DrawSliderHandle(Graphics g, Point location)
        {
            int handleSize = 20; // Boyut
            Rectangle rect = new Rectangle(location.X - handleSize / 2, location.Y - handleSize / 2, handleSize, handleSize);

            // Elipsi çizelim
            using (SolidBrush brush = new SolidBrush(Color.DarkBlue))
            {
                g.FillEllipse(brush, rect);
            }

            using (Pen borderPen = new Pen(Color.White, 2)) // Kalın dış çizgi
            {
                g.DrawEllipse(borderPen, rect);
            }

            // Hafif gölge efekti
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(100, Color.Black)))
            {
                Rectangle shadowRect = new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height);
                g.FillEllipse(shadowBrush, shadowRect);
            }

            // Dikey çizgileri çizelim
            int lineWidth = 2;
            int lineSpacing = handleSize / 5; // Çizgiler arasındaki mesafe
            for (int i = 0; i < 4; i++)
            {
                int xPosition = rect.X + (i + 1) * lineSpacing; // Dikey çizgilerin X konumu
                Rectangle lineRect = new Rectangle(xPosition - lineWidth / 2, rect.Y, lineWidth, rect.Height);
                using (Pen linePen = new Pen(Color.White, 2)) // Çizgilerin rengi ve kalınlığı
                {
                    g.DrawLine(linePen, lineRect.Left, lineRect.Top, lineRect.Left, lineRect.Bottom);
                }
            }
        }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        [Description("Image input")]
        public Image inputImage
        {
            get { return _inputImage; }
            set
            {
                if (_inputImage != value)
                {
                    _inputImage = value;
                    //pictureBox1.Image = _inputImage;
                    CenterPictureBox();
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the machine print width")]
        public int mpWidth
        {
            get { return _mpWidth; }
            set
            {
                _mpWidth = value;
            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the print width")]
        public int printWidth
        {
            get { return _printWidth; }
            set { _printWidth = value; }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the repeat height")]
        public int repeatHeight
        {
            get { return _repeatHeight; }
            set { _repeatHeight = value; }
        }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        [Description("Description: Camera position X-Axis")]
        public int cameraX
        {
            get { return _cameraX; }
            set { _cameraX = value; }
        }
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        [Description("Description: Camera position Y-Axis")]
        public int cameraY
        {
            get { return _cameraY; }
            set { _cameraY = value; }
        }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        [Description("Description: Location X-Axis of the center of print")]
        public int outputCenterX
        {
            get { return _outputCenterX; }
            set { _outputCenterX = value; }
        }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        [Description("Description: Location Y-Axis of the center of print")]
        public int outputNewWidth
        {
            get { return _outputNewWidth; }
            set { _outputNewWidth = value; }
        }
    }
}