using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(CameraContol),"Icons.3d-camera.png")]
    [Description("Moves the camera.")]
    public partial class CameraContol : UserControl
    {
        //İnput - Output
        private Image _inputImage;
        private float _outputX;
        private float _outputY;

        //Zoom
        private const float MAX_ZOOM = 10.0f;
        private bool _isPanning = false;
        private Point _lastPanPoint;
        private float _currentScale = 1.0f;
        private PointF _imagePosition = new PointF(0, 0);
        private const float ZOOM_FACTOR = 0.2f;
        private bool _isFirstLoad = true;

        //Brush Fill Circle
        private int _fillCircleRadius = 20;
        private bool _showfillCircle = false;
        private Point _fillCircleCenter;
        private Timer _fillCircleTimer;

        //Draw Circle
        private int _zoomCircleRadius = 20;
        private bool _showZoomCircle = false;
        private Point _zoomCircleCenter;
        private Timer _zoomCircleTimer;

        //Draw Line
        private bool _isDrawing = false;
        private Point _drawStart;
        private Point _drawEnd;
        private const float LINE_START_WIDTH = 3.0f;
        private const float LINE_END_WIDTH = 12.0f;
        private Timer _fadeTimer;
        private bool _showTemporaryLine = false;

        public CameraContol()
        {
            InitializeComponent();
            SetupEventHandlers();
            SetupTimer();
            pictureBox1.BringToFront();
        }
        private void SetupEventHandlers()
        {
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseDoubleClick += MouseDoubleClick;
        }
        private void SetupTimer()
        {
            //Draw Line Timer
            _fadeTimer = new Timer();
            _fadeTimer.Interval = 50;
            _fadeTimer.Tick += (s, e) =>
            {
                _showTemporaryLine = false;
                _fadeTimer.Stop();
                pictureBox1.Invalidate();
            };
            //Draw Circle Timer
            _zoomCircleTimer = new Timer();
            _zoomCircleTimer.Interval = 200; // 0.5 saniye göster
            _zoomCircleTimer.Tick += (s, e) =>
            {
                _showZoomCircle = false;
                _zoomCircleTimer.Stop();
                pictureBox1.Invalidate();
            };
            //Solid Brush Circle
            _fillCircleTimer = new Timer();
            _fillCircleTimer.Interval = 200; // 0.5 saniye göster
            _fillCircleTimer.Tick += (s, e) =>
            {
                _showfillCircle = false;
                _fillCircleTimer.Stop();
                pictureBox1.Invalidate();
            };

        }
        private float MinZoom
        {
            get
            {
                if (_inputImage == null) return 1.0f;
                return Math.Min(
                    (float)pictureBox1.Width / _inputImage.Width,
                    (float)pictureBox1.Height / _inputImage.Height
                );
            }
        }
        private void MouseDoubleClick(object sender, EventArgs e)
        {
            if (_inputImage == null) return;

            _currentScale = MinZoom;
            CenterImage();
            pictureBox1.Invalidate();
        }
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (_inputImage == null) return;

            try
            {
                //Sol tık ile gönderilen verileri sıfırla
                _outputX = 0;
                _outputY = 0;
                //Sol tık ile çizgiyi başlat
                if (e.Button == MouseButtons.Left)
                {
                    _isPanning = true;
                    _drawStart = e.Location;
                    _drawEnd = e.Location;
                    _showTemporaryLine = true;
                }

                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                ResetAllStates();
                MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_inputImage == null) return;

            try
            {
                if (_isPanning)
                {
                    Point currentPoint = new Point(
                        Math.Max(0, Math.Min(e.X, pictureBox1.Width)),
                        Math.Max(0, Math.Min(e.Y, pictureBox1.Height))
                    );

                    // Yüzde hesaplaması
                    float startImageX = (_drawStart.X - _imagePosition.X) / _currentScale;
                    float startImageY = (_drawStart.Y - _imagePosition.Y) / _currentScale;
                    float currentImageX = (currentPoint.X - _imagePosition.X) / _currentScale;
                    float currentImageY = (currentPoint.Y - _imagePosition.Y) / _currentScale;

                    float tempX = ((currentImageX - startImageX) * 100f / _inputImage.Width);
                    float tempY = ((startImageY - currentImageY) * 100f / _inputImage.Height);

                    // Geçici çizgi için
                    _drawEnd = currentPoint;
                    _showTemporaryLine = true;
                    _fadeTimer.Stop();
                    _fadeTimer.Start();

                    pictureBox1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                ResetAllStates();
                MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && _isPanning)
                {
                    Point currentPoint = new Point(
                        Math.Max(0, Math.Min(e.X, pictureBox1.Width)),
                        Math.Max(0, Math.Min(e.Y, pictureBox1.Height))
                    );

                    // Son yüzde hesaplaması
                    float startImageX = (_drawStart.X - _imagePosition.X) / _currentScale;
                    float startImageY = (_drawStart.Y - _imagePosition.Y) / _currentScale;
                    float currentImageX = (currentPoint.X - _imagePosition.X) / _currentScale;
                    float currentImageY = (currentPoint.Y - _imagePosition.Y) / _currentScale;

                    float tempX = ((currentImageX - startImageX) * 100f / _inputImage.Width);
                    float tempY = ((startImageY - currentImageY) * 100f / _inputImage.Height);

                    //Mouse tık bırakınca güncel verileri gönder
                    if (Math.Abs(tempX) > Math.Abs(tempY))
                    {
                        _outputX = tempX;
                        _outputY = 0;
                    }
                    else
                    {
                        _outputX = 0;
                        _outputY = tempY;
                    }

                    _isPanning = false;
                    _showTemporaryLine = true;
                    _fadeTimer.Stop();
                    _fadeTimer.Start();
                }

                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                ResetAllStates();
                MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void PictureBox1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_inputImage == null) return;

            try
            {
                Point mousePoint = e.Location;
                float oldScale = _currentScale;
                float minZoom = MinZoom;

                // Zoom öncesi mouse'un görüntü üzerindeki göreceli pozisyonunu hesapla
                float relativeX = (mousePoint.X - _imagePosition.X) / (_inputImage.Width * oldScale);
                float relativeY = (mousePoint.Y - _imagePosition.Y) / (_inputImage.Height * oldScale);

                // Zoom seviyesini güncelle
                //Yakınlaştırma
                if (e.Delta > 0)
                {
                    if (_currentScale >= MAX_ZOOM) return;
                    _currentScale += ZOOM_FACTOR;

                    ZoomInSetup(e);
                }
                //Uzaklaştırma
                else
                {
                    if (_currentScale <= minZoom)
                    {
                        _currentScale = minZoom;
                        CenterImage();
                        pictureBox1.Invalidate();

                        _showZoomCircle = false;
                        _zoomCircleTimer.Stop();

                        return;
                    }
                    _currentScale -= ZOOM_FACTOR;

                    if (_currentScale < minZoom)
                    {
                        _currentScale = minZoom;
                        _showZoomCircle = false;
                        _zoomCircleTimer.Stop();
                    }
                    else
                    {
                        ZoomOutSetup(e);
                    }

                }

                // Yeni boyutları hesapla
                float newWidth = _inputImage.Width * _currentScale;
                float newHeight = _inputImage.Height * _currentScale;

                // Mouse pozisyonuna göre yeni görüntü pozisyonunu hesapla
                _imagePosition.X = mousePoint.X - (relativeX * newWidth);
                _imagePosition.Y = mousePoint.Y - (relativeY * newHeight);

                // Görüntünün sınırlar içinde kalmasını sağla
                if (newWidth <= pictureBox1.Width)
                {
                    _imagePosition.X = (pictureBox1.Width - newWidth) / 2;
                }
                else
                {
                    if (_imagePosition.X > 0) _imagePosition.X = 0;
                    if (_imagePosition.X + newWidth < pictureBox1.Width)
                        _imagePosition.X = pictureBox1.Width - newWidth;
                }

                if (newHeight <= pictureBox1.Height)
                {
                    _imagePosition.Y = (pictureBox1.Height - newHeight) / 2;
                }
                else
                {
                    if (_imagePosition.Y > 0) _imagePosition.Y = 0;
                    if (_imagePosition.Y + newHeight < pictureBox1.Height)
                        _imagePosition.Y = pictureBox1.Height - newHeight;
                }

                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                ResetAllStates();
                MessageBox.Show("Zoom işlemi sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (_inputImage == null) return;

            try
            {
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                int width = (int)(_inputImage.Width * _currentScale);
                int height = (int)(_inputImage.Height * _currentScale);
                e.Graphics.DrawImage(_inputImage, _imagePosition.X, _imagePosition.Y, width, height);

                if (_isDrawing)
                {
                    DrawGradientLine(e.Graphics, _drawStart, _drawEnd);
                }

                if (_showTemporaryLine)
                {
                    DrawGradientLine(e.Graphics, _drawStart, _drawEnd);
                }
                if (_showZoomCircle)
                {
                    DrawCircle(e.Graphics);
                }
                if (_showfillCircle)
                {
                    DrawFillCircle(e.Graphics);
                }
            }
            catch (Exception ex)
            {
                ResetAllStates();
                MessageBox.Show("Çizim işlemi sırasında bir hata oluştu: " + ex.Message);
            }
        }

        private void DrawCircle(Graphics g)
        {
            using (Pen pen = new Pen(Color.FromArgb(255, ColorTranslator.FromHtml("#9E9D24")), 2))
            {
                g.DrawEllipse(pen,
                    _zoomCircleCenter.X - _zoomCircleRadius,
                    _zoomCircleCenter.Y - _zoomCircleRadius,
                    _zoomCircleRadius * 2,
                    _zoomCircleRadius * 2);
            }
        }
        private void DrawFillCircle(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, ColorTranslator.FromHtml("#9E9D24"))))
            {
                g.FillEllipse(brush,
                    _fillCircleCenter.X - (_fillCircleRadius / 2),
                    _fillCircleCenter.Y - (_fillCircleRadius / 2),
                    _fillCircleRadius,
                    _fillCircleRadius);
            }
        }
        private void DrawGradientLine(Graphics g, Point start, Point end)
        {
            if (start == end) return;

            using (Pen pen = new Pen(ColorTranslator.FromHtml("#9E9D24")))
            {
                float totalLength = (float)Math.Sqrt(
                    Math.Pow(end.X - start.X, 2) +
                    Math.Pow(end.Y - start.Y, 2));

                const float step = 0.05f;
                for (float i = 0; i < 1.0f; i += step)
                {
                    float x1 = start.X + (end.X - start.X) * i;
                    float y1 = start.Y + (end.Y - start.Y) * i;
                    float x2 = start.X + (end.X - start.X) * (i + step);
                    float y2 = start.Y + (end.Y - start.Y) * (i + step);

                    pen.Width = LINE_START_WIDTH + (LINE_END_WIDTH - LINE_START_WIDTH) * i;
                    g.DrawLine(pen, x1, y1, x2, y2);
                }
            }
        }
        private void ZoomInSetup(MouseEventArgs e)
        {
            //Draw Circle
            _zoomCircleCenter = e.Location;
            _showZoomCircle = true;
            _zoomCircleRadius = (int)(20 * _currentScale);
            _zoomCircleTimer.Stop();
            _zoomCircleTimer.Start();

            //Draw FillCircle
            _fillCircleCenter = e.Location;
            _showfillCircle = true;
            _fillCircleRadius = 20;
            _fillCircleTimer.Stop();
            _fillCircleTimer.Start();
        }
        private void ZoomOutSetup(MouseEventArgs e)
        {
            //Draw Circle
            _zoomCircleCenter = e.Location;
            _showZoomCircle = true;
            _zoomCircleRadius = (int)(20 * _currentScale);
            _zoomCircleTimer.Stop();
            _zoomCircleTimer.Start();

            //Draw FillCircle
            _fillCircleCenter = e.Location;
            _showfillCircle = true;
            _fillCircleRadius = 20;
            _fillCircleTimer.Stop();
            _fillCircleTimer.Start();
        }
        private void ResetAllStates()
        {
            _isDrawing = false;
            _isPanning = false;
            _showTemporaryLine = false;
            _fadeTimer.Stop();
            _currentScale = 1.0f;
            _imagePosition = new PointF(0, 0);
            pictureBox1.Invalidate();
        }
        private void CenterImage()
        {
            if (_inputImage == null) return;

            _currentScale = MinZoom;

            float scaledWidth = _inputImage.Width * _currentScale;
            float scaledHeight = _inputImage.Height * _currentScale;

            _imagePosition.X = (pictureBox1.Width - scaledWidth) / 2;
            _imagePosition.Y = (pictureBox1.Height - scaledHeight) / 2;
        }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public float outputX
        {
            get { return _outputX; }
            set { _outputX = value; }
        }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public float outputY
        {
            get { return _outputY; }
            set { _outputY = value; }
        }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public Image inputImage
        {
            get { return _inputImage; }
            set
            {
                if (_inputImage != value)
                {
                    _inputImage = value;
                    if (_inputImage != null)
                    {
                        if (_isFirstLoad)
                        {
                            CenterImage();
                            _isFirstLoad = false;
                        }
                        pictureBox1.Invalidate();
                    }
                }
            }
        }

    }
}

