using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WebSocketSharp; // WebSocketSharp'ı kullanalım
using HMI;
using System.ComponentModel;

namespace AuroraVision_Controls
{
    public partial class mjpegStream : UserControl
    {
        private WebSocket ws; // WebSocketSharp WebSocket
        private Boolean _isConnected = false;
        private PictureBox pictureBox;

        public mjpegStream()
        {
            InitializeComponent();

            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            this.Controls.Add(pictureBox);
            ws = new WebSocket("ws://192.168.136.100:8888/ws");
        }

        private void ConnectToWebSocket()
        {
            if (!ws.IsAlive) // WebSocket bağlantısı açık değilse
            {
                ws.OnMessage += Ws_OnMessage;
                ws.Connect(); // Bağlantıyı başlat
            }
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsBinary)
            {
                try
                {
                    using (var ms = new MemoryStream(e.RawData))
                    {
                        Bitmap frame = new Bitmap(ms);
                        frame.RotateFlip(RotateFlipType.RotateNoneFlipX); // Yatayda aynalama

                        if (pictureBox.InvokeRequired)
                        {
                            pictureBox.Invoke(new Action(() =>
                            {
                                pictureBox.Image?.Dispose();
                                pictureBox.Image = frame;
                            }));
                        }
                        else
                        {
                            pictureBox.Image?.Dispose();
                            pictureBox.Image = frame;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Görüntü okunamadı: " + ex.Message);
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            ws?.Close(); // Bağlantıyı kapat
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Stream ")]
        public Boolean isStream
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value; // Değeri güncelle
                if (_isConnected == true) // Değer değiştiyse
                {
                    _isConnected = value;
                    if (_isConnected)
                    {

                        // Eğer bağlantı kurulmamışsa, WebSocket bağlantısını başlat
                        ConnectToWebSocket();
                    }
                    else
                    {
                        ws?.Close(); // Bağlantıyı kapat
                    }
                }
            }
        }
    }
}
