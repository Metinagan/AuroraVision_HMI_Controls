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

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(CameraContol), "Icons.arrows.png")]
    public partial class Buttons : UserControl
    {
        private Button centerButton;
        private CheckBox referenceCheckBox; // Eklenen küçük checkbox
        private CircularArrowButton arrowUp, arrowDown, arrowLeft, arrowRight;


        public event EventHandler ZoomWindowRequested;
        public event EventHandler ArrowButtonClicked;

        // Dışarıdan ayarlanabilir property'ler:
        public Color CenterButtonColor { get; set; } = Color.Blue;
        public string CenterButtonText { get; set; } = "Click";
        public int CenterButtonTag { get; set; }
        public PointF CenterButtonCoordinates { get; set; } = new Point(0, 0);
        public PointF CrosshairPoint { get; set; } = Point.Empty;
        public event EventHandler ReferenceSelected;
        private bool _selectionActive = false;
        private Color _originalBackColor;
        public bool _isActive;  // Varsayılan aktif
        public bool IsActive
        {

            get { return _isActive; }
            set
            {
                _isActive = value;
                // Merkez buton, arrow butonları ve diğer alt kontrollerin Enabled özelliğini güncelleyelim:
                if (centerButton != null) centerButton.Enabled = _isActive;
                if (arrowUp != null) arrowUp.Enabled = _isActive;
                if (arrowDown != null) arrowDown.Enabled = _isActive;
                if (arrowLeft != null) arrowLeft.Enabled = _isActive;
                if (arrowRight != null) arrowRight.Enabled = _isActive;
                if (referenceCheckBox != null) referenceCheckBox.Enabled = _isActive;
                // Visual feedback için belki arka plan renginde de değişiklik yapabilirsiniz.
                if (!_isActive)
                {
                    Bitmap bmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    bmp.SetPixel(0, 0, Color.FromArgb(77, Color.DarkGray));
                    this.BackgroundImage = bmp;
                }
                this.Invalidate();
            }
        }

        // Yeni eklenen property: arrow butonları için boşluk (gap) değeri
        public int ArrowGap { get; set; } = 5;  // Örneğin, 5 piksel boşluk


        public bool IsReferenceSelected
        {
            get { return referenceCheckBox?.Checked ?? false; }
            set { if (referenceCheckBox != null) referenceCheckBox.Checked = value; }
        }
        // Yeni: Butonla ilişkilendirilecek ButtonInfo nesnesi (opsiyonel)
        //public ButtonInfo Info { get; set; }



        public Buttons()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint, true);
            this.Load += Buttons_Load;
            this.Resize += Buttons_Resize;

            _originalBackColor = Color.FromArgb(77, Color.White);
        }

        private void Buttons_Load(object sender, EventArgs e)
        {
            AddCenterButton();
            AddArrowButtons();
            InitializeReferenceCheckBox();
        }

        private void Buttons_Resize(object sender, EventArgs e)
        {
            if (centerButton != null)
            {
                centerButton.Location = new Point((this.Width - centerButton.Width) / 2,
                                                  (this.Height - centerButton.Height) / 2);
            }
            // CheckBox'ı sağ alt köşeye sabitleyelim:
            if (referenceCheckBox != null)
            {
                referenceCheckBox.Location = new Point(this.Width - referenceCheckBox.Width - 2,
                                                        this.Height - referenceCheckBox.Height - 2);
            }

            PositionArrowButtons();
        }

        /// <summary>
        /// Dört adet yön butonunu (ok şeklinde) oluşturup, UserControl'ün etrafına yerleştirir.
        /// </summary>
        private void AddArrowButtons()
        {
            arrowUp = CreateCircularArrowButton("↑", IsActive);
            arrowDown = CreateCircularArrowButton("↓", IsActive);
            arrowLeft = CreateCircularArrowButton("←", IsActive);
            arrowRight = CreateCircularArrowButton("→", IsActive);

            this.Controls.Add(arrowUp);
            this.Controls.Add(arrowDown);
            this.Controls.Add(arrowLeft);
            this.Controls.Add(arrowRight);

            PositionArrowButtons();
        }
        private CircularArrowButton CreateCircularArrowButton(string arrowText, bool isActive)
        {
            CircularArrowButton btn = new CircularArrowButton();
            btn.Text = arrowText;
            btn.Size = new Size(50, 50);
            // Arrow butonları için örneğin arka plan rengi olarak CenterButtonColor'nun tonunun biraz daha açık bir versiyonunu kullanabilirsiniz.
            btn.BackColor = Color.White;
            btn.ForeColor = GetContrastingColor(btn.BackColor);
            btn.Font = new Font(btn.Font.FontFamily, 8, FontStyle.Bold);
            btn.Enabled = isActive;
            // Border ayarları vs. circular button'un OnPaint'ında hallediliyor.
            btn.Click += (s, e) =>
            {
                // Arrow butonlarının click event handler'larını ayrı ayrı tanımlayalım.
                // (Bu event handler'lar, Buttons içinde ayrı metodlarda tanımlanacaktır.)
                arrowUp.Click += ArrowUp_Click;
                arrowDown.Click += ArrowDown_Click;
                arrowLeft.Click += ArrowLeft_Click;
                arrowRight.Click += ArrowRight_Click;
            };
            return btn;
        }

        /// <summary>
        /// Arrow butonlarının konumunu, merkezi buton etrafında ayarlar.
        /// </summary>
        private void PositionArrowButtons()
        {
            if (centerButton == null) return;
            int gap = ArrowGap; // Ek boşluk

            int centerX = centerButton.Left + centerButton.Width / 2;
            int centerY = centerButton.Top + centerButton.Height / 2;

            // Yukarı ok: merkez butonun üstünde, gap kadar boşluk eklenmiş.
            arrowUp.Location = new Point(centerX - arrowUp.Width / 2, centerButton.Top - arrowUp.Height - gap);
            // Aşağı ok: merkez butonun altında
            arrowDown.Location = new Point(centerX - arrowDown.Width / 2, centerButton.Bottom + gap);
            // Sol ok: merkez butonun solunda
            arrowLeft.Location = new Point(centerButton.Left - arrowLeft.Width - gap, centerY - arrowLeft.Height / 2);
            // Sağ ok: merkez butonun sağında
            arrowRight.Location = new Point(centerButton.Right + gap, centerY - arrowRight.Height / 2);

        }

        // Ok butonlarının Click event handler'ları:
        private void ArrowUp_Click(object sender, EventArgs e)
        {
            // Yukarı ok: y koordinatı 0.01 azaltılsın.
            CenterButtonCoordinates = new PointF(CenterButtonCoordinates.X, CenterButtonCoordinates.Y - 0.01f);
            CrosshairPoint = new PointF(CenterButtonCoordinates.X, CenterButtonCoordinates.Y - 0.01f);
            ArrowButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void ArrowDown_Click(object sender, EventArgs e)
        {
            // Aşağı ok: y koordinatı 0.01 artırılsın.
            CenterButtonCoordinates = new PointF(CenterButtonCoordinates.X, CenterButtonCoordinates.Y + 0.01f);
            CrosshairPoint = new PointF(CenterButtonCoordinates.X, CenterButtonCoordinates.Y + 0.01f);
            ArrowButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void ArrowRight_Click(object sender, EventArgs e)
        {
            // Sağ ok: x koordinatı 0.01 artırılsın.
            CenterButtonCoordinates = new PointF(CenterButtonCoordinates.X + 0.01f, CenterButtonCoordinates.Y);
            CrosshairPoint = new PointF(CenterButtonCoordinates.X + 0.01f, CenterButtonCoordinates.Y);
            ArrowButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void ArrowLeft_Click(object sender, EventArgs e)
        {
            // Sol ok: x koordinatı 0.01 azaltılsın.
            CenterButtonCoordinates = new PointF(CenterButtonCoordinates.X - 0.01f, CenterButtonCoordinates.Y);
            CrosshairPoint = new PointF(CenterButtonCoordinates.X - 0.01f, CenterButtonCoordinates.Y);
            ArrowButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Merkezde dairesel butonu oluşturur ve UserControl'e ekler.
        /// </summary>
        private void AddCenterButton()
        {
            centerButton = CreateCircularButton(CenterButtonText, new Size(50, 50), CenterButtonColor, IsActive, CenterButtonTag);
            centerButton.Location = new Point((this.Width - centerButton.Width) / 2,
                                              (this.Height - centerButton.Height) / 2);
            this.Controls.Add(centerButton);
        }

        /// <summary>
        /// Butonun sağ alt köşesine küçük bir "Referans" checkbox ekler.
        /// </summary>
        private void InitializeReferenceCheckBox()
        {
            referenceCheckBox = new CheckBox();
            referenceCheckBox.Text = "Ref.";
            referenceCheckBox.AutoSize = true;
            referenceCheckBox.Font = new Font(this.Font.FontFamily, 8, FontStyle.Bold);
            referenceCheckBox.BackColor = Color.Transparent;
            referenceCheckBox.CheckedChanged += ReferenceCheckBox_CheckedChanged;
            // İlk konumlandırma (Resize olayında da güncellenecek):
            referenceCheckBox.Location = new Point(this.Width - referenceCheckBox.Width - 2 + 60,
                                                    this.Height - referenceCheckBox.Height - 2);
            referenceCheckBox.Enabled = _isActive;
            this.Controls.Add(referenceCheckBox);
            referenceCheckBox.BringToFront();
        }

        private void ReferenceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // CheckBox işaretlendiyse, örneğin CenterButtonTag değerini veya Info nesnesini güncelleyebilirsiniz.
            if (referenceCheckBox.Checked)
            {
                ReferenceSelected?.Invoke(this, EventArgs.Empty);
                //// Örneğin, referans olarak işaretlendiğini göstermek için CenterButtonTag'ı 1 yapalım.
                //CenterButtonTag = 1;
                ////if (Info != null)
                ////{
                ////    Info.Tag = 1;
                ////}
            }
            else
            {
                //// İşaret kaldırıldıysa
                //CenterButtonTag = 0;
                ////if (Info != null)
                ////{
                ////    Info.Tag = 0;
                ////}
            }
            // İsterseniz bu değişiklikle butonun görünümünü de güncelleyebilirsiniz.
        }

        /// <summary>
        /// Verilen parametrelere göre dairesel bir buton oluşturur.
        /// </summary>
        /// <param name="text">Buton üzerindeki yazı</param>
        /// <param name="size">Butonun boyutu</param>
        /// <param name="backColor">Butonun arka plan rengi</param>
        /// <param name="isActive">Buton aktiflik durumu</param>
        /// <param name="tag">Buton Tag değeri</param>
        /// <returns>Oluşturulan dairesel Button</returns>
        private Button CreateCircularButton(string text, Size size, Color backColor, bool isActive, int tag)
        {
            CircularButton btn = new CircularButton();
            btn.Text = text;
            btn.Size = size;
            btn.Tag = tag;
            btn.Enabled = isActive;
            btn.BackColor = backColor;
            btn.ForeColor = GetContrastingColor(backColor);
            btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size, FontStyle.Bold);

            btn.Click += (s, e) =>
            {
                if (!isActive) return;
                CircularButton clickedButton = s as CircularButton;
                if (clickedButton != null)
                {
                    // Örneğin, debug mesajı:
                    //MessageBox.Show($"Butona tıklandı: {text}");
                    // ZoomWindowRequested event'ini tetikleyelim.
                    ZoomWindowRequested?.Invoke(this, EventArgs.Empty);
                }
            };

            return btn;
        }

        /// <summary>
        /// Verilen arka plan rengine göre kontrast metin rengini döner.
        /// </summary>
        private Color GetContrastingColor(Color color)
        {
            return color.GetBrightness() < 0.5 ? Color.White : Color.Black;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeBackground();
            //Bitmap bmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //bmp.SetPixel(0, 0, Color.FromArgb(77, Color.White));
            //this.BackgroundImage = bmp;
            //this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        // Diğer mevcut CircularButton sınıfı ve ilgili kodlar...
        public class CircularButton : Button
        {
            private Image smoothCircleImage;

            public CircularButton()
            {
                this.FlatStyle = FlatStyle.Flat;
                this.FlatAppearance.BorderSize = 0;
                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                              ControlStyles.OptimizedDoubleBuffer |
                              ControlStyles.UserPaint, true);
                this.Resize += CircularButton_Resize;
            }

            private void CircularButton_Resize(object sender, EventArgs e)
            {
                UpdateRegion();
                CreateSmoothCircleImage();
                Invalidate();
            }

            protected override void OnBackColorChanged(EventArgs e)
            {
                base.OnBackColorChanged(e);
                CreateSmoothCircleImage();
                Invalidate();
            }

            private void UpdateRegion()
            {
                int size = Math.Min(this.Width, this.Height);
                this.Width = this.Height = size;
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddEllipse(0, 0, size, size);
                    this.Region = new Region(gp);
                }
            }

            private void CreateSmoothCircleImage()
            {
                int diameter = this.Width;
                if (diameter <= 0)
                    return;

                int scaleFactor = 4;
                int highResDiameter = diameter * scaleFactor;

                int borderThickness = 5;
                int highResBorderThickness = borderThickness * scaleFactor;

                Bitmap bmpHighRes = new Bitmap(highResDiameter, highResDiameter, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(bmpHighRes))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (GraphicsPath outerPath = new GraphicsPath())
                    {
                        outerPath.AddEllipse(0, 0, highResDiameter, highResDiameter);
                        using (SolidBrush borderBrush = new SolidBrush(Color.Gray))
                        {
                            g.FillPath(borderBrush, outerPath);
                        }
                    }

                    Rectangle innerRect = new Rectangle(
                        highResBorderThickness,
                        highResBorderThickness,
                        highResDiameter - 2 * highResBorderThickness,
                        highResDiameter - 2 * highResBorderThickness);

                    using (GraphicsPath innerPath = new GraphicsPath())
                    {
                        innerPath.AddEllipse(innerRect);
                        g.SetClip(innerPath);
                        using (SolidBrush innerBrush = new SolidBrush(this.BackColor))
                        {
                            g.FillRectangle(innerBrush, 0, 0, highResDiameter, highResDiameter);
                        }
                        g.ResetClip();
                    }
                }

                Image smoothImage = new Bitmap(diameter, diameter);
                using (Graphics g = Graphics.FromImage(smoothImage))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(bmpHighRes, 0, 0, diameter, diameter);
                }
                if (smoothCircleImage != null)
                    smoothCircleImage.Dispose();
                smoothCircleImage = smoothImage;
                bmpHighRes.Dispose();
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                pevent.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                pevent.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                RectangleF ellipseRect = new RectangleF(0, 0, this.Width, this.Height);

                if (smoothCircleImage != null)
                {
                    pevent.Graphics.DrawImage(smoothCircleImage, ellipseRect);
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(this.BackColor))
                    {
                        pevent.Graphics.FillEllipse(brush, ellipseRect);
                    }
                }

                using (GraphicsPath path = new GraphicsPath())
                {
                    RectangleF borderRect = new RectangleF(1f, 1f, this.Width - 2f, this.Height - 2f);
                    path.AddEllipse(borderRect);
                    using (Pen borderPen = new Pen(Color.Gray, 5))
                    {
                        borderPen.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawPath(borderPen, path);
                    }
                }

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
                {
                    pevent.Graphics.DrawString(this.Text, this.Font, textBrush, ellipseRect, sf);
                }
            }
        }
        /// <summary>
        /// Indicator'ın (örneğin indicatorPB'nin) visible değerini, CenterButtonCoordinates değeri üzerinden günceller.
        /// Eğer koordinatlar (0,0)'dan farklı ise indicator görünür, aksi halde gizli olur.
        /// </summary>
        public void UpdateIndicatorVisibility()
        {
            // CenterButtonCoordinates'nin boş olup olmadığını kontrol ediyoruz.
            if (CenterButtonCoordinates != PointF.Empty)
            {
                indicatorPB.Visible = true;
            }
            else
            {
                indicatorPB.Visible = false;
            }
            // Gerekirse, kontrolü yeniden çizmek için:
            this.Invalidate();
        }
        public class CircularArrowButton : Button
        {
            private Image smoothCircleImage;

            public CircularArrowButton()
            {
                this.FlatStyle = FlatStyle.Flat;
                this.FlatAppearance.BorderSize = 0;
                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                              ControlStyles.OptimizedDoubleBuffer |
                              ControlStyles.UserPaint, true);
                this.Resize += CircularButton_Resize;
            }

            private void CircularButton_Resize(object sender, EventArgs e)
            {
                UpdateRegion();
                CreateSmoothCircleImage();
                Invalidate();
            }

            protected override void OnBackColorChanged(EventArgs e)
            {
                base.OnBackColorChanged(e);
                CreateSmoothCircleImage();
                Invalidate();
            }

            private void UpdateRegion()
            {
                int size = Math.Min(this.Width, this.Height);
                this.Width = this.Height = size;
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddEllipse(0, 0, size, size);
                    this.Region = new Region(gp);
                }
            }

            private void CreateSmoothCircleImage()
            {
                int diameter = this.Width;
                if (diameter <= 0)
                    return;

                int scaleFactor = 4;
                int highResDiameter = diameter * scaleFactor;

                int borderThickness = 5;
                int highResBorderThickness = borderThickness * scaleFactor;

                Bitmap bmpHighRes = new Bitmap(highResDiameter, highResDiameter, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(bmpHighRes))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    using (GraphicsPath outerPath = new GraphicsPath())
                    {
                        outerPath.AddEllipse(0, 0, highResDiameter, highResDiameter);
                        using (SolidBrush borderBrush = new SolidBrush(Color.Gray))
                        {
                            g.FillPath(borderBrush, outerPath);
                        }
                    }

                    Rectangle innerRect = new Rectangle(
                        highResBorderThickness,
                        highResBorderThickness,
                        highResDiameter - 2 * highResBorderThickness,
                        highResDiameter - 2 * highResBorderThickness);

                    using (GraphicsPath innerPath = new GraphicsPath())
                    {
                        innerPath.AddEllipse(innerRect);
                        g.SetClip(innerPath);
                        using (SolidBrush innerBrush = new SolidBrush(this.BackColor))
                        {
                            g.FillRectangle(innerBrush, 0, 0, highResDiameter, highResDiameter);
                        }
                        g.ResetClip();
                    }
                }

                Image smoothImage = new Bitmap(diameter, diameter);
                using (Graphics g = Graphics.FromImage(smoothImage))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(bmpHighRes, 0, 0, diameter, diameter);
                }
                if (smoothCircleImage != null)
                    smoothCircleImage.Dispose();
                smoothCircleImage = smoothImage;
                bmpHighRes.Dispose();
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                pevent.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                pevent.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                RectangleF ellipseRect = new RectangleF(0, 0, this.Width, this.Height);

                if (smoothCircleImage != null)
                {
                    pevent.Graphics.DrawImage(smoothCircleImage, ellipseRect);
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(Color.White))
                    {
                        pevent.Graphics.FillEllipse(brush, ellipseRect);
                    }
                }

                //using (GraphicsPath path = new GraphicsPath())
                //{
                //    RectangleF borderRect = new RectangleF(1f, 1f, this.Width - 2f, this.Height - 2f);
                //    path.AddEllipse(borderRect);
                //    using (Pen borderPen = new Pen(Color.Gray, 5))
                //    {
                //        borderPen.Alignment = PenAlignment.Inset;
                //        pevent.Graphics.DrawPath(borderPen, path);
                //    }
                //}

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
                {
                    pevent.Graphics.DrawString(this.Text, this.Font, textBrush, ellipseRect, sf);
                }

                if (this.Parent is Buttons parentButtons)
                {
                    parentButtons.UpdateIndicatorVisibility(); // Bu metodu public veya internal yapmanız gerekebilir.
                }
            }
        }
        /// <summary>
        /// Bu property true ise, buton seçili (aktif) kabul edilir.
        /// Setter'da arka plan rengi güncellenir.
        /// </summary>
        public bool SelectionActive
        {
            get { return _selectionActive; }
            set
            {
                _selectionActive = value;
                if (_selectionActive)
                {
                    // Seçili olduğunda arka plan rengi değişsin:
                    //this.BackColor = Color.LightGreen; // veya istediğiniz başka bir renk
                    // Eğer BackgroundImage kullanıyorsanız, onu da güncelleyebilirsiniz:
                    Bitmap bmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    bmp.SetPixel(0, 0, Color.FromArgb(77, Color.LightGreen));
                    this.BackgroundImage = bmp;
                }
                else
                {
                    this.BackgroundImage.Dispose();
                    InitializeBackground();
                    //// Seçim kaldırıldığında orijinal rengine dönsün:
                    //Bitmap bmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                    //bmp.SetPixel(0, 0, Color.FromArgb(77, Color.White));
                    //this.BackgroundImage = bmp;
                }
                this.Invalidate();
            }
        }
        private void InitializeBackground()
        {
            if (_isActive)
            {
                Bitmap bmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                bmp.SetPixel(0, 0, Color.FromArgb(77, Color.White));
                this.BackgroundImage = bmp;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                Bitmap bmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                bmp.SetPixel(0, 0, Color.FromArgb(77, Color.Black));
                this.BackgroundImage = bmp;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
    }

}
