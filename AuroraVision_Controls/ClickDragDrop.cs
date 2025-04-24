using AuroraVision_Controls.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static AuroraVision_Controls.Buttons;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    public partial class ClickDragDrop : UserControl
    {
        List<ButtonInfo> buttonInfos = new List<ButtonInfo>();
        #region Değişkenler
        private Buttons currentZoomButton;
        private Image originalImage;
        private float zoomFactor = 1.0f;
        private float minZoomFactor = 0f;   // <== Eklenen değişken, en küçük zoom seviyesi
        private PointF zoomPoint = new PointF(0, 0);
        private float zoomOffsetX = 0, zoomOffsetY = 0;

        private bool isPanning = false;  // Sürükleme modu aktif mi?
        private Point lastMousePos;      // Son mouse pozisyonu

        private float extraZoomFactor = 2.0f; // Extra zoom çarpanı
        private const int zoomWindowSize = 250; // Küçük zoom penceresinin boyutu

        private bool isCursorHidden = false; // İmlecin gizli olup olmadığını takip edelim

        private bool isZoomMoving = false; // Zoom penceresi sürükleniyor mu?
        private Point lastZoomMovePos;     // Son tıklanan nokta
        private bool isExtraZoomActive = false; // **Ekstra zoom penceresi aktif mi?**
        bool indicatorbuttonsadded = false;
        #endregion

        #region HMI INPUTS
        Image _Image = null;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Image Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                originalImage = _Image; // Orijinal resmi sakla
                pictureBox1.Invalidate(); // PictureBox'ı yeniden çiz
            }
        }

        Color _Color1 = Color.Black;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Color Color1
        {
            get
            {
                return _Color1;
            }
            set
            {
                _Color1 = value;
            }
        }

        Color _Color2 = Color.Black;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Color Color2
        {
            get
            {
                return _Color2;
            }
            set
            {
                _Color2 = value;
            }
        }

        Color _Color3 = Color.Black;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Color Color3
        {
            get
            {
                return _Color3;
            }
            set
            {
                _Color3 = value;
            }
        }

        Color _Color4 = Color.Black;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Color Color4
        {
            get
            {
                return _Color4;
            }
            set
            {
                _Color4 = value;
            }
        }

        Color _Color5 = Color.Black;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Color Color5
        {
            get
            {
                return _Color5;
            }
            set
            {
                _Color5 = value;
            }
        }

        Color _Color6 = Color.Black;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Color Color6
        {
            get
            {
                return _Color6;
            }
            set
            {
                _Color6 = value;
            }
        }

        Color _Color7 = Color.Black;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Color Color7
        {
            get
            {
                return _Color7;
            }
            set
            {
                _Color7 = value;
            }
        }

        Color _Color8 = Color.Black;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Color Color8
        {
            get
            {
                return _Color8;
            }
            set
            {
                _Color8 = value;
            }
        }

        Color _Color9 = Color.Black;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Color Color9
        {
            get
            {
                return _Color9;
            }
            set
            {
                _Color9 = value;
            }
        }

        Color _Color10 = Color.Black;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Color Color10
        {
            get
            {
                return _Color10;
            }
            set
            {
                _Color10 = value;
            }
        }

        int _printerheadercount = 0;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public int printerheadercount
        {
            get
            {
                return _printerheadercount;
            }
            set
            {
                _printerheadercount = value;
            }
        }

        bool _buttonactivepassive1 = false;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public bool buttonactivepassive1
        {
            get
            {
                return _buttonactivepassive1;
            }
            set
            {
                _buttonactivepassive1 = value;
            }
        }

        bool _buttonactivepassive2 = false;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public bool buttonactivepassive2
        {
            get
            {
                return _buttonactivepassive2;
            }
            set
            {
                _buttonactivepassive2 = value;
            }
        }

        bool _buttonactivepassive3 = false;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public bool buttonactivepassive3
        {
            get
            {
                return _buttonactivepassive3;
            }
            set
            {
                _buttonactivepassive3 = value;
            }
        }

        bool _buttonactivepassive4 = false;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public bool buttonactivepassive4
        {
            get
            {
                return _buttonactivepassive4;
            }
            set
            {
                _buttonactivepassive4 = value;
            }
        }

        bool _buttonactivepassive5 = false;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public bool buttonactivepassive5
        {
            get
            {
                return _buttonactivepassive5;
            }
            set
            {
                _buttonactivepassive5 = value;
            }
        }

        bool _buttonactivepassive6 = false;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public bool buttonactivepassive6
        {
            get
            {
                return _buttonactivepassive6;
            }
            set
            {
                _buttonactivepassive6 = value;
            }
        }

        bool _buttonactivepassive7 = false;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public bool buttonactivepassive7
        {
            get
            {
                return _buttonactivepassive7;
            }
            set
            {
                _buttonactivepassive7 = value;
            }
        }

        bool _buttonactivepassive8 = false;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public bool buttonactivepassive8
        {
            get
            {
                return _buttonactivepassive8;
            }
            set
            {
                _buttonactivepassive8 = value;
            }
        }

        bool _buttonactivepassive9 = false;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public bool buttonactivepassive9
        {
            get
            {
                return _buttonactivepassive9;
            }
            set
            {
                _buttonactivepassive9 = value;
            }
        }

        bool _buttonactivepassive10 = false;
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public bool buttonactivepassive10
        {
            get
            {
                return _buttonactivepassive10;
            }
            set
            {
                _buttonactivepassive10 = value;
            }
        }
        #endregion

        #region HMI INPUT DO
        private void AddButtons()
        {
          buttonInfos.Clear();
            for (int i = 0; i < printerheadercount; i++)
            {
                if (i == 0)
                {
                    buttonInfos.Add(new ButtonInfo { Color = Color1, Text = "1", Active = buttonactivepassive1 });
                }
                else if (i == 1)
                {
                    buttonInfos.Add(new ButtonInfo { Color = Color2, Text = "2", Active = buttonactivepassive2 });
                }
                else if (i == 2)
                {
                    buttonInfos.Add(new ButtonInfo { Color = Color3, Text = "3", Active = buttonactivepassive3 });
                }
                else if (i == 3)
                {
                    buttonInfos.Add(new ButtonInfo { Color = Color4, Text = "4", Active = buttonactivepassive4 });
                }
                else if (i == 4)
                {
                    buttonInfos.Add(new ButtonInfo { Color = Color5, Text = "5", Active = buttonactivepassive5 });
                }
                else if (i == 5)
                {
                    buttonInfos.Add(new ButtonInfo { Color = Color6, Text = "6", Active = buttonactivepassive6 });
                }
                else if (i == 6)
                {
                    buttonInfos.Add(new ButtonInfo { Color = Color7, Text = "7", Active = buttonactivepassive7 });
                }
                else if (i == 7)
                {
                    buttonInfos.Add(new ButtonInfo { Color = Color8, Text = "8", Active = buttonactivepassive8 });
                }
                else if (i == 8)
                {
                    buttonInfos.Add(new ButtonInfo { Color = Color9, Text = "9", Active = buttonactivepassive9 });
                }
                else if (i == 9)
                {
                    buttonInfos.Add(new ButtonInfo { Color = Color10, Text = "10", Active = buttonactivepassive10 });
                }
            }
           
            //buttonInfos.Add(new ButtonInfo { Color = Color.Magenta, Text = "2", Active = true });
            //buttonInfos.Add(new ButtonInfo { Color = Color.Yellow, Text = "3", Active = true });
            //buttonInfos.Add(new ButtonInfo { Color = Color.Black, Text = "4", Active = true });
            //buttonInfos.Add(new ButtonInfo { Color = Color.White, Text = "5", Active = true });
            //buttonInfos.Add(new ButtonInfo { Color = Color.Gray, Text = "6", Active = false });
            //buttonInfos.Add(new ButtonInfo { Color = Color.Gray, Text = "7", Active = false });
            //buttonInfos.Add(new ButtonInfo { Color = Color.Gray, Text = "8", Active = true });
            //buttonInfos.Add(new ButtonInfo { Color = Color.Gray, Text = "9", Active = false });
            //buttonInfos.Add(new ButtonInfo { Color = Color.Gray, Text = "10", Active = false });
            int totalButtons = buttonInfos.Count();
            //Panel yeniden boyutlandığında kontrolleri güncelleyelim.
            Size controlSize = (new Buttons()).Size;  // Buttons user control'ünün varsayılan boyutunu alır.
            //PlaceUserControlsInPanel(pictureBox1, totalButtons, controlSize, buttonInfos);
            PlaceUserControlsInPanelControl2(flowLayoutPanel1, flowLayoutPanel2, totalButtons, controlSize, buttonInfos);
            AdjustImageToFit();
            UpdateButtonPositionsInPanel2();
        }
        #endregion

        #region HMI OUTPUT
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public Point Point1 {get;set;}

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public Point Point2 { get; set; }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public Point Point3 { get; set; }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public Point Point4 { get; set; }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public Point Point5 { get; set; }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public Point Point6 { get; set; }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public Point Point7 { get; set; }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public Point Point8 { get; set; }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public Point Point9 { get; set; }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public Point Point10 { get; set; }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public int referencebutton { get; set; }
        #endregion
        public ClickDragDrop()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            this.Load += new System.EventHandler(this.ClickDragDrop_Load);
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            pictureBox1.DoubleClick += pictureBox1_DoubleClick;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

            //foreach(var r1 in _Pixel)
            //{
               
            //}
        }
        #region UserControlButtonEvents
        private void MyButtons_ZoomWindowRequested(object sender, EventArgs e)
        {
            if (sender is Buttons btn)
            {
                
                currentZoomButton = btn;
                //currentZoomButton.CenterButtonText = btn.CenterButtonText;
            }
            else if (sender is CircularArrowButton arrow)
            {
                // Eğer arrow buton tıklanıyorsa, onun parent’ı Buttons türünde olmalı.
                currentZoomButton = arrow.Parent as Buttons;
            }

            if (originalImage == null) return;

            // Zoom penceresi açma işlemleri
            if (isExtraZoomActive)
            {
                isExtraZoomActive = false;

            }
            else
            {
                isExtraZoomActive = true;
                float pbCenterX = pictureBox1.Width / 2f;
                float pbCenterY = pictureBox1.Height / 2f;
                zoomPoint = new PointF(pbCenterX, pbCenterY);
            }

            // Eğer currentZoomButton set edildiyse, onun SelectionActive özelliğini true yapalım.
            if (currentZoomButton != null)
            {
                currentZoomButton.Invalidate();
                bool selection = currentZoomButton.SelectionActive;
                if (selection)
                    currentZoomButton.SelectionActive = false;
                else
                    currentZoomButton.SelectionActive = true;
            }
            pictureBox1.Invalidate();
        }

        private void MyButtons_ArrowButtonClicked(object sender, EventArgs e)
        {
            foreach (Control x in flowLayoutPanel1.Controls)
            {
                if (x is Buttons btn)
                {
                    var y = btn.CenterButtonCoordinates;
                    var z = btn.CrosshairPoint;
                    
                    var q1 = buttonInfos.Where(w => w.Text == btn.CenterButtonText).FirstOrDefault();
                    if (q1 != null)
                    {
                        q1.CenterButtonCoordinates = btn.CrosshairPoint;
                    }
                }
            }
            pictureBox1.Invalidate();
        }
        #endregion
        #region PictureBox
        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;

            float oldZoomFactor = zoomFactor;

            // Zoom In / Out
            if (e.Delta > 0)
                zoomFactor *= 1.1f; // Büyüt
            else if (e.Delta < 0)
                zoomFactor /= 1.1f; // Küçült

            // Zoom sınırlarını belirle: artık alt sınır minZoomFactor
            zoomFactor = Math.Max(minZoomFactor, Math.Min(zoomFactor, 5.0f));

            // **Ekstra zoom faktörünü güncelle**
            extraZoomFactor = zoomFactor * 2.0f;

            // Yeni zoom noktasını hesapla
            float relativeX = (e.X - zoomOffsetX) / oldZoomFactor;
            float relativeY = (e.Y - zoomOffsetY) / oldZoomFactor;

            zoomOffsetX = e.X - (relativeX * zoomFactor);
            zoomOffsetY = e.Y - (relativeY * zoomFactor);

            // Sınırlama
            ConstrainOffsets();

            pictureBox1.Invalidate(); // Yeniden çizim
        }
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //if (isZoomMoving)
            //{
            //    // **Ekstra zoom penceresini taşıma**
            //    float dx = e.X - lastMousePos.X;
            //    float dy = e.Y - lastMousePos.Y;
            //    zoomPoint = new PointF(zoomPoint.X + dx, zoomPoint.Y + dy);
            //    lastMousePos = e.Location;
            //    pictureBox1.Invalidate();
            //}
            //else if (isPanning)
            //{
            //    // **Resmi sürükleme**
            //    zoomOffsetX += e.X - lastMousePos.X;
            //    zoomOffsetY += e.Y - lastMousePos.Y;
            //    lastMousePos = e.Location;
            //    pictureBox1.Invalidate();
            //}

            if (isZoomMoving)
            {
                // **Ekstra zoom penceresini taşıma**
                float dx = e.X - lastMousePos.X;
                float dy = e.Y - lastMousePos.Y;
                zoomPoint = new PointF(zoomPoint.X + dx, zoomPoint.Y + dy);
                lastMousePos = e.Location;
                pictureBox1.Invalidate();
            }
            else if (isPanning)
            {
                zoomOffsetX += (e.X - lastMousePos.X);
                zoomOffsetY += (e.Y - lastMousePos.Y);
                lastMousePos = e.Location;

                // Sınırlama
                ConstrainOffsets();

                pictureBox1.Invalidate();
            }


        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("pis");
            if (originalImage == null) return;

            // Tıklanan noktanın resimdeki gerçek koordinatını bul
            float originalX = (e.X - zoomOffsetX) / zoomFactor;
            float originalY = (e.Y - zoomOffsetY) / zoomFactor;

            // İhtiyaç olursa TextBox veya MessageBox ile gösterebilirsiniz.
            // textBox1.Text = $"X = {originalX}, Y = {originalY}";
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isExtraZoomActive && IsMouseInZoomWindow(e.Location))
                {
                    // **Zoom penceresi sürükleme başlatıldı**
                    isZoomMoving = true;
                    lastMousePos = e.Location;
                }
                else
                {
                    // **Resmi sürükleme başlatıldı**
                    isPanning = true;
                    lastMousePos = e.Location;
                }
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isZoomMoving = false;
                isPanning = false;
            }

            if (isExtraZoomActive)
            {
                float originalX = (zoomPoint.X - zoomOffsetX) / zoomFactor;
                float originalY = (zoomPoint.Y - zoomOffsetY) / zoomFactor;


                if (currentZoomButton != null)
                {
                    PointF newCoordinates = new PointF(originalX, originalY);
                    currentZoomButton.CenterButtonCoordinates = newCoordinates;
                    // Artık CrosshairPoint özelliğini de güncelliyoruz:
                    currentZoomButton.CrosshairPoint = newCoordinates;

                    // Ayrıca, eğer ilgili ButtonInfo varsa onu da güncelleyebilirsiniz:
                    if (currentZoomButton != null)
                    {
                        currentZoomButton.CenterButtonCoordinates = newCoordinates;
                    }
                    // İşlem bitiminde, extra zoom penceresini kapat:
                    isExtraZoomActive = false;

                    // Tıklanan (currentZoomButton) butonun SelectionActive özelliğini false yapalım,
                    // böylece arka plan rengi eski haline dönecektir.
                    if (currentZoomButton != null)
                    {
                        currentZoomButton.SelectionActive = false;
                    }
                    // Son olarak, yeniden çizim yapalım:
                    var q1 = buttonInfos.Where(x => x.Text == currentZoomButton.ActiveControl.Text).FirstOrDefault();
                    if (q1 != null)
                    {
                        q1.CenterButtonCoordinates = newCoordinates;
                    }
                    pictureBox1.Invalidate();
                    
                }
            }
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            // Çift tıklamada resmi ilk haline getir (PictureBox'a tam sığan hal)
            AdjustImageToFit();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (originalImage == null)
                return;

            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            float newWidth = originalImage.Width * zoomFactor;
            float newHeight = originalImage.Height * zoomFactor;

            // Resmi çizelim:
            g.DrawImage(originalImage, new RectangleF(zoomOffsetX, zoomOffsetY, newWidth, newHeight));

            if (isExtraZoomActive)
            {
                DrawExtraZoomWindow(g);
            }

            // Şimdi, pictureBox1'in tüm child kontrollerini kontrol edelim:
            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is Buttons btn)
                {
                    // Eğer crosshair point ayarlanmışsa (Point.Empty değilse)
                    if (btn.CrosshairPoint != Point.Empty)
                    {
                        // Orijinal resim koordinatından pictureBox koordinatına dönüşüm:
                        int cx = (int)(btn.CrosshairPoint.X * zoomFactor + zoomOffsetX);
                        int cy = (int)(btn.CrosshairPoint.Y * zoomFactor + zoomOffsetY);

                        // Crosshair çizgi uzunluğu, örneğin buton boyutunun yarısı kadar:
                        int crossLength = btn.Width / 2;
                        using (Pen crossPen = new Pen(btn.CenterButtonColor, 1))
                        {
                            // Yatay çizgi
                            g.DrawLine(crossPen, cx - crossLength / 2, cy, cx + crossLength / 2, cy);
                            // Dikey çizgi
                            g.DrawLine(crossPen, cx, cy - crossLength / 2, cx, cy + crossLength / 2);
                        }
                    }
                }
            }
            foreach (Control ctrl in flowLayoutPanel2.Controls)
            {
                if (ctrl is Buttons btn)
                {
                    // Eğer crosshair point ayarlanmışsa (Point.Empty değilse)
                    if (btn.CrosshairPoint != Point.Empty)
                    {
                        // Orijinal resim koordinatından pictureBox koordinatına dönüşüm:
                        int cx = (int)(btn.CrosshairPoint.X * zoomFactor + zoomOffsetX);
                        int cy = (int)(btn.CrosshairPoint.Y * zoomFactor + zoomOffsetY);

                        // Crosshair çizgi uzunluğu, örneğin buton boyutunun yarısı kadar:
                        int crossLength = btn.Width / 2;
                        using (Pen crossPen = new Pen(btn.CenterButtonColor, 1))
                        {
                            // Yatay çizgi
                            g.DrawLine(crossPen, cx - crossLength / 2, cy, cx + crossLength / 2, cy);
                            // Dikey çizgi
                            g.DrawLine(crossPen, cx, cy - crossLength / 2, cx, cy + crossLength / 2);
                        }
                    }
                }
            }
            //UpdateIndicatorButtonsLocation(indicatorOk, indicatorCancel);
        }
        private void AdjustImageToFit()
        {
            if (originalImage == null || pictureBox1.Width == 0 || pictureBox1.Height == 0)
                return;

            float scaleX = (float)pictureBox1.Width / originalImage.Width;
            float scaleY = (float)pictureBox1.Height / originalImage.Height;

            // **En uygun zoom faktörünü belirle (orantılı küçültme veya büyütme)**
            zoomFactor = Math.Min(scaleX, scaleY);

            // Bu zoomFactor "ilk sığdırma" olduğu için minZoomFactor olarak saklıyoruz
            minZoomFactor = zoomFactor;

            // **Resmin ortada başlaması için offset hesapla**
            float newWidth = originalImage.Width * zoomFactor;
            float newHeight = originalImage.Height * zoomFactor;

            zoomOffsetX = (pictureBox1.Width - newWidth) / 2f;
            zoomOffsetY = (pictureBox1.Height - newHeight) / 2f;

            pictureBox1.Invalidate(); // Yeniden çizim
        }
        private void DrawExtraZoomWindow(Graphics g)
        {
            if (originalImage == null) return;

            // Zoom penceresinin merkezini belirle
            float sourceX = (zoomPoint.X - zoomOffsetX) / zoomFactor;
            float sourceY = (zoomPoint.Y - zoomOffsetY) / zoomFactor;

            // Extra zoom alanını belirle
            float zoomedWidth = zoomWindowSize / extraZoomFactor;
            float zoomedHeight = zoomWindowSize / extraZoomFactor;

            RectangleF sourceRect = new RectangleF(
                sourceX - zoomedWidth / 2,
                sourceY - zoomedHeight / 2,
                zoomedWidth,
                zoomedHeight);

            RectangleF destRect = new RectangleF(
                zoomPoint.X - zoomWindowSize / 2,
                zoomPoint.Y - zoomWindowSize / 2,
                zoomWindowSize,
                zoomWindowSize);

            // Daire biçiminde klip uygulayarak çizelim
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(destRect);
                g.SetClip(path);
                g.DrawImage(originalImage, destRect, sourceRect, GraphicsUnit.Pixel);
                g.ResetClip();
            }

            // Daire çerçevesi
            g.DrawEllipse(Pens.Black, destRect);

            // Nişangah (Crosshair) çizimi
            using (Pen crosshairPen = new Pen(Color.Red, 2))
            {
                float centerX = zoomPoint.X;
                float centerY = zoomPoint.Y;
                float lineLength = zoomWindowSize / 8;
                float gapSize = lineLength / 3;

                // Yatay çizgi (Ortada boşluk)
                g.DrawLine(crosshairPen, centerX - lineLength, centerY, centerX - gapSize, centerY);
                g.DrawLine(crosshairPen, centerX + gapSize, centerY, centerX + lineLength, centerY);

                // Dikey çizgi (Ortada boşluk)
                g.DrawLine(crosshairPen, centerX, centerY - lineLength, centerX, centerY - gapSize);
                g.DrawLine(crosshairPen, centerX, centerY + gapSize, centerX, centerY + lineLength);
            }
        }
        // **Mouse'un ekstra zoom alanında olup olmadığını kontrol eden metod**
        private bool IsMouseInZoomWindow(Point mousePosition)
        {
            int radius = zoomWindowSize / 2;
            int dx = mousePosition.X - (int)zoomPoint.X;
            int dy = mousePosition.Y - (int)zoomPoint.Y;

            return (dx * dx + dy * dy) <= (radius * radius);
        }
        // 1) Clamp metodu
        private float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        // 2) Offsets'i sınırlayan metot
        private void ConstrainOffsets()
        {
            if (originalImage == null) return;

            float newWidth = originalImage.Width * zoomFactor;
            float newHeight = originalImage.Height * zoomFactor;

            // X ekseni
            if (newWidth >= pictureBox1.Width)
            {
                zoomOffsetX = Clamp(zoomOffsetX,
                                    pictureBox1.Width - newWidth,
                                    0);
            }
            else
            {
                // Resim, PictureBox'tan küçükse ortalayalım
                zoomOffsetX = (pictureBox1.Width - newWidth) / 2f;
            }

            // Y ekseni
            if (newHeight >= pictureBox1.Height)
            {
                zoomOffsetY = Clamp(zoomOffsetY,
                                    pictureBox1.Height - newHeight,
                                    0);
            }
            else
            {
                // Ortala
                zoomOffsetY = (pictureBox1.Height - newHeight) / 2f;
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            AddButtons();
            //AddIndicatorButtons();
            //UpdateIndicatorButtonsLocation(indicatorOk, indicatorCancel);
        }

        #region Pasif Edildi
        /// <summary>
        /// Verilen panel içine, sol ve sağ kenarlara toplam totalControlCount adet (çift sayı) MyUserControl ekler.
        /// Örneğin, totalControlCount = 4 gönderilirse, sol tarafta 2 ve sağ tarafta 2 adet eklenir.
        /// Kontroller, panel yüksekliğine göre eşit aralıklarla yerleştirilir.
        /// </summary>
        /// <param name="panel">UserControl'lerin ekleneceği panel</param>
        /// <param name="totalControlCount">Toplam eklenecek kontrol sayısı (çift sayı olmalı)</param>
        /// <param name="controlSize">Eklenecek her bir MyUserControl'ün boyutu</param>
        private void PlaceUserControlsInPanel(PictureBox panel, int totalControlCount, Size controlSize, List<ButtonInfo> infoList)
        {
            if (panel == null || totalControlCount <= 0)
                return;
            if (totalControlCount % 2 != 0)
            {
                MessageBox.Show("Toplam kontrol sayısı çift olmalı.");
                return;
            }
            if (infoList == null || infoList.Count != totalControlCount)
            {
                MessageBox.Show("ButtonInfo listesi toplam kontrol sayısına eşit olmalıdır.");
                return;
            }

            int sideCount = totalControlCount / 2;
            // Eski kontrolleri temizleyelim.
            panel.Controls.Clear();

            int panelHeight = panel.Height;
            int panelWidth = panel.Width;
            // Dikeyde eklenen kontrollerin toplam yüksekliği
            int totalControlsHeight = sideCount * controlSize.Height;
            // Aralarındaki eşit boşluk:
            int spacing = (panelHeight - totalControlsHeight) / (sideCount + 1);
            if (spacing < 0) spacing = 0;

            // Kenar marginleri (sol ve sağ)
            int leftMargin = 10;
            int rightMargin = 10;

            for (int i = 0; i < sideCount; i++)
            {
                // Her kontrolün üstten konumu: boşluk * (i+1) + (kontrol yüksekliği * i)
                int top = spacing * (i + 1) + i * controlSize.Height;

                // Sol tarafta Buttons ekleyelim.
                Buttons ucLeft = new Buttons();
                ucLeft.Size = controlSize;
                ucLeft.Location = new Point(leftMargin, top);
                // Sol taraftaki bilgi, listenin indeks i'sinden gelecek.
                ucLeft.CenterButtonColor = infoList[i].Color;
                ucLeft.CenterButtonText = infoList[i].Text;
                ucLeft.ZoomWindowRequested += MyButtons_ZoomWindowRequested;
                ucLeft.ReferenceSelected += ChildButton_ReferenceSelected;
                ucLeft.ArrowButtonClicked += MyButtons_ArrowButtonClicked;
                panel.Controls.Add(ucLeft);

                // Sağ tarafta Buttons ekleyelim.
                Buttons ucRight = new Buttons();
                ucRight.Size = controlSize;
                int rightX = panelWidth - controlSize.Width - rightMargin;
                ucRight.Location = new Point(rightX, top);
                // Sağ taraftaki bilgi, listenin indeks (i + sideCount)'tan gelecek.
                ucRight.CenterButtonColor = infoList[i + sideCount].Color;
                ucRight.CenterButtonText = infoList[i + sideCount].Text;
                ucRight.ZoomWindowRequested += MyButtons_ZoomWindowRequested;
                ucRight.ReferenceSelected += ChildButton_ReferenceSelected;
                ucRight.ArrowButtonClicked += MyButtons_ArrowButtonClicked;
                panel.Controls.Add(ucRight);
            }
        }
        #endregion
        private void UpdateButtonPositionsInPanel2()
        {
            int gap = 10; // Butonlar arasındaki boşluk
            int totalWidth = btn_ok.Width + btn_cancel.Width + gap;
            int leftMargin = (panel2.ClientSize.Width - totalWidth) / 2;

            // Yalnızca X koordinatlarını güncelliyoruz, Y konumları sabit kalır.
            btn_ok.Location = new Point(leftMargin, btn_ok.Location.Y);
            btn_cancel.Location = new Point(leftMargin + btn_ok.Width + gap, btn_cancel.Location.Y);
        }
        private void ChildButton_ReferenceSelected(object sender, EventArgs e)
        {
            // Tıklanan (ve referans olarak seçilen) Buttons kontrolünü alalım:
            Buttons selectedButton = sender as Buttons;
            if (selectedButton == null)
                return;

            // Container olarak butonları eklediğiniz control (örneğin pictureBox1 veya panel1) üzerinden
            // tüm Buttons örneklerini kontrol edip, sender dışındakileri unchecked yapalım:
            foreach (Control ctrl in flowLayoutPanel1.Controls)  // ya da panel1.Controls
            {
                if (ctrl is Buttons btn && btn != selectedButton)
                {
                    btn.IsReferenceSelected = false;
                    var q1 = buttonInfos.Where(x => x.Text == btn.CenterButtonText).FirstOrDefault();
                    if (q1 != null)
                        q1.ReferenceButton = 0;
                }
                else if(ctrl is Buttons btn2 && btn2 == selectedButton)
                {
                    var q1 = buttonInfos.Where(x=> x.Text == btn2.CenterButtonText).FirstOrDefault();
                    if (q1 != null)
                        q1.ReferenceButton = 1;
                }
            }
            foreach (Control ctrl in flowLayoutPanel2.Controls)  // ya da panel1.Controls
            {
                if (ctrl is Buttons btn && btn != selectedButton)
                {
                    btn.IsReferenceSelected = false;
                    var q1 = buttonInfos.Where(x => x.Text == btn.CenterButtonText).FirstOrDefault();
                    if (q1 != null)
                        q1.ReferenceButton = 0;
                }
                else if (ctrl is Buttons btn2 && btn2 == selectedButton)
                {
                    referencebutton = Convert.ToInt32(btn2.CenterButtonText);
                    var q1 = buttonInfos.Where(x => x.Text == btn2.CenterButtonText).FirstOrDefault();
                    if (q1 != null)
                        q1.ReferenceButton = 1;
                }
            }
        }
        private void PlaceUserControlsInPanelControl2(FlowLayoutPanel panelControl2Left, Panel panelControl2Right, int totalControlCount, Size controlSize, List<ButtonInfo> infoList)
        {
            // Sadece panelControl2'deki kontrolleri temizleyelim; FlowLayoutPanel'lerdeki diğer kontroller silinmesin.
            panelControl2Left.Controls.Clear();
            panelControl2Left.Controls.Add(btn_Left_Slider);

            panelControl2Right.Controls.Clear();
            panelControl2Right.Controls.Add(btn_Right_Slider);

            int half = totalControlCount / 2;

            // İlk yarıdaki butonları sol panelControl2'ye ekleyelim.
            for (int i = 0; i < half; i++)
            {
                Buttons btn = new Buttons();
                btn.Size = controlSize;
                btn.IsActive = infoList[i].Active;
                btn.CenterButtonColor = infoList[i].Color;
                btn.CenterButtonText = infoList[i].Text;
                btn.ZoomWindowRequested += MyButtons_ZoomWindowRequested;
                btn.ReferenceSelected += ChildButton_ReferenceSelected;
                btn.ArrowButtonClicked += MyButtons_ArrowButtonClicked;

                panelControl2Left.Controls.Add(btn);
            }

            // İkinci yarıdaki butonları sağ panelControl2'ye ekleyelim.
            for (int i = half; i < totalControlCount; i++)
            {
                Buttons btn = new Buttons();
                btn.Size = controlSize;
                btn.IsActive = infoList[i].Active;
                btn.CenterButtonColor = infoList[i].Color;
                btn.CenterButtonText = infoList[i].Text;
                btn.ZoomWindowRequested += MyButtons_ZoomWindowRequested;
                btn.ReferenceSelected += ChildButton_ReferenceSelected;
                btn.ArrowButtonClicked += MyButtons_ArrowButtonClicked;

                panelControl2Right.Controls.Add(btn);
            }
        }

        #endregion
        #region Panel Buton İŞlemleri
        public class ButtonInfo
        {
            public Color Color { get; set; }
            public string Text { get; set; }
            public int Tag { get; set; }
            public bool Active { get; set; }
            public PointF CenterButtonCoordinates { get; set; }
            public int ReferenceButton { get; set; }
        }
        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
      
            originalImage = pictureBox1.Image;
            AddButtons();
            timer1.Enabled = false;
        }

        private void btn_Left_Slider_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Width > 60)
            {
                flowLayoutPanel1.Width = 60;
                btn_Left_Slider.Image = Resources.Arrow_Right50X50;
                foreach (Control ctrl in flowLayoutPanel1.Controls)
                {
                    if (ctrl is Buttons btn)
                    {
                        btn.Visible = false;
                    }
                }
            }
            else
            {
                flowLayoutPanel1.Width = 206;
                btn_Left_Slider.Image = Resources.Arrow_Left50X50;
                foreach (Control ctrl in flowLayoutPanel1.Controls)
                {
                    if (ctrl is Buttons btn)
                    {
                        btn.Visible = true;
                    }
                }
            }
        }

        private void btn_Right_Slider_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel2.Width > 60)
            {
                flowLayoutPanel2.Width = 60;
                btn_Right_Slider.Image = Resources.Arrow_Left50X50;
                foreach (Control ctrl in flowLayoutPanel2.Controls)
                {
                    if (ctrl is Buttons btn)
                    {
                        btn.Visible = false;
                    }
                }
            }
            else
            {
                flowLayoutPanel2.Width = 206;
                btn_Right_Slider.Image = Resources.Arrow_Right50X50;
                foreach (Control ctrl in flowLayoutPanel2.Controls)
                {
                    if (ctrl is Buttons btn)
                    {
                        btn.Visible = true;
                    }
                }
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < buttonInfos.Count; i++)
            {
                //if (i == 0)
                //{
                //    PointF centerPointF = buttonInfos[0].CenterButtonCoordinates;
                //    Point point = new Point((int)(centerPointF.X * 1000), (int)(centerPointF.Y * 1000));
                //    Point1 = point;
                //}
                //else if( i == 1)
                //{
                //    PointF centerPointF = buttonInfos[1].CenterButtonCoordinates;
                //    Point point = new Point((int)(centerPointF.X * 1000), (int)(centerPointF.Y * 1000));
                //    Point2 = point;
                //}

                PointF centerPointF = buttonInfos[i].CenterButtonCoordinates;
                Point point = new Point((int)(centerPointF.X * 1000), (int)(centerPointF.Y * 1000));

                if (i == 0) Point1 = point;
                else if (i == 1) Point2 = point;
                else if (i == 2) Point3 = point;
                else if (i == 3) Point4 = point;
                else if (i == 4) Point5 = point;
                else if (i == 5) Point6 = point;
                else if (i == 6) Point7 = point;
                else if (i == 7) Point8 = point;
                else if (i == 8) Point9 = point;
                else if (i == 9) Point10 = point;
            }
            referencebutton = Convert.ToInt32(buttonInfos.Where(x => x.ReferenceButton == 1).FirstOrDefault().Text);
        }

        private void ClickDragDrop_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
